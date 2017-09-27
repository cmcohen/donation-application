using DonationApplication.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApplication.console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("You are creating an Admin User");
            string firstName = Prompt("First Name:");
            string lastName = Prompt("Last Name:");
            string email = Prompt("Email:");
            string password = Prompt("Create a password:");
            var userDb = new UserRepository(Properties.Settings.Default.ConStr);
            userDb.AddUser(firstName, lastName, email, password, true);
            Console.WriteLine($"Your account has been created for {firstName} {lastName}");
        }

        static string Prompt(string text)
        {
            Console.WriteLine(text);
            return Console.ReadLine();
        }
    }
}
