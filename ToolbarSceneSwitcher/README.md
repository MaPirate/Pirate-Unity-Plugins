# Unity Toolbar Scene Switcher 🎬

A lightweight, native-looking editor extension for Unity that adds a **Scene Selection Dropdown** to the main toolbar.  
Designed for **Unity 6** and **2021+**, it fits perfectly into the UI without cluttering the Play/Pause buttons.

![License](https://img.shields.io/badge/license-MIT-green)
![Unity](https://img.shields.io/badge/Unity-2021%2B%20%7C%206.0-blue)

## ✨ Features

* **📍 Optimal Placement:** Located at the far right of the *Left Toolbar Zone*, keeping your Play/Pause buttons centered and uncluttered.
* **🎨 Native Look & Feel:** Uses `IMGUIContainer` to perfectly match Unity's Dark Theme (Grey style).
* **⚡ Fast Switching:** Lists all enabled scenes from your *Build Settings*.
* **💾 Smart Save:** Automatically asks to save changes before switching scenes to prevent data loss.
* **📂 Organized:** Displays a clean dropdown list with the current scene name always visible.

## 📸 Screenshots

![Toolbar Preview](screenshot.png)

## 🚀 Installation

### Option 1: Unity Package (Recommended)
1. Download the latest `.unitypackage` from the [Releases](../../releases) section.
2. Open your Unity project.
3. Double-click the package to import it.

### Option 2: Manual Installation
1. Navigate to your project's `Assets` folder.
2. Create a folder named `Editor`.
3. Copy the `ToolbarSceneSwitcher.cs` script into this folder.

## 🛠 How to Use

1. Go to **File > Build Settings**.
2. Add the scenes you want to work with to the "Scenes In Build" list.
3. The dropdown in the main toolbar will automatically update.
4. Click the button to switch scenes instantly!

## ⚙️ Compatibility

* ✅ **Unity 6 (Recommended)**
* ✅ Unity 2022.x
* ✅ Unity 2021.x
* Requires `Unity.UI` module (standard in all projects).

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---
*Made with ❤️ for efficient Game Development.*
