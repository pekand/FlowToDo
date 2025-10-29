using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml.Serialization;

namespace FlowToDo
{
    public partial class FormFlowToDo : Form
    {
        public Data data = new Data();

        public string pathToFlowToDoFile = "";
        private bool inicialized = false;

        private bool showNormal = true;
        private bool showDone = false;
        private bool showDeleted = false;

        public Color TodoColor = Color.FromArgb(128, 255, 128);
        public Color DoneColor = Color.FromArgb(222, 222, 222);
        public Color DeletedColor = Color.FromArgb(255, 224, 108);

        public bool saved = true;
        public bool suspenUnsave = false;
        public DateTime unSavedAt = DateTime.Now;
        public string unmodifiedText = "";
        public Font defaultRitchTextFont = null;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        private const int WM_SETREDRAW = 0x0B;

        // CONSTRUCTOR
        public FormFlowToDo(string pathToFlowToDoFile)
        {
            this.DoubleBuffered = true;

            this.pathToFlowToDoFile = pathToFlowToDoFile;

            InitializeComponent();

            defaultRitchTextFont = richTextBoxNote.Font;

            if (this.pathToFlowToDoFile == "" || !File.Exists(pathToFlowToDoFile))
            {
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string appDir = Path.Combine(appDataPath, Program.appName);
                Directory.CreateDirectory(appDir);
                this.pathToFlowToDoFile = Path.Combine(appDir, Program.mainConfigFile);
                this.OpenFile(this.pathToFlowToDoFile);
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
        public void OpenFile(string filePath)
        {
            inicialized = false;
            if (filePath != "" && File.Exists(filePath))
            {
                try
                {

                    var serializer = new XmlSerializer(typeof(Data));
                    string xml = File.ReadAllText(filePath);
                    using var sr = new StringReader(xml);
                    this.data = (Data)serializer.Deserialize(sr);
                    this.saved = true;
                    this.pathToFlowToDoFile = filePath;

                    this.richTextBoxNote.Font = Tools.StringToFont(data.defaultFont);

                    if (data.currentTodoPos < data.todoList.Count())
                    {
                        data.currentTodo = data.todoList[data.currentTodoPos];
                    }
                    else
                    {
                        data.currentTodo = data.todoList[0];
                    }

                    data.TodoCount = data.todoList.Count();
                    SelectTodo(this.data.currentTodo);

                    data.TopMost = this.TopMost;

                    this.WindowState = FormWindowState.Normal;
                    this.Left = data.winX;
                    this.Top = data.winY;
                    this.Height = data.winH;
                    this.Width = data.winW;
                }
                catch (Exception)
                {

                }

            }
            inicialized = true;
        }

        // FILE SAVE
        public void SaveFile()
        {
            this.textSaveToToDo();

            removeEmptyTodoList();

            if (this.pathToFlowToDoFile == "") {
                this.saveAsToolStripMenuItem_Click(null, null);
                return;
            }

            try
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    data.winX = this.Left;
                    data.winY = this.Top;
                    data.winH = this.Height;
                    data.winW = this.Width;
                }
                if (this.WindowState == FormWindowState.Maximized) data.winStatus = 2;
                if (this.WindowState == FormWindowState.Minimized) data.winStatus = 1;
                if (this.WindowState == FormWindowState.Normal) data.winStatus = 0;
                data.TopMost = this.TopMost;
                data.defaultFont = Tools.FontToString(this.richTextBoxNote.Font);

                var serializer = new XmlSerializer(typeof(Data));
                using var sw = new StringWriter();
                serializer.Serialize(sw, data);
                string xml = sw.ToString();
                File.WriteAllText(this.pathToFlowToDoFile, xml);
                this.saved = true;
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
            this.SaveFile();
        }

        // EVENT FORM LOAD
        private void FormFlowToDo_Load(object sender, EventArgs e)
        {

        }

        // EVENT FORM RESIZE
        private void FormFlowToDo_Resize(object sender, EventArgs e)
        {
            if (inicialized && this.WindowState == FormWindowState.Normal)
            {
                data.winX = this.Left;
                data.winY = this.Top;
                data.winH = this.Height;
                data.winW = this.Width;
            }
        }

        // EVENT SHORTCUTS
        private void FormFlowToDo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                e.SuppressKeyPress = true;
                this.SaveFile();
            }

