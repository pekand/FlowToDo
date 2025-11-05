using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FlowToDo
{
    public class Data
    {        
        public int currentTodoPos = 0;

        [XmlIgnore]
        public ToDo? currentTodo = null;
        public List<ToDo> todoList = new List<ToDo>();

        public int winX = 100;
        public int winY = 100;
        public int winW = 300;
        public int winH = 300;
        public int winStatus = 0; // 0 normal, 1 minimalized, 2 maximalized
        public bool TopMost = true;

        public string defaultFont = "";

        [XmlIgnore]
        public int searchIndex = 0;
        [XmlIgnore]
        public List<SearchItem> search = new List<SearchItem>();
    }
}
