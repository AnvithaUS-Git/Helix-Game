using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelixJump

{
    public class HJGameEventHandler
    {
        private static HJGameEventHandler mInstance;

        public static HJGameEventHandler Instance()
        {
            return mInstance = mInstance == null ? new HJGameEventHandler() : mInstance;
        }

        public delegate void OnScoreChangedDel(int score);
        public delegate void voidDelegate();

        public event OnScoreChangedDel OnScoreChangedEvent;
        public event voidDelegate OnPlayerDeathEvent;
        public event voidDelegate OnRetrySameLevel;
        public event voidDelegate OnCurrentLevelPassed;
        public event voidDelegate OnNextLevelLoad;
        public event voidDelegate OnAdFailedToShowEvent;
        public event voidDelegate OnAdClosedEvent;
        
        public void TriggerOnScoreChangedEvent(int score)
        {
            OnScoreChangedEvent?.Invoke(score);
        }
        public void TriggerOnPlayerDeathEvent()
        {
            OnPlayerDeathEvent?.Invoke();
        }
        public void TriggerRetrySameLevelEvent()
        {
            OnRetrySameLevel?.Invoke();
        }
        public void TriggerOnCurrentLevelPassedEvent()
        {
            OnCurrentLevelPassed?.Invoke();
        }
        public void TriggerOnNextLevelLoadEvent()
        {
            OnNextLevelLoad?.Invoke();
        }

        public void TriggerOnAdFailedToShowEvent()
        {
            OnAdFailedToShowEvent?.Invoke();
        }
        public void TriggerOnAdClosedEvent()
        {
            OnAdClosedEvent?.Invoke();
        }
    }
}