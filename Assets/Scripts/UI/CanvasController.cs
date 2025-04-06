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
            UpdateGameplayScore(0,targetScore,0,targetMoveCount);
            levelText.text = "Level "+levelIndex;

        }
        
        public void UpdateGameplayScore(int currentScore,int targetScore,int currentMoveCount,int targetMoveCount)
        {
            gamePlayScoreText.text = "SCORE :"+currentScore+"/"+targetScore;
            gamePlayMoveText.text = "MOVE :"+currentMoveCount+"/"+targetMoveCount;
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