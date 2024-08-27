using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleLoginApp2024.Utility
{
    public class CustomValidation
    {
        //UserName should not be Empty
        //UserName should contain only letters,numbers ,underscores and dot
        public static bool IsValidNumber(string input)
        {
            // Check if the input is a valid integer (number)
            return int.TryParse(input, out _);
        }

        //Password
        //Password should have at least 4 characters,
        //including at least one uppercase letter, one lowercase letter,one digit and one special character
        public static bool IsValidPassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{4,}$");
            //? means any character between,* means all character
        }


        //Replace alphanumeric with * symbol for password
        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);//true means it only one key at a time

                //1. each keystroke from the user, replaces it with an asterik(*) and
                //add it to the password string until the user presses the enter key

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password = password + key.KeyChar;
                    Console.Write("*");
                }
                //2. allows the user to backspace and correct mistakes while typing password
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    //backspace alone
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");   //to remove * symbol
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }
    }
}
