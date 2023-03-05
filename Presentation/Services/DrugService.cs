using Core.Helpers;
using Data.Repos.Concrete;
using Core.Entities;

namespace Presentation.Services
{
    public class DrugService
    {
        private readonly DrugRepos _drugRepos;
        private readonly DrugStoreRepos _drugStoreRepos;
        public DrugService()
        {
            _drugRepos = new DrugRepos();
            _drugStoreRepos = new DrugStoreRepos();
        }
        public void Create()
        {
            Console.Clear();
            var drugstores = _drugStoreRepos.GetAll();
            if (drugstores.Count == 0)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no Drugstores to ship new drug to! Please first create Drugstore", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor(" Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }
        NameCheck:
            Console.Clear();
            Console.WriteLine("\n");
            ConsoleHelper.WriteContinuosly("Enter drug name :", ConsoleColor.DarkYellow);
            string name = Console.ReadLine();
            if (String.IsNullOrEmpty(name))
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Name is required and must be filled", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto NameCheck;
            }
        PriceCheck:
            Console.Clear();
            Console.WriteLine("\n");
            ConsoleHelper.WriteContinuosly("Enter drug price :", ConsoleColor.DarkYellow);
            double price;
            bool isRightInput = double.TryParse(Console.ReadLine(), out price);
            if (!isRightInput)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Wrong Input!", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("Please enter valid price", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto PriceCheck;
            }
            if (price <= 0)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Price cannot be set lower than 0!", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("Please enter valid price", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto PriceCheck;
            }
        CountCheck:
            Console.Clear();
            Console.WriteLine("\n");
            ConsoleHelper.WriteContinuosly("Enter drug count :", ConsoleColor.DarkYellow);
            int count;
            isRightInput = int.TryParse(Console.ReadLine(), out count);
            if (!isRightInput)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Wrong Input!", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("Please enter valid count", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto CountCheck;
            }
            if (count <= 0)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Count cannot be set lower than 1!", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("Please enter valid price", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto PriceCheck;
            }
        DrugStoreCheck:
            Console.Clear();
            Console.WriteLine("\n");
            foreach (var drugstore in drugstores)
            {
                ConsoleHelper.WriteWithColor($"Drugstore ID : {drugstore.Id} / Name : {drugstore.Name} / Owner Fullname : {drugstore.Owner.Name} {drugstore.Owner.Surname}", ConsoleColor.Cyan);
                Console.WriteLine("\n");
            }
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor("Enter drugstore ID to ship new drug to it", ConsoleColor.DarkYellow);
            int id;
            isRightInput = int.TryParse(Console.ReadLine(), out id);
            if (!isRightInput)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Wrong Input! Enter drugstore ID to ship new drug to it", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto DrugStoreCheck;
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
                goto DrugStoreCheck;
            }
            var drug = new Drug
            {
                Name = name,
                Price = price,
                Count = count,
                DrugStore = dbDrugstore,
            };
            _drugRepos.Add(drug);
            dbDrugstore.Drugs.Add(drug);

            Console.Clear();
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor($"New Drug {drug.Name} with price of {drug.Price}$ successfully created and shipped to {dbDrugstore.Name}", ConsoleColor.DarkGreen);
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
            Console.ReadKey();
        }
        public void Update()
        {
            Console.Clear();
            var drugs = _drugRepos.GetAll();
            if (drugs.Count == 0)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no drugs to update details of", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }

        IdCheck:
            Console.Clear();
            Console.WriteLine("\n");
            foreach (var drug in drugs)
            {
                ConsoleHelper.WriteWithColor($"Drug ID : {drug.Id} / Name : {drug.Name} / Price : {drug.Price} ", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor($"Drug Count at Drugstore : {drug.Count} / Drugstore ID : {drug.DrugStore.Id} / Drugstore Name : {drug.DrugStore.Name}", ConsoleColor.Cyan);
                Console.WriteLine("\n");
            }
            ConsoleHelper.WriteWithColor("Enter Drug ID to update its details or 0 to return back to menu", ConsoleColor.DarkYellow);
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
            var dbDrug = _drugRepos.Get(id);
            if (dbDrug == null)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no drug with this ID number.", ConsoleColor.Red);
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
                ConsoleHelper.WriteContinuosly("Enter drug name :", ConsoleColor.DarkYellow);
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
            PriceCheck:
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteContinuosly("Enter drug price :", ConsoleColor.DarkYellow);
                double price;
                isRightInput = double.TryParse(Console.ReadLine(), out price);
                if (!isRightInput)
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Wrong Input!", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("Please enter valid price", ConsoleColor.Yellow);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto PriceCheck;
                }
                if (price <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Price cannot be set lower than 0!", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("Please enter valid price", ConsoleColor.Yellow);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto PriceCheck;
                }
            CountCheck:
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteContinuosly("Enter drug count :", ConsoleColor.DarkYellow);
                int count;
                isRightInput = int.TryParse(Console.ReadLine(), out count);
                if (!isRightInput)
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Wrong Input!", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("Please enter valid count", ConsoleColor.Yellow);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto CountCheck;
                }
                if (count <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Count cannot be set lower than 1!", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("Please enter valid price", ConsoleColor.Yellow);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto PriceCheck;
                }
            DrugStoreCheck:
                Console.Clear();
                var drugstores = _drugStoreRepos.GetAll();
                foreach (var drugstore in drugstores)
                {
                    ConsoleHelper.WriteWithColor($"Drugstore ID : {drugstore.Id} / Name : {drugstore.Name} / Owner Fullname : {drugstore.Owner.Name} {drugstore.Owner.Surname}", ConsoleColor.Cyan);
                    Console.WriteLine("\n");
                }
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Enter drugstore ID to ship drug to it", ConsoleColor.DarkYellow);
                int drugstoreId;
                isRightInput = int.TryParse(Console.ReadLine(), out drugstoreId);
                if (!isRightInput)
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Wrong Input! Enter drugstore ID to ship new drug to it", ConsoleColor.Red);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto DrugStoreCheck;
                }
                var dbDrugstore = _drugStoreRepos.Get(drugstoreId);
                if (dbDrugstore == null)
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("There is no drugstore with this ID number.", ConsoleColor.Red);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto DrugStoreCheck;
                }

                string oldName = dbDrug.Name;
                double oldPrice = dbDrug.Price;
                int oldCount = dbDrug.Count;

                dbDrug.Name = name;
                dbDrug.Price = price;
                dbDrug.Count = count;
                dbDrug.DrugStore.Drugs.Remove(dbDrug);
                dbDrug.DrugStore = dbDrugstore;

                _drugRepos.Update(dbDrug);
                dbDrugstore.Drugs.Add(dbDrug);

                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor($"Drug {oldName} with price of {oldPrice} and count of {oldCount}", ConsoleColor.DarkGreen);
                ConsoleHelper.WriteWithColor($"Updated into {dbDrug.Name} with price of {dbDrug.Price}$ and count of {dbDrug.Count} successfully!", ConsoleColor.DarkGreen);
                ConsoleHelper.WriteWithColor($"And Shipped to {dbDrug.DrugStore.Name}", ConsoleColor.DarkGreen);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
            }
        }
        public void Delete()
        {
            Console.Clear();
            if (_drugRepos.GetAll().Count == 0)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no drug profiles to delete", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }
        IdCheck:
            Console.Clear();
            Console.WriteLine("\n");
            foreach (var drug in _drugRepos.GetAll())
            {
                ConsoleHelper.WriteWithColor($"Drug ID : {drug.Id} / Name : {drug.Name} / Drug Location {drug.DrugStore.Name}", ConsoleColor.Cyan);
                Console.WriteLine("\n");
            }
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor("Enter Drug ID to delete it's profile or 0 to return back to menu", ConsoleColor.DarkYellow);
            int id;
            bool isRightInput = int.TryParse(Console.ReadLine(), out id);
            if (!isRightInput)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Wrong Input! Enter drug ID to delete it's profile", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto IdCheck;
            }
            else if (id == 0)
            {
                return;
            }
            var dbDrug = _drugRepos.Get(id);
            if (dbDrug == null)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no drug with this ID number.", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto IdCheck;
            }
            else
            {
            yesNoCheck:
                Console.WriteLine("\n");
                ConsoleHelper.WriteContinuosly($"Are you sure you want to remove {dbDrug.Name} drug y/n", ConsoleColor.Magenta);
                ConsoleKeyInfo cki2 = Console.ReadKey();
                if (cki2.Key == ConsoleKey.Y)
                {
                    Console.Clear();
                    _drugRepos.Delete(dbDrug);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor($"{dbDrug.Name} drug from {dbDrug.DrugStore.Name} drugstore successfully deleted!", ConsoleColor.DarkGreen);
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
            var drugs = _drugRepos.GetAll();
            if (drugs.Count == 0)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no drug profiles in database", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }

            Console.Clear();
            foreach (var drug in drugs)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor($"Drug ID : {drug.Id} / Drug Name : {drug.Name} / Drug Price {drug.Price} / Drug Count : {drug.Count}", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor($"Drugstore Name and Adress :{drug.DrugStore.Name} {drug.DrugStore.Address}", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor($"Drugstore Contact number :{drug.DrugStore.ContactNumber}", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("--------------------------------------------------------------------------------------------", ConsoleColor.Yellow);
            }
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
            Console.ReadKey();

        }
        public void GetAllByDrugStore()
        {
            Console.Clear();
            var drugs = _drugRepos.GetAll();
            if (drugs.Count == 0)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no drug profiles in database", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }
        IdCheck:
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor("Enter Drugstore ID to get all drugs there or 0 to return back to menu", ConsoleColor.DarkYellow);
            int id;
            bool isRightInput = int.TryParse(Console.ReadLine(), out id);
            if (!isRightInput)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Wrong Input! Enter Drugstore ID to get all drugs there", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto IdCheck;
            }
            else if (id == 0)
            {
                return;
            }
            Console.Clear();
            var dbDrugs = _drugRepos.GetAllByDrugStore(id);
            if (dbDrugs == null)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no drug in this Drugstore.", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto IdCheck;
            }

            foreach (var drug in dbDrugs)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor($"Drug ID : {drug.Id} / Drug Name : {drug.Name} / Drug Price {drug.Price} / Drug Count : {drug.Count}", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("--------------------------------------------------------------------------------------------", ConsoleColor.Yellow);
            }
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
            Console.ReadKey();

        }
        public void Filter()
        {
            var drugs = _drugRepos.GetAll();
            if (drugs.Count == 0)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no drug profiles in database", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }
        PriceCheck:
            ConsoleHelper.WriteWithColor("Enter Max price to show all the drugs with lower price than it or 0 to return back to menu", ConsoleColor.DarkYellow);
            double price;
            bool isRightInput = double.TryParse(Console.ReadLine(), out price);
            if (!isRightInput)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Wrong Input! Enter desired price to get all the drugs lower than input", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("Please choose from list above", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto PriceCheck;
            }
            else if (price == 0)
            {
                return;
            }
            var dbDrugs = _drugRepos.GetDrugsByPrice(price);
            if(dbDrugs.Count == 0)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no drugs under this price in database", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto PriceCheck;
            }          
            foreach (var drug in dbDrugs)
            {
                if(drug.Count == 0)
                {
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor($"Drug ID : {drug.Id} / Drug Name : {drug.Name} / Drug Price {drug.Price} / Drug Count : {drug.Count}", ConsoleColor.Cyan);
                    ConsoleHelper.WriteWithColor($"{drug.DrugStore.Name} is out of {drug.Name}", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("--------------------------------------------------------------------------------------------", ConsoleColor.Yellow);
                }
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor($"Drug ID : {drug.Id} / Drug Name : {drug.Name} / Drug Price {drug.Price} / Drug Count : {drug.Count}", ConsoleColor.Cyan);
            }
            ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
            Console.ReadKey();
        }
    }
}
