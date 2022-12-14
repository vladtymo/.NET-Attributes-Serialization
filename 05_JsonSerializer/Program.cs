using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace _05_JsonSerializer
{
    // Json Serializer don`t need attribute [Serializable]
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        int _identNumber;

        [NonSerialized]
        const string Planet = "Earth";

        public Person() { }
        public Person(int number)
        {
            _identNumber = number;
        }
        public override string ToString()
        {
            return $"Name: {Name}, " +
                $"Age: {Age}, " +
                $"Identification number: {_identNumber}, " +
                $"Planet: {Planet}.";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> persons = new List<Person>()
            {
                new Person(346875) { Name = "Jack", Age = 34 },
                new Person(975648) { Name = "Bob", Age = 37 },
                new Person(870312) { Name = "John", Age = 23 }
            };

            try
            {
                string fileName = "Persons.json";

                // Serialize
                string jsonString = JsonSerializer.Serialize(persons);
                File.WriteAllText(fileName, jsonString);

                // Deserialize
                jsonString = File.ReadAllText(fileName);
                List<Person> list = JsonSerializer.Deserialize<List<Person>>(jsonString);

                foreach (Person item in list)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
