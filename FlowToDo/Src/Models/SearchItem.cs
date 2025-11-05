using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowToDo
{
    public class SearchItem
    {
        public int todoListPos = -1;
        public int posInTodo = -1;

        public SearchItem(int todoListPos, int posInTodo)
        {
            this.todoListPos = todoListPos;
            this.posInTodo = posInTodo;
        }
    }
}
