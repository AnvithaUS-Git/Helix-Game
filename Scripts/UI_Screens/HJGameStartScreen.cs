using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace HelixJump
{
    public class HJGameStartScreen : MonoBehaviour
    {
        [SerializeField] private Text mBrandName;
        [SerializeField] private Text mGameName;
        [SerializeField] private Text mTapToPlayText;
        [SerializeField] private Transform mBrandNameHolder;

        [SerializeField] private Button mStartGameBtn;
        [SerializeField] private GameObject mGamePlayUI;

        private void Start()
        {
            InitializeScreen();
        }

        private void OnEnable()
        {
            mStartGameBtn.onClick.AddListener(OnStartGameBtnTappped);
        }

        private void OnDisable()
        {
            mStartGameBtn.onClick.RemoveListener(OnStartGameBtnTappped);

        }

        void InitializeScreen()
        {
            mBrandName.text = HJGameConstants.kBrandName;
            mGameName.text = HJGameConstants.kGameName;
            mTapToPlayText.text = HJGameConstants.kTapToPlay;
            StartScreenAnimations();
        }

        void StartScreenAnimations()
        {
            mBrandNameHolder.DOLocalMoveX(0, 1.0f).OnComplete(()=> {
                mTapToPlayText.gameObject.transform.DOScale(new Vector3(1, 1, 1), 0.3f);
            });
        }

        void OnStartGameBtnTappped()
        {
            gameObject.SetActive(false);
            mGamePlayUI.SetActive(true);
            HJGameManager.Instance().CurrentGameState = HJGameState.eGamePlaying;
        }
    }
}