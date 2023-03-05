using Data.Repos.Concrete;
using Core.Helpers;
using Core.Entities;
using Core.Extentions;

namespace Presentation.Services
{
    public class DruggistService
    {
        private readonly DruggistRepos _druggistRepos;
        private readonly DrugStoreRepos _drugStoreRepos;
        public DruggistService()
        {
            _druggistRepos = new DruggistRepos();
            _drugStoreRepos = new DrugStoreRepos();
        }
        public void Create()
        {
            Console.Clear();
            var drugstores = _drugStoreRepos.GetAll();
            if (drugstores.Count == 0)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no Drugstores to assign new druggist to! Please first create Drugstore", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor(" Press any key to return to main menu", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }

        NameCheck:
            Console.WriteLine("\n");
            ConsoleHelper.WriteContinuosly("Enter druggist name:", ConsoleColor.DarkYellow);
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
            if (name.IsDetails() == false)
            {
                Console.Clear();
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
            ConsoleHelper.WriteContinuosly("Enter druggist surname:", ConsoleColor.DarkYellow);
            string surname = Console.ReadLine();
            if (String.IsNullOrEmpty(surname))
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Surname is required and must be filled", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto SurnameCheck;
            }
            if (surname.IsDetails() == false)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Surname cannot contain numbers", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto SurnameCheck;
            }

