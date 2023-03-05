using Core.Entities;
using Core.Helpers;
using Data.Repos.Concrete;

namespace Presentation.Services
{
    public class AdminService
    {
        static int fails;
        private readonly AdminRepos _adminRepos;
        public AdminService()
        {
            _adminRepos = new AdminRepos();
        }
        public Admin Authorize()
        {
        LoginCheck:
            Console.WriteLine("\n\n\n");
            ConsoleHelper.WriteWithColor("---- Login ----",ConsoleColor.Blue);
            ConsoleHelper.WriteContinuosly("Username :",ConsoleColor.Blue);
            string usernameInput = Console.ReadLine();

            ConsoleHelper.WriteContinuosly("Password :", ConsoleColor.Blue);
            var passwordInput = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && passwordInput.Length > 0)
                {
                    Console.Write("\b \b");
                    passwordInput = passwordInput[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    passwordInput += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            if (fails >= 2)
            {
                Console.Clear();
                for (int a = 10; a >= 0; a--)
                {
                    Console.WriteLine("\n\n\n");
                    ConsoleHelper.WriteWithColor("Too many wrong inputs. Wait 10 seconds then try again", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor(Convert.ToString(a),ConsoleColor.Red);
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                fails = 0;
                Console.Clear();
                goto LoginCheck;
            }
            var admin = _adminRepos.GetByUsernameAndPassword(usernameInput, passwordInput);
            if (admin is null)
            {
                Console.Clear();
                ConsoleHelper.WriteWithColor("Username and/or password is incorrect!", ConsoleColor.Red);
                fails += 1;
                goto LoginCheck;
            }
            return admin;
        }
    }
}
