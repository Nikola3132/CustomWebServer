using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SULS.Data;
using SULS.Models;

namespace SULS.Services
{
    public class UserService : IUserService
    {
        private readonly SULSContext dbContext;

        public UserService(SULSContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool AnyUserWithTheGivenUsernameAndPassword(string username, string password)
        {
            return this.dbContext.Users.Any(u => u.Username == username && u.Password == password);
        }

        public bool AnyUserWithTheGivenUsernameOrEmail(string username, string email)
        {
            return this.dbContext.Users.Any(u => u.Username == username || u.Email == email);
        }

        public bool CreateUser(string username, string email, string password)
        {
            User userForDb = new User()
            {
                Email = email,
                Password = password,
                Username = username
            };


            this.dbContext.Users.Add(userForDb);
            this.dbContext.SaveChanges();

            return true;
        }

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            return this.dbContext.Users.SingleOrDefault(u => u.Username == username
                                                        && u.Password == password);
        }
    }
}
