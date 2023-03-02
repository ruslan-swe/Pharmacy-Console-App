using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repos.Concrete;
using Core.Helpers;
using Core.Extentions;
using Core.Entities;
using System.Security.Cryptography.X509Certificates;

namespace Presentation.Services
{
    internal class OwnerService
    {
        private readonly OwnerRepos _ownerRepos;
        public OwnerService()
        {
            _ownerRepos = new OwnerRepos();
        }
        public void Create()
        {
        NameCheck:
            Console.Clear();
            Console.WriteLine("\n\n\n");
            ConsoleHelper.WriteContinuosly("Enter owner name:", ConsoleColor.Yellow);
            string name = Console.ReadLine();
            if (String.IsNullOrEmpty(name) == true)
            {
                ConsoleHelper.WriteWithColor("Wrong input format!", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                goto NameCheck;
            }

        SurnameCheck:
            Console.Clear();
            Console.WriteLine("\n\n\n");
            ConsoleHelper.WriteContinuosly("Enter owner surname:", ConsoleColor.Yellow);
            string surname = Console.ReadLine();
            if (String.IsNullOrEmpty(surname) == true && surname.IsDetails() == false)
            {
                ConsoleHelper.WriteWithColor("Wrong input format!", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                goto SurnameCheck;
            }

            var owner = new Owner
            {
                Name = name,
                Surname = surname,
            };
            _ownerRepos.Add(owner);

            Console.Clear();
            Console.WriteLine("\n\n\n");
            ConsoleHelper.WriteWithColor($"New Owner {owner.Name} {owner.Surname} successfully created!", ConsoleColor.DarkGreen);
            ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.DarkGreen);
            Console.ReadKey();
        }
        public void Update()
        {
            if (_ownerRepos.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no owners to update details of", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }
        IdCheck:
            Console.Clear();
            foreach (var owner in _ownerRepos.GetAll())
            {
                ConsoleHelper.WriteWithColor($"Owner ID : {owner.Id} / Fullname : {owner.Name} {owner.Surname}", ConsoleColor.DarkGreen);
            }
            ConsoleHelper.WriteWithColor("Enter Owner ID to update details or 0 to return back to menu", ConsoleColor.Yellow);
            int id;
            bool isRightInput = int.TryParse(Console.ReadLine(), out id);
            if (!isRightInput)
            {
                Console.Clear();
                ConsoleHelper.WriteWithColor("Wrong Input!", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                goto IdCheck;
            }
            else if (id == 0)
            {
                return;
            }
            var dbOwner = _ownerRepos.Get(id);
            if (dbOwner == null)
            {
                ConsoleHelper.WriteWithColor("There is no owner with this ID number.", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("Please choose from list above", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
            }
            else
            {
            NameCheck:
                Console.Clear();
                ConsoleHelper.WriteContinuosly("Enter new owner name:", ConsoleColor.Blue);
                string name = Console.ReadLine();
                if (String.IsNullOrEmpty(name) == true && name.IsDetails() == false)
                {
                    ConsoleHelper.WriteWithColor("Wrong input format!", ConsoleColor.Red);
                    Thread.Sleep(5000);
                    goto NameCheck;
                }
            SurnameCheck:
                Console.Clear();
                ConsoleHelper.WriteContinuosly("Enter new owner surname:", ConsoleColor.Blue);
                string surname = Console.ReadLine();
                if (String.IsNullOrEmpty(surname) == true)
                {
                    ConsoleHelper.WriteWithColor("Owner must have a surname!", ConsoleColor.Red);
                    Thread.Sleep(3000);
                    goto SurnameCheck;
                }
                if (surname.IsDetails() == false)
                {
                    ConsoleHelper.WriteWithColor("Surname can't contain numbers!", ConsoleColor.Red);
                    Thread.Sleep(3000);
                    goto SurnameCheck;

                }
                string oldOwnerName = dbOwner.Name;
                string oldOwnerSurname = dbOwner.Surname;

                dbOwner.Name = name;
                dbOwner.Surname = surname;

                _ownerRepos.Update(dbOwner);
                Console.Clear();
                Console.WriteLine("\n\n\n");
                ConsoleHelper.WriteWithColor($"{oldOwnerName} {oldOwnerSurname} Owner profile updated into {dbOwner.Name} {dbOwner.Surname} successfully!", ConsoleColor.Green);
                Thread.Sleep(5000);
            }
        }
        public void Delete()
        {
            if (_ownerRepos.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no owners to update details of", ConsoleColor.Red);
                Thread.Sleep(5000);
                return;
            }
        IdCheck:
            Console.Clear();
            Console.WriteLine("\n\n\n");
            foreach (var owner in _ownerRepos.GetAll())
            {
                ConsoleHelper.WriteWithColor($"Owner ID : {owner.Id} / Fullname : {owner.Name} {owner.Surname}", ConsoleColor.DarkGreen);
            }
            ConsoleHelper.WriteWithColor("Enter Owner ID to update details or 0 to return back to menu", ConsoleColor.Yellow);
            int id;
            bool isRightInput = int.TryParse(Console.ReadLine(), out id);
            if (!isRightInput)
            {
                Console.Clear();
                Console.WriteLine("\n\n\n");
                ConsoleHelper.WriteWithColor("Wrong Input!\nEnter Owner ID to uptade it's profile details", ConsoleColor.Red);
                goto IdCheck;
            }
            else if (id == 0)
            {
                return;
            }
            var dbOwner = _ownerRepos.Get(id);
            if (dbOwner == null)
            {
                Console.WriteLine("\n\n\n");
                ConsoleHelper.WriteWithColor("There is no owner with this ID number.", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("Please choose from list above", ConsoleColor.Red);
            }
            else
            {
            yesNoCheck:
                Console.WriteLine("\n\n\n");
                ConsoleHelper.WriteContinuosly("Are you sure you want to remove this owner y/n", ConsoleColor.Red);
                ConsoleKeyInfo cki2 = Console.ReadKey();
                if (cki2.Key == ConsoleKey.Y)
                {
                    Console.Clear();
                    _ownerRepos.Delete(dbOwner);
                    Console.WriteLine("\n\n\n");
                    ConsoleHelper.WriteWithColor($"{dbOwner.Name} {dbOwner.Surname} owner profile successfully deleted!", ConsoleColor.Green);
                    Thread.Sleep(5000);
                }
                else if (cki2.Key == ConsoleKey.N)
                {
                    Console.Clear();
                    goto IdCheck;
                }
                else
                {
                    Console.Clear();
                    ConsoleHelper.WriteWithColor("Please select y/n", ConsoleColor.Red);
                    goto yesNoCheck;
                }
            }
        }
        public void GetAll()
        {

            var owners = _ownerRepos.GetAll();
            if (owners.Count == 0)
            {
                Console.Clear();
                ConsoleHelper.WriteWithColor("There is no owner profiles in database", ConsoleColor.Red);
                Thread.Sleep(3000);
                return;
            }
            Console.Clear();
            foreach (var owner in owners)
            {
                ConsoleHelper.WriteWithColor($"Owner ID : {owner.Id} / Fullname : {owner.Name} {owner.Surname}", ConsoleColor.DarkGreen);
                ConsoleHelper.WriteWithColor($"List of drugstores of {owner.Name} {owner.Surname}", ConsoleColor.Yellow);

                if (owner.DrugStores is null)
                {
                    ConsoleHelper.WriteWithColor("No drugstores have been created yet", ConsoleColor.Red);
                    continue;
                }
                else
                {
                    if (owner.DrugStores.Count == 0)
                    {
                        ConsoleHelper.WriteWithColor("This owner doesn't have any drugstores", ConsoleColor.Yellow);
                    }
                    foreach (var drugstore in owner.DrugStores)
                    {
                        ConsoleHelper.WriteWithColor($"Group Id : {drugstore.Id} / Name : {drugstore.Name} / Adress : {drugstore.Address}", ConsoleColor.Yellow);
                    }
                }
            }
            ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Green);
            Console.ReadKey();
        }

    }
}
