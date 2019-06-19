using SULS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.Services
{
    public interface IUserService
    {
        bool CreateUser(string username, string email, string password);
        User GetUserByUsernameAndPassword(string username, string password);

        bool AnyUserWithTheGivenUsernameAndPassword(string username, string password);

        bool AnyUserWithTheGivenUsernameOrEmail(string username, string email);
    }
}
