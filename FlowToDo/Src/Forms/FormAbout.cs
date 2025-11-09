using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowToDo.Src.Forms
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
            Text = "About This Application";

            Label lblTitle = new Label
            {
                Text = Program.appName+" v" + UpdateManager.getAppVersion(),
                Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold),
                AutoSize = true,
                Location = new System.Drawing.Point(20, 20)
            };

            Label lblDescription = new Label
            {
                Text = "A small todo application.\nDeveloped by pekand.",
                AutoSize = true,
                Location = new System.Drawing.Point(20, 60)
            };

            LinkLabel linkWebsite = new LinkLabel
            {
                Text = "Visit Website",
                AutoSize = true,
                Location = new System.Drawing.Point(20, 110)
            };
            linkWebsite.LinkClicked += (s, e) =>
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "https://pekand.com",
                    UseShellExecute = true
                });
            };

            Button btnClose = new Button
            {
                Text = "Close",
                DialogResult = DialogResult.OK,
                Location = new System.Drawing.Point(150, 150),
                Width = 80,
                Height = 40
            };

            Controls.Add(lblTitle);
            Controls.Add(lblDescription);
            Controls.Add(linkWebsite);
            Controls.Add(btnClose);

            AcceptButton = btnClose;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new System.Drawing.Size(320, 200);
        }

        private async void FormAbout_Load(object sender, EventArgs e)
        {
/*
  
 xml example

<update>
  <version>1.0.2</version>
  <installerUrl>https://project.com/FlowToDo/FlowToDo-v1.0.2.exe</installerUrl>
  <sha256>123456</sha256>
</update>
*/
            //await UpdateManager.CheckAndInstallUpdateAsync(Program.updateXmlUrl, this);
        }
    }
}
