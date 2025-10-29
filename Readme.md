# FlowToDo

**FlowToDo** is a lightweight, focused to-do list application designed to help you stay in flow while managing multiple tasks.  
It organizes tasks as a **stack** — new to-do lists are added **right after the currently active one**, making it easier to focus on what’s next.

---

## ✨ Features

- 🗒️ **Stack-based workflow** – new to-dos are always added after the current one  
- 🟢 **Color-coded states**:
  - **Green** – active tasks (to be done)  
  - **Gray background** – completed tasks  
  - **Orange** – deleted tasks  
- ❌ **Delete button (X)** – removes a to-do  
- ✅ **Done button (Y)** – marks a to-do as completed  
- ➕ **Add button (+)** – creates a new to-do list  
- 🧭 **Navigation buttons**:
  - `<<` – jump to the first to-do  
  - `>>` – jump to the last to-do  
  - `<`  – move to previous to-do  
  - `>`  – move to next to-do  
- 📝 **RTF-based text editor** – supports **font and color styling**  
- 💾 **Custom file format** – save and open files with the `.FlowToDo` extension  
- 📂 **Drag & Drop support** – open `.FlowToDo` files by dropping them onto the form  
- 🪄 **Automatic cleanup** – empty to-dos are deleted automatically on save  
- ⚙️ **Context menu options**:
  - Toggle **visibility** of completed and deleted to-dos  
  - Set **“Always on top”** window mode  
  - Choose **default font** for new to-dos  

---

## 🖥️ How to Use

1. Create a new task list using the **+** button.  
2. Type and style your notes in the **RTF editor** (change font, size, or color).  
3. Use **Y** to mark tasks as done or **X** to delete them.  
4. Navigate between tasks using **<**, **>**, **<<**, **>>**.  
5. Save your work as a `.FlowToDo` file — double-click or drag it back into the app to reopen.

---

## 🧩 File Format

FlowToDo uses its own extension:

```
*.FlowToDo
```

Each file stores your styled text and the state of each task (active, done, deleted).

---

## ⚙️ Settings

Accessible through the context menu:
- **Always on top** – keeps the app above other windows  
- **Default font** – sets your preferred style for all new to-dos  
- **Visibility toggles** – show or hide completed/deleted tasks  

---

## 📸 Example

```
[+] Create new task
[Y] Review code
[X] Delete old notes
```

---

## 🧠 Philosophy

FlowToDo is built around the **“flow” mindset** — one active task at a time.  
No clutter, no overwhelm — just smooth task transitions.

---

## 📦 License

MIT License  
Feel free to use, modify, and share.
