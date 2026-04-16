# 2D Scene Setup Guide

## Hierarchy Structure
- **Root (Scene)**
  - **GameObjects**
    - Player
    - Enemies
    - Background
    - UI

## Component Setup
### Player
- **Sprite Renderer**: Assign player sprite.
- **Rigidbody2D**: Set Body Type to Dynamic.
- **Collider2D**: Add appropriate collider size.
- **Script**: Attach PlayerController.cs script.

### Enemies
- **Sprite Renderer**: Assign enemy sprite.
- **Rigidbody2D**: Set Body Type to Kinematic.
- **Collider2D**: Add appropriate collider size.
- **Script**: Attach EnemyAI.cs script.

### Background
- **Sprite Renderer**: Assign background sprite.
- **Tilemap** (if using tile-based background).

### UI
- **Canvas**: Create a canvas for UI elements.
  - **Text**: For score display.
  - **Buttons**: For interaction (Start, Quit).

## Wiring Instructions
1. **Player to Enemy**: Set up triggers for enemy detection using colliders.
2. **UI to Game Logic**: Connect buttons to game control scripts.
3. **Camera**: Adjust camera settings to fit the scene and set it to follow the player if needed.

## Final Notes
Ensure all GameObjects are properly named and organized in the hierarchy to keep the project manageable and easy to navigate. Test the scene frequently during setup to troubleshoot any issues early on.  

