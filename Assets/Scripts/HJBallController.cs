using UnityEngine;

public class HJBallController : MonoBehaviour
{
    [SerializeField] private Rigidbody mBallRigidBody;
    [SerializeField] private float mImpulseForce = 5.0f;

    private bool mIgnoreNextCollison;

    private void OnCollisionEnter(Collision collision)
    {
        if (mIgnoreNextCollison)
            return;
        
        
        mBallRigidBody.velocity = Vector3.zero;
        mBallRigidBody.AddForce(Vector3.up * mImpulseForce, ForceMode.Impulse);
        mIgnoreNextCollison = true;
        Invoke("AllowNextCollision", 0.2f);
    }

    void AllowNextCollision()
    {
        mIgnoreNextCollison = false;
    }
}
