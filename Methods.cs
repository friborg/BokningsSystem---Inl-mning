using BokningsSystem___Inlämning.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Globalization;

namespace BokningsSystem___Inlämning
{
    internal class Methods
    {
        internal static void RunApplication()
        {
            while (true)
            {
                MenuChoices();
                Console.ReadKey(true);
            }
        }

        private static void BookRoom()
        {
            using (var db = new Context())
            {
                Console.WriteLine("Ange kundens namn: ");
                string customerName = Console.ReadLine();
                var customer = new Customer
                {
                    Name = customerName
                };
                var customerList = db.Customers;
                customerList.Add(customer);
                // gör till metod, möjlighet att välja befintlig 


                bool runSchedule = true;
                int week = 1;
                List<string> days = new List<string>();
                string[] dayNames = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
                days.AddRange(dayNames);
                while (runSchedule)   // få schemat att uppdateras i real-tid && visa när rum är upptagna eller lediga 
                {
                    Console.WriteLine($"Week number: {week}\n" +
                        $"Press + or - to navigate the weekly schedule.");
                    string input = Console.ReadLine();
                    if (input == "+")
                    {
                        week++;
                    }
                    if (input == "-" && week > 0)
                    {
                        week--;
                    }
                    else if (input == "-" && week == 0)
                    {
                        Console.WriteLine("You cannot view weeks prior to this!");
                    }

                    foreach (var day in days)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine($"\n{day}");
                        Console.ResetColor();
                        foreach (var room in db.ConferenceRooms)
                        {
                            Console.WriteLine($"Id: {room.Id}  Name: {room.Name}\tCapacity: {room.Capacity}");
                        }
                    }
                    Console.WriteLine("Input room-id of the room you wish to book:");
                    int roomId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Which day do you want to book? (1-5)");
                    int dayInput = int.Parse(Console.ReadLine());
                    foreach (var r in db.ConferenceRooms.Where(x => x.Id == roomId))
                    {
                        // if (week inte är bokad eller något)
                        var room = new BookedRoom
                        {
                            ConferenceRoomId = roomId,
                            CustomerId = 1,
                            Week = week,
                            Day = dayInput
                        };
                        var bookedList = db.Bookedrooms;
                        bookedList.Add(room);
                    }
                    db.SaveChanges();
                }
            }
            Console.ReadLine();
            Console.Clear();
        }
        //ConfirmBooking();
        private static void ConfirmBooking()
        {
            using (var db = new Context())
            {

            }
        }
        private static void AddRoom()
        {
            Console.WriteLine("Enter the name of the new room: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the new room-number: ");
            int newRoomNumber = int.Parse(Console.ReadLine());
            Console.WriteLine("How many chairs/spaces is there?");
            int newRoomCapacity = int.Parse(Console.ReadLine());
            Console.WriteLine("Does the room have a whiteboard? y / n");
            var choice = Console.ReadLine();
            bool whiteBoard = false;
            if (choice == "y")
            {
                whiteBoard = true;
            }
            else if (choice == "n")
            {
                whiteBoard = false;
            }
            Console.WriteLine("Does the room have a projector? y / n");
            var choice2 = Console.ReadLine();
            bool projector = false;
            if (choice == "y")
            {
                projector = true;
            }
            else if (choice == "n")
            {
                projector = false;
            }

            using (var db = new Context())
            {
                var newRoom = new ConferenceRoom
                {
                    Name = name,
                    RoomNumber = newRoomNumber,
                    Capacity = newRoomCapacity,
                    WhiteBoard = whiteBoard,
                    Projector = projector
                };
                var roomList = db.ConferenceRooms;
                roomList.Add(newRoom);
                db.SaveChanges();
            }
            Console.WriteLine("The new room has been added!");
        }
        private static void MenuChoices()
        {
            Console.Clear();
            Console.WriteLine($"Welcome back admin!" +
                    $"\n[1] Book a conference room " +
                    $"\n[2] Add a new room to the system" +
                    $"\n[3] View statistics");
            int adminChoice = int.Parse(Console.ReadLine());
            Menu(adminChoice);
        }
        private static void Menu(int adminChoice)
        {
            Console.Clear();
            switch (adminChoice)
            {
                case 1:
                    BookRoom();
                    break;
                case 2:
                    AddRoom();
                    break;
                case 3:
                    Console.WriteLine("Här ska querys läggas in");
                    break;
                default:
                    Console.WriteLine("Wrong input, please try again!");
                    break;
            }
        }
    }
}



