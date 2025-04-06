using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] LevelData levelData;
    int allLevelsCount;
    
    public BoardCreator BoardCreator;
    public ChipGenerator chipGenerator;
    public CameraController cameraController;
    public InputChecker inputChecker;
    public CanvasController canvasController;
    
    [OnValueChanged("GetLevelData")]
    public int levelIndex;
    
    
    public int remainingMoves;
    public int targetScore;
    public int score;

    private void Awake()
    {
        inputChecker.OnMakeMove+=DecreaseMoves;
        canvasController.OnNextLevelClicked += NextLevel;
        canvasController.OnRetryLevelClicked += RetryLevel;
        allLevelsCount = Resources.LoadAll<LevelData>("Levels").Length;
    }

    

    private void Start()
    {
        GetLevelData();
        PrepareManagers();
        
    }
    public void GetLevelData()
    {
        levelData = Resources.Load<LevelData>("Levels/LevelData " + (levelIndex%allLevelsCount));
        if (levelData == null)
        {
            Debug.LogError("Level data not found for level " + levelIndex);
            return;
        }
    }
    public void PrepareManagers()
    {
        if (levelData ==null) return;
        
        remainingMoves = levelData.targetMoves;
        targetScore = levelData.targetScore;
        score = 0;

        
        BoardCreator.xCount = levelData.RowCount;
        BoardCreator.yCount = levelData.ColumnCount;
        BoardCreator.Create();
        
        chipGenerator.GenerateChips();

        cameraController.FitCamera(BoardCreator);
        
        inputChecker.Enable();
        canvasController.OpenGameplayPanel(targetScore,remainingMoves,levelIndex+1);

    }
    
    public void NextLevel()
    {
        levelIndex++;
        GetLevelData();
        PrepareManagers();
    }
    
    public void RetryLevel()
    {
        this.levelIndex = levelIndex;
        PrepareManagers();
    }
    
    

    #region Requiriment Checker
    

    public void AddScore(int amount)
    {
        score += amount;
        if (score >= targetScore)
        {
            GameOver(true);
        }
        canvasController.UpdateGameplayScore(score, targetScore, remainingMoves, levelData.targetMoves);
    }
    
    public void DecreaseMoves()
    {
        remainingMoves--;
        if (remainingMoves <= 0)
        {
            GameOver();
        }
        canvasController.UpdateGameplayScore(score, targetScore, remainingMoves, levelData.targetMoves);
    }
    public void GameOver(bool isWin = false)
    {
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


    }
}
