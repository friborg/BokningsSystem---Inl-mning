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
        private static void AddCustomer()
        {
            using (var db = new Context())
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
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("The customer has been added, go back to booking to choose from existing customers!");
                Console.ResetColor();
                Console.WriteLine("Press any key to go to the main menu.");
                Console.ReadKey();
                MenuChoices();
            }
        }
        private static void RunSchedule()
        {
            Console.WriteLine("[1]Add new customer or [2] choose existing?");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                AddCustomer();
            }
            else if (choice == 2)
            {
                using (var db = new Context())
                {
                    foreach (var room in db.ConferenceRooms)
                    {
                        Console.WriteLine($"Id: {room.Id}  Name: {room.Name}\tRoom number: {room.RoomNumber}  Capacity: {room.Capacity}" +
                            $"\nWhiteboard? {room.WhiteBoard}\tProjector? {room.Projector}\n");
                    }

                    Console.WriteLine("Input room-id of the room you wish to book:");
                    int roomId = int.Parse(Console.ReadLine());

                    bool runSchedule = true;
                    int week = 1;

                    while (runSchedule)
                    {
                        Console.Clear();
                        Console.WriteLine($"Press A to view the next week" +
                              $"\nPress S to view the previous week " +
                              $"\nPress D to book a room" +
                              $"\nPress Q to return to main menu" +
                              $"\n\nWeek number: {week}\n");

                        List<string> days = new List<string>();
                        string[] dayNames = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
                        days.AddRange(dayNames);

                        int dayCounter = 1;
                        string status = "Available";

                        foreach (var day in days) 
                        {
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.WriteLine($"\n{day}");
                            Console.ResetColor();
                            foreach (var booked in db.Bookedrooms.Where(b => b.ConferenceRoomId == roomId))
                            {
                                if (booked == null)
                                {
                                    status = "Available";
                                }
                                else if (booked != null)
                                {
                                    if (booked.Day == dayCounter && booked.Week == week)
                                    {
                                        status = "Booked";
                                        break;
                                    }
                                    else
                                    {
                                        status = "Available";
                                    }

                                }
                            }
                            Console.WriteLine(status);
                            dayCounter++;
                        }

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
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("You cannot view weeks earlier than this!");
                                        Console.ResetColor();
                                        Thread.Sleep(1500);
                                    }
                                    break;
                                case ConsoleKey.D:
                                    runSchedule = false;
                                    BookRoom(week, roomId);
                                    break;
                                    case ConsoleKey.Q:
                                    runSchedule = false;
                                    MenuChoices();
                                    break;
                                default:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Unknown error");
                                    Console.ResetColor();
                                    break;
                            }
                        }
                    }
                }
            }
        }
        private static void BookRoom(int week, int roomId)
        {
            using (var db = new Context())
            {
                Console.WriteLine("Which day do you want to book? (1-5)");
                int dayInput = int.Parse(Console.ReadLine());
                foreach (var r in db.Bookedrooms)
                {
                    if (r.ConferenceRoomId == roomId && r.Day == dayInput && r.Week == week)
                    {
                        Console.WriteLine("The room is occupied that day, choose another room, day or week!" +
                            "\nPress any key to continue.");
                        Console.ReadKey();
                        return;
                    }
                    else
                    {
                        continue;
                    }
                }

                foreach (var customer in db.Customers)
                {
                    Console.WriteLine($"Id: [{customer.Id}]\tName: {customer.Name}\n" +
                        $"Social security number: {customer.SocialSecurityNumber}\tPhone number: {customer.PhoneNumber}\n");
                }
                Console.WriteLine("Choose a customer-id:");
                int cId = int.Parse(Console.ReadLine());

                var customers = from c in db.Customers
                                where c.Id == cId
                                select c;
                foreach (var c in customers)
                {
                    var room = new BookedRoom
                    {
                        ConferenceRoomId = roomId,
                        CustomerId = cId,
                        Week = week,
                        Day = dayInput
                    };
                    var bookedList = db.Bookedrooms;
                    bookedList.Add(room);
                }
                db.SaveChanges();
                Console.Clear();
                ConfirmBooking(cId);
            }
        }
        private static void ConfirmBooking(int customerId)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The booking has been placed!");
            Console.ResetColor();
            Console.WriteLine("Below you can see all of the customers active bookings: ");
            using (var db = new Context())
            {
                var customersBookedRooms = from b in db.Bookedrooms
                                           join r in db.ConferenceRooms on b.ConferenceRoomId equals r.Id
                                           where b.CustomerId == customerId
                                           select new
                                           {
                                               RoomName = r.Name,
                                               RoomNumber = r.RoomNumber,
                                               CustomerName = b.Customer.Name,
                                               SocialSecurityNumber = b.Customer.SocialSecurityNumber,
                                               BookedDay = b.Day,
                                               BookedWeek = b.Week
                                           };
                foreach (var room in customersBookedRooms) // visar 4 gånger?? fel i metoden där bokningen sparas
                {
                    Console.WriteLine($"{room.CustomerName} with social security number: [{room.SocialSecurityNumber}] has booked room {room.RoomName} \n" +
                        $"with room number [{room.RoomNumber}] on day {room.BookedDay} of week {room.BookedWeek}\n");
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Press any button to go back to the main menu!");
            Console.ResetColor();
            Console.ReadLine();
            MenuChoices();
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
                    RunSchedule();
                    break;
                case 2:
                    AddRoom();
                    break;
                case 3:
                    Statistics();
                    break;
                default:
                    Console.WriteLine("Wrong input, please try again!");
                    break;
            }
        }
        private static void Statistics()
        {
            Console.WriteLine("Which statistic do you want to see?" +
                    "\n[1] 3 most popular conference rooms" +
                    "\n[2] Most popular day of the week" +
                    "\n[3] Amount of bookings per week" +
                    "\n[4] Go back to Admin Menu");

            ConsoleKeyInfo key = Console.ReadKey(true);
            using (var db = new Context())
            {

                switch (key.KeyChar)
                {
                    case '1':
                        var topRooms = (from b in db.Bookedrooms
                                        join r in db.ConferenceRooms on b.ConferenceRoomId equals r.Id
                                        select new { RoomName = r.Name, }).ToList().GroupBy(p => p.RoomName);
                        int count = 1;
                        Console.WriteLine();
                        foreach (var room in topRooms.OrderByDescending(p => p.Count()).Take(3))
                        {
                            Console.WriteLine($"Top {count} : {room.Key} with {room.Count()} bookings\n");
                            count++;
                        }
                        break;
                    case '2':
                        var topWeekDay = (from b in db.Bookedrooms
                                          select new { Day = b.Day }).ToList().GroupBy(b => b.Day);

                        Console.WriteLine();
                        foreach (var day in topWeekDay.OrderByDescending(b => b.Count()).Take(1))
                        {
                            Console.WriteLine($"The most popular day is day {day.Key}\n");
                        }
                        break;
                    case '3':
                        var weeklyBookings = (from b in db.Bookedrooms
                                              select new { Week = b.Week }).ToList().GroupBy(b => b.Week);
                        int count2 = 1;
                        Console.WriteLine();
                        foreach (var day in weeklyBookings.OrderByDescending(b => b.Count()).Take(4))
                        {
                            Console.WriteLine($"Week {count2} has {day.Count()} bookings\n");
                            count2++;
                        }
                        break;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }
    }
}




