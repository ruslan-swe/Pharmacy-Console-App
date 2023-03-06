using Core.Entities;
using Core.Extentions;
using Core.Helpers;
using Data.Context;
using Data.Repos.Concrete;
using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;

namespace Presentation.Services
{
    public class DrugStoreService
    {
        private readonly DrugStoreRepos _drugStoreRepos;
        private readonly OwnerRepos _ownerRepos;
        private readonly DrugRepos _drugRepos;
        public DrugStoreService()
        {
            _drugStoreRepos = new DrugStoreRepos();
            _ownerRepos = new OwnerRepos();
            _drugRepos = new DrugRepos();
        }

        public void Create()
        {
            Console.Clear();
            var owners = _ownerRepos.GetAll();
            if (owners.Count == 0)
            {
                Console.WriteLine("\n\n");
                ConsoleHelper.WriteWithColor("There is no Owners to assign new drugstore to! Please first create Owner", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor(" Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }

        NameCheck:
            Console.Clear();
            Console.WriteLine("\n\n");
            ConsoleHelper.WriteContinuosly("Enter drugstore name:", ConsoleColor.DarkYellow);
            string name = Console.ReadLine();
            if (String.IsNullOrEmpty(name))
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Name is required and must be filled", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto NameCheck;
            }

        AddressCheck:
            Console.Clear();
            Console.WriteLine("\n\n");
            ConsoleHelper.WriteContinuosly("Enter drugstore address:", ConsoleColor.DarkYellow);
            string address = Console.ReadLine();
            if (String.IsNullOrEmpty(address))
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Address is required and must be filled", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto AddressCheck;
            }

        NumberCheck:
            Console.Clear();
            Console.WriteLine("\n\n");
            ConsoleHelper.WriteContinuosly("Enter drugstore contact number:", ConsoleColor.DarkYellow);
            string number = Console.ReadLine();
            if (String.IsNullOrEmpty(number))
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Number is required and must be filled", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto NumberCheck;
            }
            if (number.IsNumber() == false)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Number can't contain letters, space or any special characters except '+'", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto NumberCheck;
            }

