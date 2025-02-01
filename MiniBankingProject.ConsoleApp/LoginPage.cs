using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MiniBankingProject.ConsoleApp
{
    internal class LoginPage
    {
        string? Fullname;
        string? Pin;
        string? Mobileno;

        int mobileno_check;
        int pin_check;

        int mobileno_db = 0;
        int pin_db = 0;

        string? order;
        int count = 0;

        public void login()
        {
            Console.WriteLine("Enter your Mobile Number");
            Mobileno = Console.ReadLine()!;

            if (mobileno_db != mobileno_check)
            {
                Console.WriteLine("Don`t have your Mobile Number");
                Console.WriteLine("Create new account for A or Try again L");
                Console.WriteLine("Enter your Mobile Number");
                Mobileno = Console.ReadLine()!;
            }

            Console.WriteLine("Enter your pin");
            Pin = Console.ReadLine()!;

            mobileno_check = Convert.ToInt16(Mobileno);
            pin_check = Convert.ToInt16(Pin);
            if (pin_db == pin_check)
            {
                // Login Success
            }
            else
            {
                Console.WriteLine("Invalid Mobile Number or Pin");
                Console.WriteLine("Please try again");

            }
        }

        public void createAccount()
        {
            // Ask to Create Account
            Console.WriteLine("Create your Fullname");
            Fullname = Console.ReadLine()!;

            Console.WriteLine("Enter your Mobile Number to create new account");
            Console.WriteLine("Please type only 13 numbers");
            Mobileno = Console.ReadLine()!;

            Console.WriteLine("Create your Pin");
            Console.WriteLine("Please type only 6 numbers");
            Pin = Console.ReadLine()!;

            Console.WriteLine("You have minimun");
            Console.WriteLine("Total cost is 10500.");
            Console.WriteLine("Account Created Successfully");

        }

        public void Start()
        {
            // Initial Message
            Console.WriteLine("Welcome To Mini Banking System");
            Console.WriteLine("How can I help you?");
            Console.WriteLine("Login for L or Create a new account For A");
            order = Console.ReadLine()!;

            Order(order);
        }

        public void Order(string order)
        {
            // Check the order
            switch (order)
            {
                case "L" or "l":
                    login();
                    MainPage();
                    break;

                case "A" or "a":
                    createAccount();
                    MainPage();
                    break;

                default:
                    Console.WriteLine("Invalid Order");
                    Console.WriteLine("Please Type L/A");
                    order = Console.ReadLine()!;

                    // Check the count and stop the program
                    count++;
                    if (count > 1)
                    {
                        Console.WriteLine("You have reached the maximum number of attempts");
                        Console.WriteLine("Please try again later");
                        End();
                    }
                    else
                    {
                        // Loop the order
                        Order(order);
                    }
                    break;
            }
        }

        public void End()
        {
            // End Message
            Console.WriteLine("Thank you for using Mini Banking System");
            Console.WriteLine("Goodbye");
            Console.ReadKey();
        }

        public void MainPage()
        {
            // Need to call the user data from the database

            // User Main Page
            Fullname = "a";
            Mobileno = "b";
            var Balance = 10000;

            Console.WriteLine("Welcome to Mini Banking System");
            Console.WriteLine("Name: " + Fullname);
            Console.WriteLine("Mobile No: " + Mobileno);
            Console.WriteLine("Balance: " + Balance);
        }





    }
}
