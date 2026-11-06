using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 moveOffset = new Vector3(0f, 0f, 8f);
    public float speed = 0.3f;
    private Vector3 startPos;
    void Start()
    {
        startPos = transform.position; 
    }

    
    void Update()
    {
        float t = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = startPos + moveOffset * t;
    }
}
