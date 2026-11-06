using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float killHeight = -50f;
    private Rigidbody rb;
    private Transform stuckTo;
    private Vector3 localPosition;
    private bool isStuck;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isStuck) return;
        isStuck = true;
        stuckTo = collision.collider.transform;
        localPosition = stuckTo.InverseTransformPoint(transform.position);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
    }
    void LateUpdate()
    {
        if (transform.position.y < killHeight)
        {
            Destroy(gameObject);
            return;
        }
        if (!isStuck) return;
        if (stuckTo == null)
        {
            isStuck = false;
            rb.isKinematic = false;
            return;
        }
        transform.position = stuckTo.TransformPoint(localPosition);
    }
}
