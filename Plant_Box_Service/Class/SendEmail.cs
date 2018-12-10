using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using Plant_Box_Service.Models;

namespace Plant_Box_Service.Class
{
    public static class SendEmail
    {
        public static void Main(Customer customer = null)
        {
            string message = CreateMessage(customer);
            string subject = CreateSubject(customer);
            NewRegistration(message, subject);
        }
        private static string CreateMessage(Customer customer)
        {
            string accountStatus = customer.AccountStatus == true ? "Active" : "Inactive";
            string message = "<strong>Customer Information<strong/>\nName: " + customer.FirstName + " " + customer.LastName + "\nAddress " + customer.Address + "\n" + customer.City + ", " + customer.State.Name + " " + customer.ZipCode.ToString() + "\nAccount Status: " + accountStatus;
            if (customer.AccountStatus == true)
            {
                message = message + "\n\n<strong>Time to Ship!</strong>";
            }
            return message;
        }

        private static string CreateSubject(Customer customer)
        {
            string subject;
            if (customer.AccountStatus == true)
            {
                subject = customer.FirstName + " " + customer.LastName + " has just paid! - Plant Service";
            }
            else
            {
                subject = "You have a new user! - Plant Service";
            }

            return subject;
        }

        static async Task NewRegistration(string message, string subjectDynamic)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("daytradersdream@gmail.com", "David");
            var subject = subjectDynamic;
            var to = new EmailAddress("davidnlagrange@hotmail.com", "David L");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}