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
            public string Number { get; set; }
            public Passport()
            {
                Number = Guid.NewGuid().ToString();
            }
            public override string ToString()
            {
                return Number;
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

            var person = new Person(870312) { Name = "Oleg", Age = 44 };

            BinaryFormatter binFormat = new BinaryFormatter();

            try
            {
                // serialization
                using (Stream fStream = File.Create("test.bin"))
                {
                    binFormat.Serialize(fStream, persons);
                    binFormat.Serialize(fStream, person);
                }
                WriteLine("BinarySerialize OK!\n");


                // deserialization
                List<Person> list = null;
                Person one = null;
                using (Stream fStream = File.OpenRead("test.bin"))
                {
                    list = (List<Person>)binFormat.Deserialize(fStream);
                    one = (Person)binFormat.Deserialize(fStream);
                }
                foreach (Person item in list)
                {
                    WriteLine(item);
                }
                Console.WriteLine(one);
            }
            catch (Exception ex)
            {
                WriteLine(ex);
            }
        }

    }
}
