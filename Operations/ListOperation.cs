using ROITodo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Reflection.Metadata;

namespace ROITodo.Operations
{
    class ListOperation : IOperation
    {
        /**
         * Argument format
         *  - list [sort] [id|title|deadline] [asc|desc]
         *  - list [filter] [deadline|done] [gt|lt|done|undone] [date]
         */
        public List<ToDoEntry> Run(List<ToDoEntry> todos, string[] args)
        {

            if (todos.Count > 0)
            {
                if (args.Length < 2 || args[1] == "sort")
                {
                    // If just 'list' or 'list sort'
                    string field, direction;
                    if (args.Length < 4)
                    {
                        // If there's fewer than 3 arguments, use "Id ASC" as the default
                        field = "id";
                        direction = "asc";
                    }
                    else
                    {
                        field = args[2].ToLower();
                        direction = args[3].ToLower();
                    }

                    ListSorted(todos, field, direction);
                }
                else
                {
                    if (args[1] != "filter")
                    {
                        Console.WriteLine("Invalid second argument. Use either 'list', 'list sort' or 'list filter'.");
                        return todos;
                    }

                    ListFiltered(todos, args);
                }
            } 
            else
            {
                Console.WriteLine("Things to do:");
                Console.WriteLine("===================");
                Console.WriteLine("No things to do...");
            }

            return todos;
        }

        /* Only handles sorting by a single field, could be worth changing the "field" and "direction"
         * parameters into a single object and passing in a list.         
         */
        private void ListSorted(List<ToDoEntry> todos, string field, string direction)
        {

            if (field != "id" && field != "title" && field != "deadline")
            {
                Console.WriteLine("Invalid sort field. The valid options are 'id', 'title' and 'deadline'.");
                return;
            }

            if (direction != "asc" && direction != "desc")
            {
                Console.WriteLine("Invalid sort direction. The valid options are 'asc' and 'desc'.");
                return;
            }

            List<ToDoEntry> sortedTodos = SortEntries(todos, field, direction);

            Console.WriteLine("Things to do:");
            Console.WriteLine("===================");

            foreach (ToDoEntry todo in sortedTodos)
            {
                Console.WriteLine(todo.Id + " - " + todo.Title);
                Console.WriteLine(todo.Deadline.ToShortDateString() + " (" + todo.Deadline.DayOfWeek + ")");
                Console.WriteLine(todo.Done ? "Done" : "! NOT DONE !");
                Console.WriteLine("====");
            }
        }

        // I'm sure there's a better way to do this
        private List<ToDoEntry> SortEntries(List<ToDoEntry> todos, string field, string direction)
        {
            bool asc = direction == "asc";

            if (field == "id")
            {
                if (asc)
                {
                    todos.Sort(Comparer<ToDoEntry>.Create((todo1, todo2) => todo1.Id.CompareTo(todo2.Id)));
                } else
                {
                    todos.Sort(Comparer<ToDoEntry>.Create((todo1, todo2) => todo2.Id.CompareTo(todo1.Id)));
                }
            }
            else if (field == "title")
            {
                if (asc)
                {
                    todos.Sort(Comparer<ToDoEntry>.Create((todo1, todo2) => todo1.Title.CompareTo(todo2.Title)));
                }
                else
                {
                    todos.Sort(Comparer<ToDoEntry>.Create((todo1, todo2) => todo2.Title.CompareTo(todo1.Title)));
                }
            }
            else if (field == "deadline")
            {
                if (asc)
                {
                    todos.Sort(Comparer<ToDoEntry>.Create((todo1, todo2) => todo1.Deadline.CompareTo(todo2.Deadline)));
                }
                else
                {
                    todos.Sort(Comparer<ToDoEntry>.Create((todo1, todo2) => todo2.Deadline.CompareTo(todo1.Deadline)));
                }
            }

            return todos;

        }


        /* The way this method handles the arguments is a little messy. 
         * 
         * It might be worth refactoring this to validate them first, perhaps creating a request object to store them?
         */
        private void ListFiltered(List<ToDoEntry> todos, string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Invalid arguments. The valid format is 'list filter [deadline|done] [gt|lt|done|undone] [date]'");
                return;
            }

            List<ToDoEntry> filteredTodos;

            string field = args[2];

            if (field == "deadline")
            {
                if (args.Length < 5 || (args[3] != "gt" && args[3] != "lt"))
                {
                    Console.WriteLine("Invalid arguments. The valid format is 'list filter deadline [gt|lt] [date]'");
                    return;
                }

                string sign = args[3];

                DateTime date;
                try
                {
                    date = DateTime.Parse(args[4]);
                }
                catch
                {
                    Console.WriteLine("Invalid date: " + args[4]);
                    return;
                }

                filteredTodos = FilterTodosDeadline(todos, sign, date);
            }
            else if(field == "done")
            {
                if (args.Length < 4 || (args[3] != "done" &&  args[3] != "undone"))
                {
                    Console.WriteLine("Invalid arguments. The valid format is 'list filter done [done|undone]'");
                    return;
                }

                bool done = args[3] == "done";

                filteredTodos = (from todo in todos
                                 where todo.Done == done
                                 select todo).ToList();
            }
            else
            {
                Console.WriteLine("Invalid arguments. The valid format is 'list filter [deadline|done] [gt|lt|done|undone] [date]'");
                return;
            }

            Console.WriteLine("Things to do:");
            Console.WriteLine("===================");

            foreach (ToDoEntry todo in filteredTodos)
            {
                Console.WriteLine(todo.Id + " - " + todo.Title);
                Console.WriteLine(todo.Deadline.ToShortDateString() + " (" + todo.Deadline.DayOfWeek + ")");
                Console.WriteLine(todo.Done ? "Done" : "! NOT DONE !");
                Console.WriteLine("====");
            }
        }

        private List<ToDoEntry> FilterTodosDeadline(List<ToDoEntry> todos, string sign, DateTime date)
        {
            if (sign == "gt")
            {
                return (from todo in todos
                    where todo.Deadline > date
                    select todo).ToList();
            }
            else //if (sign == "lt")
            {
                return (from todo in todos
                    where todo.Deadline < date
                    select todo).ToList();
            }
        }
    }
}
