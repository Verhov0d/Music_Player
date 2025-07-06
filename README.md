
# 📝 Functional Analysis of the WPF Text Editor (`MainWindow.xaml`)

## 🎯 Purpose
This program is a basic **WPF-based text editor**, designed to provide users with essential tools for working with text documents in a graphical interface. It mimics common functionality of programs like Notepad but with extended features typical for desktop applications.

## 🧩 Implemented Features

### 1. Text Input Area
- **Element:** `<RichTextBox Name="richTextBox">`
- Provides a rich-text editor for users to write, format, and edit content.
- Supports multiple lines, text formatting (bold, italics, underline), font changes, etc.

### 2. Menu System (Menu Bar)
Contains the main controls organized in a standard GUI layout:

#### 📁 File Menu
- **New (Создать):** Clears current content.
- **Open (Открыть):** Opens `.txt` or `.rtf` files from the disk.
- **Save (Сохранить):** Saves current content to disk.
- **Exit (Выход):** Closes the application.

#### 🖋️ Edit Menu
- **Cut (Вырезать)**
- **Copy (Копировать)**
- **Paste (Вставить)**
- **Select All (Выделить всё)**

These operations are connected to the clipboard and the `RichTextBox` selection.

#### 🧱 Format Menu
- **Font Selection (Шрифт):** Allows users to select different fonts and styles.
- **Font Size (Размер):** Likely provides a set of predefined or adjustable font sizes.
- **Bold / Italic / Underline:** Standard text emphasis options.
- **Text Color (Цвет текста):** Allows changing the font color.

#### 🧾 Paragraph Menu
- **Text Alignment (Выравнивание):** Left, center, right, or justify text.
- **Indentation / Line Spacing (Отступы / Межстрочный интервал):** Optional if implemented.

### 3. Toolbar (if defined in .xaml)
There may be quick-access buttons to execute commands like New, Open, Save, Bold, etc. These are usually implemented using `<ToolBar>` controls.

### 4. Dialogs
- **OpenFileDialog / SaveFileDialog:** For interacting with the filesystem.
- **FontDialog / ColorDialog:** For font and color customization.

## ⚙️ Technologies Used
- **Language:** C# (.NET)
- **Framework:** WPF (Windows Presentation Foundation)
- **Markup:** XAML (Extensible Application Markup Language)
- **UI Elements:** `RichTextBox`, `Menu`, `MenuItem`, `OpenFileDialog`, `FontDialog`, etc.

## 📦 Summary of Functionality

| Category     | Features                                                                 |
|--------------|--------------------------------------------------------------------------|
| File Control | New, Open, Save, Exit                                                    |
| Editing      | Cut, Copy, Paste, Select All                                             |
| Formatting   | Font selection, Bold, Italic, Underline, Color                           |
| Paragraph    | Alignment (left, center, right), spacing options (if implemented)        |
| UI/UX        | Menu-driven interface with standard Windows dialogs                      |
