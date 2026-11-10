# 💀 Bone To Be Wild

<p align="center">
  <em>Every hero has to start somewhere. Some just start with fewer bones.</em>
</p>

<p align="center">
  A medieval fantasy action RPG where you play as a weak skeleton trying to prove that even the lowliest undead can become a legendary adventurer.
</p>

---



<div align="left">
 

  <h2>⚔️ The Adventure Begins</h2>
   <img src="https://github.com/user-attachments/assets/63522a0a-f2e8-477e-9446-10c78dcbb45a"
       width="48%"
       align="right"
       alt="Bone To Be Wild gameplay">

  In a world filled with powerful adventurers stealing all the glory, you're just a weak skeleton trying to make a name for yourself.

  Fight your way through a medieval fantasy world, cast spells, master different weapons, and interact with the characters you meet along the way.

  Whether through combat or conversation, every encounter is a chance to prove your worth.

  <strong>Your goal:</strong> Rise through the ranks and reach the top of the adventure world.

  
</div>
<div align="center">
  <em>You may be just a pile of bones, but you've got ambition.</em>
</div>





## Features

### Branching Dialogue
- Dialogue system powered by [Ink](https://www.inklestudios.com/ink/).
- Branching conversations that allow interactions to develop in different directions.

### Modular Game Architecture
- Abstraction and interfaces for different NPC types, including civilians, guards, and enemies.
- Modular Quest and Interaction systems.
- Reusable components designed to make extending gameplay systems easier.

### Enemy AI & Combat
- State Machine architecture for enemy combat behaviour.
- AI states include:
  - Idle
  - Chase
  - Attack
  - Hit
  - Death


<p align="center">
  <img src="https://github.com/user-attachments/assets/0c6c4ab3-abe4-4edf-b589-1d574d1700a6"
       width="48%"
       alt="Modular Ability System - Image 1">
  <img src="https://github.com/user-attachments/assets/e637507d-f5a8-40f5-a61d-a2487a7165ea"
       width="48%"
       alt="Modular Ability System - Image 2">
</p>



### Flexible Weapon System
- Strategy Pattern used to handle different weapon behaviours.
- Supports different weapon types, such as Normal Sword and Ice Sword, without tightly coupling weapon logic to the player.

### Interface-Based Damage System
- Attackable entities use specialized interfaces:
  - `ISpellDamageable`
  - `ISwordDamageable`
- Allows different attack types to interact with entities through clearly defined contracts.

### Normal Sword / Normal Damage

<p align="center">
  <img src="https://github.com/user-attachments/assets/214f8c2d-1253-4757-a464-deab8c125cdf" width="48%" alt="Normal Sword">
  <img src="https://github.com/user-attachments/assets/351cff9a-f049-46dc-8ca5-66b63eb34664" width="48%" alt="Normal Damage">
</p>

### Ice Sword / Ice Damage

<p align="center">
  <img src="https://github.com/user-attachments/assets/01ab0226-0821-4e3c-a0c3-82a90e882231" width="48%" alt="Ice Sword">
  <img src="https://github.com/user-attachments/assets/dfe47c1a-f388-498e-b91a-fa9c204c7d01" width="48%" alt="Ice Damage">
</p>

---

## Technical Highlights

This project explores several software design principles and patterns applied to Unity game development.

| Pattern / Principle | Implementation |
|---|---|
| **State Machine** | Enemy AI and combat states |
| **Strategy Pattern** | Different weapon behaviours |
| **Abstraction & Interfaces** | Modular NPCs, interactions, and damage handling |
| **Inheritance** | Extensible ability system |
| **Ink Integration** | Branching dialogue and narrative |

---


https://github.com/user-attachments/assets/88eeb4c1-f7e8-48b2-a617-e5c84caaaa18

https://github.com/user-attachments/assets/77c85bd0-406b-4f79-a5d3-c11a3234b574




<p align="center">
  **No fame. No fortune. Just bones and determination.**
</p>

<p align="center">
  <em>Bone To Be Wild — An underdog adventure worth fighting for.</em>
</p>
