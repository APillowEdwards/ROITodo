using ROITodo.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ROITodo.Data
{
    /**
     * To add a new operation from the command line, add a new enum value, create an Operation class implementing
     * IOperation, and add to the switch-case statement in the GetInstance method. 
     */

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