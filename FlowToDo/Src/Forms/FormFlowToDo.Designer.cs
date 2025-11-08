namespace FlowToDo
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
            bckgToolStripMenuItem = new ToolStripMenuItem();
            fontToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            copyStyleToolStripMenuItem = new ToolStripMenuItem();
            pasteStyleToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            topMostToolStripMenuItem = new ToolStripMenuItem();
            defaultFontToolStripMenuItem = new ToolStripMenuItem();
            autorunToolStripMenuItem = new ToolStripMenuItem();
            colorsToolStripMenuItem = new ToolStripMenuItem();
            setTodoColorToolStripMenuItem = new ToolStripMenuItem();
            setDoneColorToolStripMenuItem = new ToolStripMenuItem();
            setDeleteColorToolStripMenuItem = new ToolStripMenuItem();
            showToolStripMenuItem = new ToolStripMenuItem();
            normalToolStripMenuItem = new ToolStripMenuItem();
            doneToolStripMenuItem = new ToolStripMenuItem();
            deletedToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            closeToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanel1 = new TableLayoutPanel();
            richTextBoxNote = new CustomRichTextBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            buttonSkipRight = new Button();
            buttonRight = new Button();
            buttonAdd = new Button();
            buttonLeft = new Button();
            buttonSkipLeft = new Button();
            buttonDone = new Button();
            buttonDelete = new Button();
            buttonSearchRight = new Button();
            textBoxSearch = new TextBox();
            buttonSearchLeft = new Button();
            flowLayoutPanel2 = new FlowLayoutPanel();
            textBoxPosition = new TextBox();
            timer = new System.Windows.Forms.Timer(components);
            contextMenuStrip.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { cutToolStripMenuItem, copyToolStripMenuItem, pasteToolStripMenuItem, colorToolStripMenuItem, bckgToolStripMenuItem, fontToolStripMenuItem, toolStripMenuItem1, fileToolStripMenuItem, toolsToolStripMenuItem, optionsToolStripMenuItem, showToolStripMenuItem, toolStripMenuItem2, closeToolStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(131, 280);
            contextMenuStrip.Opening += contextMenuStrip_Opening;
            // 
            // cutToolStripMenuItem
            // 
            cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            cutToolStripMenuItem.Size = new Size(130, 24);
            cutToolStripMenuItem.Text = "Cut";
            cutToolStripMenuItem.Click += cutToolStripMenuItem_Click;
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new Size(130, 24);
            copyToolStripMenuItem.Text = "Copy";
            copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.Size = new Size(130, 24);
            pasteToolStripMenuItem.Text = "Paste";
            pasteToolStripMenuItem.Click += pasteToolStripMenuItem_Click;
            // 
            // colorToolStripMenuItem
            // 
            colorToolStripMenuItem.Name = "colorToolStripMenuItem";
            colorToolStripMenuItem.Size = new Size(130, 24);
            colorToolStripMenuItem.Text = "Color";
            colorToolStripMenuItem.Click += colorToolStripMenuItem_Click;
            // 
            // bckgToolStripMenuItem
            // 
            bckgToolStripMenuItem.Name = "bckgToolStripMenuItem";
            bckgToolStripMenuItem.Size = new Size(130, 24);
            bckgToolStripMenuItem.Text = "Bckg";
            bckgToolStripMenuItem.Click += bckgToolStripMenuItem_Click;
            // 
            // fontToolStripMenuItem
            // 
            fontToolStripMenuItem.Name = "fontToolStripMenuItem";
            fontToolStripMenuItem.Size = new Size(130, 24);
            fontToolStripMenuItem.Text = "Font";
            fontToolStripMenuItem.Click += fontToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(127, 6);
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem, openToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(130, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(127, 24);
            newToolStripMenuItem.Text = "New";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(127, 24);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(127, 24);
            saveAsToolStripMenuItem.Text = "Save as";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(127, 24);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { copyStyleToolStripMenuItem, pasteStyleToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(130, 24);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // copyStyleToolStripMenuItem
            // 
            copyStyleToolStripMenuItem.Name = "copyStyleToolStripMenuItem";
            copyStyleToolStripMenuItem.Size = new Size(146, 24);
            copyStyleToolStripMenuItem.Text = "Copy style";
            copyStyleToolStripMenuItem.Click += copyStyleToolStripMenuItem_Click;
            // 
            // pasteStyleToolStripMenuItem
            // 
            pasteStyleToolStripMenuItem.Name = "pasteStyleToolStripMenuItem";
            pasteStyleToolStripMenuItem.Size = new Size(146, 24);
            pasteStyleToolStripMenuItem.Text = "Paste style";
            pasteStyleToolStripMenuItem.Click += pasteStyleToolStripMenuItem_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { topMostToolStripMenuItem, defaultFontToolStripMenuItem, autorunToolStripMenuItem, colorsToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(130, 24);
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
            // autorunToolStripMenuItem
            // 
            autorunToolStripMenuItem.Name = "autorunToolStripMenuItem";
            autorunToolStripMenuItem.Size = new Size(158, 24);
            autorunToolStripMenuItem.Text = "Autorun";
            autorunToolStripMenuItem.Click += autorunToolStripMenuItem_Click;
            // 
            // colorsToolStripMenuItem
            // 
            colorsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { setTodoColorToolStripMenuItem, setDoneColorToolStripMenuItem, setDeleteColorToolStripMenuItem });
            colorsToolStripMenuItem.Name = "colorsToolStripMenuItem";
            colorsToolStripMenuItem.Size = new Size(158, 24);
            colorsToolStripMenuItem.Text = "Colors";
            // 
            // setTodoColorToolStripMenuItem
            // 
            setTodoColorToolStripMenuItem.Name = "setTodoColorToolStripMenuItem";
            setTodoColorToolStripMenuItem.Size = new Size(183, 24);
            setTodoColorToolStripMenuItem.Text = "Set todo color";
            setTodoColorToolStripMenuItem.Click += setTodoColorToolStripMenuItem_Click;
            // 
            // setDoneColorToolStripMenuItem
            // 
            setDoneColorToolStripMenuItem.Name = "setDoneColorToolStripMenuItem";
            setDoneColorToolStripMenuItem.Size = new Size(183, 24);
            setDoneColorToolStripMenuItem.Text = "Set done color";
            setDoneColorToolStripMenuItem.Click += setDoneColorToolStripMenuItem_Click;
            // 
            // setDeleteColorToolStripMenuItem
            // 
            setDeleteColorToolStripMenuItem.Name = "setDeleteColorToolStripMenuItem";
            setDeleteColorToolStripMenuItem.Size = new Size(183, 24);
            setDeleteColorToolStripMenuItem.Text = "Set delete color";
            setDeleteColorToolStripMenuItem.Click += setDeleteColorToolStripMenuItem_Click;
            // 
            // showToolStripMenuItem
            // 
            showToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { normalToolStripMenuItem, doneToolStripMenuItem, deletedToolStripMenuItem });
            showToolStripMenuItem.Name = "showToolStripMenuItem";
            showToolStripMenuItem.Size = new Size(130, 24);
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
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(127, 6);
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(130, 24);
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.Transparent;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(richTextBoxNote, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(10, 10);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 63F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(1238, 616);
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
            richTextBoxNote.Size = new Size(1230, 545);
            richTextBoxNote.TabIndex = 5;
            richTextBoxNote.Text = "";
            richTextBoxNote.LinkClicked += richTextBoxNote_LinkClicked;
            richTextBoxNote.TextChanged += richTextBoxNote_TextChanged;
            richTextBoxNote.KeyDown += richTextBoxNote_KeyDown;
            richTextBoxNote.MouseDown += richTextBoxNote_MouseDown;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = Color.Transparent;
            tableLayoutPanel2.ColumnCount = 11;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            tableLayoutPanel2.Controls.Add(buttonSkipRight, 10, 0);
            tableLayoutPanel2.Controls.Add(buttonRight, 9, 0);
            tableLayoutPanel2.Controls.Add(buttonAdd, 8, 0);
            tableLayoutPanel2.Controls.Add(buttonLeft, 7, 0);
            tableLayoutPanel2.Controls.Add(buttonSkipLeft, 6, 0);
            tableLayoutPanel2.Controls.Add(buttonDone, 5, 0);
            tableLayoutPanel2.Controls.Add(buttonDelete, 4, 0);
            tableLayoutPanel2.Controls.Add(buttonSearchRight, 3, 0);
            tableLayoutPanel2.Controls.Add(textBoxSearch, 2, 0);
            tableLayoutPanel2.Controls.Add(buttonSearchLeft, 1, 0);
            tableLayoutPanel2.Controls.Add(flowLayoutPanel2, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 556);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RightToLeft = RightToLeft.No;
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1232, 57);
            tableLayoutPanel2.TabIndex = 3;
            // 
            // buttonSkipRight
            // 
            buttonSkipRight.BackColor = Color.FromArgb(192, 255, 192);
            buttonSkipRight.FlatStyle = FlatStyle.Flat;
            buttonSkipRight.Font = new Font("Consolas", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonSkipRight.Location = new Point(1165, 3);
            buttonSkipRight.Name = "buttonSkipRight";
            buttonSkipRight.Size = new Size(64, 46);
            buttonSkipRight.TabIndex = 27;
            buttonSkipRight.Text = ">>";
            buttonSkipRight.UseVisualStyleBackColor = false;
            buttonSkipRight.Click += buttonSkipRight_Click;
            // 
            // buttonRight
            // 
            buttonRight.BackColor = Color.FromArgb(192, 255, 192);
            buttonRight.FlatStyle = FlatStyle.Flat;
            buttonRight.Font = new Font("Consolas", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonRight.Location = new Point(1115, 3);
            buttonRight.Name = "buttonRight";
            buttonRight.Size = new Size(44, 46);
            buttonRight.TabIndex = 18;
            buttonRight.Text = ">";
            buttonRight.UseVisualStyleBackColor = false;
            buttonRight.Click += buttonRight_Click;
            // 
            // buttonAdd
            // 
            buttonAdd.BackColor = Color.FromArgb(192, 255, 192);
            buttonAdd.FlatStyle = FlatStyle.Flat;
            buttonAdd.Font = new Font("Consolas", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonAdd.Location = new Point(1065, 3);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(44, 46);
            buttonAdd.TabIndex = 19;
            buttonAdd.Text = "+";
            buttonAdd.UseVisualStyleBackColor = false;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // buttonLeft
            // 
            buttonLeft.BackColor = Color.FromArgb(192, 255, 192);
            buttonLeft.FlatStyle = FlatStyle.Flat;
            buttonLeft.Font = new Font("Consolas", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonLeft.Location = new Point(1015, 3);
            buttonLeft.Name = "buttonLeft";
            buttonLeft.Size = new Size(44, 46);
            buttonLeft.TabIndex = 17;
            buttonLeft.Text = "<";
            buttonLeft.UseVisualStyleBackColor = false;
            buttonLeft.Click += buttonLeft_Click;
            // 
            // buttonSkipLeft
            // 
            buttonSkipLeft.BackColor = Color.FromArgb(192, 255, 192);
            buttonSkipLeft.FlatStyle = FlatStyle.Flat;
            buttonSkipLeft.Font = new Font("Consolas", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonSkipLeft.Location = new Point(945, 3);
            buttonSkipLeft.Name = "buttonSkipLeft";
            buttonSkipLeft.Size = new Size(64, 46);
            buttonSkipLeft.TabIndex = 20;
            buttonSkipLeft.Text = "<<";
            buttonSkipLeft.UseVisualStyleBackColor = false;
            buttonSkipLeft.Click += buttonSkipLeft_Click;
            // 
            // buttonDone
            // 
            buttonDone.BackColor = Color.FromArgb(224, 224, 224);
            buttonDone.FlatStyle = FlatStyle.Flat;
            buttonDone.Font = new Font("Consolas", 24F);
            buttonDone.Location = new Point(895, 3);
            buttonDone.Name = "buttonDone";
            buttonDone.Size = new Size(44, 46);
            buttonDone.TabIndex = 22;
            buttonDone.Text = "Y";
            buttonDone.UseVisualStyleBackColor = false;
            buttonDone.Click += buttonDone_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.BackColor = Color.FromArgb(255, 192, 192);
            buttonDelete.FlatStyle = FlatStyle.Flat;
            buttonDelete.Font = new Font("Consolas", 24F);
            buttonDelete.Location = new Point(845, 3);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(44, 46);
            buttonDelete.TabIndex = 23;
            buttonDelete.Text = "X";
            buttonDelete.UseVisualStyleBackColor = false;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // buttonSearchRight
            // 
            buttonSearchRight.BackColor = Color.FromArgb(192, 255, 192);
            buttonSearchRight.FlatStyle = FlatStyle.Flat;
            buttonSearchRight.Location = new Point(795, 3);
            buttonSearchRight.Name = "buttonSearchRight";
            buttonSearchRight.Size = new Size(44, 46);
            buttonSearchRight.TabIndex = 29;
            buttonSearchRight.Text = ">";
            buttonSearchRight.UseVisualStyleBackColor = false;
            buttonSearchRight.Visible = false;
            buttonSearchRight.Click += buttonSearchRight_Click;
            // 
            // textBoxSearch
            // 
            textBoxSearch.BackColor = Color.FromArgb(192, 255, 192);
            textBoxSearch.BorderStyle = BorderStyle.None;
            textBoxSearch.Dock = DockStyle.Fill;
            textBoxSearch.Font = new Font("Consolas", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxSearch.Location = new Point(303, 5);
            textBoxSearch.Margin = new Padding(3, 5, 3, 3);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(486, 44);
            textBoxSearch.TabIndex = 28;
            textBoxSearch.Visible = false;
            textBoxSearch.TextChanged += textBoxSearch_TextChanged;
            textBoxSearch.KeyDown += textBoxSearch_KeyDown;
            // 
            // buttonSearchLeft
            // 
            buttonSearchLeft.BackColor = Color.FromArgb(192, 255, 192);
            buttonSearchLeft.FlatStyle = FlatStyle.Flat;
            buttonSearchLeft.Location = new Point(253, 3);
            buttonSearchLeft.Name = "buttonSearchLeft";
            buttonSearchLeft.Size = new Size(44, 46);
            buttonSearchLeft.TabIndex = 30;
            buttonSearchLeft.Text = "<";
            buttonSearchLeft.UseVisualStyleBackColor = false;
            buttonSearchLeft.Visible = false;
            buttonSearchLeft.Click += buttonSearchLeft_Click;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(textBoxPosition);
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.Location = new Point(3, 3);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(244, 51);
            flowLayoutPanel2.TabIndex = 16;
            // 
            // textBoxPosition
            // 
            textBoxPosition.BackColor = Color.FromArgb(192, 255, 192);
            textBoxPosition.BorderStyle = BorderStyle.None;
            textBoxPosition.Font = new Font("Consolas", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxPosition.Location = new Point(3, 3);
            textBoxPosition.Name = "textBoxPosition";
            textBoxPosition.Size = new Size(241, 44);
            textBoxPosition.TabIndex = 21;
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
            // FormFlowToDo
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(12F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(128, 255, 128);
            ClientSize = new Size(1258, 636);
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Consolas", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Margin = new Padding(4);
            Name = "FormFlowToDo";
            Padding = new Padding(10);
            StartPosition = FormStartPosition.Manual;
            Text = "FlowToDo";
            TopMost = true;
            FormClosed += FormFlowToDo_FormClosed;
            Load += FormFlowToDo_Load;
            Shown += FormFlowToDo_Shown;
            DragDrop += FormFlowToDo_DragDrop;
            DragEnter += FormFlowToDo_DragEnter;
            KeyDown += FormFlowToDo_KeyDown;
            Resize += FormFlowToDo_Resize;
            contextMenuStrip.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem closeToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Button buttonLeft;
        private Button buttonDelete;
        private Button buttonDone;
        private Button buttonSkipLeft;
        private Button buttonAdd;
        private FlowLayoutPanel flowLayoutPanel2;
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
        private ToolStripMenuItem autorunToolStripMenuItem;
        private Button buttonRight;
        private ToolStripMenuItem colorsToolStripMenuItem;
        private ToolStripMenuItem setTodoColorToolStripMenuItem;
        private ToolStripMenuItem setDoneColorToolStripMenuItem;
        private ToolStripMenuItem setDeleteColorToolStripMenuItem;
        private TextBox textBoxPosition;
        private Button buttonSkipRight;
        private TextBox textBoxSearch;
        private Button buttonSearchRight;
        private Button buttonSearchLeft;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem copyStyleToolStripMenuItem;
        private ToolStripMenuItem pasteStyleToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem bckgToolStripMenuItem;
    }
}
