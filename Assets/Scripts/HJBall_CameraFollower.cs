using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HJBall_CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform mBall;
    private float mOffsetBetCamAndBall;
    private void Awake()
    {
        mOffsetBetCamAndBall = transform.position.y - mBall.position.y;
    }

    private void LateUpdate()
    {
        float currentYPosOfBall = mBall.position.y;
        this.transform.position = new Vector3(this.transform.position.x, currentYPosOfBall + mOffsetBetCamAndBall, this.transform.position.z);
    }
}
