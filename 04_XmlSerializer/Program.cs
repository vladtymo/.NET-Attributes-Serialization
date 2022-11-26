using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using static System.Console;

namespace _04_XmlSerializer
{
    // Xml Serializer don`t need attribute [Serializable]
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

            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Person>));

            try
            {
                using (Stream fStream = File.Create("test.xml"))
                {
                    xmlFormat.Serialize(fStream, persons);
                }
                WriteLine("XmlSerialize OK!\n");

                List<Person> list = null;
                using (Stream fStream = File.OpenRead("test.xml"))
                {
                    list = (List<Person>)xmlFormat.Deserialize(fStream);
                }
                foreach (Person item in list)
                {
                    WriteLine(item);
                }
            }
            catch (Exception ex)
            {
                WriteLine(ex);
            }
        }
    }
}
