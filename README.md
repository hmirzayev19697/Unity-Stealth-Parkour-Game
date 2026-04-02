# 🎮 Parkour Game 

A simple 3D parkour game built in Unity where the player navigates across platforms while avoiding drones with vision-based detection.

## 🚀 About the Project

This project was developed as part of a game development course. The focus is on combining physics-based movement with basic AI behavior and realistic detection using raycasting.

The goal is simple:
- Jump across platforms
- Avoid being detected
- Reach the finish point

---

## 🧩 Features

### 🧍 Player
- Rigidbody-based movement
- Jump using physics (`ForceMode.Impulse`)
- Smooth directional control

### 🧱 Environment
- 8+ platforms
- Multiple 3D models (non-primitive)
- Parkour-style level design

### 🤖 AI Drones
- Patrol between waypoints
- Smooth rotation and movement
- Non-primitive models

### 👁️ Detection System
Each AI uses a 3-part vision system:
- **Field of View (angle check)**
- **Distance check**
- **Raycasting (line of sight)**

This prevents AI from seeing through walls and makes detection more realistic.

### 🔁 Player Reset
- Player resets to start when:
  - Detected by AI
  - Collides with a drone

---

## 🧠 How It Works

- AI continuously checks for the player in `Update()`
- Direction and angle are calculated using vectors
- Raycasting confirms visibility
- If all conditions pass → player is detected


---

## 🛠️ Tech Stack

- Unity (3D)
- C#
- Rigidbody physics
- Raycasting
- Basic AI logic (rule-based)
