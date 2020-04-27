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
        [SerializeField] private TextMeshProUGUI mCurrentScoreText;
        [SerializeField] private TextMeshProUGUI mBestScoreText;

        [Header("Progress Bar")]
        [SerializeField] private TextMeshProUGUI mCurrentLevelText;
        [SerializeField] private TextMeshProUGUI mNextLevelText;
        [SerializeField] private Image mProgressBar;

        private void Start()
        {
            mBestScoreText.text = string.Format(HJGameConstants.kBestScoreText, HJPlayerScoreAndLevelManager.Instance().BestScore);
            SetProgressBarDetails();
        }
        private void OnEnable()
        {
            HJGameEventHandler.Instance().OnScoreChangedEvent += SetScoreTextInUI;
        }
        private void OnDisable()
        {
            HJGameEventHandler.Instance().OnScoreChangedEvent -= SetScoreTextInUI;
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
            mCurrentLevelText.text = currentLevel.ToString();
            mNextLevelText.text = currentLevel + 1.ToString();
            float completionPercent = (float)(HJPlayerScoreAndLevelManager.Instance().NumberOfPlatformsPassed + 1) / (float)HJConfigManager.Instance().GetTotalPlatformCountForLevel(currentLevel);
            mProgressBar.fillAmount = completionPercent;
        }
    }
}