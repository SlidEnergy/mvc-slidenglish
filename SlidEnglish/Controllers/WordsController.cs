using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
		private readonly IMapper _mapper;
		private readonly WordsService _service;

		public WordsController(IMapper mapper, WordsService wordsService)
        {
			_mapper = mapper;
			_service = wordsService;
        }

        // GET: Words
        public async Task<IActionResult> Index()
        {
			var userId = User.GetUserId();
			var list = await _service.GetListAsync(userId);
			return View(_mapper.Map<List<WordViewModel>>(list));
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

			return View(_mapper.Map<DetailsWordViewModel>(word));
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
        public async Task<IActionResult> Create([Bind("Id,Text,Association,Description")] WordViewModel word)
        {
            if (ModelState.IsValid)
            {
				var userId = User.GetUserId();
				await _service.AddAsync(userId, _mapper.Map<Word>(word));
                return RedirectToAction(nameof(Index));
            }
            return View(_mapper.Map<WordViewModel>(word));
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

			var editWord = _mapper.Map<DetailsWordViewModel>(word);
			editWord.Sinonyms.Add(new WordViewModel() { Id = 1, Text = "Test1" });
			editWord.Sinonyms.Add(new WordViewModel() { Id = 2, Text = "Test2" });
			editWord.Sinonyms.Add(new WordViewModel() { Id = 3, Text = "Test3" });
			return View(editWord);
		}

		// POST: Words/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Text,Association,Description,Sinonyms")] DetailsWordViewModel word)
		{
			if (id != word.Id)
				return NotFound();

			if (ModelState.IsValid)
			{
				var userId = User.GetUserId();

				try
				{
					await _service.EditAsync(userId, _mapper.Map<Word>(word));
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!await _service.ExistsAsync(userId, _mapper.Map<Word>(word)))
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

			return View(_mapper.Map<DetailsWordViewModel>(word));
		}

		public ActionResult BlankEditorRow()
		{
			return PartialView("~/Views/Shared/EditorTemplates/EditEmployeeViewModel.cshtml", new WordViewModel());
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

			return View(_mapper.Map<WordViewModel>(word));
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
