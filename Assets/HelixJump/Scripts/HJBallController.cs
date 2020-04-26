using UnityEngine;

namespace HelixJump

{
    public class HJBallController : MonoBehaviour
    {
        [SerializeField] private Renderer mBallMeshRenderer;
        [SerializeField] private Rigidbody mBallRigidBody;
        [SerializeField] private float mImpulseForce = 5.0f;

        private bool mIgnoreNextCollison;
        private Vector3 mInitailBallPos;

        void Awake()
        {
            mInitailBallPos = this.transform.position;
        }
        private void Start()
        {
            mBallMeshRenderer.material.color = HJConfigManager.Instance().GetBallColorFor(HJPlayerScoreAndLevelManager.Instance().CurrentLevel);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (mIgnoreNextCollison)
                return;

            HJSinglePlatformPiece slice = collision.gameObject.GetComponent<HJSinglePlatformPiece>();
            if (slice.IsDeathSlice())
                slice.OnHitDeathSlice();

            mBallRigidBody.velocity = Vector3.zero;
            mBallRigidBody.AddForce(Vector3.up * mImpulseForce, ForceMode.Impulse);
            mIgnoreNextCollison = true;
            Invoke("AllowNextCollision", 0.2f);
        }

        void AllowNextCollision()
        {
            mIgnoreNextCollison = false;
        }

        public void ResetBallPosition()
        {
            this.transform.position = mInitailBallPos;
        }
    }
}