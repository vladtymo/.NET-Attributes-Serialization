using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Console;

namespace _03_Collection_Serialize
{
    public class Program
    {
        [Serializable]
        public class Passport
        {
            public int Number { get; set; }
            public Passport()
            {
                Number = 600045;
            }
            public override string ToString()
            {
                return Number.ToString();
            }
        }

        [Serializable]
        public class Person
        {
            public Passport Passport { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }

            private int _identNumber;

            [NonSerialized]
            const string Planet = "Earth";

            public Person() { }
            public Person(int number)
            {
                _identNumber = number;
                Passport = new Passport();
            }
            public override string ToString()
            {
                return $"Name: {Name}, Age: {Age}, Identification number: {_identNumber}, Planet: {Planet}, Passport: {Passport}.";
            }
        }

        static void Main(string[] args)
        {
            List<Person> persons = new List<Person>()
            {
                new Person(346875) { Name = "Jack", Age = 34 },
                new Person(975648) { Name = "Bob", Age = 37 },
                new Person(870312) { Name = "John", Age = 23 }
            };

            BinaryFormatter binFormat = new BinaryFormatter();

            try
            {
                // serialization
                using (Stream fStream = File.Create("test.bin"))
                {
                    binFormat.Serialize(fStream, persons);
                }
                WriteLine("BinarySerialize OK!\n");


                // deserialization
                List<Person> list = null;
                using (Stream fStream = File.OpenRead("test.bin"))
                {
                    list = (List<Person>)binFormat.Deserialize(fStream);
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
