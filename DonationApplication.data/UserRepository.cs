using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApplication.data
{
    public class UserRepository
    {
        private string _connectionString;
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(string firstName, string lastName, string email, string password, bool isAdmin)
        {
            string salt = PasswordHelper.GenerateSalt();
            string passwordHash = PasswordHelper.HashPassword(password, salt);
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PasswordHash = passwordHash,
                PasswordSalt = salt,
                IsAdmin = isAdmin
            };
            using (var context = new donationApplicationDataContext())
            {
                context.Users.InsertOnSubmit(user);
                context.SubmitChanges();
            }
        }

        public User GetUserByEmail(string email)
        {
            using (var context = new donationApplicationDataContext())
            {
                return context.Users.FirstOrDefault(u => u.Email == email);
            }
        }
        public User Login(string email, string password)
        {
            User user = GetUserByEmail(email);
            if (user == null)
            {
                return null;
            }

            bool CorrectPassword = PasswordHelper.PasswordMatch(password, user.PasswordSalt, user.PasswordHash);
            if (!CorrectPassword)
            {
                return null;
            }
            return user;
        }

        public bool EmailExists(string email)
        {
            using (var context = new donationApplicationDataContext(_connectionString))
            {
                return context.Users.Any(u => u.Email == email);
            }
        }
    }
}
