using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowToDo
{
    public class ToDo
    {
        public bool isEmpty = true;
        public string text = "";
        public string rawText = "";

        public bool deleted = false;
        public bool done = false;

        public string createdAt = "";
        public string deletedAt = "";
        public string doneAt = "";
    }
}
