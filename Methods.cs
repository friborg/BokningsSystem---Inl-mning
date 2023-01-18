using BokningsSystem___Inlämning.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private static void PrintSchedule(int week, List<string> days)
        {
            using (var db = new Context())
            {
                int dayCounter = 0;
                int bookedRoomId = 0;
                string status = "";
                foreach (var day in days)
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine($"\n{day}");
                    Console.ResetColor();
                    dayCounter++;
                    foreach (var room in db.ConferenceRooms)
                    {
                        foreach (var r in db.Bookedrooms.Where(x => x.ConferenceRoomId == room.Id))
                        {
                            if (r.Week == week && r.Day == dayCounter)
                            {
                                status = "Booked!";
                                bookedRoomId = r.ConferenceRoomId;
                                break;
                            }
                            else
                            {
                                status = "Available!";
                                bookedRoomId = r.ConferenceRoomId;
                                break;
                            }
                        }
                        Console.WriteLine($"Id: {room.Id}  Name: {room.Name}\tCapacity: {room.Capacity}\t{status}\n");
                    }
                }
            }
        }
        private static void BookingInfoInput(int week)
        {
            Console.WriteLine("Input room-id of the room you wish to book:"); // fångar denna input istället för weeks
            int roomId = int.Parse(Console.ReadLine());
            Console.WriteLine("Which day do you want to book? (1-5)");
            int dayInput = int.Parse(Console.ReadLine());
            Console.WriteLine("[1]Add new customer or [2] choose existing?");
            int choice = int.Parse(Console.ReadLine());
            int cId = 0;
            using (var db = new Context())
            {

                if (choice == 1)
                {
                    Console.WriteLine("Customer name: ");
                    string customerName = Console.ReadLine();
                    Console.WriteLine("Customer social security number: ");
                    string socialSecurityNumber = Console.ReadLine();
                    Console.WriteLine("Customer phone number: ");
                    string phoneNumber = Console.ReadLine();

                    var customer = new Customer
                    {
                        Name = customerName,
                        SocialSecurityNumber = socialSecurityNumber,
                        PhoneNumber = phoneNumber
                    };
                    var customerList = db.Customers;
                    customerList.Add(customer);
                    db.SaveChanges();
                    return;
                }
                else if (choice == 2)
                {
                    foreach (var customer in db.Customers)
                    {
                        Console.WriteLine($"Id: [{customer.Id}]\tName: {customer.Name}\n" +
                            $"Social security number: {customer.SocialSecurityNumber}\tPhone number: {customer.PhoneNumber}");
                    }
                    Console.WriteLine("Choose a customer-id:");
                    cId = int.Parse(Console.ReadLine());

                    var rooms = from r in db.ConferenceRooms  // kanske inte kan joina här heller 
                                join b in db.Bookedrooms on r.Id equals b.ConferenceRoomId into allRooms
                                from ar in allRooms.DefaultIfEmpty()
                                select new { Id = r.Id, Name = r.Name, Capacity = r.Capacity, BookedRoomId = ar.ConferenceRoomId, Day = ar.Day, Week = ar.Week };
                    foreach (var r in rooms.Where(x => x.Id == roomId)) // kör tre gånger (för varje rum?)
                    {
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
        }
        private static void BookRoom()
        {
            bool runSchedule = true;
            bool runWeeks = true;
            int week = 1;
            while (runSchedule)
            {
                Console.Clear();
                Console.WriteLine($"Press A to view the next week" +
                      $"\nPress S to view the previous week " +
                      $"\nPress D to book a room" +
                      $"\n\nWeek number: {week}\n");

                List<string> days = new List<string>();
                string[] dayNames = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
                days.AddRange(dayNames);

                PrintSchedule(week, days);

                if (!Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.A:
                            week++;
                            break;
                        case ConsoleKey.S:
                            if (week > 1)
                            {
                                week--;
                            }
                            else if (week <= 1)
                            {
                                Console.WriteLine("You cannot view weeks earlier than this!");
                            }
                            break;
                        case ConsoleKey.D:
                            runWeeks = false;
                            BookingInfoInput(week);
                            break;
                        default:
                            Console.WriteLine("Unknown error");
                            break;
                    }
                }
            }
        }
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




