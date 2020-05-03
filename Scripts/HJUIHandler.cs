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
        [Header("Game Play UI")]
        [SerializeField] private GameObject mGamePlayUI;
        [SerializeField] private TextMeshProUGUI mCurrentScoreText;
        [SerializeField] private TextMeshProUGUI mBestScoreText;

        [Header("Progress Bar")]
        [SerializeField] private TextMeshProUGUI mCurrentLevelText;
        [SerializeField] private TextMeshProUGUI mNextLevelText;
        [SerializeField] private Image mProgressBar;

        //----------------------------------------------------------------------------------------------------

        private void Start()
        {
            mBestScoreText.text = string.Format(HJGameConstants.kBestScoreText, HJPlayerScoreAndLevelManager.Instance().BestScore);
            SetProgressBarDetails();
        }
        //----------------------------------------------------------------------------------------------------

        private void OnEnable()
        {
            HJGameEventHandler.Instance().OnScoreChangedEvent += SetScoreTextInUI;
            HJGameEventHandler.Instance().OnRetrySameLevel += ResetUI;
            HJGameEventHandler.Instance().OnNextLevelLoad += ResetUI;
        }
        //----------------------------------------------------------------------------------------------------

        private void OnDisable()
        {
            HJGameEventHandler.Instance().OnScoreChangedEvent -= SetScoreTextInUI;
            HJGameEventHandler.Instance().OnRetrySameLevel -= ResetUI;
            HJGameEventHandler.Instance().OnNextLevelLoad += ResetUI;

        }
        //----------------------------------------------------------------------------------------------------

        void SetScoreTextInUI(int score)
        {
            mCurrentScoreText.text = score.ToString();

            string bestScoreText = string.Format(HJGameConstants.kBestScoreText, HJPlayerScoreAndLevelManager.Instance().BestScore);
            if (mBestScoreText.text != bestScoreText)
                mBestScoreText.text = bestScoreText;

            SetProgressBarDetails();
        }
        //----------------------------------------------------------------------------------------------------

        void SetProgressBarDetails()
        {
            int currentLevel = HJPlayerScoreAndLevelManager.Instance().CurrentLevel;
            int totalLevelsInGame = HJConfigManager.Instance().GetTotalNumberOfLevelsInGame();

            //mCurrentLevelText.text = currentLevel.ToString();
            //mNextLevelText.text = (currentLevel + 1)> totalLevelsInGame?"": (currentLevel + 1).ToString();

            mCurrentLevelText.text = "0";
            mNextLevelText.text = "100";


            float completionPercent = (float)(HJPlayerScoreAndLevelManager.Instance().NumberOfPlatformsPassed) / (float)(HJConfigManager.Instance().GetTotalPlatformCountForLevel(currentLevel) - 1);
            mProgressBar.fillAmount = completionPercent;
        }
        //----------------------------------------------------------------------------------------------------

        void ResetUI()
        {
            mProgressBar.fillAmount = 0.0f;
            mCurrentScoreText.text = HJPlayerScoreAndLevelManager.Instance().CurrentScore.ToString();
        }
    }
}