        AgeCheck:
            Console.Clear();
            Console.WriteLine("\n");
            ConsoleHelper.WriteContinuosly("Enter druggist age : ", ConsoleColor.DarkYellow);
            int age;
            bool isRightInput = int.TryParse(Console.ReadLine(), out age);
            if (!isRightInput)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Wrong Input!", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("Please enter valid age", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto AgeCheck;
            }
            if (age < 18)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Druggist's age cannot be set lower than 18!", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("Please enter valid age", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto AgeCheck;
            }

        ExperienceCheck:
            Console.Clear();
            Console.WriteLine("\n");
            ConsoleHelper.WriteContinuosly("Enter druggist experience : ", ConsoleColor.DarkYellow);
            int exp;
            isRightInput = int.TryParse(Console.ReadLine(), out exp);
            if (!isRightInput)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Wrong Input!", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("Please enter valid experience", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto ExperienceCheck;
            }
            if (age < 18)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Druggist's age cannot be set lower than 18!", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("Please enter valid age", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto ExperienceCheck;
            }
            if (age - 18 < exp)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Druggist's experience cannot be set higher than his age!", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("Please enter valid age", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto ExperienceCheck;
            }

        DrugStoreCheck:
            Console.Clear();
            Console.WriteLine("\n\n");
            foreach (var drugStore in drugstores)
            {
                ConsoleHelper.WriteWithColor($"Drugstore ID : {drugStore.Id}/ Name : {drugStore.Name} / Owner Fullname : {drugStore.Owner.Name} {drugStore.Owner.Surname}", ConsoleColor.Cyan);
                Console.WriteLine("\n");
            }
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor("Enter drugstore ID to assign new druggist to", ConsoleColor.DarkYellow);
            int id;
            isRightInput = int.TryParse(Console.ReadLine(), out id);
            if (!isRightInput)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Wrong Input! Enter drugstore ID to assign new druggist to", ConsoleColor.Red);
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

            var druggist = new Druggist
            {
                Name = name,
                Surname = surname,
                Age = age,
                Experience = exp,
                DrugStore = dbDrugstore,
            };

            _druggistRepos.Add(druggist);
            dbDrugstore.Druggists.Add(druggist);

            Console.Clear();
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor($"New Druggist {druggist.Name} {druggist.Surname} successfully created and assigned to {dbDrugstore.Name}", ConsoleColor.DarkGreen);
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
            Console.ReadKey();
        }
        public void Update()
        {
            Console.Clear();
            var druggists = _druggistRepos.GetAll();
            if (druggists.Count == 0)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no druggists to update details of", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }

        IdCheck:
            Console.Clear();
            Console.WriteLine("\n");
            foreach (var druggist in druggists)
            {
                ConsoleHelper.WriteWithColor($"Druggist ID : {druggist.Id} / Fullname : {druggist.Name} {druggist.Surname} / Drugstore Id : {druggist.DrugStore.Id} /  Name : {druggist.DrugStore.Name}", ConsoleColor.Cyan);
                Console.WriteLine("\n");
            }
            ConsoleHelper.WriteWithColor("Enter Druggist ID to update its details or 0 to return back to menu", ConsoleColor.DarkYellow);
            int id;
            bool isRightInput = int.TryParse(Console.ReadLine(), out id);
            if (!isRightInput)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Wrong Id Input Format! ", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto IdCheck;
            }
            else if (id == 0)
            {
                return;
            }
            var dbDruggist = _druggistRepos.Get(id);
            if (dbDruggist == null)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no druggist with this ID number.", ConsoleColor.Red);
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
                ConsoleHelper.WriteContinuosly("Enter new druggist name:", ConsoleColor.DarkYellow);
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
                if (name.IsDetails() == false)
                {
                    Console.Clear();
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
                ConsoleHelper.WriteContinuosly("Enter new druggist surname:", ConsoleColor.DarkYellow);
                string surname = Console.ReadLine();
                if (String.IsNullOrEmpty(surname))
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Surname is required and must be filled", ConsoleColor.Red);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto SurnameCheck;
                }
                if (surname.IsDetails() == false)
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Surname cannot contain numbers", ConsoleColor.Red);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto SurnameCheck;
                }

            AgeCheck:
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteContinuosly("Enter druggist age : ", ConsoleColor.DarkYellow);
                int age;
                isRightInput = int.TryParse(Console.ReadLine(), out age);
                if (!isRightInput)
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Wrong Input!", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("Please enter valid age", ConsoleColor.Yellow);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto AgeCheck;
                }
                if (age < 18)
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Druggist's age cannot be set lower than 18!", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("Please enter valid age", ConsoleColor.Yellow);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto AgeCheck;
                }

            ExperienceCheck:
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteContinuosly("Enter druggist experience : ", ConsoleColor.DarkYellow);
                int exp;
                isRightInput = int.TryParse(Console.ReadLine(), out exp);
                if (!isRightInput)
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Wrong Input!", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("Please enter valid experience", ConsoleColor.Yellow);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto ExperienceCheck;
                }
                if (age < 18)
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Druggist's age cannot be set lower than 18!", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("Please enter valid age", ConsoleColor.Yellow);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto ExperienceCheck;
                }
                if (age - 18 < exp)
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Druggist's experience cannot be set higher than his age!", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("Please enter valid age", ConsoleColor.Yellow);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto ExperienceCheck;
                }

            DrugStoreCheck:
                Console.Clear();
                var drugstores = _drugStoreRepos.GetAll();
                foreach (var drugstore in drugstores)
                {
                    ConsoleHelper.WriteWithColor($"Drugstore ID : {drugstore.Id} / Name {drugstore.Name} / Owner Fullname : {drugstore.Owner.Name} {drugstore.Owner.Surname}", ConsoleColor.Cyan);
                    Console.WriteLine("\n");
                }
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Enter drugstore ID to assign new druggist to", ConsoleColor.DarkYellow);
                int input;
                isRightInput = int.TryParse(Console.ReadLine(), out input);
                if (!isRightInput)
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Wrong Input! Enter drugstore ID to assign new druggist to", ConsoleColor.Red);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                    Console.ReadKey();
                    goto DrugStoreCheck;
                }
                var dbDrugstore = _drugStoreRepos.Get(input);
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

                string oldDruggist = dbDruggist.Name;
                string oldDruggistSurname = dbDruggist.Surname;
                string oldDrugstore = dbDruggist.DrugStore.Name;

                dbDruggist.Name = name;
                dbDruggist.Surname = surname;
                dbDruggist.Age = age;
                dbDruggist.Experience = exp;
                dbDruggist.DrugStore.Druggists.Remove(dbDruggist);
                dbDruggist.DrugStore = dbDrugstore;

                _druggistRepos.Update(dbDruggist);
                dbDrugstore.Druggists.Add(dbDruggist);

                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor($"{oldDruggist} {oldDruggistSurname} Druggist of {oldDrugstore} details updated into {dbDruggist.Name} {dbDruggist.Surname} successfully!", ConsoleColor.DarkGreen);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
            }
        }
        public void Delete()
        {
            Console.Clear();
            if (_druggistRepos.GetAll().Count == 0)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no druggist profiles to delete", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }
        IdCheck:
            Console.Clear();
            Console.WriteLine("\n");
            foreach (var druggist in _druggistRepos.GetAll())
            {
                ConsoleHelper.WriteWithColor($"Druggist ID : {druggist.Id} / Fullname : {druggist.Name} {druggist.Surname} / Drugstore ID : {druggist.DrugStore.Id} / Name : {druggist.DrugStore.Name}", ConsoleColor.Cyan);
                Console.WriteLine("\n");
            }
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor("Enter druggist ID to delete it's profile or 0 to return back to menu", ConsoleColor.DarkYellow);
            int id;
            bool isRightInput = int.TryParse(Console.ReadLine(), out id);
            if (!isRightInput)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Wrong Input! Enter druggist ID to delete it's profile", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto IdCheck;
            }
            else if (id == 0)
            {
                return;
            }
            var dbDruggist = _druggistRepos.Get(id);
            if (dbDruggist == null)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no druggist with this ID number.", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                goto IdCheck;
            }
            else
            {
            yesNoCheck:
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteContinuosly($"Are you sure you want to remove {dbDruggist.Name} {dbDruggist.Surname }druggist profile y/n", ConsoleColor.Magenta);
                ConsoleKeyInfo cki2 = Console.ReadKey();
                if (cki2.Key == ConsoleKey.Y)
                {
                    Console.Clear();
                    _druggistRepos.Delete(dbDruggist);
                    Console.WriteLine("\n");
                    ConsoleHelper.WriteWithColor($"{dbDruggist.Name} {dbDruggist.Surname} druggist of {dbDruggist.DrugStore.Name} drugstore successfully deleted!", ConsoleColor.DarkGreen);
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
            var druggists = _druggistRepos.GetAll();
            if (druggists.Count == 0)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("There is no druggist profiles in database", ConsoleColor.Yellow);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                return;
            }

            foreach (var druggist in druggists)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor($"Druggist ID : {druggist.Id} / Fullname : {druggist.Name} {druggist.Surname} / Drugstore Name : {druggist.DrugStore.Name}", ConsoleColor.Cyan);
            }
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
            Console.ReadKey();
        }
        public void GetAllByDrugstore()
        {
        IdCheck:
            Console.Clear();
            var drugstores = _drugStoreRepos.GetAll();
            foreach (var drugstore in drugstores)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor($"Drugstore ID : {drugstore.Id} / Drugstore Name : {drugstore.Name} / Drugstore Owner Fullname {drugstore.Owner.Name} {drugstore.Owner.Surname} ", ConsoleColor.Cyan);
            }
            ConsoleHelper.WriteWithColor("--------------------------------------------------------------------------------------------------", ConsoleColor.DarkMagenta);
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor("Enter Drugstore ID to get all druggists working there or 0 to return back to menu", ConsoleColor.DarkYellow);
            int id;
            bool isRightInput = int.TryParse(Console.ReadLine(), out id);
            if (!isRightInput)
            {
                Console.Clear();
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Wrong Input! Enter Drugstore ID to show list of druggists working there", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to continue", ConsoleColor.Yellow);
                Console.ReadKey();
                goto IdCheck;
            }
            else if (id == 0)
            {
                return;
            }
            var druggists = _druggistRepos.GetAllByDrugStore(id);
            if (druggists.Count == 0)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("This drugstore does not have any druggist.", ConsoleColor.Red);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
                Console.ReadKey();
                goto IdCheck;
            }
            Console.Clear();
            ConsoleHelper.WriteWithColor($"List of Druggists", ConsoleColor.Cyan);
            Console.WriteLine("\n");
            foreach(var druggist in druggists)
            {
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor($"Druggist ID : {druggist.Id} / Druggist Fullname : {druggist.Name} {druggist.Surname}", ConsoleColor.Cyan);
            }
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor("Press any key to return to menu", ConsoleColor.Yellow);
            Console.ReadKey();
        }
    }
}