            if (e.Control && e.Shift && e.KeyCode == Keys.S)
            {
                e.SuppressKeyPress = true;
                this.saveAsToolStripMenuItem_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.O)
            {
                e.SuppressKeyPress = true;
                this.openToolStripMenuItem_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.N)
            {
                e.SuppressKeyPress = true;
                this.buttonAdd_Click(sender, e);
            }

            if (e.KeyCode == Keys.F5)
            {
                e.SuppressKeyPress = true;
                this.buttonAdd_Click(sender, e);
            }

            if (e.KeyCode == Keys.F8)
            {
                e.SuppressKeyPress = true;
                this.buttonDelete_Click(sender, e);
            }

            if (e.KeyCode == Keys.F9)
            {
                e.SuppressKeyPress = true;
                this.buttonDone_Click(sender, e);
            }

            if (e.KeyCode == Keys.F7)
            {
                e.SuppressKeyPress = true;
                this.buttonRight_Click(sender, e);
            }

            if (e.KeyCode == Keys.F6)
            {
                e.SuppressKeyPress = true;
                this.buttonLeft_Click(sender, e);
            }

        }

        /******************************************************************************************/

        // TIMER TICK
        private void timer_Tick(object sender, EventArgs e)
        {
            if (!saved && DateTime.Now - unSavedAt > TimeSpan.FromMinutes(1))
            {
                this.SaveFile();
            }
        }

        /******************************************************************************************/

        // RESET TO DEFAULT EMPTY STATE
        public void initTodoList()
        {
            this.data.todoList.Clear();
            this.data.TodoCount = 1;
            this.data.currentTodoPos = 0;            
            this.data.currentTodo = new ToDo(); 
            this.data.todoList.Add(this.data.currentTodo);
          
            this.saved = false;
            this.suspenUnsave = false;
            this.unSavedAt = DateTime.Now;
            richTextBoxNote.Font = defaultRitchTextFont;
            this.unmodifiedText = "";
            this.richTextBoxNote.Text = this.data.currentTodo.text;            

            this.showNormal = true;
            this.showDone = false;
            this.showDeleted = false;

            inicialized = true;

            updatePager();
        }

        // REMOVE EMPTY CLEANING
        public void removeEmptyTodoList()
        {

            this.data.todoList.RemoveAll(x => x.isEmpty && x != this.data.currentTodo);


            if (this.data.todoList.Count() == 0)
            {
                this.initTodoList();
            }
            else
            {
                if (this.data.todoList.Contains(this.data.currentTodo))
                {
                    this.data.currentTodoPos = this.data.todoList.IndexOf(this.data.currentTodo);
                }
                else
                {
                    this.data.currentTodo = null;
                }
            }

            updatePager();
        }

