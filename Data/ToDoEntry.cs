using System;
using System.Collections.Generic;
using System.Text;

namespace ROITodo.Data
{
    public class ToDoEntry
    {
        private long _id;
        private string _title;
        private DateTime _deadline;
        private bool _done;

        // Have used properties here in case access to fields needs to be restricted.
        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        public DateTime Deadline
        {
            get { return _deadline; }
            set { _deadline = value; }
        }
        public bool Done
        {
            get { return _done; }
            set { _done = value; }
        }

        public ToDoEntry(long id, string title, DateTime deadline, bool done)
        {
            this.Id = id;
            this.Title = title;
            this.Deadline = deadline;
            this.Done = done;
        }
    }
}
