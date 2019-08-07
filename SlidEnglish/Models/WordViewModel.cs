using System;
using System.ComponentModel.DataAnnotations;

namespace SlidEnglish.Web
{
	public class WordViewModel
	{
		public int Id { get; set; }

		[Required]
		public string Text { get; set; }

		public string Association { get; set; }

		public string Description { get; set; }
	}
}
