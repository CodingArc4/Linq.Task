﻿using Linq.Task.Data_Models;
using Linq.Task.Services;

namespace Linq.Task
{
    internal class Program
    {
        static List<Internship> internships = new List<Internship>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Options:");
                Console.WriteLine("1. Add and Display Internships");
                Console.WriteLine("2. Filter and Display Internships");
                Console.WriteLine("3. Delete an Internship");
                Console.WriteLine("0. Exit");

                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddAndDisplayInternships();
                        break;
                    case 2:
                        FilterAndDisplayInternships();
                        break;
                    case 3:
                        DeleteInternship();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }


            //method to add and display internships
            static void AddAndDisplayInternships()
            {
                Console.WriteLine();
                Console.WriteLine("Adding Internships:");
                internships = Services.Services.SeedData();

                Console.WriteLine();
                Console.WriteLine("Displaying Internships:");
                foreach (var internship in internships)
                {
                    DisplayDetails.DisplayInternshipDetails(internship);
                }
            }

            //method to filter and display internships
            static void FilterAndDisplayInternships()
            {
                Console.WriteLine();
                if (internships.Count == 0)
                {
                    Console.WriteLine("No internships available. Please add internships first.");
                    Console.WriteLine();
                    return;
                }

                // Create a filter criteria object based on user input
                var filter = new Internship
                {
                    Company = new Company(),
                    Details = new InternshipDetails(),
                    Reviews = new List<Review>()
                };

                Console.Write("Enter company location (leave empty for no filter): ");
                filter.Company.Location = Console.ReadLine();

                Console.Write("Enter company industry (leave empty for no filter): ");
                filter.Company.Industry = Console.ReadLine();

                Console.Write("Enter minimum salary (leave empty for no filter): ");
                if (int.TryParse(Console.ReadLine(), out int Salary))
                {
                    filter.Details.Salary = Salary;
                }

                Console.Write("Enter minimum average rating (leave empty for no filter): ");
                if (double.TryParse(Console.ReadLine(), out double Rating))
                {
                    filter.Reviews.Add(new Review { Rating = Rating });
                }

                Console.Write("Enter the duration (leave empty for no filter): ");
                if (!int.TryParse(Console.ReadLine(), out int duration))
                {
                    filter.Details.Duration = duration;

                }

                // Filter internships
                var filteredInternships = Services.Services.FilterInternship(filter);

                if (filteredInternships.Any())
                {
                    // Display the filtered internships
                    Console.WriteLine("Filtered Internships:");
                    foreach (var internship in filteredInternships)
                    {
                        foreach(var item in internship)
                        {
                            Console.WriteLine();
                            DisplayDetails.DisplayInternshipDetails(item);
                        }
                       
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("No internships matched.");
                }
            }

            //delete internship method
            static void DeleteInternship()
            {
                Console.WriteLine();
                Console.WriteLine("Deleting an Internship:");

                if (Services.Services.InternDetails.Count == 0)
                {
                    Console.WriteLine("No internships available to delete.");
                    Console.WriteLine();
                    return;
                }

                Console.WriteLine();
                Console.Write("Enter the ID of the internship you want to delete: ");
                if (int.TryParse(Console.ReadLine(), out int internshipId))
                {
                    // Call the Delete method with the entered ID to remove the internship
                    Console.WriteLine();
                    Services.Services.Delete(internshipId);
                    Console.WriteLine("Internship deleted successfully.");
                    Console.WriteLine() ;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid internship ID.");
                }
            }
        }
    }
}