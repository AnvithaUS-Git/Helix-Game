﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HelixJump
{
    public class HJPlatFormController : MonoBehaviour
    {
        private Color mBaseSliceColor = Color.white;
        [SerializeField] List<HJSinglePlatformPiece> mSinglePlatformSlice;
        private List<int> mTempSingleSliceRefId = new List<int>();
        HJPlatformDetails mCurrentPlatformDetails;
        private void Start()
        {


        }
        public void InitializePlatformForLevel(HJPlatformDetails platformDet)
        {
            mCurrentPlatformDetails = platformDet;

            mTempSingleSliceRefId = Enumerable.Range(0, HJGameConstants.kTotalSlicesInPlatform-1).ToList();

            foreach (int sliceId in GetSlicesToBeDeactivated())
            {
                mSinglePlatformSlice[sliceId].Initialize(sliceId, HJPlatformSliceType.eDisabledSlice);
                mTempSingleSliceRefId.Remove(sliceId);
            }

            foreach (int sliceId in GetSlicesThatAreDeathPoints())
            {
                mSinglePlatformSlice[sliceId].Initialize(sliceId, HJPlatformSliceType.eDeathSlice);
                mTempSingleSliceRefId.Remove(sliceId);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            HJPlayerScoreAndLevelManager.Instance().AddScore(1);
        }

        List<int> GetSlicesToBeDeactivated()
        {
            List<int> list = HJUtility.GetRandomNumber(mTempSingleSliceRefId, HJGameConstants.kTotalSlicesInPlatform - mCurrentPlatformDetails.BaseSliceCount);
            return list;
        }
        List<int> GetSlicesThatAreDeathPoints()
        {
            List<int> list = HJUtility.GetRandomNumber(mTempSingleSliceRefId, mCurrentPlatformDetails.DeathSliceCount);
            return list;
        }
    }
}