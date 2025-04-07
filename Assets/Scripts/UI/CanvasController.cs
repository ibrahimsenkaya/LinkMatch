using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
        public GameObject winPanel,losePanel,gamePlayPanel;
        public TMP_Text gamePlayScoreText,gamePlayMoveText,levelText;
        public Button nextLevelButton,retryLevelButton;

        private void Awake()
        {
            nextLevelButton.onClick.AddListener(NextLevel);
            retryLevelButton.onClick.AddListener(RetryLevel);
        }

        public Action OnNextLevelClicked,OnRetryLevelClicked;
        
        public void OpenGameplayPanel(int targetScore,int targetMoveCount,int levelIndex)
        {
            gamePlayPanel.SetActive(true);
            winPanel.SetActive(false);
            losePanel.SetActive(false);
            UpdateMoves(targetMoveCount);
            UpdateScore(0,targetScore);
            levelText.text = "Level "+levelIndex;

        }
        public void UpdateMoves(int currentMoveCount)
        {
            gamePlayMoveText.text = "MOVE :"+currentMoveCount;
        }
        public void UpdateScore(int currentScore,int targetScore)
        {
            gamePlayScoreText.text = "SCORE :"+currentScore+"/"+targetScore;
        }
        public void GameOver(bool isWin)
        {
            gamePlayPanel.SetActive(false);
            if (isWin)
                winPanel.SetActive(true);
            else
                losePanel.SetActive(true);
        }
        public void NextLevel()
        {
            OnNextLevelClicked?.Invoke();
        }
        
        public void RetryLevel()
        {
            OnRetryLevelClicked?.Invoke();
        }
}