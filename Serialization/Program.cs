using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace Serialization
{
    internal class Program
    {
        static void Main()
        {

            #pragma warning disable SYSLIB0011

            string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\", @"..\", @"..\");
            string textPath = Path.Combine(basePath, @"event.txt");
            textPath = Path.GetFullPath(textPath);

            string jsonPath = Path.Combine(basePath, @"event.json");
            jsonPath = Path.GetFullPath(jsonPath);

            string wordPath = Path.Combine(basePath, @"word.txt");
            wordPath = Path.GetFullPath(wordPath);

            // Instruction #2
            Event myEvent = new Event(1, "Calgary");

            // Instruction #3
            SerializeEvent(myEvent, textPath);

            // Instruction #4
            DeserializeEvent(textPath);

            // Instruction #5
            List<Event> events = new List<Event>();
            events.Add(new Event(2, "Bogotá"));
            events.Add(new Event(3, "Cartagena"));
            events.Add(new Event(4, "Vancouver"));
            events.Add(new Event(5, "Mexico City"));

            Console.WriteLine("Start of JSON Serialization.");
            foreach (Event e in events)
            {
                SerializeJson(e, jsonPath);
            }

            DeserializeJson(jsonPath);

            // Instruction #6
            ReadFromFile(wordPath);

            void SerializeEvent(Event e, string path)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    formatter.Serialize(stream, e);
                }

                Console.WriteLine("Serialization Completed.");
            }

            void DeserializeEvent (string path)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    Event myEvent = (Event) formatter.Deserialize(stream);                 
                }
                Console.WriteLine($"Event Number: {myEvent.EventNumber} \nLocation: {myEvent.Location}");
                Console.WriteLine("Deserialization Completed.");
            }

            void SerializeJson(Event e, string path)
            {
                string serializedEvent = JsonSerializer.Serialize(e) + "\n";
                File.AppendAllText(path, serializedEvent);
                Console.WriteLine("JSON Serialization Completed.");
            }

            void DeserializeJson(string path)
            {
                string[] lines = File.ReadAllLines(path);

                foreach (string line in lines)
                {
                    Event e = JsonSerializer.Deserialize<Event>(line);
                    Console.WriteLine($"Event Number: {e.EventNumber} \nLocation: {e.Location}");
                }

                Console.WriteLine("JSON Deserialization Completed.");
            }

            void ReadFromFile(string path)
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.WriteLine("Hackathon");
                }

                using (FileStream stream = new FileStream (path, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        string word = reader.ReadLine();

                        stream.Seek(0, SeekOrigin.Begin);
                        char firstChar = (char)reader.Read();

                        stream.Seek(-1, SeekOrigin.End);
                        char lastChar = (char)reader.Read();

                        long middlePosition = stream.Length / 2;
                        stream.Seek(middlePosition, SeekOrigin.Begin);
                        char middleChar = (char)reader.Read();

                        Console.WriteLine($"In Word: {word}");
                        Console.WriteLine($"The First Character is: {firstChar}");
                        Console.WriteLine($"The Middle Character is: {middleChar}");
                        Console.WriteLine($"The Last Character is: {lastChar}");
                    }
                }
            }
        }
    }
}