        public int CountTodos()
        {
            int count = 0;
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

        public int GetPosOfTodos(ToDo todo)
        {
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
            if (this.data.currentTodo != null)
            {
                if (this.unmodifiedText != this.richTextBoxNote.Rtf)
                {
                    this.unsave();
                }

                this.data.currentTodo.text = this.richTextBoxNote.Rtf;
                this.data.currentTodo.isEmpty = this.richTextBoxNote.Text.Trim() == "";
            }
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
        }

        // PAGER UPDATE
        public void updatePager()
        {
            this.textBoxPosition.Text = (this.GetPosOfTodos(this.data.currentTodo) + 1) + "/" + this.CountTodos();

            if (this.data.currentTodo != null)
            {
                if (this.data.currentTodo.deleted)
                {
                    this.BackColor = this.DeletedColor;
                    this.richTextBoxNote.BackColor = this.DeletedColor;
                }
                else if (this.data.currentTodo.done)
                {
                    this.BackColor = this.DoneColor;
                    this.richTextBoxNote.BackColor = this.DoneColor;
                }
                else
                {
                    this.BackColor = this.TodoColor;
                    this.richTextBoxNote.BackColor = this.TodoColor;
                }
            }

            HighlightFilePaths();
        }

        // TOOL GET PREV TODO
        public ToDo GetPrevTodo()
        {


            ToDo prevTodo = null;

            for (int i = this.data.currentTodoPos - 1; i >= 0; i--)
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
        public ToDo GetNextTodo()
        {
            ToDo nextTodo = null;

            for (int i = this.data.currentTodoPos + 1; i < this.data.todoList.Count(); i++)
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
        public ToDo GetFirstTodo()
        {


            ToDo prevTodo = null;

            for (int i = this.data.currentTodoPos - 1; i >= 0; i--)
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
        public ToDo GetLastTodo()
        {
            ToDo nextTodo = null;

            for (int i = this.data.currentTodoPos + 1; i < this.data.todoList.Count(); i++)
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
        public void SelectTodo(ToDo todo)
        {
            suspenUnsave = true;
            if (todo != null)
            {
                int position = this.data.todoList.IndexOf(todo);
                if (position > -1)
                {
                    this.data.currentTodo = todo;
                    this.data.currentTodoPos = position;
                    if (this.data.currentTodo.text.TrimStart().StartsWith(@"{\rtf", StringComparison.OrdinalIgnoreCase))
                    {
                        try
                        {
                            this.richTextBoxNote.Rtf = this.data.currentTodo.text;
                            this.unmodifiedText = this.richTextBoxNote.Rtf;
                        }
                        catch
                        {
                            this.richTextBoxNote.Text = this.data.currentTodo.text;
                            this.unmodifiedText = this.richTextBoxNote.Rtf;
                        }
                    }
                    else
                    {
                        this.richTextBoxNote.Text = this.data.currentTodo.text;
                        this.unmodifiedText = this.richTextBoxNote.Rtf;
                    }

                }
            }

            suspenUnsave = false;

            updatePager();
        }

        // BUTTON  +
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            this.textSaveToToDo();

            ToDo todo = new ToDo();
            this.data.currentTodo.createdAt = Tools.Timestamp();
            this.data.currentTodo = todo;
            this.data.currentTodoPos++;
            this.data.todoList.Insert(this.data.currentTodoPos, todo);
            this.data.TodoCount++;
            this.richTextBoxNote.Rtf = this.data.currentTodo.text;
            this.unsave();
            updatePager();
        }

        // BUTTON DELETE
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            this.textSaveToToDo();


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

                ToDo nextTodo = GetNextTodo();
                if (nextTodo == null)
                {
                    nextTodo = GetPrevTodo();
                }
                this.SelectTodo(nextTodo);

            }
        }

        // BUTTON DONE
        private void buttonDone_Click(object sender, EventArgs e)
        {
            this.textSaveToToDo();

            if (!this.data.currentTodo.deleted)
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

                    ToDo nextTodo = GetNextTodo();
                    if (nextTodo == null)
                    {
                        nextTodo = GetPrevTodo();
                    }
                    this.SelectTodo(nextTodo);
                }
            }
        }

        // BUTTON >
        private void buttonRight_Click(object sender, EventArgs e)
        {
            this.textSaveToToDo();
            ToDo nextTodo = GetNextTodo();
            this.SelectTodo(nextTodo);
        }

        // BUTTON <
        private void buttonLeft_Click(object sender, EventArgs e)
        {
            this.textSaveToToDo();

            ToDo prevTodo = GetPrevTodo();
            this.SelectTodo(prevTodo);
        }

