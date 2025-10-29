using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowToDo
{
    [DesignerCategory("Code")]
    [ToolboxBitmap(typeof(RichTextBox))]
    class CustomRichTextBox : RichTextBox
    {
        public CustomRichTextBox()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }
    }
}
