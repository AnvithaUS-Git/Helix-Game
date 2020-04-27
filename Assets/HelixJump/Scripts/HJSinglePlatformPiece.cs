﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelixJump
{
    public class HJSinglePlatformPiece : MonoBehaviour
    {
        [SerializeField] private Renderer mMeshRenderer;
        private bool mIsLastPlatformPiece;
        private bool mIsDeathSlice;
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
                    mMeshRenderer.material.color = Color.red;
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
            Debug.Log("Death slice ");
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
                    OnHitDeathSlice();
                    break;
                case HJPlatformSliceType.eGoalSlice:
                    OnGoalReached();
                    break;
            }
        }
    }
}