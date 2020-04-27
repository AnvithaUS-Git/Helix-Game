using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System;

namespace HelixJump
{
    public class HJConfigManager
    {
        private static HJConfigManager mInstance;
        private List<HJLevelDetails> mAllLevelDetails = new List<HJLevelDetails>();
        private const int kTotalGameLevels = 5;

        public static HJConfigManager Instance()
        {
            return mInstance = mInstance == null ? new HJConfigManager() : mInstance;
        }

        public HJConfigManager()
        {
            ReadAllConfigFromFile();
        }

        public void ReadAllConfigFromFile()
        {
            string filePath = string.Format(HJGameConstants.kConfigFilePath, HJPlayerScoreAndLevelManager.Instance().CurrentLevel);
            Debug.Log(filePath);
            TextAsset txt = (TextAsset)Resources.Load(filePath, typeof(TextAsset));
            string json = txt.text;
            
            if (mAllLevelDetails.FindIndex(x => x.LevelId == HJPlayerScoreAndLevelManager.Instance().CurrentLevel) < 0)
                mAllLevelDetails.Add(JsonConvert.DeserializeObject<HJLevelDetails>(json));
        }
        public List<HJPlatformDetails> GetPlatformDetailsForLevel(int level)
        {
            return mAllLevelDetails.Find(x => x.LevelId == level).PlatformDetails;
        }
        public int GetTotalPlatformCountForLevel(int level)
        {
            return mAllLevelDetails.Find(x => x.LevelId == level).PlatformDetails.Count;
        }

        public int GetTotalNumberOfLevelsInGame()
        {
            return kTotalGameLevels;
        }
        public int GetScoreUnitForLevel(int level)
        {
            return mAllLevelDetails.Find(x => x.LevelId == level).ScoreUnit;
        }
        public Color GetBackgroundColorForLevel(int level)
        {
            string color = mAllLevelDetails.Find(x => x.LevelId == level).BackGroundColor;
            return HJUtility.ConvertHexaToColor(color);
        }
        public Color GetPoleColorFor(int level)
        {
            string color = mAllLevelDetails.Find(x => x.LevelId == level).PoleColor;
            return HJUtility.ConvertHexaToColor(color);
        }
        public Color GetBallColorFor(int level)
        {
            string color = mAllLevelDetails.Find(x => x.LevelId == level).BallColor;
            return HJUtility.ConvertHexaToColor(color);
        }
        public Color GetBasePlatformColor(int level)
        {
            string color = mAllLevelDetails.Find(x => x.LevelId == level).BasePlatformSliceColor;
            return HJUtility.ConvertHexaToColor(color);
        }
        public float GetDistanceBetweenPlatforms(int level)
        {
            return mAllLevelDetails.Find(x => x.LevelId == level).DistanceBetPlatform;
        }
    }
}
