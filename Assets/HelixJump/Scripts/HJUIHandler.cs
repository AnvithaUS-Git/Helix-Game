using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace HelixJump
{
    public class HJUIHandler : MonoBehaviour
    {
        

        [Header("Game Start UI")]
        [SerializeField] private GameObject mGameStartUI;
        [SerializeField] private TextMeshProUGUI mStartGameBtnText;
        [SerializeField] private Button mStartGameBtn;

        [Header("Game Play UI")]
        [SerializeField] private GameObject mGamePlayUI;
        [SerializeField] private TextMeshProUGUI mCurrentScoreText;
        [SerializeField] private TextMeshProUGUI mBestScoreText;

        [Header("Progress Bar")]
        [SerializeField] private TextMeshProUGUI mCurrentLevelText;
        [SerializeField] private TextMeshProUGUI mNextLevelText;
        [SerializeField] private Image mProgressBar;


        [Space(10)][Header("Restart UI")]
        [SerializeField] private GameObject mRetryScreen;
        [SerializeField] private TextMeshProUGUI mCompeletionPercentText;
        [SerializeField] private TextMeshProUGUI mRS_BestScoreText;
        [SerializeField] private TextMeshProUGUI mTapToRetryText;
        [SerializeField] private Button mRetryBtn;

        [Header("Level completion UI")]
        [SerializeField] private GameObject mLevelPassedUI;
        [SerializeField] private TextMeshProUGUI mLevelPassText;
        [SerializeField] private TextMeshProUGUI mTapToContinueText;
        [SerializeField] private Button mLoadNextLevelBtn;

        private void Start()
        {
            mBestScoreText.text = string.Format(HJGameConstants.kBestScoreText, HJPlayerScoreAndLevelManager.Instance().BestScore);
            SetProgressBarDetails();
        }
        private void OnEnable()
        {
            HJGameEventHandler.Instance().OnScoreChangedEvent += SetScoreTextInUI;
            HJGameEventHandler.Instance().OnPlayerDeathEvent += ShowRetryScreen;
            HJGameEventHandler.Instance().OnCurrentLevelPassed += ShowLevelCompletionUI;


            mStartGameBtn.onClick.AddListener(OnStartGameBtnClicked);
            mRetryBtn.onClick.AddListener(OnRetryBtnClicked);
            mLoadNextLevelBtn.onClick.AddListener(OnLoadNextLevelClicked);
        }
        private void OnDisable()
        {
            HJGameEventHandler.Instance().OnScoreChangedEvent -= SetScoreTextInUI;
            HJGameEventHandler.Instance().OnPlayerDeathEvent -= ShowRetryScreen;
            HJGameEventHandler.Instance().OnCurrentLevelPassed -= ShowLevelCompletionUI;


            mStartGameBtn.onClick.RemoveListener(OnStartGameBtnClicked);
            mRetryBtn.onClick.RemoveListener(OnRetryBtnClicked);
            mLoadNextLevelBtn.onClick.RemoveListener(OnLoadNextLevelClicked);

        }
        void SetScoreTextInUI(int score)
        {
            mCurrentScoreText.text = score.ToString();

            string bestScoreText = string.Format(HJGameConstants.kBestScoreText, HJPlayerScoreAndLevelManager.Instance().BestScore);
            if (mBestScoreText.text != bestScoreText)
                mBestScoreText.text = bestScoreText;

            SetProgressBarDetails();
        }

        void SetProgressBarDetails()
        {
            int currentLevel = HJPlayerScoreAndLevelManager.Instance().CurrentLevel;
            int totalLevelsInGame = HJConfigManager.Instance().GetTotalNumberOfLevelsInGame();
            
            mCurrentLevelText.text = currentLevel.ToString();
            mNextLevelText.text = (currentLevel + 1)> totalLevelsInGame?"": (currentLevel + 1).ToString();
            
            float completionPercent = (float)(HJPlayerScoreAndLevelManager.Instance().NumberOfPlatformsPassed) / (float)(HJConfigManager.Instance().GetTotalPlatformCountForLevel(currentLevel)-1);
            mProgressBar.fillAmount = completionPercent;
        }
        #region Game Start UI

        public void ShowGameStartUI()
        {
            mGameStartUI.SetActive(true);
            mStartGameBtnText.text = HJGameConstants.kGameStartText;
        }
        void OnStartGameBtnClicked()
        {
            mGameStartUI.SetActive(false);
            mGamePlayUI.SetActive(true);
            HJGameManager.Instance().CurrentGameState = HJGameState.eGamePlaying;
        }
        #endregion

        #region Level retry UI
        void ShowRetryScreen()
        {
            Time.timeScale = 0.0f;
            mRetryScreen.SetActive(true);
            int currentLevel = HJPlayerScoreAndLevelManager.Instance().CurrentLevel;
            float completionPercent = (float)(HJPlayerScoreAndLevelManager.Instance().NumberOfPlatformsPassed) / (float)HJConfigManager.Instance().GetTotalPlatformCountForLevel(currentLevel);

            mCompeletionPercentText.text = string.Format(HJGameConstants.kCompletionPercentText, (int)(completionPercent * 100));
            mRS_BestScoreText.text = string.Format(HJGameConstants.kRS_BestScoreText, HJPlayerScoreAndLevelManager.Instance().BestScore);
            mTapToRetryText.text = HJGameConstants.kTapToRetryText;
        }

        void OnRetryBtnClicked()
        {
            Time.timeScale = 1.0f;
            HJGameManager.Instance().CurrentGameState = HJGameState.eGamePlaying;
            HJGameEventHandler.Instance().TriggerRetrySameLevelEvent();
            HJPlayerScoreAndLevelManager.Instance().RestartCurrentLevel();
            mRetryScreen.SetActive(false);
        }
        #endregion

        #region Level Completion UI
        void ShowLevelCompletionUI()
        {
            Time.timeScale = 0.0f;
            mLevelPassedUI.SetActive(true);
            int currentLevel = HJPlayerScoreAndLevelManager.Instance().CurrentLevel;
            mLevelPassText.text = string.Format(HJGameConstants.kLevelPassedText, currentLevel);
            mTapToContinueText.text = HJGameConstants.kTapToContinueText;
        }

        void OnLoadNextLevelClicked()
        {
            Time.timeScale = 1.0f;
            mLevelPassedUI.SetActive(false);
            HJPlayerScoreAndLevelManager.Instance().LoadNextLevel();
            HJGameEventHandler.Instance().TriggerOnNextLevelLoadEvent();
        }
        #endregion
    }
}