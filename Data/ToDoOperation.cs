using ROITodo.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ROITodo.Data
{
    public enum ToDoOperation
    {
        add,
        list,
        mark
    }

    public static class ToDoOperationHelper {
        // This method will throw an exception if the string is invalid
        public static ToDoOperation GetFromString(string operation)
        {
            return (ToDoOperation)Enum.Parse(typeof(ToDoOperation), operation);
        }

        public static IOperation GetInstance(ToDoOperation operation)
        {
            switch(operation)
            {
                case ToDoOperation.add:
                    return new AddOperation();
                case ToDoOperation.list:
                    return new ListOperation();
                case ToDoOperation.mark:
                    return new MarkOperation();
                default:
                    throw new NotImplementedException();
            }
        }
    }

}