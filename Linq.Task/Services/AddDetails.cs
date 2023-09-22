using Linq.Task.Data_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Task.Services
{
    internal class AddDetails
    {
        public static List<Internship> InternDetails = new List<Internship>(); 

        //method to add internship details
        public static List<Internship> SeedData()
        {
            Console.WriteLine("Enter Intership details");
            //while (true)
            //{

                Console.Write("Enter internship name: ");
                string internshipName = Console.ReadLine();
                ////condition to exit the program
                //if (internshipName.ToLower() == "q")
                //    break;

                Console.Write("Enter company name: ");
                string companyName = Console.ReadLine();

                Console.Write("Enter company location: ");
                string companyLocation = Console.ReadLine();

                Console.Write("Enter company industry: ");
                string companyIndustry = Console.ReadLine();

                Console.Write("Enter internship salary: ");
                if (!int.TryParse(Console.ReadLine(), out int salary))
                {
                    Console.WriteLine("Invalid input. Please enter a valid salary.");
                    
                }

                Console.Write("Enter internship start date (yyyy-mm-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                {
                    Console.WriteLine("Invalid input. Please enter a valid start date.");
                    
                }

                Console.Write("Enter required skills (comma-separated): ");
                string skillsInput = Console.ReadLine();
                List<string> requiredSkills = skillsInput.Split(',').Select(s => s.Trim()).ToList();

                Console.Write("Enter internship duration in weeks: ");
                if (!int.TryParse(Console.ReadLine(), out int duration))
                {
                    Console.WriteLine("Invalid input. Please enter a valid duration.");
                    
                }

                Console.Write("Is the internship remote? (yes/no): ");
                bool isRemote = Console.ReadLine().ToLower() == "yes";

                Console.Write("Enter internship average rating: ");
                if (!double.TryParse(Console.ReadLine(), out double rating))
                {
                    Console.WriteLine("Invalid input. Please enter a valid rating.");
                    
                }

                // Create an Internship object based on user input
                var internship = new Internship
                {
                    Name = internshipName,
                    Company = new Company 
                    {     
                        Name = companyName, 
                        Location = companyLocation, 
                        Industry = companyIndustry 
                    },
                    Details = new InternshipDetails
                    {
                        Salary = salary,
                        StartDate = startDate,
                        Skills = requiredSkills,
                        Duration = duration,
                        IsRemote = isRemote
                    },
                    Reviews = new List<Review>
                    {
                         new Review { Rating = rating }
                    }
                };

                InternDetails.Add(internship);
                //Console.WriteLine("Internship added.");

                //Console.WriteLine();

                // Display the details of the added internship
               ///  DisplayDetails.DisplayInternshipDetails(internship);

            //}
            return InternDetails;
        }

        //method to filter inputs
        public static List<Internship> FilterInternship(Internship internship)
        {
            //filternig data
            var filter = InternDetails.Where(intern =>
                    (string.IsNullOrWhiteSpace(internship.Company.Location) || intern.Company.Location == internship.Company.Location) &&
                    (string.IsNullOrWhiteSpace(internship.Company.Industry) || intern.Company.Industry == internship.Company.Industry) &&
                    (internship.Details.Salary == 0 || intern.Details.Salary == internship.Details.Salary) &&
                    (internship.Details.StartDate == DateTime.MinValue || intern.Details.StartDate == internship.Details.StartDate) &&
                    //(internship.Details.Skills.Count == 0 || internship.Details.Skills.All(skill => internship.Details.Skills.Contains(skill))) &&
                    (internship.Details.Duration == 0 || intern.Details.Duration == internship.Details.Duration) &&
                    (!internship.Details.IsRemote || intern.Details.IsRemote == internship.Details.IsRemote) &&
                    (internship.Reviews.Count == 0 || intern.Reviews.Average(review => review.Rating) == internship.Reviews.Average(review => review.Rating))
            
                 ).ToList();

            //sorting the filtered data
            var sortedlist = filter
                            .OrderByDescending(x => x.Reviews.Average(x => x.Rating))
                            .ThenByDescending(x => x.Details.Salary)
                            .ThenBy(x => x.Details.Duration)
                            .ThenBy(x => x.Company.Name).ToList();

            return sortedlist;
        }
    }
}
