using ROITodo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;

namespace ROITodo.Operations
{
    class AddOperation : IOperation
    {
        /**
         * Argument format
         *  - add title 01/01/2021 [--done]
         */
        public List<ToDoEntry> Run(List<ToDoEntry> todos, string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Invalid arguments. Format should be 'add title 01/01/2021 [--done]'.");
                return todos;
            }

            // Find the largest Id of a ToDoEntry, and add 1 for the new Id
            long id = (from todo in todos
                  orderby todo.Id descending
                  select todo.Id)
                 .First() + 1;

            string title = args[1];

            DateTime deadline;
            try
            {
                deadline = DateTime.Parse(args[2]);
            }
            catch
            {
                Console.WriteLine("Invalid date: " + args[2]);
                return todos;
            }

            bool done = (args.Length >= 4) && (args[3] == "--done");

            todos.Add(new ToDoEntry(id, title, deadline, done));

            Console.WriteLine("Successfully added '" + title + "'.");

            return todos;
        }
    }
}
