using SlidEnglish.App;
using SlidEnglish.Domain;
using System.Threading.Tasks;

namespace SlidEnglish.Infrastructure
{
	public class DataAccessLayer : IDataAccessLayer
	{
		private readonly ApplicationDbContext _context;

		public IRepositoryWithAccessCheck<Word> Words { get; }
		public IRepository<User, string> Users { get; }

		public DataAccessLayer(
			ApplicationDbContext context,
			IRepository<User, string> users,
			IRepositoryWithAccessCheck<Word> words)
		{
			_context = context;
			Users = users;
			Words = words;
		}

		public Task SaveChangesAsync() => _context.SaveChangesAsync();
    }
}
