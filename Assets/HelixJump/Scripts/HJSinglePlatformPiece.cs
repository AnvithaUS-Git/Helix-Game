using System.Collections;
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
            }
        }
  
        public bool IsDeathSlice()
        {
            return mCurrentSliceType == HJPlatformSliceType.eDeathSlice;
        }

        public void OnHitDeathSlice()
        {
            //restart the level
        }
    }
}