        EmailCheck:
            Console.Clear();
            Console.WriteLine("\n\n");
            ConsoleHelper.WriteContinuosly("Enter drugstore email address:", ConsoleColor.DarkYellow);
            string email = Console.ReadLine();
            if (String.IsNullOrEmpty(email))
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Email address is required and must be filled", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto EmailCheck;
            }
            if (email.IsEmail() == false)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Wrong email address format", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto EmailCheck;
            }

        OwnerCheck:
            Console.Clear();
            Console.WriteLine("\n\n");
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
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Wrong Input! Enter Owner ID to assign new drugstore to", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto OwnerCheck;
            }
            var dbOwner = _ownerRepos.Get(id);
            if (dbOwner == null)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no owner with this ID number.", ConsoleColor.Red);
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
            dbOwner.DrugStores.Add(drugstore);

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
            Console.WriteLine("\n\n");
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
                ConsoleHelper.WriteWithColor("Wrong Input Format!", ConsoleColor.Red);
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
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no drugstore with this ID number.", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto IdCheck;
            }
            else
            {
            NameCheck:
                Console.Clear();
                Console.WriteLine("\n\n");
                ConsoleHelper.WriteContinuosly("Enter new drugstore name:", ConsoleColor.DarkYellow);
                string name = Console.ReadLine();
                if (String.IsNullOrEmpty(name))
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Name is required and must be filled", ConsoleColor.Red);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto NameCheck;
                }

            AddressCheck:
                Console.Clear();
                Console.WriteLine("\n\n");
                ConsoleHelper.WriteContinuosly("Enter new drugstore address:", ConsoleColor.DarkYellow);
                string address = Console.ReadLine();
                if (String.IsNullOrEmpty(address))
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Address is required and must be filled", ConsoleColor.Red);
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
                    Console.Clear();
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Number is required and must be filled", ConsoleColor.Red);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto NumberCheck;
                }
                if (number.IsNumber() == false)
                {
                    Console.Clear();
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
                    Console.Clear();
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Email address is required and must be filled", ConsoleColor.Red);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto EmailCheck;
                }
                if (email.IsEmail() == false)
                {
                    Console.Clear();
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
                ConsoleHelper.WriteWithColor("Enter Owner ID to assign new drugstore to it or 0 to return back to menu", ConsoleColor.DarkYellow);
                int input;
                isRightInput = int.TryParse(Console.ReadLine(), out input);
                if (!isRightInput)
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Wrong Input! Enter Owner ID to assign new drugstore to", ConsoleColor.Red);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto OwnerCheck;
                }
                var dbOwner = _ownerRepos.Get(input);
                if (dbOwner == null)
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("There is no owner with this ID number.", ConsoleColor.Red);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto OwnerCheck;
                }

                string oldStoreName = dbDrugstore.Name;
                string oldOwnerName = dbDrugstore.Owner.Name;
                string oldOwnerSurname = dbDrugstore.Owner.Surname;

                dbDrugstore.Name = name;
                dbDrugstore.Address = address;
                dbDrugstore.ContactNumber = number;
                dbDrugstore.Email = email;
                dbDrugstore.Owner.DrugStores.Remove(dbDrugstore);
                dbDrugstore.Owner = dbOwner;

                _drugStoreRepos.Update(dbDrugstore);
                dbOwner.DrugStores.Add(dbDrugstore);

                Console.Clear();
                Console.WriteLine("\n\n");
                ConsoleHelper.WriteWithColor($"{oldStoreName} of {oldOwnerName} {oldOwnerSurname} drugstore details updated into", ConsoleColor.DarkGreen);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor($" {dbDrugstore.Name} successfully!", ConsoleColor.DarkGreen);
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
                ConsoleHelper.WriteWithColor($"DrugStore ID : {drugStore.Id} / Name : {drugStore.Name} / Owner Fullname : {drugStore.Owner.Name} {drugStore.Owner.Surname}", ConsoleColor.Cyan);
                Console.WriteLine("\n");
            }
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor("Enter Drugstore ID to delete it's profile or 0 to return back to menu", ConsoleColor.DarkYellow);
            int id;
            bool isRightInput = int.TryParse(Console.ReadLine(), out id);
            if (!isRightInput)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Wrong Input! Enter drugstore ID to delete it's profile", ConsoleColor.Red);
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
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no drugstore with this ID number.", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto IdCheck;
            }
            else
            {
            yesNoCheck:
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteContinuosly($"Are you sure you want to remove {dbDrugstore.Name} drugstore profile y/n", ConsoleColor.Magenta);
                ConsoleKeyInfo cki2 = Console.ReadKey();
                if (cki2.Key == ConsoleKey.Y)
                {
                    Console.Clear();
                    _drugStoreRepos.Delete(dbDrugstore);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor($"{dbDrugstore.Name} drugstore of {dbDrugstore.Owner.Name} {dbDrugstore.Owner.Surname} owner successfully deleted!", ConsoleColor.DarkGreen);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                    Console.ReadKey();
                }
                else if (cki2.Key == ConsoleKey.N)
                {
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
            var drugstores = _drugStoreRepos.GetAll();
            if (drugstores.Count == 0)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no drugstore profiles in database", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }

            foreach (var drugstore in drugstores)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor($"Drugstore ID : {drugstore.Id} / Name : {drugstore.Name} / Owner Fullname : {drugstore.Owner.Name} {drugstore.Owner.Surname}", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor($"Adress : {drugstore.Address}", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor($"Contact Number : {drugstore.ContactNumber}", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor($"Email Address : {drugstore.Email}", ConsoleColor.Cyan);
            }
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
            Console.ReadKey();
        }
        public void GetAllByOwner()
        {
            Console.Clear();
            var drugstores = _drugStoreRepos.GetAll();
            if (drugstores.Count == 0)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no drugstore profiles in database", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }

        IdCheck:
            var owners = _ownerRepos.GetAll();
            foreach (var owner in owners)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor($"Owner ID : {owner.Id} / Fullname : {owner.Name} {owner.Surname} ", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("----------------------------------------------------------------------------", ConsoleColor.DarkMagenta);
            }
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor("Enter Owner ID to get all drugstores by owner or 0 to return back to menu", ConsoleColor.DarkYellow);
            int id;
            bool isRightInput = int.TryParse(Console.ReadLine(), out id);
            if (!isRightInput)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Wrong Input!", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto IdCheck;
            }
            else if (id == 0)
            {
                return;
            }
            var dbDrugstores = _drugStoreRepos.GetAllByOwner(id);
            if (dbDrugstores.Count == 0)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("This Owner does not have any drugstores assigned to it.", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return back to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }
            Console.Clear();
            foreach (var drugstore in dbDrugstores)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor($"Drugstore ID : {drugstore.Id} / Name : {drugstore.Name} / Address  {drugstore.Address}", ConsoleColor.Cyan);
            }
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
            Console.ReadKey();
        }
        public void Sale()
        {
            Console.Clear();
            var availableDrugs = _drugStoreRepos.GetAvailableDrugs();
            if (availableDrugs.Count == 0)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no drugs in any of the drugstores to sell.", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return back to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }
        DrugsIdCheck:
            Console.Clear();
            ConsoleHelper.WriteWithColor("List of Drugs which we have in stock", ConsoleColor.DarkCyan);
            ConsoleHelper.WriteWithColor("------------------------------------------------------------------------------------", ConsoleColor.DarkMagenta);

            foreach (var drug in availableDrugs)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor($"Drug ID : {drug.Id} / Name : {drug.Name} / Price  {drug.Price}$ / Left in stock : {drug.Count} ", ConsoleColor.Cyan);
            }
            ConsoleHelper.WriteWithColor("------------------------------------------------------------------------------------", ConsoleColor.DarkMagenta);
            ConsoleHelper.WriteWithColor("Enter Drug ID to choose and add drugs to basket you want to buy from the list", ConsoleColor.DarkYellow);
            Console.WriteLine("\n");
            int id;
            bool isRightInput = int.TryParse(Console.ReadLine(), out id);
            if (!isRightInput)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Wrong Input!", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto DrugsIdCheck;
            }
            var dbDrug = _drugStoreRepos.BuyAvailableDrug(id);
            if (dbDrug == null)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no drugs with this ID.", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto DrugsIdCheck;
            }
        CountCheck:
            Console.Clear();
            Console.WriteLine("\n\n");
            ConsoleHelper.WriteWithColor($"Drug ID : {dbDrug.Id} / Name : {dbDrug.Name} / Price  {dbDrug.Price}$ / Left in stock : {dbDrug.Count} ", ConsoleColor.Cyan);
            ConsoleHelper.WriteWithColor("Enter amount you want to buy or 0 to choose another drug", ConsoleColor.Cyan);
            int count;
            isRightInput = int.TryParse(Console.ReadLine(), out count);
            if (!isRightInput)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Wrong count input format!", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                count = 0;
                goto CountCheck;
            }
            if (count > dbDrug.Count)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor($"There is only {dbDrug.Count} {dbDrug.Name} left in Drugstore", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor($"Do you want to buy all of {dbDrug.Name} that's left?", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleKeyInfo cki2 = Console.ReadKey();
                if (cki2.Key == ConsoleKey.Y)
                {
                    dbDrug.DrugStore.BuyList.Add(dbDrug);
                    dbDrug.DrugStore.BuyAmount.Add(dbDrug.Count);
                    dbDrug.Count = 0;
                }
                else if (cki2.Key == ConsoleKey.N)
                {
                    goto DrugsIdCheck;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\n\n");
                    ConsoleHelper.WriteWithColor("Please select y/n", ConsoleColor.Yellow);
                    goto CountCheck;
                }
            }
            else
            {
                dbDrug.DrugStore.BuyList.Add(dbDrug);
                dbDrug.DrugStore.BuyAmount.Add(count);
                dbDrug.Count -= count;
            }
        yesNoCheck:
            if (_drugStoreRepos.GetAvailableDrugs().Count != 0)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("If you want to continue shopping press - y", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("If you want to see total and checkout press - n", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleKeyInfo cki3 = Console.ReadKey();
                if (cki3.Key == ConsoleKey.Y)
                {
                    goto DrugsIdCheck;
                }
                else if (cki3.Key == ConsoleKey.N)
                {
                    Console.Clear();
                    var buyList = dbDrug.DrugStore.BuyList;
                    var buyAmount = dbDrug.DrugStore.BuyAmount;
                    double total = 0;
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("List of goods in basket and their total", ConsoleColor.DarkCyan);
                    for (int i = 0; i < buyList.Count; i++)
                    {
                        ConsoleHelper.WriteWithColor($"|| Name|{buyList[i].Name} ||| Amount|{buyAmount[i]} ||| Price|{buyList[i].Price}$ ||| Total|{buyList[i].Price * buyAmount[i]}$ || ", ConsoleColor.Cyan);
                        total += buyList[i].Price * buyAmount[i];
                    }
                    Console.WriteLine("\n\n");
                    ConsoleHelper.WriteWithColor($"Your overall total = {total}$ ", ConsoleColor.Cyan);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();

                    dbDrug.DrugStore.BuyList.Clear();
                    dbDrug.DrugStore.BuyAmount.Clear();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\n\n");
                    ConsoleHelper.WriteWithColor("Please select y/n", ConsoleColor.Yellow);
                    goto yesNoCheck;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\n\n");
                ConsoleHelper.WriteWithColor("We are out of stock", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("Proceed to checkout", ConsoleColor.Cyan);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();

                Console.Clear();
                var buyList = dbDrug.DrugStore.BuyList;
                var buyAmount = dbDrug.DrugStore.BuyAmount;
                double total = 0;
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("List of goods in basket and their total", ConsoleColor.DarkCyan);
                for (int i = 0; i < buyList.Count; i++)
                {
                    ConsoleHelper.WriteWithColor($"|| Name|{buyList[i].Name} ||| Amount|{buyAmount[i]} ||| Price|{buyList[i].Price}$ ||| Total|{buyList[i].Price * buyAmount[i]}$ || ", ConsoleColor.Cyan);
                    total += buyList[i].Price * buyAmount[i];
                }
                Console.WriteLine("\n\n");
                ConsoleHelper.WriteWithColor($"Your overall total = {total}$ ", ConsoleColor.Cyan);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();

                dbDrug.DrugStore.BuyList.Clear();
                dbDrug.DrugStore.BuyAmount.Clear();
            }
        }
    }
}