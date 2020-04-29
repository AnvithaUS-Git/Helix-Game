using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelixJump
{
    public class HJSinglePlatformPiece : MonoBehaviour
    {
        [SerializeField] private Renderer mMeshRenderer;
        [SerializeField] private Color mBaseColor;

        private HJPlatformSliceType mCurrentSliceType;
        public void Initialize(int sliceId, HJPlatformSliceType sliceType)
        {
            mCurrentSliceType = sliceType;
            SetStateAndColorToSlice();
        }

        void SetStateAndColorToSlice()
        {
            switch(mCurrentSliceType)
            {
                case HJPlatformSliceType.eDisabledSlice:
                    this.gameObject.SetActive(false);
                    break;
                case HJPlatformSliceType.eDeathSlice:
                    mMeshRenderer.material.color = HJConfigManager.Instance().GetDeathPaltformColor(HJPlayerScoreAndLevelManager.Instance().CurrentLevel);
                    break;
                case HJPlatformSliceType.eRegularSlice:
                    mMeshRenderer.material.color = HJConfigManager.Instance().GetBasePlatformColor(HJPlayerScoreAndLevelManager.Instance().CurrentLevel);
                    break;
            }
        }
  
        public bool IsDeathSlice()
        {
            return mCurrentSliceType == HJPlatformSliceType.eDeathSlice;
        }

        void OnHitDeathSlice()
        {
            HJGameManager.Instance().CurrentGameState = HJGameState.eGamePlayerDeath;
            HJGameEventHandler.Instance().TriggerOnPlayerDeathEvent();
        }
        void OnGoalReached()
        {
            HJGameEventHandler.Instance().TriggerOnCurrentLevelPassedEvent();
        }

        public void CheckWhetherDeathOrGoal()
        {
            switch(mCurrentSliceType)
            {
                case HJPlatformSliceType.eDeathSlice:
                    //OnHitDeathSlice();
                    break;
                case HJPlatformSliceType.eGoalSlice:
                    OnGoalReached();
                    break;
            }
        }

        public void ClearAllVariableData()
        {
            gameObject.SetActive(true);
            mMeshRenderer.material.color = mBaseColor;
            mCurrentSliceType = HJPlatformSliceType.eNone;
        }
    }
}