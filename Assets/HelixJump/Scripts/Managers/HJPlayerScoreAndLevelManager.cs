using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelixJump
{
    public class HJPlayerScoreAndLevelManager
    {
        private static HJPlayerScoreAndLevelManager mInstance;

        private int mCurrentScore;
        private int mBestScore;
        private int mCurrentLevel = 1;
        private int mNumberOfPlatformsPassed = 0;

        public static HJPlayerScoreAndLevelManager Instance()
        {
            return mInstance = mInstance == null ? new HJPlayerScoreAndLevelManager() : mInstance;
        }

        public int CurrentScore
        {
            get
            {
                return mCurrentScore;
            }
            set
            {
                mCurrentScore = value;
                if (mCurrentScore > BestScore)
                {
                    BestScore = mCurrentScore;
                }
            }
        }

        public int BestScore
        {
            get
            {
                mBestScore = PlayerPrefs.GetInt("bst_scr");
                return mBestScore;
            }
            set
            {
                mBestScore = value;
                PlayerPrefs.SetInt("bst_scr", mBestScore);
            }
        }

        public int CurrentLevel
        {
            get { return mCurrentLevel; }
            set { mCurrentLevel = value; }
        }

        public int NumberOfPlatformsPassed { get; set; }

        public void AddScore(int scoreUnit)
        {
            CurrentScore += scoreUnit;
            NumberOfPlatformsPassed++;
            HJGameEventHandler.Instance().TriggerOnScoreChangedEvent(CurrentScore);
        }

        public void RestartCurrentLevel()
        {
            CurrentScore = 0;
            NumberOfPlatformsPassed = 0;
            HJGameEventHandler.Instance().TriggerOnPlayerDeathEvent();
        }

    }
}