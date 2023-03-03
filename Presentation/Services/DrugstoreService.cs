using Core.Entities;
using Core.Extentions;
using Core.Helpers;
using Data.Repos.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public class DrugStoreService
    {
        private readonly DrugStoreRepos _drugStoreRepos;
        private readonly OwnerRepos _ownerRepos;
        public DrugStoreService()
        {
            _drugStoreRepos = new DrugStoreRepos();
            _ownerRepos = new OwnerRepos();
        }

        public void Create()
        {
            var owners = _ownerRepos.GetAll();
            if (owners.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no Owners to assign new drugstore to! Please first create Owner", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor(" Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }
        NameCheck:
            Console.Clear();
            ConsoleHelper.WriteContinuosly("Enter drugstore name:", ConsoleColor.DarkYellow);
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

        AddressCheck:
            Console.Clear();
            ConsoleHelper.WriteContinuosly("Enter drugstore address:", ConsoleColor.DarkYellow);
            string address = Console.ReadLine();
            if (String.IsNullOrEmpty(address))
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Address is required and must be filled", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto AddressCheck;
            }

        NumberCheck:
            Console.Clear();
            ConsoleHelper.WriteContinuosly("Enter drugstore contact number:", ConsoleColor.DarkYellow);
            string number = Console.ReadLine();
            if (String.IsNullOrEmpty(number))
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Number is required and must be filled", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto NumberCheck;
            }
            if (number.IsNumber() == false)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Number can't contain letters, space or any special characters except '+'", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto NumberCheck;
            }

        EmailCheck:
            Console.Clear();
            ConsoleHelper.WriteContinuosly("Enter drugstore email address:", ConsoleColor.DarkYellow);
            string email = Console.ReadLine();
            if (String.IsNullOrEmpty(email))
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Email address is required and must be filled", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto EmailCheck;
            }
            if (email.IsEmail() == false)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Wrong email address format", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto EmailCheck;
            }

        OwnerCheck:
            Console.Clear();
            foreach (var owner in owners)
            {
                ConsoleHelper.WriteWithColor($"Owner ID : {owner.Id} / Fullname : {owner.Name} {owner.Surname}", ConsoleColor.Cyan);
                Console.WriteLine("\n");
            }
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor("Enter Owner ID to assign new drugstore to it or 0 to return back to menu", ConsoleColor.DarkYellow);
            int id;
            bool isRightInput = int.TryParse(Console.ReadLine(), out id);
            if (!isRightInput)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Wrong Input! Enter Owner ID to assign new drugstore to", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("Please choose from list above", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto OwnerCheck;
            }
            var dbOwner = _ownerRepos.Get(id);
            if (dbOwner == null)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no owner with this ID number.", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("Please choose from list above", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto OwnerCheck;
            }
            
            var drugstore = new DrugStore
            {
                Name = name,
                Address = address,
                ContactNumber = number,
                Email = email,
                Owner = dbOwner
            };

            _drugStoreRepos.Add(drugstore);

            Console.Clear();
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor($"New Drugstore {drugstore.Name} successfully created and assigned to {drugstore.Owner.Name} {drugstore.Owner.Surname}", ConsoleColor.DarkGreen);
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
            Console.ReadKey();
        }
        public void Update()
        {
            Console.Clear();
            if (_drugStoreRepos.GetAll().Count == 0)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no drugstores to update details of", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }
        IdCheck:
            Console.Clear();
            Console.WriteLine("\n");
            foreach (var drugstore in _drugStoreRepos.GetAll())
            {
                ConsoleHelper.WriteWithColor($"Drugstore ID : {drugstore.Id} / Name : {drugstore.Name} / Owner Fullname : {drugstore.Owner.Surname} {drugstore.Owner.Name}", ConsoleColor.Cyan);
                Console.WriteLine("\n");
            }
            ConsoleHelper.WriteWithColor("Enter drugstore ID to update its details or 0 to return back to menu", ConsoleColor.DarkYellow);
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
            var dbDrugstore = _drugStoreRepos.Get(id);
            if (dbDrugstore == null)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no drugstore with this ID number.", ConsoleColor.Yellow);
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
                ConsoleHelper.WriteContinuosly("Enter new drugstore name:", ConsoleColor.DarkYellow);
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

            AddressCheck:
                Console.Clear();
                ConsoleHelper.WriteContinuosly("Enter new drugstore address:", ConsoleColor.DarkYellow);
                string address = Console.ReadLine();
                if (String.IsNullOrEmpty(address))
                {
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Address is required and must be filled", ConsoleColor.Yellow);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto AddressCheck;
                }

            NumberCheck:
                Console.Clear();
                ConsoleHelper.WriteContinuosly("Enter drugstore contact number:", ConsoleColor.DarkYellow);
                string number = Console.ReadLine();
                if (String.IsNullOrEmpty(number))
                {
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Number is required and must be filled", ConsoleColor.Yellow);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto NumberCheck;
                }
                if (number.IsNumber() == false)
                {
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Number can't contain letters, space or any special characters except '+'", ConsoleColor.Red);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto NumberCheck;
                }

            EmailCheck:
                Console.Clear();
                ConsoleHelper.WriteContinuosly("Enter drugstore email address:", ConsoleColor.DarkYellow);
                string email = Console.ReadLine();
                if (String.IsNullOrEmpty(email))
                {
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Email address is required and must be filled", ConsoleColor.Yellow);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto EmailCheck;
                }
                if (email.IsEmail() == false)
                {
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Wrong email address format", ConsoleColor.Red);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto EmailCheck;
                }
            OwnerCheck:
                var owners = _ownerRepos.GetAll();
                foreach (var owner in owners)
                {
                    ConsoleHelper.WriteWithColor($"Owner ID : {owner.Id} / Fullname : {owner.Name} {owner.Surname}", ConsoleColor.Cyan);
                    Console.WriteLine("\n");
                }
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Enter Owner ID to delete it's profile or 0 to return back to menu", ConsoleColor.DarkYellow);
                int input;
                isRightInput = int.TryParse(Console.ReadLine(), out input);
                if (!isRightInput)
                {
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Wrong Input! Enter Owner ID to assign new drugstore to", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("Please choose from list above", ConsoleColor.Yellow);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto OwnerCheck;
                }
                var dbOwner = _ownerRepos.Get(input);
                if (dbOwner == null)
                {
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("There is no owner with this ID number.", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("Please choose from list above", ConsoleColor.Yellow);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto OwnerCheck;
                }

                string oldStoreName = dbDrugstore.Name;
                string oldOwnerName = dbDrugstore.Owner.Name;

                dbDrugstore.Name = name;
                dbDrugstore.Address = address;
                dbDrugstore.ContactNumber = number;
                dbDrugstore.Email = email;
                dbDrugstore.Owner = dbOwner;

                _drugStoreRepos.Update(dbDrugstore);
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor($"{oldStoreName} of {oldOwnerName} drugstore details updated into {dbDrugstore.Name} {dbDrugstore.Owner} successfully!", ConsoleColor.DarkGreen);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
            }
        }
        public void Delete()
        {
            Console.Clear();
            if (_drugStoreRepos.GetAll().Count == 0)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no drugstore profiles to delete", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }
        IdCheck:
            Console.Clear();
            Console.WriteLine("\n");
            foreach (var drugStore in _drugStoreRepos.GetAll())
            {
                ConsoleHelper.WriteWithColor($"DrugStore ID : {drugStore.Id} / Owner Fullname : {drugStore.Owner.Name} {drugStore.Owner.Surname}", ConsoleColor.Cyan);
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
        }
        public void GetAll()
        {
            Console.Clear();
            if (_drugStoreRepos.GetAll().Count == 0)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no drugstore profiles in database", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }
        }
    }
}
