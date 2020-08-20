using ROITodo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ROITodo.Operations
{
    class MarkOperation : IOperation
    {
        /**
         * Argument format
         *  - mark id [--undone]
         */
        public List<ToDoEntry> Run(List<ToDoEntry> todos, string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Invalid arguments. Format should be 'mark id [--undone]'.");
                return todos;
            }

            long id = Convert.ToInt64(args[1]);

            IEnumerable<ToDoEntry> todosWithId =
                from todo in todos
                where todo.Id == id
                select todo;

            if (!todosWithId.Any())
            {
                Console.WriteLine("Entry with id '" + id + "' not found.");
                return todos;
            }

            bool done = (args.Length < 3) || (args[2] != "--undone");
            foreach (ToDoEntry todo in todosWithId)
            {
                todo.Done = done;
            }

            Console.WriteLine("Updated " + todosWithId.Count() + " record(s)");

            return todos;
        }
    }
}
