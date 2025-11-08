using static System.Net.Mime.MediaTypeNames;

namespace FlowToDo.Src.Forms
{
    partial class FormNotification
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNotification));
            buttonOk = new Button();
            labelNotification = new Label();
            SuspendLayout();
            // 
            // buttonOk
            // 
            buttonOk.Font = new System.Drawing.Font("Broadway", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonOk.Location = new Point(458, 274);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(235, 58);
            buttonOk.TabIndex = 0;
            buttonOk.Text = "Ok";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += buttonOk_Click;
            // 
            // labelNotification
            // 
            labelNotification.Dock = DockStyle.Top;
            labelNotification.Font = new System.Drawing.Font("Segoe UI", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelNotification.Location = new Point(0, 0);
            labelNotification.Name = "labelNotification";
            labelNotification.Padding = new Padding(12, 8, 12, 8);
            labelNotification.Size = new Size(716, 255);
            labelNotification.TabIndex = 1;
            labelNotification.Text = "notification";
            labelNotification.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FormNotification
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(716, 344);
            Controls.Add(labelNotification);
            Controls.Add(buttonOk);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormNotification";
            ShowInTaskbar = false;
            Text = "Notification";
            TopMost = true;
            Load += FormNotification_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button buttonOk;
        private Label labelNotification;
    }
}