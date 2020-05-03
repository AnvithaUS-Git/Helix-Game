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
        //----------------------------------------------------------------------------------------------------

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
        //----------------------------------------------------------------------------------------------------

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
        //----------------------------------------------------------------------------------------------------

        public int CurrentLevel
        {
            get
            {
                mCurrentLevel = PlayerPrefs.GetInt("crnt_lvl");
                return mCurrentLevel > 0 ? mCurrentLevel : 1;
            }
            set
            {
                mCurrentLevel = value;
                PlayerPrefs.SetInt("crnt_lvl", mCurrentLevel);
            }
        }
        //----------------------------------------------------------------------------------------------------

        public int NumberOfPlatformsPassed { get; set; }
        //----------------------------------------------------------------------------------------------------

        public void AddScore()
        {
            int scoreUnit = HJConfigManager.Instance().GetScoreUnitForLevel(CurrentLevel);
            CurrentScore += scoreUnit;
            NumberOfPlatformsPassed++;
            HJGameEventHandler.Instance().TriggerOnScoreChangedEvent(CurrentScore);
        }
        //----------------------------------------------------------------------------------------------------

        public void RestartCurrentLevel()
        {
            CurrentScore = 0;
            NumberOfPlatformsPassed = 0;
        }
        //----------------------------------------------------------------------------------------------------

        public void LoadNextLevel()
        {
            NumberOfPlatformsPassed = 0;
            CurrentLevel = CurrentLevel + 1 > HJConfigManager.Instance().GetTotalNumberOfLevelsInGame() ? 1 : CurrentLevel + 1;
        }
        //----------------------------------------------------------------------------------------------------
    }
}