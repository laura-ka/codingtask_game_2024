# Color Switch Clone Game
Coding task game made with Unity.

When opening the project in Unity, navigate to the 'Scenes' folder and double-click on 'GameScene' to open it.

<h2>Demo</h2>

Link to the game: https://lariaw.itch.io/coding-task-game

password: laura


<h2>Additional features that could be added to the game:</h2>

<h3>Additional Levels 📈</h3>

- More levels can be added to the game so that after finishing the first one, the player continues to the next.
- Each new level can have different obstacles, increased difficulty, and new challenges.
- This can be done by creating different level designs and using a level manager script to move between levels.

<h3>Hearts ❤️</h3>

There could be "hearts" that grant extra lives to the player. Collecting hearts allows the player to survive an obstacle collision or fall, preventing immediate game over.

**Implementation Steps:**

**Create Heart Collectibles:**

- Design a heart sprite.
- Create a new GameObject in Unity with a Collider2D component.
- Tag the heart GameObject as "Heart".

**Add Heart Collection Logic:**

- Create a HeartController script to manage heart collection.
- On collision with the player (ball), increment the player's lives and destroy the heart GameObject.
- Update the UI to reflect the new number of lives.
   
**Modify Game Over Logic:**

- Update the BallController script to check for remaining lives.
- If lives are greater than zero, reduce the count and reset the player’s position instead of ending the game.
- Implement logic to handle game restart or continue after losing a life.
   
**UI Updates:**

- Add a UI element to display the number of hearts or lives.
- Create a method in UIManager to update the lives display.

<h3>Power-Ups 🌟</h3>

There could be power-ups that temporarily give the player an advantage, such as a shield power-up that makes the player invincible for a limited time, allowing them to pass through obstacles without needing to match the color.

**Implementation Steps:**

**Design Power-Ups:**

- Create a power-up sprite.
- Implement a PowerUpController script to handle power-up effect.
  
**Implement Power-Up Logic:**

- Add logic to the BallController script to check for power-up collisions.
- Apply effects when a power-up is collected.
  
**Handle Power-Up Duration:**

- Use coroutines or timers to manage how long the power-up effect last.
- Revert to normal abilities after the power-up duration ends.
  
**UI Updates:**

- Show active power-ups on the UI.
- Create visual indicators for when a power-up is active.

<h3>Debuffs 💥</h3>

There could be a debuff collectible that temporarily increases the ball's weight, making it harder to jump. This could add an extra layer of challenge by reducing the player's jump height for a limited time.

**Implementation Steps:**

**Design the Power-Down Collectible:**

- Create a new sprite.
  
**Modify Ball Physics:**

- In the BallController script, add a new variable to track the ball's weight and another to track the debuff duration.
  
**Apply the Debuff:**

- When the ball collides with the collectible, increase the ball's weight by modifying its Rigidbody2D.mass or directly reduce the jump force.
  
**Timed Reset:**

- Use a coroutine to reset the ball's weight/jump force back to normal after a set duration.
  
**Feedback to Player:**
- Provide visual or audio feedback to indicate the active debuff effect.
  
**Balancing:**

- Test and adjust the duration and intensity of the weight increase to ensure it's challenging but fair.
