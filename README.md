# 🎮 Unity UI Boost Panel — Interactive UI Demo

> **Unity Version:** `2021.3.45f1`

---

## 🎯 Task Goal

Integration and optimization of art assets, implementation of an interactive **UI panel**, animations, and scripts according to the described mechanics.

---

## 🧩 UI Panel Structure

- 📌 **Header & Description**
- 🚀 **Three clickable boost icons** available for selection
- 🔄 **Reset Button** (refreshes boost icons)
- ✅ **OK Button** (confirms selected boost)
- 📂 **Side Panel** with an interactive arrow (clicking the arrow slides the panel in/out of the screen)

---

## ⚙️ Mechanics

1️⃣ **Initial State:**  
The window displays three boost icons with a header and description.

2️⃣ **Reset Button:**  
Pressing **Reset** refreshes the three boost icons:
- Two icons must always change (guaranteed to be different from the previous set).
- One icon can randomly repeat.

**Example of random generations:**  
- bomb, green flask, hourglass  
- pink flask, bomb, shield  
- shield, hourglass, blue flask  
- hourglass, green flask, bomb  
- pink flask, blue flask, shield

3️⃣ **Selecting a Boost:**  
When the player clicks a boost icon:
- The icon is **highlighted**.
- The **OK Button** appears for confirmation.

4️⃣ **Reset After Selection:**  
If the player clicks **Reset** again:
- The **OK Button** disappears.
- Highlight is removed.
- New boost icons are generated.

5️⃣ **Confirming a Boost:**  
When the **OK Button** is clicked:
- The **selected boost** stays; the other two disappear.
- The selected boost **animates to the center**, gets a checkmark, and plays effects.
- Then, the boost **flies** from the center to the first available slot in the **side panel**.
- If the side panel is hidden off-screen, it slides out automatically.
- The UI panel fades out, the side panel slides back out of view.

---

## 🎞️ Animations & Tech

- 📌 Uses **DoTween** for all animations.
- 📱 Animations respect **Safe Area** for all major mobile devices.
- 🧱 Fully **extensible** via **Prefabs** and **EventSystems**.
- 📐 Uses **Canvas Layout** components for easy UI structure.

---

## 🚀 Tech Stack

- ✅ Unity `2021.3.45f1`
- ✅ DoTween for smooth and customizable animations
- ✅ Prefabs for reusable UI elements
- ✅ EventSystem for clean input handling
- ✅ Canvas Layouts for responsive design

---

## 💡 Notes

This project demonstrates how to create an **interactive boost selection UI** with fluid animations, safe mobile scaling, and a flexible structure that can easily be extended for production use.

---

### 📌 Demo Ready to Rock!

Watch the demonstration demo on YouTube:  
📺 [Demonstration Video](https://www.youtube.com/watch?v=fyFiQObXt_U)

---

**Happy boosting! 🚀**
