using FlowToDo.Src.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowToDo
{
    public class Notifications
    {
        private static readonly List<FormNotification> active = new List<FormNotification>();
        private const int Margin = 10;

        public static void Show(string text)
        {
            var nf = new FormNotification(text);
            nf.FormClosed += (s, e) =>
            {
                active.Remove(nf);
                RepositionAll();
            };
            active.Add(nf);
            RepositionAll();
            nf.Show();
            nf.StartFadeIn();
        }

        private static void RepositionAll()
        {
            if (Screen.PrimaryScreen == null) { 
                return;
            }

            var working = Screen.PrimaryScreen.WorkingArea;
            int x = working.Right;
            int y = working.Bottom - Margin;

            for (int i = active.Count - 1; i >= 0; i--)
            {
                var nf = active[i];
                int w = nf.Width;
                int h = nf.Height;
                x = working.Right - w - Margin;
                y -= h;
                nf.SetLocation(new Point(x, y));
                y -= Margin;
            }
        }
    }
}
