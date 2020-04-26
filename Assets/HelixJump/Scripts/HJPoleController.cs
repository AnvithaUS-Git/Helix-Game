using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelixJump
{
    public class HJPoleController : MonoBehaviour
    {
        [SerializeField] private Renderer mPoleMaterial;
        [SerializeField] private GameObject mPlatformPrefab;
        private Vector3 mLastTapPosition;
        private List<GameObject> mInstantiatedPlatforms = new List<GameObject>();
        private void Start()
        {
            mPoleMaterial.material.color = HJConfigManager.Instance().GetPoleColorFor(HJPlayerScoreAndLevelManager.Instance().CurrentLevel);
            InitializePlatformsForLevel(1);
        }
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 currentTapPos = Input.mousePosition;

                if (mLastTapPosition == Vector3.zero)
                    mLastTapPosition = currentTapPos;
                float xDelta = mLastTapPosition.x - currentTapPos.x;
                mLastTapPosition = currentTapPos;

                this.transform.Rotate(Vector3.up * xDelta);

            }
            if (Input.GetMouseButtonUp(0))
            {
                mLastTapPosition = Vector3.zero;
            }
        }

        public void InitializePlatformsForLevel(int level)
        {
            List<HJPlatformDetails> platformDetails = HJConfigManager.Instance().GetPlatformDetailsForLevel(level);
            float initailYPos = HJGameConstants.kFirstPlatformYPos;
           
            for (int i = 0; i < platformDetails.Count; i++)
            {
                GameObject platfm = Instantiate(mPlatformPrefab, this.transform);
                
                platfm.transform.localPosition = new Vector3(0, initailYPos - (HJConfigManager.Instance().GetDistanceBetweenPlatforms(level) * i), 0);
                
                platfm.GetComponent<HJPlatFormController>().InitializePlatformForLevel(platformDetails[i]);
                mInstantiatedPlatforms.Add(platfm);
            }
        }
    }
}