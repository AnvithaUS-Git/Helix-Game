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
            //string fileData = string.Empty;
            //List<HJLevelDetails> disList = null;
            //using (StreamReader reader = new StreamReader(HJGameConstants.kConfigFilePath))
            //{
            //    fileData = reader.ReadToEnd();
            //    disList = JsonConvert.DeserializeObject<List<HJLevelDetails>>(fileData) ?? new List<HJLevelDetails>();
            //}
            TextAsset txt = (TextAsset)Resources.Load(HJGameConstants.kConfigFilePath, typeof(TextAsset));
            string json = txt.text;
            mAllLevelDetails = JsonConvert.DeserializeObject<List<HJLevelDetails>>(json);
        }
        public List<HJPlatformDetails> GetPlatformDetailsForLevel(int level)
        {
            return mAllLevelDetails.Find(x => x.LevelId == level).PlatformDetails;
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
