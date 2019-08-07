using System;
using System.ComponentModel.DataAnnotations;

namespace SlidEnglish.Domain
{
	public class Word : IUniqueObject
	{
		public int Id { get; set; }

		[Required]
		public string Text { get; set; }

		[Required]
		public virtual User User { get; set; }

		/// <summary>
		/// Описание, Этимология, ссылки на ресурсы
		/// </summary>
		[Required(AllowEmptyStrings = true)]
		public string Description { get; set; }

		[Required(AllowEmptyStrings = true)]
		public string Association { get; set; }

		public bool IsBelongsTo(string userId) => User.Id == userId;
	}
}
