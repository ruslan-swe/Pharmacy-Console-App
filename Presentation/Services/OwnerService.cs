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
        private readonly DrugStoreRepos _drugStoreRepos;
        public OwnerService()
        {
            _ownerRepos = new OwnerRepos();
            _drugStoreRepos = new DrugStoreRepos();
        }
        public void Create()
        {
        NameCheck:
            Console.Clear();
            Console.WriteLine("\n");
            ConsoleHelper.WriteContinuosly("Enter owner name:", ConsoleColor.DarkYellow);
            string name = Console.ReadLine();
            if (String.IsNullOrEmpty(name))
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Name is required and must be filled", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto NameCheck;
            }
            if (name.IsDetails() == false)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Name cannot contain numbers", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto NameCheck;
            }

        SurnameCheck:
            Console.Clear();
            Console.WriteLine("\n");
            ConsoleHelper.WriteContinuosly("Enter owner surname:", ConsoleColor.DarkYellow);
            string surname = Console.ReadLine();
            if (String.IsNullOrEmpty(surname))
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Surname is required and must be filled", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto SurnameCheck;
            }
            if (surname.IsDetails() == false)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Surname cannot contain numbers", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
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
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor($"New Owner {owner.Name} {owner.Surname} successfully created!", ConsoleColor.DarkGreen);
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
            Console.ReadKey();
        }
        public void Update()
        {
            Console.Clear();
            if (_ownerRepos.GetAll().Count == 0)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no owners to update details of", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }
        IdCheck:
            Console.Clear();
            Console.WriteLine("\n");
            foreach (var owner in _ownerRepos.GetAll())
            {
                ConsoleHelper.WriteWithColor($"Owner ID : {owner.Id} / Fullname : {owner.Name} {owner.Surname}", ConsoleColor.Cyan);
                Console.WriteLine("\n");
            }
            ConsoleHelper.WriteWithColor("Enter Owner ID to update its details or 0 to return back to menu", ConsoleColor.DarkYellow);
            int id;
            bool isRightInput = int.TryParse(Console.ReadLine(), out id);
            if (!isRightInput)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Wrong Input!", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("Please choose from list above", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
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
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no owner with this ID number.", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor("Please choose from list above", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto IdCheck;
            }
            else
            {
            NameCheck:
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteContinuosly("Enter new owner name:", ConsoleColor.DarkYellow);
                string name = Console.ReadLine();
                if (String.IsNullOrEmpty(name))
                {
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Name is required and must be filled", ConsoleColor.Yellow);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto NameCheck;
                }
                if (name.IsDetails() == false)
                {
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Name cannot contain numbers", ConsoleColor.Red);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto NameCheck;
                }
            SurnameCheck:
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteContinuosly("Enter new owner surname:", ConsoleColor.DarkYellow);
                string surname = Console.ReadLine();
                if (String.IsNullOrEmpty(surname))
                {
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Surname is required and must be filled", ConsoleColor.Yellow);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto SurnameCheck;
                }
                if (surname.IsDetails() == false)
                {
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Surname cannot contain numbers", ConsoleColor.Red);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto SurnameCheck;
                }

                string oldOwnerName = dbOwner.Name;
                string oldOwnerSurname = dbOwner.Surname;

                dbOwner.Name = name;
                dbOwner.Surname = surname;

                _ownerRepos.Update(dbOwner);
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor($"{oldOwnerName} {oldOwnerSurname} Owner profile updated into {dbOwner.Name} {dbOwner.Surname} successfully!", ConsoleColor.DarkGreen);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
            }
        }
        public void Delete()
        {
            Console.Clear();
            if (_ownerRepos.GetAll().Count == 0)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no owner profiles to delete", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }
        IdCheck:
            Console.Clear();
            Console.WriteLine("\n");
            foreach (var owner in _ownerRepos.GetAll())
            {
                ConsoleHelper.WriteWithColor($"Owner ID : {owner.Id} / Fullname : {owner.Name} {owner.Surname}", ConsoleColor.Cyan);
                Console.WriteLine("\n");
            }
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor("Enter Owner ID to delete it's profile or 0 to return back to menu", ConsoleColor.DarkYellow);
            int id;
            bool isRightInput = int.TryParse(Console.ReadLine(), out id);
            if (!isRightInput)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Wrong Input! Enter Owner ID to delete it's profile", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("Please choose from list above", ConsoleColor.Yellow);
                Console.WriteLine("\n");
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
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no owner with this ID number.", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("Please choose from list above", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                goto IdCheck;
            }
            else
            {
            yesNoCheck:
                Console.WriteLine("\n");
                ConsoleHelper.WriteContinuosly("Are you sure you want to remove this owner profile y/n", ConsoleColor.Magenta);
                ConsoleKeyInfo cki2 = Console.ReadKey();
                if (cki2.Key == ConsoleKey.Y)
                {
                    Console.Clear();
                    _ownerRepos.Delete(dbOwner);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor($"{dbOwner.Name} {dbOwner.Surname} owner profile successfully deleted!", ConsoleColor.DarkGreen);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                    Console.ReadKey();
                }
                else if (cki2.Key == ConsoleKey.N)
                {
                    Console.Clear();
                    goto IdCheck;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Please select y/n", ConsoleColor.Red);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto yesNoCheck;
                }
            }
        }
        public void GetAll()
        {
            Console.Clear();
            var owners = _ownerRepos.GetAll();
            if (owners.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no owner profiles in database", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }
            Console.Clear();

            foreach (var owner in owners)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor($"Owner ID : {owner.Id} / Fullname : {owner.Name} {owner.Surname}", ConsoleColor.Cyan);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor($"List of drugstores of {owner.Name} {owner.Surname}", ConsoleColor.Yellow);
                Console.WriteLine("\n");

                var drugstores =_drugStoreRepos.GetAll();
                if (drugstores.Count == 0)
                {
                    ConsoleHelper.WriteWithColor("This owner doesn't have any drugstores", ConsoleColor.Yellow);
                }
                foreach (var drugstore in drugstores)
                {
                    ConsoleHelper.WriteWithColor($"Group Id : {drugstore.Id} / Name : {drugstore.Name} / Adress : {drugstore.Address}", ConsoleColor.Cyan);
                }

            }
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
            Console.ReadKey();
        }

    }
}
