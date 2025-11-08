using FlowToDo.Src.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace FlowToDo
{
    public partial class FormFlowToDo : Form
    {
        public Data data = new Data();

        public string? pathToFlowToDoFile = "";
        private bool inicialized = false;

        private bool showNormal = true;
        private bool showDone = false;
        private bool showDeleted = false;

        public bool saved = true;
        public bool suspenUnsave = false;
        public DateTime unSavedAt = DateTime.Now;
        public string? unmodifiedText = "";
        public System.Drawing.Font? defaultRitchTextFont = null;

        private DateTime? nextBackup = null;


        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        private const int WM_SETREDRAW = 0x0B;

        // CONSTRUCTOR
        public FormFlowToDo(string filePath)
        {
            this.DoubleBuffered = true;

            InitializeComponent();

            defaultRitchTextFont = richTextBoxNote.Font;

            this.Width = 1000;
            this.Height = 500;
            this.CenterToScreen();

            if (filePath == "" || !File.Exists(filePath))
            {

                if (this.OpenFile(Program.defaultFlowTodoFile))
                {
                    this.pathToFlowToDoFile = Program.defaultFlowTodoFile;
                }
                else
                {
                    initTodoList();
                }
                updatePager();
            }
            else
            {
                initTodoList();
            }

            richTextBoxNote.AllowDrop = true; // must be enabled!
            richTextBoxNote.DragEnter += FormFlowToDo_DragEnter;
            richTextBoxNote.DragDrop += FormFlowToDo_DragDrop;

            inicialized = true;
        }

        /******************************************************************************************/

        // FILE OPEN
        public bool OpenFile(string? filePath)
        {
            inicialized = false;
            if (filePath != "" && File.Exists(filePath))
            {
                bool createdNew;
                string mutexName = "Global\\" + Program.appName + " " + filePath
                .Replace("\\", "_")
                .Replace(":", "_")
                .Replace("/", "_");

                Mutex? mutex = new Mutex(true, mutexName, out createdNew);
                if (!createdNew)
                {
                    mutex.ReleaseMutex();
                    mutex = null;
                    return false;
                }

                try
                {
                    var serializer = new XmlSerializer(typeof(Data));
                    string xml = File.ReadAllText(filePath);
                    using var sr = new StringReader(xml);
                    Data? data = (Data?)serializer.Deserialize(sr);
                    if (data == null)
                    {
                        return false;
                    }

                    this.data = data;
                    this.markAsSaved();
                    this.pathToFlowToDoFile = filePath;

                    this.richTextBoxNote.Font = Tools.StringToFont(this.data.defaultFont);

                    if (0 <= this.data.currentTodoPos && this.data.currentTodoPos < data.todoList.Count())
                    {
                        this.data.currentTodo = data.todoList[this.data.currentTodoPos];
                    }
                    else
                    {
                        this.data.currentTodo = this.data.todoList[data.todoList.Count() - 1];
                    }

                    SelectTodo(this.data.currentTodo);

                    data.TopMost = this.TopMost;

                    this.WindowState = FormWindowState.Normal;
                    this.Left = data.winX;
                    this.Top = data.winY;
                    this.Height = data.winH;
                    this.Width = data.winW;

                    nextBackup = DateTime.Now.AddHours(1);

                    this.getTimeEvents();

                    if (Program.mutex != null)
                    {
                        Program.mutex.ReleaseMutex();
                        Program.mutex = null;
                    }
                    Program.mutex = mutex;

                    return true;
                }
                catch (Exception)
                {
                    mutex.ReleaseMutex();
                    return false;
                }

            }
            inicialized = true;

            return false;
        }

        // FILE SAVE
        public void SaveFile(string? path = null)
        {
            if (data == null)
            {
                return;
            }

            if (path == null)
            {
                path = this.pathToFlowToDoFile;
            }

            this.textSaveToToDo();

            removeEmptyTodoList();

            if (path == "")
            {
                this.saveAsToolStripMenuItem_Click(null, null);
                return;
            }

            try
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    this.data.winX = this.Left;
                    this.data.winY = this.Top;
                    this.data.winH = this.Height;
                    this.data.winW = this.Width;
                }
                if (this.WindowState == FormWindowState.Maximized) this.data.winStatus = 2;
                if (this.WindowState == FormWindowState.Minimized) this.data.winStatus = 1;
                if (this.WindowState == FormWindowState.Normal) this.data.winStatus = 0;
                this.data.TopMost = this.TopMost;
                this.data.defaultFont = Tools.FontToString(this.richTextBoxNote.Font);
                this.data.currentTodoPos = this.currentTodoPos();
                var serializer = new XmlSerializer(typeof(Data));
                using var sw = new StringWriter();
                serializer.Serialize(sw, this.data);
                string xml = sw.ToString();
                if (path != null)
                {
                    File.WriteAllText(path, xml);
                }
                this.markAsSaved();
            }
            catch (Exception)
            {

            }
        }

        // FILE SAVE AS 
        public void SaveAsFile(string filePath = "")
        {
            this.pathToFlowToDoFile = filePath;
            this.SaveFile();
        }

        /******************************************************************************************/

        // EVENT FORM CLOSED
        private void FormFlowToDo_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!this.saved)
            {
                this.SaveFile();
            }
        }

        // EVENT FORM LOAD
        private void FormFlowToDo_Load(object sender, EventArgs e)
        {

        }

        // EVENT FORM SHOWN
        private void FormFlowToDo_Shown(object sender, EventArgs e)
        {
            this.richTextBoxNote.Focus();
        }

        // EVENT FORM RESIZE
        private void FormFlowToDo_Resize(object sender, EventArgs e)
        {
            if (this.textBoxSearch.Visible)
            {
                int newWidth = this.Width - 500 - 300;
                this.textBoxSearch.Width = newWidth > 100 ? newWidth : 100;
            }

            if (this.data == null)
            {
                return;
            }

            if (inicialized && this.WindowState == FormWindowState.Normal)
            {
                this.data.winX = this.Left;
                this.data.winY = this.Top;
                this.data.winH = this.Height;
                this.data.winW = this.Width;
            }
        }

        // EVENT SHORTCUTS
        private void FormFlowToDo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S) // KEY CTRL+S
            {
                e.SuppressKeyPress = true;
                this.saveToolStripMenuItem_Click(null, null);
            }

            if (e.Control && e.Shift && e.KeyCode == Keys.S) // KEY CTRL+SHIFT+S
            {
                e.SuppressKeyPress = true;
                this.saveAsToolStripMenuItem_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.O) // KEY CTRL+O
            {
                e.SuppressKeyPress = true;
                this.openToolStripMenuItem_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.N) // KEY CTRL+N
            {
                e.SuppressKeyPress = true;
                this.buttonAdd_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.F) // KEY CTRL+F
            {
                e.SuppressKeyPress = true;
                this.SearchBarShow();
            }

            if (e.KeyCode == Keys.F5) // KEY F5
            {
                e.SuppressKeyPress = true;
                this.buttonAdd_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.Delete) // KEY Delete
            {
                e.SuppressKeyPress = true;
                this.buttonDelete_Click(sender, e);
            }

            if (e.KeyCode == Keys.F9) // KEY F9
            {
                e.SuppressKeyPress = true;
                this.buttonDone_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.Right) // KEY RIGHT
            {
                e.SuppressKeyPress = true;
                this.buttonRight_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.Left) // KEY LEFT
            {
                e.SuppressKeyPress = true;
                this.buttonLeft_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.Up) // KEY UP
            {
                e.SuppressKeyPress = true;
                this.buttonSkipRight_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.Down) // KEY DOWN
            {
                e.SuppressKeyPress = true;
                this.buttonSkipLeft_Click(sender, e);
            }

        }

        // EVENT DRAG ENTER
        private void FormFlowToDo_DragEnter(object? sender, DragEventArgs? e)
        {
            if (!inicialized)
            {
                return;
            }

            if (e != null && e.Data != null)
            {
                bool hasContent = false;
                try
                {
                    if (e.Data.GetData(DataFormats.FileDrop) != null)
                        hasContent = true;
                    else if (e.Data.GetData(DataFormats.Text) != null)
                        hasContent = true;
                    else if (e.Data.GetData(DataFormats.UnicodeText) != null)
                        hasContent = true;
                    else if (e.Data.GetData(DataFormats.Html) != null)
                        hasContent = true;

                }
                catch (Exception)
                {


                }

                e.Effect = DragDropEffects.None;

                if (hasContent)
                {
                    e.Effect = DragDropEffects.Copy;
                }
            }
        }

        // EVENT DRAG DROP
        private void FormFlowToDo_DragDrop(object? sender, DragEventArgs? e)
        {
            if (!inicialized)
            {
                return;
            }

            if (e == null || e.Data == null)
            {
                return;
            }

            string? text = null;
            string? html = null;
            string[]? files = null;

            try
            {
                if (e.Data.GetData(DataFormats.FileDrop) != null)
                {
                    files = (string[]?)e.Data.GetData(DataFormats.FileDrop);
                }

                if (e.Data.GetData(DataFormats.Text) != null)
                {
                    text = (string?)e.Data.GetData(DataFormats.Text);
                }

                if (e.Data.GetData(DataFormats.UnicodeText) != null)
                {
                    text = (string?)e.Data.GetData(DataFormats.UnicodeText);
                }

                if (e.Data.GetData(DataFormats.Html) != null)
                {
                    html = (string?)e.Data.GetData(DataFormats.Html);
                }
            }
            catch (Exception)
            {


            }

            if (files != null && files.Length == 1 && Path.GetExtension(files[0]) == ".FlowToDo")
            {
                if (this.OpenFile(files[0]))
                {
                    this.pathToFlowToDoFile = files[0];
                }
            }
            else if (text != null)
            {
                richTextBoxNote.SelectedText = text;
            }
            else if (html != null)
            {
                richTextBoxNote.SelectedText = html;
            }
            else if (html != null)
            {
                richTextBoxNote.SelectedText = html;
            }
            else if (files != null && files.Length > 0)
            {
                foreach (string f in files)
                {
                    richTextBoxNote.SelectedText = "\"" + f + "\"" + "\r\n";
                }
            }
        }

        /******************************************************************************************/

        // TOOL MAKE BACKUP 
        public void makeBackup()
        {
            long unixTimestampMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            this.SaveFile(Path.Combine(Program.backupDir, "backup-" + unixTimestampMs.ToString() + Program.defaultExtension));
        }

        // TIMER TICK
        private void timer_Tick(object sender, EventArgs e)
        {
            if (!saved && DateTime.Now - unSavedAt > TimeSpan.FromMinutes(1))
            {
                this.SaveFile();
            }

            if (nextBackup != null && DateTime.Now >= this.nextBackup)
            {
                nextBackup = DateTime.Now.AddHours(1);
                makeBackup();
            }

            if (this.data.nextEvent != null && DateTime.Now >= this.data.nextEvent.time) {
                Notifications.Show(this.data.nextEvent.name);
                this.getTimeEvents();
            }
        }

        /******************************************************************************************/

        // RESET TO DEFAULT EMPTY STATE
        public void initTodoList()
        {
            this.data = new Data();
            this.data.todoList.Clear();
            this.data.currentTodo = new ToDo();
            this.data.todoList.Add(this.data.currentTodo);

            this.markAsSaved();
            this.suspenUnsave = false;
            this.unSavedAt = DateTime.Now;
            this.richTextBoxNote.Font = defaultRitchTextFont;
            this.richTextBoxNote.Text = this.data.currentTodo.text;
            this.unmodifiedText = this.richTextBoxNote.Rtf;

            this.showNormal = true;
            this.showDone = false;
            this.showDeleted = false;

            inicialized = true;

            updatePager();
        }

        // REMOVE EMPTY CLEANING
        public void removeEmptyTodoList()
        {
            if (this.data != null)
            {
                this.data.todoList.RemoveAll(x => x.isEmpty && x != this.data.currentTodo);
            }

            updatePager();
        }

        // TOOL COUNT BY CATEGORY
        public int CountTodos()
        {
            int count = 0;

            if (this.data == null)
            {
                return count;
            }

            for (int i = 0; i < this.data.todoList.Count(); i++)
            {
                if ((this.data.todoList[i].deleted && !showDeleted) ||
                    (this.data.todoList[i].done && !showDone))
                {
                    continue;
                }

                count++;
            }

            return count;
        }

        // TOOL GET TODO INDEX RELATIVE TO CATEGORY
        public int GetPosOfTodos(ToDo? todo)
        {
            if (todo == null || this.data == null)
            {
                return -1;
            }

            int pos = this.data.todoList.IndexOf(todo);

            if (pos < 0)
            {
                return -1;
            }

            int relativePos = -1;
            for (int i = 0; i <= pos; i++)
            {
                if ((this.data.todoList[i].deleted && !showDeleted) ||
                    (this.data.todoList[i].done && !showDone))
                {
                    continue;
                }

                relativePos++;
            }

            return relativePos;
        }

        // SAVE TEXT FROM TEXTBOX TO TODO 
        public void textSaveToToDo()
        {
            if (this.data != null && this.data.currentTodo != null)
            {
                if (this.unmodifiedText != this.richTextBoxNote.Rtf)
                {
                    this.unsave();
                }

                this.data.currentTodo.text = this.richTextBoxNote.Rtf;
                this.unmodifiedText = this.richTextBoxNote.Rtf;
                this.data.currentTodo.rawText = this.richTextBoxNote.Text;
                this.data.currentTodo.isEmpty = this.richTextBoxNote.Text.Trim() == "";                
            }
        }

        public void markAsSaved()
        {

            if (this.saved)
            {
                return;
            }

            this.saved = true;
            this.Text = Program.appName;
        }

        // UNSAVE
        public void unsave()
        {

            if (!this.saved || this.suspenUnsave)
            {
                return;
            }

            this.saved = false;
            this.unSavedAt = DateTime.Now;
            this.Text = Program.appName + "*";
        }

        // PAGER UPDATE
        public void updatePager()
        {
            if (this.data == null || this.data.currentTodo == null)
            {
                this.BackColor = Color.Green;
                this.richTextBoxNote.BackColor = this.BackColor;
                this.textBoxPosition.Text = "";
                return;
            }

            this.textBoxPosition.Text = (this.GetPosOfTodos(this.data.currentTodo) + 1) + "/" + this.CountTodos();


            if (this.data.currentTodo != null)
            {
                if (this.data.currentTodo.deleted)
                {
                    this.BackColor = Tools.StringToColor(this.data.deletedColor, Color.Green);
                    this.richTextBoxNote.BackColor = this.BackColor;
                }
                else if (this.data.currentTodo.done)
                {
                    this.BackColor = Tools.StringToColor(this.data.doneColor, Color.Gray);
                    this.richTextBoxNote.BackColor = this.BackColor;
                }
                else
                {
                    this.BackColor = Tools.StringToColor(this.data.todoColor, Color.Green);
                    this.richTextBoxNote.BackColor = this.BackColor;
                }
            }
        }

        // TOOL GET PREV TODO
        public ToDo? GetPrevTodo()
        {
            if (this.data == null)
            {
                return null;
            }

            ToDo? prevTodo = null;

            for (int i = this.currentTodoPos() - 1; i >= 0; i--)
            {
                if ((this.data.todoList[i].deleted && !showDeleted) ||
                    (this.data.todoList[i].done && !showDone))
                {
                    continue;
                }

                prevTodo = this.data.todoList[i];
                break;
            }

            return prevTodo;
        }

        // TOOL GET NEXT TODO
        public ToDo? GetNextTodo()
        {
            if (this.data == null)
            {
                return null;
            }

            ToDo? nextTodo = null;

            for (int i = this.currentTodoPos() + 1; i < this.data.todoList.Count(); i++)
            {
                if ((this.data.todoList[i].deleted && !showDeleted) ||
                    (this.data.todoList[i].done && !showDone))
                {
                    continue;
                }

                nextTodo = this.data.todoList[i];
                break;
            }

            return nextTodo;
        }

        // TOOL GET FIRT TODO
        public ToDo? GetFirstTodo()
        {
            if (this.data == null)
            {
                return null;
            }

            ToDo? prevTodo = null;

            for (int i = this.currentTodoPos() - 1; i >= 0; i--)
            {
                if ((this.data.todoList[i].deleted && !showDeleted) ||
                    (this.data.todoList[i].done && !showDone))
                {
                    continue;
                }

                prevTodo = this.data.todoList[i];
            }

            return prevTodo;
        }

        // TOOL GET LAST TODO
        public ToDo? GetLastTodo()
        {
            if (this.data == null)
            {
                return null;
            }

            ToDo? nextTodo = null;

            for (int i = this.currentTodoPos() + 1; i < this.data.todoList.Count(); i++)
            {
                if ((this.data.todoList[i].deleted && !showDeleted) ||
                    (this.data.todoList[i].done && !showDone))
                {
                    continue;
                }

                nextTodo = this.data.todoList[i];
            }

            return nextTodo;
        }

        // TOOL SET TODO AS ACTUAL
        public void SelectTodo(ToDo? todo)
        {
            if (this.data == null)
            {
                return;
            }

            suspenUnsave = true;
            if (todo != null)
            {
                int position = this.data.todoList.IndexOf(todo);
                if (position > -1)
                {

                    this.data.currentTodo = todo;
                    if (this.data != null && this.data.currentTodo != null && this.data.currentTodo.text != null)
                    {
                        if (this.data.currentTodo.text.TrimStart().StartsWith(@"{\rtf", StringComparison.OrdinalIgnoreCase))
                        {
                            try
                            {
                                this.richTextBoxNote.Rtf = this.data.currentTodo.text;
                                this.unmodifiedText = this.richTextBoxNote.Rtf;
                                HighlightFilePaths();
                            }
                            catch
                            {
                                this.richTextBoxNote.Text = this.data.currentTodo.text;
                                this.unmodifiedText = this.richTextBoxNote.Rtf;
                                HighlightFilePaths();
                            }
                        }
                        else
                        {
                            this.richTextBoxNote.Text = this.data.currentTodo.text;
                            this.unmodifiedText = this.richTextBoxNote.Rtf;
                            HighlightFilePaths();
                        }
                    }

                }
            }

            suspenUnsave = false;

            updatePager();
        }

        // TOOL GO TO NODE BY INDEX (SKIP DONE AND DELETED)
        public ToDo? getTodoByNumber(int pos)
        {
            if (this.data == null)
            {
                return null;
            }

            ToDo? selectedTodo = null;
            int currentPos = -1;
            for (int i = 0; i < this.data.todoList.Count(); i++)
            {
                if ((this.data.todoList[i].deleted && !showDeleted) ||
                    (this.data.todoList[i].done && !showDone))
                {
                    continue;
                }

                currentPos++;

                if (pos - 1 == currentPos)
                {
                    selectedTodo = this.data.todoList[i];
                    break;
                }
            }

            return selectedTodo;
        }

        // TOOL GET CURRENT TODO POSITION
        public int currentTodoPos()
        {
            if (this.data == null || this.data.currentTodo == null)
            {
                return -1;
            }

            return this.data.todoList.IndexOf(this.data.currentTodo);
        }

        /******************************************************************************************/

        // TOOLBAR BUTTON  +
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (this.data == null)
            {
                return;
            }

            this.textSaveToToDo();

            ToDo todo = new ToDo();
            int index = this.currentTodoPos();
            this.data.currentTodo = todo;
            this.data.currentTodo.createdAt = Tools.Timestamp();
            this.data.todoList.Insert(index + 1, todo);
            this.richTextBoxNote.Rtf = this.data.currentTodo.text;
            this.unsave();
            updatePager();
        }

        // TOOLBAR BUTTON DELETE
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            this.textSaveToToDo();

            if (this.data == null || this.data.currentTodo == null)
            {
                return;
            }

            if (this.data.currentTodo.deleted)
            {
                this.data.currentTodo.deleted = false;
                this.data.currentTodo.deletedAt = "";
                this.unsave();
                updatePager();
            }
            else
            {
                this.data.currentTodo.deleted = true;
                this.data.currentTodo.deletedAt = Tools.Timestamp();
                this.unsave();

                ToDo? nextTodo = GetNextTodo();
                if (nextTodo == null)
                {
                    nextTodo = GetPrevTodo();
                }
                this.SelectTodo(nextTodo);

            }
        }

        // TOOLBAR BUTTON DONE
        private void buttonDone_Click(object sender, EventArgs e)
        {
            this.textSaveToToDo();

            if (this.data != null && this.data.currentTodo != null && !this.data.currentTodo.deleted)
            {
                if (this.data.currentTodo.done)
                {
                    this.data.currentTodo.done = false;
                    this.data.currentTodo.doneAt = "";
                    this.unsave();
                    updatePager();
                }
                else
                {
                    this.data.currentTodo.done = true;
                    this.data.currentTodo.doneAt = Tools.Timestamp();
                    this.unsave();

                    ToDo? nextTodo = GetNextTodo();
                    if (nextTodo == null)
                    {
                        nextTodo = GetPrevTodo();
                    }
                    this.SelectTodo(nextTodo);
                }
            }
        }

        // TOOLBAR BUTTON >
        private void buttonRight_Click(object sender, EventArgs e)
        {
            if (this.data == null)
            {
                return;
            }

            this.textSaveToToDo();

            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                ToDo? nextTodo = GetNextTodo();
                if (nextTodo != null && this.data.currentTodo != null)
                {
                    int indexNext = this.data.todoList.IndexOf(nextTodo);

                    int index = this.data.todoList.IndexOf(this.data.currentTodo);
                    if (index != -1 && indexNext != -1 && index != indexNext)
                    {
                        var temp = this.data.todoList[indexNext];
                        this.data.todoList[indexNext] = this.data.todoList[index];
                        this.data.todoList[index] = temp;
                        this.unsave();
                        updatePager();
                    }
                }

            }
            else
            {
                ToDo? nextTodo = GetNextTodo();
                this.SelectTodo(nextTodo);
            }
        }

        // TOOLBAR BUTTON <
        private void buttonLeft_Click(object sender, EventArgs e)
        {
            if (this.data == null)
            {
                return;
            }

            this.textSaveToToDo();

            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                ToDo? prevTodo = GetPrevTodo();
                if (prevTodo != null && this.data.currentTodo != null)
                {
                    int indexPrev = this.data.todoList.IndexOf(prevTodo);

                    int index = this.data.todoList.IndexOf(this.data.currentTodo);
                    if (index != -1 && indexPrev != -1 && index != indexPrev)
                    {
                        var temp = this.data.todoList[indexPrev];
                        this.data.todoList[indexPrev] = this.data.todoList[index];
                        this.data.todoList[index] = temp;
                        this.unsave();
                        updatePager();
                    }
                }

            }
            else
            {
                ToDo? prevTodo = GetPrevTodo();
                this.SelectTodo(prevTodo);
            }
        }

        // TOOLBAR BUTTON >>
        private void buttonSkipRight_Click(object sender, EventArgs e)
        {
            if (this.data == null)
            {
                return;
            }

            this.textSaveToToDo();

            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                if (this.data.currentTodo != null)
                {
                    int index = this.data.todoList.IndexOf(this.data.currentTodo);
                    if (index != -1 && index < this.data.todoList.Count - 1)
                    {
                        this.data.todoList.RemoveAt(index);
                        this.data.todoList.Add(this.data.currentTodo);
                        this.unsave();
                        updatePager();
                    }
                }
            }
            else
            {
                ToDo? nextTodo = GetLastTodo();
                this.SelectTodo(nextTodo);
            }

        }

        // TOOLBAR BUTTON <<
        private void buttonSkipLeft_Click(object? sender, EventArgs? e)
        {
            if (this.data == null)
            {
                return;
            }

            this.textSaveToToDo();

            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                if (this.data.currentTodo != null)
                {
                    int index = this.data.todoList.IndexOf(this.data.currentTodo);
                    if (index != -1 && index > 0)
                    {
                        this.data.todoList.RemoveAt(index);
                        this.data.todoList.Insert(0, this.data.currentTodo);
                        this.unsave();
                        updatePager();
                    }
                }

            }
            else
            {
                ToDo? nextTodo = GetFirstTodo();
                this.SelectTodo(nextTodo);
            }

        }

        /******************************************************************************************/

        // CONTEXTMENU OPENING
        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.topMostToolStripMenuItem.Checked = this.TopMost;

            normalToolStripMenuItem.Checked = showNormal;
            doneToolStripMenuItem.Checked = showDone;
            deletedToolStripMenuItem.Checked = showDeleted;

            autorunToolStripMenuItem.Checked = Autorun.IsCurrentAppInAutorun();
        }

        // CONTEXTMENU NEW FILE
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveFile();


            this.pathToFlowToDoFile = "";
            this.initTodoList();
        }

        // CONTEXTMENU FILE SAVE
        private void saveToolStripMenuItem_Click(object? sender, EventArgs? e)
        {
            this.textSaveToToDo();
            this.SaveFile();
            this.getTimeEvents();
        }

        // CONTEXTMENU FILE SAVE AS
        private void saveAsToolStripMenuItem_Click(object? sender, EventArgs? e)
        {
            this.textSaveToToDo();

            using (var dlg = new SaveFileDialog())
            {
                dlg.Title = "Save file as";
                dlg.Filter = "Text Files (*.FlowToDo)|*.FlowToDo";
                dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                dlg.FileName = "TodoList.FlowToDo";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.SaveAsFile(dlg.FileName);
                }
            }


        }

        // CONTEXTMENU FILE OPEN
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textSaveToToDo();

            if (!saved) // for some error maybe wrong path...
            {
                var result = MessageBox.Show(
                "Do you want to save changes?",
                "Unsaved changes",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    this.SaveFile();
                }
            }

            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Select a file";
                dlg.Filter = "Text Files (*.FlowToDo)|*.FlowToDo";
                dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (this.OpenFile(dlg.FileName))
                    {
                        this.pathToFlowToDoFile = dlg.FileName;
                    }
                }
            }
        }

        // CONTEXTMENU CLOSE
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // CONTEXTMENU TOPMOST
        private void topMostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
        }

        // CONTEXTMENU DEFAULT FONT
        private void defaultFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FontDialog fontDialog = new FontDialog())
            {
                fontDialog.Font = this.richTextBoxNote.Font;

                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    this.richTextBoxNote.Font = fontDialog.Font;
                }
            }
        }

        // CONTEXTMENU SHOW NORMAL
        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showNormal = !showNormal;
            this.updatePager();
        }

        // CONTEXTMENU SHOW DONE
        private void doneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showDone = !showDone;
            this.updatePager();
        }

        // CONTEXTMENU SHOW DELETED
        private void deletedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showDeleted = !showDeleted;
            this.updatePager();
        }

        // CONTEXTMENU TEXTBOX CUT SELECTION
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxNote.Cut();

        }

        // CONTEXTMENU TEXTBOX COPY SELECTION 
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxNote.Copy();
        }

        // CONTEXTMENU TEXTBOX PASTE SELECTION 
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxNote.Paste();
        }

        // CONTEXTMENU TEXTBOX COLOR OD SELECTION
        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var cd = new ColorDialog())
            {
                cd.Color = richTextBoxNote.SelectionColor.IsEmpty ? richTextBoxNote.ForeColor : richTextBoxNote.SelectionColor;
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    if (richTextBoxNote.SelectionLength > 0) richTextBoxNote.SelectionColor = cd.Color;
                    else richTextBoxNote.ForeColor = cd.Color;
                }
            }
        }

        // CONTEXTMENU TEXTBOX FONT OD SELECTION
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fd = new FontDialog())
            {
                fd.Font = richTextBoxNote.SelectionFont ?? richTextBoxNote.Font;
                if (fd.ShowDialog() == DialogResult.OK)
                {

                    if (richTextBoxNote.SelectionLength > 0)
                    {
                        richTextBoxNote.SelectionFont = fd.Font;
                    }
                    else
                    {
                        richTextBoxNote.Font = fd.Font;
                    }
                }
            }


        }

        // CONTEXTMENU AUTORUN SET
        private void autorunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Autorun.IsCurrentAppInAutorun())
            {
                Autorun.RemoveCurrentAppFromAutorun();
            }
            else
            {
                Autorun.AddCurrentAppToAutorun();
            }
        }

        // CONTEXTMENU OPTIONS SET COLOR TODO
        private void setTodoColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.data == null)
            {
                return;
            }

            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = Tools.StringToColor(this.data.todoColor, Color.Green);
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    this.data.todoColor = Tools.ColorToString(colorDialog.Color);
                    updatePager();
                    this.unsave();
                }
            }
        }

        // CONTEXTMENU OPTIONS SET COLOR DONE
        private void setDoneColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.data == null)
            {
                return;
            }

            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = Tools.StringToColor(this.data.doneColor, Color.Blue);
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    this.data.doneColor = Tools.ColorToString(colorDialog.Color);
                    updatePager();
                    this.unsave();
                }
            }
        }

        // CONTEXTMENU OPTIONS SET COLOR DELETED
        private void setDeleteColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.data == null)
            {
                return;
            }

            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = Tools.StringToColor(this.data.deletedColor, Color.Red);
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    this.data.deletedColor = Tools.ColorToString(colorDialog.Color);
                    updatePager();
                    this.unsave();
                }
            }
        }
        
        /******************************************************************************************/

        // TEXTBOX TECH CHANGE
        private void richTextBoxNote_TextChanged(object sender, EventArgs e)
        {
            if (saved && this.unmodifiedText != this.richTextBoxNote.Rtf)
            {
                this.unsave();
            }
        }

        // TEXTBOX MOUSE DOWN
        private void richTextBoxNote_MouseDown(object sender, MouseEventArgs e)
        {            
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                int index = richTextBoxNote.GetCharIndexFromPosition(e.Location);
                bool found = false;

                foreach (Match match in Regex.Matches(richTextBoxNote.Text, "(\"[a-zA-Z]:\\\\[^\"]+\")|(https?://\\S+)"))
                {
                    if (index >= match.Index && index <= match.Index + match.Length)
                    {
                        string link = match.Value.Trim('"');
                        try
                        {
                            Process.Start(new ProcessStartInfo(link) { UseShellExecute = true });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Cannot open link/path: {ex.Message}");
                        }
                        found = true;
                        break;
                    }
                }

                

                if (!found)
                foreach (Match match in Regex.Matches(richTextBoxNote.Text, "[@#][0-9]+"))
                {
                    if (index >= match.Index && index <= match.Index + match.Length)
                    {
                        found = true;
                        string link = match.Value.TrimStart('@').TrimStart('#').Replace("_", " ");
                        if (int.TryParse(link, out int number))
                        {
                            ToDo? selectedTodo = getTodoByNumber(number);
                            if (selectedTodo != null)
                            {
                                    this.textSaveToToDo();
                                    this.SelectTodo(selectedTodo);
                            }
                        }
                        break;
                    }
                }

                if (!found) 
                foreach (Match match in Regex.Matches(richTextBoxNote.Text, "[@#][A-Za-z0-9_]+"))
                {
                    if (index >= match.Index && index <= match.Index + match.Length)
                    {
                        found = true;
                        string link = match.Value.TrimStart('@').TrimStart('#').Replace("_", " ");
                        this.textSaveToToDo();
                        this.Search(link, new SearchItem(this.currentTodoPos(), match.Index));
                        break;
                    }
                }
            }
        }

        // TEXTBOX LINK CLICK
        private void richTextBoxNote_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            if (e.LinkText == null)
            {
                return;
            }

            string? link = e.LinkText.Trim('"'); // remove quotes if needed
            try
            {
                if (link != null)
                {
                    Process.Start(new ProcessStartInfo(link) { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot open link/path: {ex.Message}");
            }
        }

        // TEXTBOX HIGLIGHT
        private void HighlightFilePaths()
        {
            /*SuspendDrawing(richTextBoxNote);

            int selectionStart = richTextBoxNote.SelectionStart;
            int selectionLength = richTextBoxNote.SelectionLength;

            string pattern = "(\"[a-zA-Z]:\\\\[^\"]+\")|(https?://\\S+)";
            foreach (Match match in Regex.Matches(richTextBoxNote.Text, pattern))
            {
                richTextBoxNote.Select(match.Index, match.Length);
                richTextBoxNote.SelectionColor = Color.Blue;
                richTextBoxNote.SelectionFont = new Font(richTextBoxNote.Font, FontStyle.Underline);
            }

            richTextBoxNote.Select(richTextBoxNote.TextLength, 0);
            
            // Restore cursor position
            richTextBoxNote.SelectionStart = selectionStart;
            richTextBoxNote.SelectionLength = selectionLength;

            ResumeDrawing(richTextBoxNote);*/
        }

        // TEXTBOX SUSPEN DRAWING
        private void SuspendDrawing(Control ctrl)
        {
            SendMessage(ctrl.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
        }

        // TEXTBOX SUSPEN RESUME
        private void ResumeDrawing(Control ctrl)
        {
            SendMessage(ctrl.Handle, WM_SETREDRAW, new IntPtr(1), IntPtr.Zero);
            ctrl.Invalidate();
        }

        // TEXTBOX SUSPEN RESUME
        private void textBoxPosition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (int.TryParse(textBoxPosition.Text.Trim(), out int number))
                {
                    ToDo? selectedTodo = getTodoByNumber(number);
                    if (selectedTodo != null)
                    {
                        this.SelectTodo(selectedTodo);
                    }
                }
            }

            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                this.updatePager();
            }

        }

        // TEXTBOX GO TO POSITION
        private void GoToPosition(SearchItem searchItem)
        {
            if (this.data == null)
            {
                return;
            }

            if (this.data.currentTodo != this.data.todoList[searchItem.todoListPos])
            {
                this.textSaveToToDo();
                this.SelectTodo(this.data.todoList[searchItem.todoListPos]);
            }

            richTextBoxNote.SelectionStart = searchItem.posInTodo;
            richTextBoxNote.SelectionLength = 0;
            richTextBoxNote.ScrollToCaret();
        }

        // TEXTBOX KEY DOWN
        private void richTextBoxNote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.D)
            {
                e.SuppressKeyPress = true;
                string date = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]";
                int pos = this.richTextBoxNote.SelectionStart;
                this.richTextBoxNote.Text = this.richTextBoxNote.Text.Insert(pos, date);
                this.richTextBoxNote.SelectionStart = pos + date.Length;
            }

            if (e.Control && e.KeyCode == Keys.T)
            {
                e.SuppressKeyPress = true;
                string date = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
                int pos = this.richTextBoxNote.SelectionStart;
                this.richTextBoxNote.Text = this.richTextBoxNote.Text.Insert(pos, date);
                this.richTextBoxNote.SelectionStart = pos + date.Length;
            }

            if (e.Control && e.Shift && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true;
                if (Clipboard.ContainsText())
                    richTextBoxNote.SelectedText = Clipboard.GetText(TextDataFormat.Text);
            }

            if (e.Control && e.KeyCode == Keys.R)
            {
                e.SuppressKeyPress = true;
                richTextBoxNote.SelectionFont = richTextBoxNote.Font;
                richTextBoxNote.SelectionColor = Color.Black;
            }

           
        }
        
        /******************************************************************************************/

        // SEARCHBAR SHOW
        private void SearchBarShow()
        {
            if (textBoxSearch.Visible) {
                return;
            }

            textBoxSearch.Visible = true;
            buttonSearchLeft.Visible = true;
            buttonSearchRight.Visible = true;
            textBoxSearch.Focus();
        }

        // SEARCHBAR HIDE
        private void SearchBarHide()
        {
            if (!textBoxSearch.Visible)
            {
                return;
            }

            textBoxSearch.Visible = false;
            buttonSearchLeft.Visible = false;
            buttonSearchRight.Visible = false;
            richTextBoxNote.Focus();
        }

        // SEARCH WITHOUT SEARCH BAR
        private void Search(string searchFor, SearchItem? skipSearchItem = null)
        {
            string? text = "";
            int index = 0;
            SearchItem? searchItem = null;
            int textIndex = -1;

            for (int i = 0; i < this.data.todoList.Count(); i++)
            {
                if ((this.data.todoList[i].deleted && !showDeleted) || (this.data.todoList[i].done && !showDone))
                {
                    continue;
                }

                text = this.data.todoList[i].rawText;

                if (text == null)
                {
                    continue;
                }

                textIndex = text.IndexOf(searchFor, index, StringComparison.OrdinalIgnoreCase);
                if (textIndex != -1 && (skipSearchItem != null && skipSearchItem.todoListPos != i && skipSearchItem.posInTodo != textIndex)) {
                    searchItem = new SearchItem(i, textIndex);
                    break;
                }
                                
            }

            if (searchItem != null)
            {
                this.GoToPosition(searchItem);
            }
        }

        private void SearchFirst(string searchFor)
        {
            string? text = "";
            int index = 0;

            this.data.search.Clear();

            for (int i = 0; i < this.data.todoList.Count(); i++)
            {
                if ((this.data.todoList[i].deleted && !showDeleted) || (this.data.todoList[i].done && !showDone))
                {
                    continue;
                }

                text = this.data.todoList[i].rawText;

                if (text == null)
                {
                    continue;
                }

                index = 0;
                while (index < text.Length)
                {
                    int foundIndex = text.IndexOf(searchFor, index, StringComparison.OrdinalIgnoreCase);
                    if (foundIndex == -1)
                        break;

                    this.data.search.Add(new SearchItem(i, foundIndex));
                    index = foundIndex + searchFor.Length;
                }
            }

            if (this.data.search.Count() > 0)
            {
                this.data.searchIndex = 0;
                this.GoToPosition(this.data.search[this.data.searchIndex]);
            }
        }

        // SEARCHBAR TEXT CHANGE
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchFor = textBoxSearch.Text;

            if (this.data == null || searchFor.Length <= 2)
            {
                this.data.search.Clear();
            }
            else {
                this.SearchFirst(searchFor);
            }
                
        }

        // SEARCHBAR NEXT
        private void buttonSearchRight_Click(object? sender, EventArgs? e)
        {
            if (this.data == null)
            {
                return;
            }

            if (this.data.search.Count() > 0)
            {
                this.data.searchIndex++;
                if (this.data.searchIndex >= this.data.search.Count())
                {
                    this.data.searchIndex = 0;
                }

                this.GoToPosition(this.data.search[this.data.searchIndex]);

            }
        }

        // SEARCHBAR PREV
        private void buttonSearchLeft_Click(object? sender, EventArgs? e)
        {
            if (this.data == null)
            {
                return;
            }

            if (this.data.search.Count() > 0)
            {
                this.data.searchIndex--;
                if (this.data.searchIndex < 0)
                {
                    this.data.searchIndex = this.data.search.Count() - 1;
                }

                this.GoToPosition(this.data.search[this.data.searchIndex]);

            }
        }

        // SEARCHBAR KEY DOWN
        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                this.SearchBarHide();
            }

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.buttonSearchRight_Click(null, null);
            }

            if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                this.buttonSearchLeft_Click(null, null);
            }

            if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                this.buttonSearchRight_Click(null, null);
            }
        }

        /******************************************************************************************/

        public void getTimeEvents() { 
            this.data.timeEvents.Clear();

            var rx = new Regex(@"\[(\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2})(?:\s+(.*?))?\]");

            foreach (ToDo todo in this.data.todoList) {

                if (todo.rawText == null) {
                    continue;
                }

                foreach (Match m in rx.Matches(todo.rawText))
                {
                    if (DateTime.TryParseExact(m.Groups[1].Value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var dt)) {
                        TimeEvent timeEvent = new TimeEvent();
                        timeEvent.timeString = m.Groups[1].Value;
                        timeEvent.name = m.Groups[2].Value;
                        timeEvent.time = dt;
                        this.data.timeEvents.Add(timeEvent); 
                    }
                }
            }

            var sorted = this.data.timeEvents.OrderBy(d => d.time).ToList();
            var now = DateTime.Now;
            this.data.nextEvent = sorted.FirstOrDefault(d => d.time > now);
        }

        /******************************************************************************************/

        // SPLIT CODE TO SORTEn AND UNSORTED PART
        public void separator() { }

    }

    // RITCHTEXT HELPER
    static class RichTextBoxExtensions
    {
        public static void SetSelectionLink(this RichTextBox rtb, bool link)
        {
            rtb.SelectionFont = new System.Drawing.Font(rtb.SelectionFont ?? rtb.Font,
                rtb.SelectionFont?.Style | System.Drawing.FontStyle.Underline ?? System.Drawing.FontStyle.Underline);
            rtb.SelectionColor = link ? System.Drawing.Color.Blue : rtb.ForeColor;
        }
    }
}
