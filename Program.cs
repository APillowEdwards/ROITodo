using Newtonsoft.Json;
using ROITodo.Data;
using ROITodo.Operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace ROITodo
{
    class Program
    {
        const string STORAGE_FILE_PATH = "todo.txt";

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please specify an operation. The valid options are 'add', 'list', 'mark'.");
                return;
            }

            List<ToDoEntry> todos = LoadToDoEntries(STORAGE_FILE_PATH);

            ToDoOperation operation;
            try
            {
                operation = ToDoOperationHelper.GetFromString(args[0].ToLower());
            } catch
            {
                Console.WriteLine("Invalid operation. The valid options are 'add', 'list', 'mark'.");
                return;
            }

            IOperation operationInstance = ToDoOperationHelper.GetInstance(operation);

            todos = operationInstance.Run(todos, args);

            SaveToDoEntries(STORAGE_FILE_PATH, todos);
        }

        private static List<ToDoEntry> LoadToDoEntries(string filePath)
        {
            if (File.Exists(filePath))
            {
                using StreamReader reader = new StreamReader(filePath);

                // Read the stream as a string, and write the string to the console.
                string todoJson = reader.ReadToEnd();

                return JsonConvert.DeserializeObject<List<ToDoEntry>>(todoJson);
            }
            else
            {
                // Otherwise this is probably the first run, so start a blank list
                return new List<ToDoEntry>();
            }
        }

        private static void SaveToDoEntries(string filePath, List<ToDoEntry> todos)
        {
            // Write the string array to a new file named "WriteLines.txt".
            using StreamWriter writer = new StreamWriter(filePath);

            writer.WriteLine(JsonConvert.SerializeObject(todos));
        }
    }
}
