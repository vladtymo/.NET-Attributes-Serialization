using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace _04_Attributes_DataAnotation
{
    [Serializable]
    public class User
    {
        [Required(ErrorMessage = "Id not defined")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Name not setted")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Illegal length")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age not setted")]
        [Range(1, 100, ErrorMessage = "Illegal age")]
        public int Age { get; set; }

        //[RegularExpression(@"^\+\d{4}-\d{3}-\d{4}$", ErrorMessage = "Phone format +xxxx-xxx-xxxx")]     
        [Phone]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Not Confirm Password")]
        public string ConfirmPassword { get; set; }

        [NonSerialized]
        public string version;
    }

    class Program
    {
        static void Main()
        {
            User user = new User();
            user.version = "1.0.0";
            user.Id = "1";

            bool isValid = true;
            do
            {
                Console.WriteLine("Enter name:");
                string name = Console.ReadLine();

                Console.WriteLine("Enter age");
                int age = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter Login");
                string login = Console.ReadLine();

                Console.WriteLine("Enter password");
                string password = Console.ReadLine();

                Console.WriteLine("Confirm password");
                string confirmPassword = Console.ReadLine();

                Console.WriteLine("Enter email");
                string email = Console.ReadLine();

                Console.WriteLine("Enter phone");
                string phone = Console.ReadLine();

                user.Name = name;
                user.Age = age;
                user.Password = password;
                user.ConfirmPassword = confirmPassword;
                user.Email = email;
                user.Login = login;
                user.Phone = phone;

                var results = new List<ValidationResult>();
                var context = new ValidationContext(user);
                if (!(isValid = Validator.TryValidateObject(user, context, results, true)))
                {
                    foreach (ValidationResult error in results)
                    {
                        Console.WriteLine(error.MemberNames.FirstOrDefault() + ": " + error.ErrorMessage);
                    }
                }

            } while (!isValid);

            Console.WriteLine("OK, model is VALID!");

            Console.ReadLine();
        }
    }
}