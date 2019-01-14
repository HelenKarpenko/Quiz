using Quiz.DAL.Context;
using Quiz.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using Quiz.DAL.Entities.User;

namespace Quiz.DAL.Repositories
{
	public class UserRepository : IRepository<UserInfo>
	{
		private EFContext db;

		public UserRepository(EFContext context)
		{
			db = context ?? throw new ArgumentNullException("Сontext must not be null.");
		}

		public UserInfo Create(UserInfo user)
		{
			if (user == null)
				throw new ArgumentNullException("User must not be null.");

			return db.UsersInfo.Add(user);
		}

		public UserInfo Delete(int id)
		{
			if (id <= 0)
				throw new ArgumentException("Incorrect user id.");

			UserInfo user = db.UsersInfo.Find(id);

			if (user == null) return null;

			return db.UsersInfo.Remove(user);
		}

		public IEnumerable<UserInfo> Find(Func<UserInfo, bool> predicate)
		{
			if (predicate == null)
				throw new ArgumentNullException("Predicate must not be null.");

			return db.UsersInfo.Where(predicate).ToList();
		}

		public async Task<IEnumerable<UserInfo>> FindAsync(Func<UserInfo, bool> predicate)
		{
			if (predicate == null)
				throw new ArgumentNullException("Predicate must not be null.");

			IEnumerable<UserInfo> users = await db.UsersInfo.ToListAsync();

			List<UserInfo> returnerUsers = users.Where(predicate).ToList();

			return returnerUsers;
		}

		public UserInfo Get(int id)
		{
			if (id <= 0)
				throw new ArgumentException("Incorrect user id.");

			return db.UsersInfo.Find(id);
		}

		public IEnumerable<UserInfo> GetAll()
		{
			return db.UsersInfo;
		}

		public async Task<IEnumerable<UserInfo>> GetAllAsync()
		{
			return await db.UsersInfo.ToListAsync();
		}

		public async Task<UserInfo> GetAsync(int id)
		{
			if (id <= 0)
				throw new ArgumentException("Incorrect test id.");

			return await db.UsersInfo.FindAsync(id);
		}

		public UserInfo Update(int id, UserInfo item)
		{
			if (item == null)
				throw new ArgumentNullException("Item must not be null.");

			if (id <= 0)
				throw new ArgumentException("Incorrect user id ");

			UserInfo user = db.UsersInfo.FirstOrDefault(x => x.Id == item.Id);
			if (user == null) return null;

			db.Entry(user).CurrentValues.SetValues(item);

			return user;
		}
	}
}
