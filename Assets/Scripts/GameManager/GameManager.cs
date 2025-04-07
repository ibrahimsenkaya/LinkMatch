using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] LevelData levelData;

    #region Managers

    public BoardManager boardManager;
    [FormerlySerializedAs("boardChecker")] public BoardController boardController;
    public ChipGenerator chipGenerator;
    public CameraController cameraController;
    public InputChecker inputChecker;
    public CanvasController canvasController;

    #endregion

    
    [OnValueChanged("GetLevelData")]
    public int levelIndex;
    int allLevelsCount;
    
    public int remainingMoves;
    public int targetScore;
    public int score;

    #region Initialize

    private void Awake()
    {
        inputChecker.OnMakeMove+=DecreaseMoves;
        inputChecker.OnSelectedTileList += ManageSelectedTiles;
        canvasController.OnNextLevelClicked += NextLevel;
        canvasController.OnRetryLevelClicked += RetryLevel;
        
        allLevelsCount = Resources.LoadAll<LevelData>("Levels").Length;
    }

    private void Start()
    {
        inputChecker.Initialize(boardManager);
        chipGenerator.Initialize(boardManager);
        boardController.Initialize(boardManager, chipGenerator);
        
        GetLevelData();
        PrepareManagers();
        
    }
    
    public void GetLevelData()
    {
        levelData = Resources.Load<LevelData>("Levels/LevelData " + (levelIndex%allLevelsCount));
        if (levelData == null)
        {
            Debug.LogError("Level data not found for level " + levelIndex);
        }
    }
    
    public void PrepareManagers()
    {
        if (levelData ==null) return;
        
        remainingMoves = levelData.targetMoves;
        targetScore = levelData.targetScore;
        score = 0;

        
        boardManager.xCount = levelData.RowCount;
        boardManager.yCount = levelData.ColumnCount;
        boardManager.Create();
        
        chipGenerator.GenerateChips();

        while (!boardController.HasAnyMatchingChips(3))
        {
            Debug.Log("Regenerating chips");
            chipGenerator.GenerateChips();
        }
        

        cameraController.FitCamera(boardManager);
        
        inputChecker.Enable();
        canvasController.OpenGameplayPanel(targetScore,remainingMoves,levelIndex+1);

    }

    #endregion

    #region LevelFunctions

    public void NextLevel()
    {
        levelIndex++;
        GetLevelData();
        PrepareManagers();
        isGameOver = false;
    }
    
    public void RetryLevel()
    {
        PrepareManagers();
        isGameOver = false;
    }

    #endregion
    
    #region TileCheckers

    private void ManageSelectedTiles(List<Tile> tileList)
    {
        chipGenerator.RemoveChips(tileList);
        var coloumnIndexSet= boardManager.CollectTiles(tileList);
        AddScore((tileList.Count)* 10);
        foreach (var coloumnIndex in coloumnIndexSet)
        {
            var tempTiles= boardManager.columns[coloumnIndex].ReplaceChips();
            chipGenerator.GenerateChipWithAnim(tempTiles);
        }
    }

    #endregion

    #region Requiriment Checker

    public void AddScore(int amount)
    {
        score += amount;
        if (score >= targetScore)
            GameOver(true);
        else
            canvasController.UpdateScore(score, targetScore);
    }
    
    public void DecreaseMoves()
    {
        remainingMoves--;
        if (remainingMoves <= 0)
            GameOver();
        else
            canvasController.UpdateMoves(remainingMoves);
    }
    
    bool isGameOver = false;
    public void GameOver(bool isWin = false)
    {
        if (isGameOver) return;
     
        isGameOver = true;
        inputChecker.Enable(false);
        canvasController.GameOver(isWin);
    }

    #endregion 


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            AddScore(1000);
        if (Input.GetKeyDown(KeyCode.N))
            NextLevel();
        if (Input.GetKeyDown(KeyCode.R))
            RetryLevel();


    }
}
