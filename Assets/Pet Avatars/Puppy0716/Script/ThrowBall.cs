using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public float throwSpeed = 10f;
    private Rigidbody rb;
    private bool isThrown = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    public void ThrowOrStop()
    {
        if (!isThrown)
        {
            isThrown = true;
            rb.linearVelocity = new Vector3(0, throwSpeed, throwSpeed);
        }
        else if (transform.position.y <= 0f)
        {
            StopAtGround();
        }
    }

    private void StopAtGround()
    {
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;
        isThrown = false; // Reset to allow re-throwing if necessary
    }
}
