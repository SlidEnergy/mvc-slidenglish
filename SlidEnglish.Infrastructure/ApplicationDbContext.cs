using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SlidEnglish.Domain;

namespace SlidEnglish.Infrastructure
{
	public class ApplicationDbContext : IdentityDbContext<SlidEnglish.Domain.User>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<SlidEnglish.Domain.Word> Words { get; set; }
	}
}
