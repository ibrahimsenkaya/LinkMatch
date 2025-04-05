using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] LevelData levelData;
    public BoardCreator BoardCreator;
    public ChipGenerator chipGenerator;
    public CameraController cameraController;
    public BoardChecker boardChecker;
    public InputChecker inputChecker;
    
    [OnValueChanged("GetLevelData")]
    public int levelIndex;
    
    
    public int remainingMoves;
    public int targetScore;
    public int score;

    private void Awake()
    {
        inputChecker.OnMakeMove+=DecreaseMoves;
    }


    private void Start()
    {
        GetLevelData();
        PrepareManagers();
        
    }
    public void GetLevelData()
    {
        levelData = Resources.Load<LevelData>("Levels/LevelData " + levelIndex);
        if (levelData == null)
        {
            Debug.LogError("Level data not found for level " + levelIndex);
            return;
        }
    }
    public void PrepareManagers()
    {
        if (levelData ==null) return;
        
        BoardCreator.xCount = levelData.RowCount;
        BoardCreator.yCount = levelData.ColumnCount;
        BoardCreator.Create();
        
        chipGenerator.GenerateChips();

        cameraController.FitCamera(BoardCreator);
        
        inputChecker.Enable();
        
        remainingMoves = levelData.targetMoves;
        targetScore = levelData.targetScore;
        score = 0;

    }
    

    #region Requiriment Checker

    public void AddScore(int amount)
    {
        score += amount;
        if (score >= targetScore)
        {
            GameOver(true);
        }
    }
    
    public void DecreaseMoves()
    {
        remainingMoves--;
        if (remainingMoves <= 0)
        {
            GameOver();
        }
    }
    public void GameOver(bool isWin = false)
    {
        inputChecker.Enable(false);
        if (isWin)
        {
            Debug.Log("You Win!");
        }
        else
        {
            Debug.Log("Game Over");
        }
    }

    #endregion
    

 
    
    
    
    
    
    
    
   
}
