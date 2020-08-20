using ROITodo.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ROITodo.Operations
{
    public interface IOperation
    {
        public List<ToDoEntry> Run(List<ToDoEntry> todos, string[] args);
    }
}
