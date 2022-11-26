using System;
using System.Reflection;
using static System.Console;

namespace SimpleProject
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Constructor)]
    public class CoderAttribute : Attribute
    {
        public string Name { get; set; } = "Vlad";
        public DateTime Date { get; set; } = DateTime.Now;
        public CoderAttribute() { }
        public CoderAttribute(string name, string date)
        {
            try
            {
                Name = name;
                Date = Convert.ToDateTime(date);
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }
        public override string ToString()
        {
            return $"Coder: {Name}, Date: {Date}";
        }
    }

    [Coder]
    [Obsolete, Serializable]
    class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        
        [CoderAttribute]
        public Employee()
        {

        }

        [Coder("John", "2017-3-29")]
        public void IncreaseWages(double wage)
        {
            Salary += wage;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("\tAttributes of class Employee:");
            foreach (var attr in typeof(Employee).GetCustomAttributes())
            {
                WriteLine(attr.ToString());
            }

            Console.ReadKey();

            WriteLine("\n\tAttributes of members of class Employee :");
            foreach (MemberInfo info in typeof(Employee).GetMembers())
            {
                Console.WriteLine("\t" + info.ToString());
                foreach (var attr in info.GetCustomAttributes<CoderAttribute>(true))
                {
                    WriteLine(attr);
                }
            }
        }
    }
}