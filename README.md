
Unity Chip Matching Game

This is a simple chip matching game built in Unity. The game generates chips on a board, and the goal is to match chips based on their type. The game includes features such as level management, chip generation, scoring, and animations.

Features

- Level Management: Progress through levels with different board configurations and target scores.
- Chip Generation: Chips are randomly generated and placed on the board.
- Tile Management: Each tile can hold a chip and has neighbor relationships.
- Chip Animation: Chips can be moved and animated on the board.
- Score & Moves Tracking: Track the player's score and remaining moves.
- Game Over Logic: Handles win and loss conditions.
- Camera Control: The camera automatically adjusts to fit the board's dimensions.

Components

GameManager
The GameManager class controls the overall game flow, including level management, scoring, and game over conditions.

Fields:
- levelData: Data for the current level (number of rows/columns, target score, target moves).
- boardManager, chipGenerator, cameraController: Various managers that control the board, chip generation, and camera adjustments.
- remainingMoves, score: Tracks the player's remaining moves and current score.

Methods:
- NextLevel: Moves to the next level.
- RetryLevel: Resets the level and starts it again.
- AddScore: Adds score when tiles are matched.
- DecreaseMoves: Decreases the remaining moves on each turn.
- GameOver: Ends the game, displaying the result (win or lose).

BoardManager
The BoardManager class handles the generation and management of the game board.

Fields:
- tiles: List of all tiles on the board.
- columns: Columns in the grid, each containing a list of tiles.

Methods:
- Create: Generates the game board and places tiles based on the selected configuration.
- GenerateTile: Generates a tile at a specific position.
- SetTilesNeighbors: Sets neighboring relationships between tiles.

ChipGenerator
The ChipGenerator class is responsible for creating and removing chips from the board.

Fields:
- chipPrefab: The prefab used to create new chips.
- chipDataContainer: Holds the data for the chip types and sprites.

Methods:
- GenerateChips: Generates chips for all tiles.
- RemoveChips: Removes chips from the board based on a list of selected tiles.
- GenerateChipWithAnim: Creates chips with an animation to fall into place.

BoardController
The BoardController class checks for matching chips on the board.

Methods:
- HasAnyMatchingChips: Checks if there are any matching chips on the board based on the required match count.

Tile
The Tile class handles individual tiles on the board.

Fields:
- neighbors: List of neighboring tiles.
- chip: The chip currently placed on the tile.

Methods:
- AddNeighbor: Adds a neighboring tile.
- Highlight: Highlights the tile when selected.
- Collect: Removes the chip from the tile.

CanvasController
The CanvasController manages the UI elements of the game.

Fields:
- winPanel, losePanel, gamePlayPanel: UI panels for win/lose states and gameplay.
- gamePlayScoreText, gamePlayMoveText, levelText: UI elements to display score, moves, and level.

Methods:
- OpenGameplayPanel: Opens the gameplay UI panel.
- UpdateScore, UpdateMoves: Updates the score and move count on the UI.
- GameOver: Displays the game over screen based on win/loss.
- NextLevel, RetryLevel: Button actions for progressing to the next level or retrying the current level.

InputChecker
The InputChecker class handles user input for selecting tiles.

Fields:
- selectedTiles: List of currently selected tiles.

Methods:
- DetectTileUnderMouse: Detects which tile the player is hovering over with the mouse.
- ManageSelectedTileList: Manages the list of selected tiles based on user input.
- CollectTileList: Collects the selected tiles if there are three or more, triggering a move.
