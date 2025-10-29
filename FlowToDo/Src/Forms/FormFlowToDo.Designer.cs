﻿namespace FlowToDo
{
    partial class FormFlowToDo
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFlowToDo));
            contextMenuStrip = new ContextMenuStrip(components);
            cutToolStripMenuItem = new ToolStripMenuItem();
            copyToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            colorToolStripMenuItem = new ToolStripMenuItem();
            fontToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            closeToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            topMostToolStripMenuItem = new ToolStripMenuItem();
            defaultFontToolStripMenuItem = new ToolStripMenuItem();
            showToolStripMenuItem = new ToolStripMenuItem();
            normalToolStripMenuItem = new ToolStripMenuItem();
            doneToolStripMenuItem = new ToolStripMenuItem();
            deletedToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanel1 = new TableLayoutPanel();
            richTextBoxNote = new CustomRichTextBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            buttonSkipRight = new Button();
            buttonRight = new Button();
            buttonAdd = new Button();
            buttonLeft = new Button();
            buttonSkipLeft = new Button();
            buttonDone = new Button();
            buttonDelete = new Button();
            flowLayoutPanel2 = new FlowLayoutPanel();
            textBoxPosition = new TextBox();
            timer = new System.Windows.Forms.Timer(components);
            newToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { cutToolStripMenuItem, copyToolStripMenuItem, pasteToolStripMenuItem, colorToolStripMenuItem, fontToolStripMenuItem, toolStripMenuItem1, fileToolStripMenuItem, closeToolStripMenuItem, optionsToolStripMenuItem, showToolStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(181, 248);
            contextMenuStrip.Opening += contextMenuStrip_Opening;
            // 
            // cutToolStripMenuItem
            // 
            cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            cutToolStripMenuItem.Size = new Size(180, 24);
            cutToolStripMenuItem.Text = "Cut";
            cutToolStripMenuItem.Click += cutToolStripMenuItem_Click;
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new Size(180, 24);
            copyToolStripMenuItem.Text = "Copy";
            copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.Size = new Size(180, 24);
            pasteToolStripMenuItem.Text = "Paste";
            pasteToolStripMenuItem.Click += pasteToolStripMenuItem_Click;
            // 
            // colorToolStripMenuItem
            // 
            colorToolStripMenuItem.Name = "colorToolStripMenuItem";
            colorToolStripMenuItem.Size = new Size(180, 24);
            colorToolStripMenuItem.Text = "Color";
            colorToolStripMenuItem.Click += colorToolStripMenuItem_Click;
            // 
            // fontToolStripMenuItem
            // 
            fontToolStripMenuItem.Name = "fontToolStripMenuItem";
            fontToolStripMenuItem.Size = new Size(180, 24);
            fontToolStripMenuItem.Text = "Font";
            fontToolStripMenuItem.Click += fontToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(177, 6);
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem, openToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(180, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(180, 24);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(180, 24);
            saveAsToolStripMenuItem.Text = "Save as";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(180, 24);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(180, 24);
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { topMostToolStripMenuItem, defaultFontToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(180, 24);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // topMostToolStripMenuItem
            // 
            topMostToolStripMenuItem.Name = "topMostToolStripMenuItem";
            topMostToolStripMenuItem.Size = new Size(158, 24);
            topMostToolStripMenuItem.Text = "TopMost";
            topMostToolStripMenuItem.Click += topMostToolStripMenuItem_Click;
            // 
            // defaultFontToolStripMenuItem
            // 
            defaultFontToolStripMenuItem.Name = "defaultFontToolStripMenuItem";
            defaultFontToolStripMenuItem.Size = new Size(158, 24);
            defaultFontToolStripMenuItem.Text = "Default font";
            defaultFontToolStripMenuItem.Click += defaultFontToolStripMenuItem_Click;
            // 
            // showToolStripMenuItem
            // 
            showToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { normalToolStripMenuItem, doneToolStripMenuItem, deletedToolStripMenuItem });
            showToolStripMenuItem.Name = "showToolStripMenuItem";
            showToolStripMenuItem.Size = new Size(180, 24);
            showToolStripMenuItem.Text = "Show";
            // 
            // normalToolStripMenuItem
            // 
            normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            normalToolStripMenuItem.Size = new Size(131, 24);
            normalToolStripMenuItem.Text = "Normal";
            normalToolStripMenuItem.Click += normalToolStripMenuItem_Click;
            // 
            // doneToolStripMenuItem
            // 
            doneToolStripMenuItem.Name = "doneToolStripMenuItem";
            doneToolStripMenuItem.Size = new Size(131, 24);
            doneToolStripMenuItem.Text = "Done";
            doneToolStripMenuItem.Click += doneToolStripMenuItem_Click;
            // 
            // deletedToolStripMenuItem
            // 
            deletedToolStripMenuItem.Name = "deletedToolStripMenuItem";
            deletedToolStripMenuItem.Size = new Size(131, 24);
            deletedToolStripMenuItem.Text = "Deleted";
            deletedToolStripMenuItem.Click += deletedToolStripMenuItem_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(richTextBoxNote, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(10, 10);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            tableLayoutPanel1.Size = new Size(1091, 616);
            tableLayoutPanel1.TabIndex = 10;
            // 
            // richTextBoxNote
            // 
            richTextBoxNote.AcceptsTab = true;
            richTextBoxNote.BackColor = Color.FromArgb(128, 255, 128);
            richTextBoxNote.BorderStyle = BorderStyle.None;
            richTextBoxNote.ContextMenuStrip = contextMenuStrip;
            richTextBoxNote.DetectUrls = false;
            richTextBoxNote.Dock = DockStyle.Fill;
            richTextBoxNote.Font = new Font("Consolas", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            richTextBoxNote.Location = new Point(4, 4);
            richTextBoxNote.Margin = new Padding(4);
            richTextBoxNote.Name = "richTextBoxNote";
            richTextBoxNote.Size = new Size(1083, 538);
            richTextBoxNote.TabIndex = 5;
            richTextBoxNote.Text = "";
            richTextBoxNote.LinkClicked += richTextBoxNote_LinkClicked;
            richTextBoxNote.TextChanged += richTextBoxNote_TextChanged;
            richTextBoxNote.MouseDown += richTextBoxNote_MouseDown;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(flowLayoutPanel1, 1, 0);
            tableLayoutPanel2.Controls.Add(flowLayoutPanel2, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 549);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1085, 64);
            tableLayoutPanel2.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(buttonSkipRight);
            flowLayoutPanel1.Controls.Add(buttonRight);
            flowLayoutPanel1.Controls.Add(buttonAdd);
            flowLayoutPanel1.Controls.Add(buttonLeft);
            flowLayoutPanel1.Controls.Add(buttonSkipLeft);
            flowLayoutPanel1.Controls.Add(buttonDone);
            flowLayoutPanel1.Controls.Add(buttonDelete);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel1.Location = new Point(253, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(829, 58);
            flowLayoutPanel1.TabIndex = 15;
            // 
            // buttonSkipRight
            // 
            buttonSkipRight.BackColor = Color.FromArgb(192, 255, 192);
            buttonSkipRight.Font = new Font("Consolas", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonSkipRight.Location = new Point(764, 3);
            buttonSkipRight.Name = "buttonSkipRight";
            buttonSkipRight.Size = new Size(62, 53);
            buttonSkipRight.TabIndex = 24;
            buttonSkipRight.Text = ">>";
            buttonSkipRight.UseVisualStyleBackColor = false;
            buttonSkipRight.Click += buttonSkipRight_Click;
            // 
            // buttonRight
            // 
            buttonRight.BackColor = Color.FromArgb(192, 255, 192);
            buttonRight.Font = new Font("Consolas", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonRight.Location = new Point(709, 3);
            buttonRight.Name = "buttonRight";
            buttonRight.Size = new Size(49, 53);
            buttonRight.TabIndex = 18;
            buttonRight.Text = ">";
            buttonRight.UseVisualStyleBackColor = false;
            buttonRight.Click += buttonRight_Click;
            // 
            // buttonAdd
            // 
            buttonAdd.BackColor = Color.FromArgb(192, 255, 192);
            buttonAdd.Font = new Font("Consolas", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonAdd.Location = new Point(654, 3);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(49, 53);
            buttonAdd.TabIndex = 19;
            buttonAdd.Text = "+";
            buttonAdd.UseVisualStyleBackColor = false;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // buttonLeft
            // 
            buttonLeft.BackColor = Color.FromArgb(192, 255, 192);
            buttonLeft.Font = new Font("Consolas", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonLeft.Location = new Point(599, 3);
            buttonLeft.Name = "buttonLeft";
            buttonLeft.Size = new Size(49, 53);
            buttonLeft.TabIndex = 17;
            buttonLeft.Text = "<";
            buttonLeft.UseVisualStyleBackColor = false;
            buttonLeft.Click += buttonLeft_Click;
            // 
            // buttonSkipLeft
            // 
            buttonSkipLeft.BackColor = Color.FromArgb(192, 255, 192);
            buttonSkipLeft.Font = new Font("Consolas", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonSkipLeft.Location = new Point(531, 3);
            buttonSkipLeft.Name = "buttonSkipLeft";
            buttonSkipLeft.Size = new Size(62, 53);
            buttonSkipLeft.TabIndex = 20;
            buttonSkipLeft.Text = "<<";
            buttonSkipLeft.UseVisualStyleBackColor = false;
            buttonSkipLeft.Click += buttonSkipLeft_Click;
            // 
            // buttonDone
            // 
            buttonDone.BackColor = Color.FromArgb(224, 224, 224);
            buttonDone.Font = new Font("Consolas", 24F);
            buttonDone.Location = new Point(463, 3);
            buttonDone.Name = "buttonDone";
            buttonDone.Size = new Size(62, 53);
            buttonDone.TabIndex = 22;
            buttonDone.Text = "Y";
            buttonDone.UseVisualStyleBackColor = false;
            buttonDone.Click += buttonDone_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.BackColor = Color.FromArgb(255, 192, 192);
            buttonDelete.Font = new Font("Consolas", 24F);
            buttonDelete.Location = new Point(395, 3);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(62, 53);
            buttonDelete.TabIndex = 23;
            buttonDelete.Text = "X";
            buttonDelete.UseVisualStyleBackColor = false;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(textBoxPosition);
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.Location = new Point(3, 3);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(244, 58);
            flowLayoutPanel2.TabIndex = 16;
            // 
            // textBoxPosition
            // 
            textBoxPosition.BackColor = Color.FromArgb(192, 255, 192);
            textBoxPosition.BorderStyle = BorderStyle.None;
            textBoxPosition.Font = new Font("Consolas", 32.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxPosition.Location = new Point(3, 3);
            textBoxPosition.Name = "textBoxPosition";
            textBoxPosition.Size = new Size(241, 51);
            textBoxPosition.TabIndex = 15;
            textBoxPosition.Text = "0/1000";
            textBoxPosition.TextAlign = HorizontalAlignment.Center;
            textBoxPosition.KeyDown += textBoxPosition_KeyDown;
            // 
            // timer
            // 
            timer.Enabled = true;
            timer.Interval = 1000;
            timer.Tick += timer_Tick;
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(180, 24);
            newToolStripMenuItem.Text = "New";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // FormFlowToDo
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(12F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(128, 255, 128);
            ClientSize = new Size(1111, 636);
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Consolas", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Margin = new Padding(4);
            Name = "FormFlowToDo";
            Padding = new Padding(10);
            StartPosition = FormStartPosition.Manual;
            Text = "Flow ToDo";
            TopMost = true;
            FormClosed += FormFlowToDo_FormClosed;
            Load += FormFlowToDo_Load;
            DragDrop += FormFlowToDo_DragDrop;
            DragEnter += FormFlowToDo_DragEnter;
            KeyDown += FormFlowToDo_KeyDown;
            Resize += FormFlowToDo_Resize;
            contextMenuStrip.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem closeToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button buttonRight;
        private Button buttonLeft;
        private Button buttonDelete;
        private Button buttonDone;
        private Button buttonSkipLeft;
        private Button buttonAdd;
        private FlowLayoutPanel flowLayoutPanel2;
        private TextBox textBoxPosition;
        private Button buttonSkipRight;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem topMostToolStripMenuItem;
        private ToolStripMenuItem defaultFontToolStripMenuItem;
        private CustomRichTextBox richTextBoxNote;
        private ToolStripMenuItem showToolStripMenuItem;
        private ToolStripMenuItem normalToolStripMenuItem;
        private ToolStripMenuItem doneToolStripMenuItem;
        private ToolStripMenuItem deletedToolStripMenuItem;
        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripMenuItem colorToolStripMenuItem;
        private ToolStripMenuItem fontToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.Timer timer;
        private ToolStripMenuItem newToolStripMenuItem;
    }
}
