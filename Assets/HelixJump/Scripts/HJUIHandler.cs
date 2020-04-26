using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace HelixJump
{
    public class HJUIHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI mCurrentScoreText;
        [SerializeField] private TextMeshProUGUI mBestScoreText;

        private void Start()
        {
            mBestScoreText.text = string.Format(HJGameConstants.kBestScoreText, HJPlayerScoreAndLevelManager.Instance().BestScore);
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
        }
    }
}