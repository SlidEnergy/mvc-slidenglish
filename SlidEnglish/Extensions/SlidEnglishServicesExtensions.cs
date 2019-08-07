using Microsoft.Extensions.DependencyInjection;
using SlidEnglish.App;
using SlidEnglish.Domain;
using SlidEnglish.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlidEnglish.Web
{
	public static class SlidEnglishServicesExtensions
	{
		public static void AddSlidEnglishServices(this IServiceCollection services)
		{
			services.AddScoped<IRepository<User, string>, EfRepository<User, string>>();
			services.AddScoped<IRepositoryWithAccessCheck<Word>, EfWordsRepository>();
			services.AddScoped<WordsService>();
			services.AddScoped<IDataAccessLayer, DataAccessLayer>();
		}
	}
}
