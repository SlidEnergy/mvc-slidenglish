﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SlidEnglish.Domain
{
	public class Word: IUniqueObject
	{
		public int Id { get; set; }

		[Required]
		public string Text { get; set; }

		[Required]
		public virtual User User { get; set; }

		public bool IsBelongsTo(string userId) => User.Id == userId;
	}
}
