﻿using Core.Constants;
using Core.Entities;
using Core.Helpers;
using Data;
using Presentation.Services;
using System.Net;
using System.Xml;

namespace Presentation
{
    public static class Program
    {
        private readonly static AdminService _adminService;
        private readonly static DruggistService _druggistService;
        private readonly static DrugService _drugService;
        private readonly static DrugStoreService _drugStoreService;
        private readonly static OwnerService _ownerService;
        static Program()
        {
            DbInitializer.SeedAdmins();
            _adminService = new AdminService();
            _druggistService = new DruggistService();
            _drugService = new DrugService();
            _drugStoreService = new DrugStoreService();
            _ownerService = new OwnerService();
        }
        static void Main()
        {
            Console.SetWindowSize(Console.WindowWidth = Console.LargestWindowWidth, Console.WindowHeight = Console.LargestWindowHeight);
        Authentication:
            _adminService.Authorize();
            while (true)
            {
                Console.Clear();
            MainMenuCheck:
                Console.WriteLine("\n\n\n");
                ConsoleHelper.WriteWithColor("██████╗ ██╗  ██╗ █████╗ ██████╗ ███╗   ███╗ █████╗  ██████╗██╗   ██╗     ██████╗ ██████╗    ", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor("██╔══██╗██║  ██║██╔══██╗██╔══██╗████╗ ████║██╔══██╗██╔════╝╚██╗ ██╔╝    ██╔════╝██╔═══██╗   ", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor("██████╔╝███████║███████║██████╔╝██╔████╔██║███████║██║      ╚████╔╝     ██║     ██║   ██║   ", ConsoleColor.Yellow);
                ConsoleHelper.WriteBWithColor("██╔═══╝ ██╔══██║██╔══██║██╔══██╗██║╚██╔╝██║██╔══██║██║       ╚██╔╝      ██║     ██║   ██║   ", ConsoleColor.Yellow);
                ConsoleHelper.WriteBWithColor("██║     ██║  ██║██║  ██║██║  ██║██║ ╚═╝ ██║██║  ██║╚██████╗   ██║       ╚██████╗╚██████╔╝██╗", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor("╚═╝     ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝     ╚═╝╚═╝  ╚═╝ ╚═════╝   ╚═╝        ╚═════╝ ╚═════╝ ╚═╝", ConsoleColor.Yellow);
                Console.WriteLine("\n\n");
                ConsoleHelper.WriteWithColor("[1] - Owner Menu", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("[2] - Drugstore Menu", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("[3] - Druggist Menu", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("[4] - Drug Menu", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("[5] - Log Out", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("[0] - Terminate Session", ConsoleColor.Cyan);

                int menu;
                bool isRightInput = int.TryParse(Console.ReadLine(), out menu);
                if (!isRightInput)
                {
                    Console.Clear();
                    ConsoleHelper.WriteWithColor("Incorrect input format!  Please select from 0 to 5", ConsoleColor.Red);
                    goto MainMenuCheck;
                }
                switch (menu)
                {
                    case (int)MainMenuOptions.OwnerMenu:
                        while (true)
                        {
                            Console.Clear();
                        OwnerMenu:
                            Console.WriteLine("\n\n\n");
                            ConsoleHelper.WriteWithColor("Owner Menu", ConsoleColor.Yellow);
                            Console.WriteLine("\n\n");
                            ConsoleHelper.WriteWithColor("[1] - Create Owner", ConsoleColor.Cyan);
                            ConsoleHelper.WriteWithColor("[2] - Update Owner", ConsoleColor.Cyan);
                            ConsoleHelper.WriteWithColor("[3] - Remove Owner", ConsoleColor.Cyan);
                            ConsoleHelper.WriteWithColor("[4] - Get All Owners", ConsoleColor.Cyan);
                            ConsoleHelper.WriteWithColor("[0] - Back To Main Menu", ConsoleColor.Cyan);

                            int input;
                            isRightInput = int.TryParse(Console.ReadLine(), out input);
                            if (!isRightInput)
                            {
                                Console.Clear();
                                ConsoleHelper.WriteWithColor("Incorrect input format!  Please select from 0 to 4", ConsoleColor.Red);
                                goto MainMenuCheck;
                            }
                            switch (input)
                            {
                                case (int)OwnerMenu.CreateOwner:
                                    _ownerService.Create();
                                    break;
                                case (int)OwnerMenu.UpdateOwner:
                                    break;
                                case (int)OwnerMenu.RemoveOwner:
                                    break;
                                case (int)OwnerMenu.GetAllOwners:
                                    break;
                                case (int)OwnerMenu.MainMenu:
                                    Console.Clear();
                                    goto MainMenuCheck;
                                default:
                                    {
                                        Console.Clear();
                                        ConsoleHelper.WriteWithColor("Incorrect input format!  Please select from 0 to 4", ConsoleColor.Red);
                                        goto OwnerMenu;
                                    }
                            }
                        }
                    case (int)MainMenuOptions.DrugStoreMenu:
                        while (true)
                        {
                        DrugStoreMenu:
                            Console.Clear();
                            Console.WriteLine("\n\n\n");
                            ConsoleHelper.WriteWithColor("Drugstore Menu", ConsoleColor.Yellow);
                            Console.WriteLine("\n\n");
                            ConsoleHelper.WriteWithColor("[1] - Create Drugstore", ConsoleColor.Cyan);
                            ConsoleHelper.WriteWithColor("[2] - Update Drugstore", ConsoleColor.Cyan);
                            ConsoleHelper.WriteWithColor("[3] - Remove Drugstore", ConsoleColor.Cyan);
                            ConsoleHelper.WriteWithColor("[4] - Get All Drugstores", ConsoleColor.Cyan);
                            ConsoleHelper.WriteWithColor("[5] - Get All Drugstores by Owner", ConsoleColor.Cyan);
                            ConsoleHelper.WriteWithColor("[6] - Sale", ConsoleColor.Cyan);
                            ConsoleHelper.WriteWithColor("[0] - Back To Main Menu", ConsoleColor.Cyan);

                            int input;
                            isRightInput = int.TryParse(Console.ReadLine(), out input);
                            if (!isRightInput)
                            {
                                Console.Clear();
                                ConsoleHelper.WriteWithColor("Incorrect input format!  Please select from 0 to 6", ConsoleColor.Red);
                                goto MainMenuCheck;
                            }
                            switch (input)
                            {
                                case (int)DrugStoreMenu.CreateDrugStore:
                                    break;
                                case (int)DrugStoreMenu.UpdateDrugStore:
                                    break;
                                case (int)DrugStoreMenu.RemoveDrugStore:
                                    break;
                                case (int)DrugStoreMenu.GetAllDrugStores:
                                    break;
                                case (int)DrugStoreMenu.GetAllByOwner:
                                    break;
                                case (int)DrugStoreMenu.MainMenu:
                                    Console.Clear();
                                    goto MainMenuCheck;
                                default:
                                    {
                                        Console.Clear();
                                        ConsoleHelper.WriteWithColor("Incorrect input format!  Please select from 0 to 5", ConsoleColor.Red);
                                        goto DrugStoreMenu;
                                    }
                            }
                        }
                    case (int)MainMenuOptions.DruggistMenu:
                        while (true)
                        {
                            Console.Clear();
                        DruggistMenuCheck:
                            Console.WriteLine("\n\n\n");
                            ConsoleHelper.WriteWithColor("Druggist Menu", ConsoleColor.Yellow);
                            Console.WriteLine("\n\n");
                            ConsoleHelper.WriteWithColor("[1] - Create Druggist", ConsoleColor.Cyan);
                            ConsoleHelper.WriteWithColor("[2] - Update Druggist", ConsoleColor.Cyan);
                            ConsoleHelper.WriteWithColor("[3] - Remove Druggist", ConsoleColor.Cyan);
                            ConsoleHelper.WriteWithColor("[4] - Get All Druggists", ConsoleColor.Cyan);
                            ConsoleHelper.WriteWithColor("[0] - Back To Main Menu", ConsoleColor.Cyan);

                            int input;
                            isRightInput = int.TryParse(Console.ReadLine(), out input);
                            if (!isRightInput)
                            {
                                Console.Clear();
                                ConsoleHelper.WriteWithColor("Incorrect input format!  Please select from 0 to 5", ConsoleColor.Red);
                                goto MainMenuCheck;
                            }
                            switch (input)
                            {
                                case (int)DruggistMenu.CreateDruggist:
                                    break;
                                case (int)DruggistMenu.UpdateDruggist:
                                    break;
                                case (int)DruggistMenu.RemoveDruggist:
                                    break;
                                case (int)DruggistMenu.GetAllDruggists:
                                    break;
                                case (int)DruggistMenu.GetAllByDrugstore:
                                    break;
                                case (int)DruggistMenu.MainMenu:
                                    Console.Clear();
                                    goto MainMenuCheck;
                                default:
                                    {
                                        Console.Clear();
                                        ConsoleHelper.WriteWithColor("Incorrect input format! Please select from 0 to 5", ConsoleColor.Red);
                                        goto DruggistMenuCheck;
                                    }
                            }
                        }
                    case (int)MainMenuOptions.DrugMenu:
                        while (true)
                        {
                            Console.Clear();
                        DrugMenuCheck:
                            Console.WriteLine("\n\n\n");
                            ConsoleHelper.WriteWithColor("Drug Menu", ConsoleColor.Yellow);
                            Console.WriteLine("\n\n");
                            ConsoleHelper.WriteWithColor("[1] - Add Drug", ConsoleColor.Cyan);
                            ConsoleHelper.WriteWithColor("[2] - Update Drug", ConsoleColor.Cyan);
                            ConsoleHelper.WriteWithColor("[3] - Remove Drug", ConsoleColor.Cyan);
                            ConsoleHelper.WriteWithColor("[4] - Get All Drugs", ConsoleColor.Cyan);
                            ConsoleHelper.WriteWithColor("[0] - Back To Main Menu", ConsoleColor.Cyan);

                            int input;
                            isRightInput = int.TryParse(Console.ReadLine(), out input);
                            if (!isRightInput)
                            {
                                Console.Clear();
                                ConsoleHelper.WriteWithColor("Incorrect input format!  Please select from 0 to 5", ConsoleColor.Red);
                                goto MainMenuCheck;
                            }
                            switch (input)
                            {
                                case (int)DrugMenu.CreateDrug:
                                    break;
                                case (int)DrugMenu.UpdateDrug:
                                    break;
                                case (int)DrugMenu.RemoveDrug:
                                    break;
                                case (int)DrugMenu.GetAllDrugs:
                                    break;
                                case (int)DrugMenu.GetAllByDrugStore:
                                    break;
                                case (int)DrugMenu.MainMenu:
                                    Console.Clear();
                                    goto MainMenuCheck;
                                default:
                                    {
                                        Console.Clear();
                                        ConsoleHelper.WriteWithColor("Incorrect input format! Please select from 0 to 5", ConsoleColor.Red);
                                        goto DrugMenuCheck;
                                    }
                            }
                        }
                    case (int)MainMenuOptions.LogOut:
                        Console.Clear();
                    LogOutCheck:
                        Console.WriteLine("\n\n\n");
                        ConsoleHelper.WriteWithColor("Are you sure you want to log out of system? y/n", ConsoleColor.Red);
                        ConsoleKeyInfo cki = Console.ReadKey();
                        if (cki.Key == ConsoleKey.Y)
                        {
                            Console.Clear();
                            goto Authentication;
                        }
                        else if (cki.Key == ConsoleKey.N)
                        {
                            Console.Clear();
                            goto MainMenuCheck;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("\n\n\n");
                            ConsoleHelper.WriteWithColor("Please select y/n", ConsoleColor.Red);
                            goto LogOutCheck;
                        }
                    case (int)MainMenuOptions.Exit:
                        Console.Clear();
                    ExitCheck:
                        Console.WriteLine("\n\n\n");
                        ConsoleHelper.WriteWithColor("Are you sure you want to terminate current session? y/n", ConsoleColor.Red);
                        ConsoleKeyInfo cki2 = Console.ReadKey();
                        if (cki2.Key == ConsoleKey.Y)
                        {
                            return;
                        }
                        else if (cki2.Key == ConsoleKey.N)
                        {
                            Console.Clear();
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("\n\n\n");
                            ConsoleHelper.WriteWithColor("Please select y/n", ConsoleColor.Red);
                            goto ExitCheck;
                        }
                    default:
                        Console.Clear();
                        ConsoleHelper.WriteWithColor("Please select valid option from 0 to 5", ConsoleColor.Red);
                        goto MainMenuCheck;
                }
            }
        }
    }
}