
# **Hangman Game - WPF Application**

### **Overview**

This project is a Hangman game built using the Windows Presentation Foundation (WPF) framework. It includes a graphical user interface where players guess letters to figure out a hidden word. The game features multiple difficulty levels, a timer, a hint system, and a visual representation of the player's remaining lives.

### **Features**

- **Difficulty Levels**: Choose between *Easy*,*Hard*, and *Extreme* difficulties each with different hints and timers.
-  **Hint System**: Provides a number of letters based on the selected difficulty.
-  **Timer**: Players must guess the word within a set amount of time, which varies by difficulty.
-  **Lives System**: Players have 6 lives to guess the word. A visual hangman is drawn as lives are lost.
-  **Win/Loss Tracking**: Displays the number of wins and losses after each round.
-  **Random Word Generator**: Words are randomly selected from predefined lists based on the difficulty level.
-  **Keyboard Input**: Players can use the keyboard to input their guesses.

### **Difficulty Levels**

- **Easy**:
  - 1 letter hint 
  - 10 seconds timer
- **Hard**:
  - 4 letter hints
  - 15 seconds timer
- **Extreme**:
  - 7 letter hints
  - 20 seconds timer

### **Installation 

**Step 1: Clone the repo**
- Open a terminal (Command Prompt, PowerShell, or Git Bash).
- Run the following command to clone the repo.
  ```bash
  git clone https://github.com/srmagar2002/hangmanGame.git

**Step 2: Open the project in Visual Studio**
- Open **Visual Studio**.
- Click **Open a project or solution**.
- Navigate to the clone repository folder, select the .sln (solution) file, and open it.

**Step 3: Run the application**
- Click Build > Build Soution or press `ctrl+shift+B` to complie the project
- Once the build is successful, press `f5` to run the application or click **Debug > Start Debugging**.

### **Some Screenshots**
![DIFFICULTYSCREEN](/images/difficultyScreen.png)

![THEGAME](/images/theGame1.png)

![THEGAME](/images/theGame2.png)

![LOSE](/images/loseGraphics.png)