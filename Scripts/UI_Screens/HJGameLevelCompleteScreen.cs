using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

namespace HelixJump
{
    public class HJGameLevelCompleteScreen : MonoBehaviour
    {
        [Header("Level completion UI")]
        [SerializeField] private Transform mMainTextHolder;
        [SerializeField] private TextMeshProUGUI mLevelPassText;
        [SerializeField] private TextMeshProUGUI mTapToContinueText;
        [SerializeField] private Button mLoadNextLevelBtn;

        private Vector3 mInitialTextHolderPos;
        //----------------------------------------------------------------------------------------------------

        private void Start()
        {
            mInitialTextHolderPos = mMainTextHolder.localPosition;
            gameObject.transform.localScale = Vector3.zero;
        }
        //----------------------------------------------------------------------------------------------------

        private void OnEnable()
        {
            HJGameEventHandler.Instance().OnCurrentLevelPassed += ShowLevelCompletionUI;
            mLoadNextLevelBtn.onClick.AddListener(OnLoadNextLevelClicked);
        }
        //----------------------------------------------------------------------------------------------------

        private void OnDisable()
        {
            HJGameEventHandler.Instance().OnCurrentLevelPassed -= ShowLevelCompletionUI;
            mLoadNextLevelBtn.onClick.RemoveListener(OnLoadNextLevelClicked);
            DOTween.KillAll();
        }
        //----------------------------------------------------------------------------------------------------

        void ShowLevelCompletionUI()
        {
            Time.timeScale = 0.0f;
            gameObject.transform.localScale = Vector3.one;

            int currentLevel = HJPlayerScoreAndLevelManager.Instance().CurrentLevel;
            mLevelPassText.text = string.Format(HJGameConstants.kLevelPassedText, currentLevel);
            mTapToContinueText.text = HJGameConstants.kTapToContinueText;
            StartScreenAnimations();
        }
        //----------------------------------------------------------------------------------------------------

        void StartScreenAnimations()
        {
            float punchScale = 0.2f;
            mMainTextHolder.DOLocalMoveX(0, 0.2f).OnComplete(() =>
            {
                mTapToContinueText.transform.DOPunchScale(new Vector3(punchScale, punchScale, punchScale), 1, 1, 1).SetLoops(-1);
            });
        }
        //----------------------------------------------------------------------------------------------------

        void OnLoadNextLevelClicked()
        {
            AdsManager.GetInstance().ShowInterstialAds();

            Time.timeScale = 1.0f;
            gameObject.transform.localScale = Vector3.zero;
            mMainTextHolder.localPosition = mInitialTextHolderPos;
           
            HJPlayerScoreAndLevelManager.Instance().LoadNextLevel();
            HJGameEventHandler.Instance().TriggerOnNextLevelLoadEvent();
        }
        //----------------------------------------------------------------------------------------------------

    }
}
