using Quiz.DAL.Context;
using Quiz.DAL.Entities;
using Quiz.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.DAL.Repositories
{
    public class UserRepository : IRepository<ApplicationUser>
    {
        private EFContext db;

        public UserRepository(EFContext context)
        {
            db = context ?? throw new ArgumentNullException("Сontext must not be null.");
        }

        public ApplicationUser Create(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("User must not be null.");

            return db.Users.Add(user);
        }

        public ApplicationUser Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Incorrect user id.");

            ApplicationUser user = db.Users.Find(id);

            if (user == null) return null;

            return db.Users.Remove(user);
        }

        public IEnumerable<ApplicationUser> Find(Func<ApplicationUser, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("Predicate must not be null.");

            return db.Users.Where(predicate).ToList();
        }

    public Task<IEnumerable<ApplicationUser>> FindAsync(Func<ApplicationUser, bool> predicate)
    {
      throw new NotImplementedException();
    }

    public ApplicationUser Get(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Incorrect user id.");

            return db.Users.Find(id);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return db.Users;
        }

    public Task<IEnumerable<ApplicationUser>> GetAllAsync()
    {
      throw new NotImplementedException();
    }

    public Task<ApplicationUser> GetAsync(int id)
    {
      throw new NotImplementedException();
    }

    public ApplicationUser Update(int id, ApplicationUser item)
        {
            if (item == null)
                throw new ArgumentNullException("Item must not be null.");

            if (id <= 0)
                throw new ArgumentException("Incorrect user id ");

            ApplicationUser user = db.Users.FirstOrDefault(x => x.Id == item.Id);
            if (user == null) return null;

            db.Entry(user).CurrentValues.SetValues(item);

            return user;
        }
    }
}