        // BUTTON >>
        private void buttonSkipRight_Click(object sender, EventArgs e)
        {
            this.textSaveToToDo();
            ToDo nextTodo = GetLastTodo();
            this.SelectTodo(nextTodo);
        }

        // BUTTON <<
        private void buttonSkipLeft_Click(object sender, EventArgs e)
        {
            this.textSaveToToDo();
            ToDo nextTodo = GetFirstTodo();
            this.SelectTodo(nextTodo);
        }

        /******************************************************************************************/

        // CONTEXTMENU OPENING
        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.topMostToolStripMenuItem.Checked = this.TopMost;

            normalToolStripMenuItem.Checked = showNormal;
            doneToolStripMenuItem.Checked = showDone;
            deletedToolStripMenuItem.Checked = showDeleted;
        }

        // CONTEXTMENU FILE SAVE
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textSaveToToDo();
            this.SaveFile();
        }

        // CONTEXTMENU FILE SAVE AS
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
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

            this.SaveFile();

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
                    this.OpenFile(dlg.FileName);
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
                    if (richTextBoxNote.SelectionLength > 0) richTextBoxNote.SelectionFont = fd.Font;
                    else richTextBoxNote.Font = fd.Font;
                }
            }


        }

        /******************************************************************************************/

        // TEXTBOX TECH CHANGE
        private void richTextBoxNote_TextChanged(object sender, EventArgs e)
        {
            // Save cursor position
            int selectionStart = richTextBoxNote.SelectionStart;
            int selectionLength = richTextBoxNote.SelectionLength;

            HighlightFilePaths();

            // Restore cursor position
            richTextBoxNote.SelectionStart = selectionStart;
            richTextBoxNote.SelectionLength = selectionLength;

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
                        break;
                    }
                }
            }
        }

        // TEXTBOX LINK CLICK
        private void richTextBoxNote_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            string link = e.LinkText.Trim('"'); // remove quotes if needed
            try
            {
                Process.Start(new ProcessStartInfo(link) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot open link/path: {ex.Message}");
            }
        }

        // TEXTBOX HIGLIGHT
        private void HighlightFilePaths()
        {
            SuspendDrawing(richTextBoxNote);
            string pattern = "(\"[a-zA-Z]:\\\\[^\"]+\")|(https?://\\S+)";
            foreach (Match match in Regex.Matches(richTextBoxNote.Text, pattern))
            {
                richTextBoxNote.Select(match.Index, match.Length);
                richTextBoxNote.SelectionColor = Color.Blue;
                richTextBoxNote.SelectionFont = new Font(richTextBoxNote.Font, FontStyle.Underline);
            }

            richTextBoxNote.Select(richTextBoxNote.TextLength, 0);
            ResumeDrawing(richTextBoxNote);
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


        /******************************************************************************************/
        public void separator() { }


        public ToDo selectTodoByNumber(int pos)
        {
            ToDo selectedTodo = null;
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
        private void textBoxPosition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (int.TryParse(textBoxPosition.Text.Trim(), out int number))
                {
                    ToDo selectedTodo = selectTodoByNumber(number);
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

        private void FormFlowToDo_DragEnter(object sender, DragEventArgs e)
        {
            if (!inicialized) {
                return;
            }

            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void FormFlowToDo_DragDrop(object sender, DragEventArgs e)
        {
            if (!inicialized)
            {
                return;
            }

            if (e.Data == null || !e.Data.GetDataPresent(DataFormats.FileDrop))
                return;

            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files == null || files.Length == 0 || files.Length > 1)
                return;

            foreach (var f in files)
            {
                try
                {
                    var ext = Path.GetExtension(f);
                    if (ext == ".FlowToDo")
                    {
                        this.OpenFile(f);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        // CONTEXTMENU NEW FILE
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveFile();


            this.pathToFlowToDoFile = "";
            this.initTodoList();
        }
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
