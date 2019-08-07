using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SlidEnglish.App;
using SlidEnglish.Domain;

namespace SlidEnglish.Web
{
	[Authorize]
    public class WordsController : Controller
    {
        //private readonly ApplicationDbContext _context;
		private readonly WordsService _service;

		public WordsController(WordsService wordsService)
        {
			_service = wordsService;
        }

        // GET: Words
        public async Task<IActionResult> Index()
        {
			var userId = User.GetUserId();
			return View(await _service.GetListAsync(userId));
        }

		// GET: Words/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var userId = User.GetUserId();

			var word = await _service.GetAsync(userId, id.Value);

			if (word == null)
			{
				return NotFound();
			}

			return View(word);
		}

		// GET: Words/Create
		public IActionResult Create()
        {
            return View();
        }

        // POST: Words/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text")] WordBindingModel word)
        {
            if (ModelState.IsValid)
            {
				var userId = User.GetUserId();
				await _service.AddAsync(userId, new Word() { Text = word.Text });
                return RedirectToAction(nameof(Index));
            }
            return View(word);
        }

		// GET: Words/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
				return NotFound();

			var userId = User.GetUserId();

			var word = await _service.GetAsync(userId, id.Value);

			if (word == null)
				return NotFound();

			return View(word);
		}

		// POST: Words/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Text")] WordBindingModel word)
		{
			if (id != word.Id)
				return NotFound();

			if (ModelState.IsValid)
			{
				var userId = User.GetUserId();

				try
				{
					await _service.EditAsync(userId, new Word { Id = word.Id, Text = word.Text });
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!await _service.ExistsAsync(userId, new Word { Id = word.Id, Text = word.Text }))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(word);
		}

		// GET: Words/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
				return NotFound();

			var userId = User.GetUserId();
			var word = await _service.GetAsync(userId, id.Value);

			if (word == null)
				return NotFound();

			return View(word);
		}

		// POST: Words/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var userId = User.GetUserId();

			await _service.DeleteAsync(userId, id);

			return RedirectToAction(nameof(Index));
		}
	}
}
