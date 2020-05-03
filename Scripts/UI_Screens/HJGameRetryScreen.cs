using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

namespace HelixJump
{
    public class HJGameRetryScreen : MonoBehaviour
    {
        [SerializeField] private Transform mComple_Score_Holder;
        [SerializeField] private TextMeshProUGUI mCompeletionPercentText;
        [SerializeField] private TextMeshProUGUI mRS_BestScoreText;
        [SerializeField] private TextMeshProUGUI mTapToRetryText;
        [SerializeField] private Button mRetryBtn;

        private Vector3 mInitialComple_Score_HolderPos;


        private void Start()
        {
            mInitialComple_Score_HolderPos = mComple_Score_Holder.localPosition;
        }
        private void OnEnable()
        {
            Debug.Log("Onenable ");
            mRetryBtn.onClick.AddListener(OnRetryBtnClicked);

            HJGameEventHandler.Instance().OnPlayerDeathEvent += ShowRetryScreen;
            HJGameEventHandler.Instance().OnAdFailedToShowEvent += RestartTheSameLevel;
            HJGameEventHandler.Instance().OnAdClosedEvent += RestartTheSameLevel;
        }

        private void OnDisable()
        {
            mRetryBtn.onClick.RemoveListener(OnRetryBtnClicked);

            HJGameEventHandler.Instance().OnPlayerDeathEvent -= ShowRetryScreen;
            HJGameEventHandler.Instance().OnAdFailedToShowEvent -= RestartTheSameLevel;
            HJGameEventHandler.Instance().OnAdClosedEvent -= RestartTheSameLevel;

            DOTween.KillAll();
        }


        void ShowRetryScreen()
        {
            Debug.Log("Screen showing ");
            Time.timeScale = 0.0f;
            this.gameObject.transform.localScale = Vector3.one;
            int currentLevel = HJPlayerScoreAndLevelManager.Instance().CurrentLevel;
            float completionPercent = (float)(HJPlayerScoreAndLevelManager.Instance().NumberOfPlatformsPassed) / (float)HJConfigManager.Instance().GetTotalPlatformCountForLevel(currentLevel);

            mCompeletionPercentText.text = string.Format(HJGameConstants.kCompletionPercentText, (int)(completionPercent * 100));
            mRS_BestScoreText.text = string.Format(HJGameConstants.kRS_BestScoreText, HJPlayerScoreAndLevelManager.Instance().BestScore);
            mTapToRetryText.text = HJGameConstants.kTapToRetryText;
            StartScreenAnimations();
        }

        void StartScreenAnimations()
        {
            float punchScale = 0.2f;
            mComple_Score_Holder.DOLocalMoveX(0, 0.2f).OnComplete(() =>
            {
                mTapToRetryText.transform.DOPunchScale(new Vector3(punchScale, punchScale, punchScale), 1, 1, 1).SetLoops(-1);
            });
        }
        //----------------------------------------------------------------------------------------------------
        void OnRetryBtnClicked()
        {
            HJGameManager.Instance().ShowInterstialAd((isSucess) =>
            {
                //If sucess restarLevel will be called on AdClosed Event Handler
                if (!isSucess)
                    RestartTheSameLevel();
            });
        }
        void RestartTheSameLevel()
        {
            Time.timeScale = 1.0f;
            this.gameObject.transform.localScale = Vector3.zero;
            mComple_Score_Holder.localPosition = mInitialComple_Score_HolderPos;


            HJGameManager.Instance().OnRestartSameLevel();
        }

    }
}
