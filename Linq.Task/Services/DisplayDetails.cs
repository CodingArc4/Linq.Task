using Linq.Task.Data_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Task.Services
{
    internal class DisplayDetails
    {
        //method to display data
        public static void DisplayInternshipDetails(Internship internship)
        {
            Console.WriteLine("Internship Details:");
            Console.WriteLine($"Id: {internship.Id}");
            Console.WriteLine($"Name: {internship.Name}");
            Console.WriteLine($"Company Name: {internship.Company.Name}");
            Console.WriteLine($"Company Location: {internship.Company.Location}");
            Console.WriteLine($"Company Industry: {internship.Company.Industry}");
            Console.WriteLine($"Salary: {internship.Details.Salary}");
            Console.WriteLine($"Start Date: {internship.Details.StartDate:yyyy-MM-dd}");
            Console.WriteLine($"Required Skills: {string.Join(", ", internship.Details.Skills)}");
            Console.WriteLine($"Duration (weeks): {internship.Details.Duration}");
            Console.WriteLine($"Is Remote: {internship.Details.IsRemote}");
            Console.WriteLine($"Average Rating: {internship.Reviews.Average(review => review.Rating):F1} ");
            Console.WriteLine();
        }
    }
}
