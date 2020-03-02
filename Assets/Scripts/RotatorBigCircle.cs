using UnityEngine;

public class RotatorBigCircle : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Rotate(0f, 0f, 10 * speed * Time.deltaTime, Space.Self);
    }
}
