using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowToDo.Src.Forms
{
    public partial class FormNotification : Form
    {
        private System.Windows.Forms.Timer fadeTimer = new System.Windows.Forms.Timer { Interval = 100 };
        private int fadeStep = 20;
        public FormNotification(string text)
        {
            this.InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            ShowInTaskbar = false;
            TopMost = true;
            Opacity = 0;
            this.labelNotification.Text = text;


            BackColor = Color.FromArgb(250, 250, 250);
            ApplyRoundedCorners(12);

        }

        public void SetLocation(Point p)
        {
            Location = p;
        }

        public void StartFadeIn()
        {
            fadeTimer.Tick += (s, e) =>
            {
                Opacity += fadeStep / 255.0;
                if (Opacity >= 1 && fadeTimer != null)
                {
                    Opacity = 1;
                    fadeTimer.Stop();
                    fadeTimer.Dispose();
                    fadeTimer = null;
                }
            };
            fadeTimer.Start();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }

        private void ApplyRoundedCorners(int radius)
        {
            var path = RoundedRect(new Rectangle(0, 0, Width, Height), radius);
            Region = new Region(path);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyRoundedCorners(12);
        }

        private GraphicsPath RoundedRect(Rectangle r, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(r.Left, r.Top, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Top, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.Left, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void MakeDropShadow()
        {
            var attr = this.GetType().GetProperty("DropShadow", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        }

        private void FormNotification_Load(object sender, EventArgs e)
        {
            this.MakeDropShadow();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}