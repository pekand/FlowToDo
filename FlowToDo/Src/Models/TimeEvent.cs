using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FlowToDo
{
    public class TimeEvent
    {
        public string timeString = "";
        public string name = "";
        [XmlIgnore]
        public DateTime? time = null;
    }
}
