using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace _02_Serialization
{
    class Program
    {
        [Serializable]
        public class Person
        {
            private int _identNumber;

            public string Name { get; set; }
            public int Age { get; set; }

            [NonSerialized]
            const string PLANET = "Earth";

            public Person(int number)
            {
                _identNumber = number;
            }
            public override string ToString()
            {
                return $"Name: {Name}, Age: {Age}, Identification number: {_identNumber}, Planet: {PLANET}.";
            }
        }

        static void Main(string[] args)
        {
            Person person = new Person(346875)
            {
                Name = "Jack",
                Age = 34
            };

            BinaryFormatter binFormat = new BinaryFormatter();

            // binFormat.Serialize();   - convert object to bytes and put to a stream
            // binFormat.Deserialize(); - read data from a stream and return object

            try
            {
                // serialization
                using (Stream fStream = File.Create("test.bin"))
                {
                    binFormat.Serialize(fStream, person);
                }
                WriteLine("BinarySerialize OK!\n");

                // deserialization
                Person p = null;
                using (Stream fStream = File.OpenRead("test.bin"))
                {
                    p = (Person)binFormat.Deserialize(fStream);
                }
                WriteLine(p);
            }
            catch (Exception ex)
            {
                WriteLine(ex);
            }
        }
    }
}
