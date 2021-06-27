using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AssignmentFileIO
{
    [Serializable]
    public class Car 
    { 
        public string Model { get; set; }
        public void Ignition()
        {
            Console.WriteLine("Engine Start");
        }
    }
    class Program
    {
        #region FileIO
        public static void WriteFile()
        {
            using (StreamWriter sw = File.CreateText(@"File.txt"))
            {
                sw.WriteLine("Line 1");
                sw.WriteLine("Line 2");
                sw.Write("Line 3.1"); sw.WriteLine("Line 3.2");
            }
            Console.WriteLine("Written");
        }
        public static void ReadFile()
        {
            using (StreamReader sr = File.OpenText(@"File.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }

        #endregion
        #region Serialization
        static void SerializeDemo()
        {
            Car car = new Car();
            car.Model = "Carerra911";
            #region Binary Serialization
            try
            {
                Console.WriteLine("Binary Serialization");
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                Stream ofstream = new FileStream("BinarySerialised.txt", FileMode.Create, FileAccess.Write, FileShare.None);
                binaryFormatter.Serialize(ofstream, car);
                ofstream.Close();
                //deserialization  
                Stream ifstream = new FileStream("BinarySerialised.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
                Car car2 = (Car)binaryFormatter.Deserialize(ifstream);

                string model = car2.Model;
                Console.WriteLine($"Model:{model} ");
            }
            catch (Exception)
            {

                throw;
            } 
            #endregion
            #region Soap
            try
            {
                Console.WriteLine("SOAP Serialization");
                SoapFormatter formatter = new SoapFormatter();
                Stream ofstream = new FileStream("SoapSerialised.txt", FileMode.Create, FileAccess.Write, FileShare.None);

                formatter.Serialize(ofstream, car);
                ofstream.Close();
                //deserialization  
                Stream ifstream = new FileStream("SoapSerialised.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
                Car car2 = (Car)formatter.Deserialize(ifstream);

                string model = car2.Model;
                Console.WriteLine($"Model:{model} ");
            }
            catch (Exception)
            {

                throw;
            }
            #endregion
        }
        #endregion
        static void TPL()
        {
            Console.WriteLine("C# For Loop");
            int number = 10;
            for (int count = 0; count < number; count++)
            {
                //Thread.CurrentThread.ManagedThreadId returns an integer that 
                //represents a unique identifier for the current managed thread.
                Console.WriteLine($"value of count = {count}, thread = {Thread.CurrentThread.ManagedThreadId}");
                //Sleep the loop for 10 miliseconds
                Thread.Sleep(10);
            }
            Console.WriteLine();
            Console.WriteLine("Parallel For Loop");
            Parallel.For(0, number, count =>
            {
                Console.WriteLine($"value of count = {count}, thread = {Thread.CurrentThread.ManagedThreadId}");
                //Sleep the loop for 10 miliseconds
                Thread.Sleep(10);
            });
        }
        static void Main(string[] args)
        {
            //WriteFile();
            //ReadFile();
            //SerializeDemo();
            TPL();
            Console.ReadLine();
        }
    }
}
