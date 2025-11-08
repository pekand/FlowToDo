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

        public string todoColor = Tools.ColorToString(Color.FromArgb(128, 255, 128));
        public string doneColor = Tools.ColorToString(Color.FromArgb(222, 222, 222));
        public string deletedColor = Tools.ColorToString(Color.FromArgb(255, 224, 108));

        [XmlIgnore]
        public int searchIndex = 0;
        [XmlIgnore]
        public List<SearchItem> search = new List<SearchItem>();

        public TimeEvent? nextEvent = null;
        [XmlIgnore]
        public List<TimeEvent> timeEvents = new List<TimeEvent>();
    }
}
