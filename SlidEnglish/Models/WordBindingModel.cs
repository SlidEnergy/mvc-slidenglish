using System;
using System.ComponentModel.DataAnnotations;

namespace SlidEnglish.Domain
{
	public class WordBindingModel
	{
		public int Id { get; set; }

		[Required]
		public string Text { get; set; }
	}
}
