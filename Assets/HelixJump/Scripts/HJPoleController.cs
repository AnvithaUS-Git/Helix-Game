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
            InitializePlatformsForLevel();
        }

        private void OnEnable()
        {
            HJGameEventHandler.Instance().OnNextLevelLoad += InitializePlatformsForLevel;
        }
        private void OnDisable()
        {
            HJGameEventHandler.Instance().OnNextLevelLoad -= InitializePlatformsForLevel;

        }
        private void Update()
        {
            if (HJGameManager.Instance().CurrentGameState == HJGameState.eGamePlaying)
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
        }

        public void InitializePlatformsForLevel()
        {
            mPoleMaterial.material.color = HJConfigManager.Instance().GetPoleColorFor(HJPlayerScoreAndLevelManager.Instance().CurrentLevel);
            int level = HJPlayerScoreAndLevelManager.Instance().CurrentLevel;
            List<HJPlatformDetails> platformDetails = HJConfigManager.Instance().GetPlatformDetailsForLevel(level);
            float initailYPos = HJGameConstants.kFirstPlatformYPos;

            for (int i = 0; i < platformDetails.Count; i++)
            {
                GameObject platfm = GetPlatformGameObject(i);

                platfm.transform.localPosition = new Vector3(0, initailYPos - (HJConfigManager.Instance().GetDistanceBetweenPlatforms(level) * i), 0);

                platfm.GetComponent<HJPlatFormController>().InitializePlatformForLevel(platformDetails[i], i == platformDetails.Count - 1);
               
            }
            DeactivateRemainingPaltforms(platformDetails.Count);
        }

        GameObject GetPlatformGameObject(int index)
        {
            if (index < mInstantiatedPlatforms.Count)
                return mInstantiatedPlatforms[index];
            else
            {
                GameObject platform = Instantiate(mPlatformPrefab, this.transform);
                mInstantiatedPlatforms.Add(platform);
                return platform;
            }
        }

        void DeactivateRemainingPaltforms(int startIndex)
        {
            for(int i= startIndex;i<mInstantiatedPlatforms.Count;i++)
            {
                mInstantiatedPlatforms[i].SetActive(false);
            }
        }
    }
}