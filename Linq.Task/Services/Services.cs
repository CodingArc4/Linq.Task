using Linq.Task.Data_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Task.Services
{
    internal class Services
    {
        public static List<Internship> InternDetails = new List<Internship>();
        public static int id = 1;

        //method to add internship details
        public static List<Internship> SeedData()
        {
           
            string internshipName, companyName, companyLocation, comapnyIndustry, skillsInput;
            int salary, duration;
            double rating;
            bool isRemote;
            List<string> requiredSkills;
            DateTime startDate;

            
            Console.WriteLine("Enter Intership details");

            //internship name
            Console.Write("Enter internship name: ");
            internshipName = Console.ReadLine();
            
            //company name
            Console.Write("Enter company name: ");
            companyName = Console.ReadLine();
                
            //company location
            Console.Write("Enter company location: ");
            companyLocation = Console.ReadLine();

            //company industry
            Console.Write("Enter company industry: ");
            comapnyIndustry = Console.ReadLine();

            //internship salary
            do {
                Console.Write("Enter internship salary: ");
            } while (!int.TryParse(Console.ReadLine(), out salary));

            //internship start date
            do {
                Console.Write("Enter internship start date (yyyy-mm-dd): ");
            }
            while (!DateTime.TryParse(Console.ReadLine(), out startDate));
           
            //skills
            Console.Write("Enter required skills (comma-separated): ");
            skillsInput = Console.ReadLine();
            requiredSkills = skillsInput.Split(',').Select(s => s.Trim()).ToList();

            //duration of internship in weeks
            do
            {
                Console.Write("Enter internship duration in weeks: ");
            }
            while (!int.TryParse(Console.ReadLine(), out duration));
            

            //is internship remote?
            Console.Write("Is the internship remote? (yes/no): ");
            isRemote = Console.ReadLine().ToLower() == "yes";

            //internship rating
            do
            {
                Console.Write("Enter internship average rating: ");
            }
            while (!double.TryParse(Console.ReadLine(), out rating));

            // Create an Internship object based on user input  
            var internship = new Internship
            {
                Id = id,
                Name = internshipName,
                Company = new Company
                {
                        Name = companyName,
                        Location = companyLocation,
                        Industry = comapnyIndustry
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
            id++;
            return InternDetails;
        }

        //method to filter inputs
        public static List<IGrouping<string,Internship>> FilterInternship(Internship internship)
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

            //grouping the sorted data
            IEnumerable<IGrouping<string,Internship>>  groupedList = sortedlist.GroupBy(x => x.Company.Industry);  

            return groupedList.ToList();
        }

        //method to delete internee 
        public static void Delete(int id)
        {
            var delete = InternDetails.FirstOrDefault(x => x.Id  == id);
            InternDetails.Remove(delete);
        }
    }
}
