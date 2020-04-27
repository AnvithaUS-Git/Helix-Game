using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelixJump
{
    public class HJBall_CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform mBall;
        [SerializeField] private Camera mCamera;
        [SerializeField] private float mSmoothness;
        private float mOffsetBetCamAndBall;
        private Vector3 mVelocity;
        private Vector3 mInitialCameraPos;
        private void Awake()
        {
            mOffsetBetCamAndBall = transform.position.y - mBall.position.y;
            mInitialCameraPos = transform.position;
        }
        private void OnEnable()
        {
            HJGameEventHandler.Instance().OnPlayerDeathEvent += ResetCameraPos;
        }

        private void OnDisable()
        {
            HJGameEventHandler.Instance().OnPlayerDeathEvent -= ResetCameraPos;
        }
        private void Start()
        {
            mCamera.backgroundColor = HJConfigManager.Instance().GetBackgroundColorForLevel(HJPlayerScoreAndLevelManager.Instance().CurrentLevel);
        }

        private void LateUpdate()
        {
            float currentYPosOfBall = mBall.position.y;
            if (currentYPosOfBall + mOffsetBetCamAndBall < this.transform.position.y)
            {
                transform.position = new Vector3(this.transform.position.x, currentYPosOfBall + mOffsetBetCamAndBall, this.transform.position.z);
                //this.transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref mVelocity, mSmoothness);
            }

        }
        void ResetCameraPos()
        {
            transform.position = mInitialCameraPos;
        }
    }
}