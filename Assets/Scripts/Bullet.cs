using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float speed = 8f;

    private void Awake()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("LOOL");
        Destroy(gameObject);
    }
}
