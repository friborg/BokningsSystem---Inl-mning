using BokningsSystem___Inlämning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BokningsSystem___Inlämning
{
    internal class Methods
    {
        static string? loggedInCustomer;
        internal static void RunApplication()
        {
            while (true)
            {
                LogInOptions();

                Console.ReadKey(true);
            }
        }
        internal static void LogInOptions()
        {
            Console.Clear();
            Console.WriteLine($"Welcome to Hannas Hotel!" +
                $"\nPlease log in or browse our website as a guest!" +
                $"\n[1] Log in" +
                $"\n[2] Log in as guest" +
                $"\n[3] Register a new account" +
                $"\n[4] Admin log in");

            int choice = int.Parse(Console.ReadLine());

            LogIn(choice);
        }
        internal static void LogIn(int choice)
        {
            Console.Clear();
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter your username: "); // gör till unique? 
                    var userName = Console.ReadLine();
                    Console.WriteLine("Enter your password: "); // kolla att det stämmer, annars "fel lösenord"
                    var password = Console.ReadLine();
                    loggedInCustomer = userName;

                    Thread.Sleep(1000);
                    Console.Write(".");
                    Thread.Sleep(1000);
                    Console.Write(".");
                    Thread.Sleep(1000);
                    Console.Write(".");
                    Thread.Sleep(1000);

                    MainMenuChoices();
                    break;
                case 2:
                    MainMenuChoices();
                    break;
                case 3:
                    Console.WriteLine("Choose a username: "); 
                    string username = Console.ReadLine(); // säkra så att man måste skriva någonting på alla inputs
                    Console.WriteLine("Choose a password: ");
                    string newPassword = Console.ReadLine();
                    Console.WriteLine("Enter your name: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter your phone-number: ");
                    string phoneNumber = Console.ReadLine();

                    using (var db = new Context())
                    {
                        var uniqueUsername = db.Customers.Where(c => c.UserName == username).SingleOrDefault();
                        if (uniqueUsername != null)
                        {
                            Console.WriteLine("Username already exists, please try again!");
                            Console.ReadKey();
                            LogIn(choice);
                        }
                        else if (uniqueUsername == null)
                        {
                            var newCustomer = new Customer
                            {
                                Name = name,
                                PhoneNumber = phoneNumber,
                                Password = newPassword,
                                UserName = username
                            };
                            var customerList = db.Customers;
                            customerList.Add(newCustomer);
                            db.SaveChanges();
                        }
                    }
                    LogInOptions();
                    break;
                case 4:
                    Console.WriteLine("Enter admin password: ");
                    var adminPassword = Console.ReadLine();
                    if (adminPassword == "1234")
                    {
                        AdminMenuChoices();
                    }
                    break;
                default:
                    Console.WriteLine("Wrong input, please try again!");
                    break;
            }
        }
        private static void MainMenuChoices()
        {
            Console.Clear();
            using (var db = new Context())
            {
                string? loggedInName;
                if (loggedInCustomer != null)
                {
                    loggedInName = db.Customers.Where(c => c.UserName == loggedInCustomer).Select(c => c.Name).FirstOrDefault();
                }
                else { loggedInName = "guest"; }

                Console.WriteLine($"Welcome {loggedInName} to Hannas Hotel!" +
                    $"\nBrowse our website by choosing from the menu options below!" +
                    $"\n\n[1] Book a room " +
                    $"\n[2] Cancel a booked room " +
                    //$"\n[3]" + idé till vg, kunna lämna reviews som syns på startsidan för andra användare 1§sawt56vgfccc287uxzskmj>"%#
                    $"\n[3] Log out");

                int menuChoice = int.Parse(Console.ReadLine());
                MainMenu(menuChoice);
            }
        }
        private static void MainMenu(int menuChoice)
        {
            Console.Clear();

            switch (menuChoice)
            {
                case 1:
                    BookRoom();
                    break;
                case 2:
                    // Visa alla bokningar som den inloggade har gjort, välj id för att avboka
                    break;
                case 3:
                    loggedInCustomer = null;
                    LogInOptions();
                    break;
                default:
                    break;
            }
        }
        private static void BookRoom()
        {
            ConfirmBooking();
        }
        private static void ConfirmBooking()
        {
            using (var db = new Context())
            {

            }
        }
        private static void AdminMenuChoices()
        {
            Console.Clear();
            Console.WriteLine($"Welcome back admin!" +
                    $"\n\n[1] Add a new room to the system" +
                    $"\n[2] View statistics" +
                    $"\n[3] Log out");
            int adminChoice = int.Parse(Console.ReadLine());
            AdminMenu(adminChoice);
        }
        private static void AdminMenu(int adminChoice)
        {
            Console.Clear();
            switch (adminChoice)
            {
                case 1:
                    Console.WriteLine("Enter the new room-number: ");
                    int newRoomNumber = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the floor-number of the new room: ");
                    int newFloorNumber = int.Parse(Console.ReadLine());
                    Console.WriteLine("How many people can be booked in the new room?");
                    int newRoomCapacity = int.Parse(Console.ReadLine());
                    Console.WriteLine("How much does the new room cost? (Per night)");
                    int newRoomCost = int.Parse(Console.ReadLine());

                    using (var db = new Context())
                    {
                        var newRoom = new HotelRoom
                        {
                            RoomNumber = newRoomNumber,
                            FloorNumber = newFloorNumber,
                            Capacity = newRoomCapacity,
                            Cost = newRoomCost
                        };
                        var roomList = db.Hotelrooms;
                        roomList.Add(newRoom);
                        db.SaveChanges();
                    }
                    Console.WriteLine("The new room has been added!");
                    AdminMenuChoices();
                    break;
                case 2:
                    Console.WriteLine("Här ska querys läggas in");
                    AdminMenuChoices();
                    break;
                case 3:
                    LogInOptions();
                    break;
                default:
                    Console.WriteLine("Wrong input, please try again!");
                    break;
            }
        }
    }
}
