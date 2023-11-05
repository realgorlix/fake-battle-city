using System;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [HideInInspector] public Vector2 Move;
    [HideInInspector] public bool Shoot;
    [HideInInspector] public event Action<Tank> Died;

    public int Side = 0;

    private SpriteAnimation sprite_animation;
    private Rigidbody2D rb;

    private Bullet bullet = null;
    private Vector2 tank_forward = Vector2.up;

    private const float speed = 2f;

    private void Awake()
    {
        sprite_animation = GetComponent<SpriteAnimation>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Move.magnitude > 0f)
        {
            bool direction_changed = Move != tank_forward;
            if (direction_changed)
            {
                Round_Tank_Position();
                tank_forward = Move;
            }
            transform.rotation = 
                Quaternion.Euler(0,0, Vector2.SignedAngle(Vector2.up, tank_forward));
            sprite_animation.Loop = true;
        }
        else
        {
            sprite_animation.Loop = false;
        }
        rb.velocity = (Vector3)Move * speed;
        if(Shoot && bullet == null)
            Fire();
    }

    private void Fire()
    {
        bullet = Instantiate(GameResources.Instance.Bullet_prefab,
            transform.position + (Vector3)(tank_forward * 0.25f),
            Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, tank_forward)))
            .GetComponent<Bullet>();
        bullet.Owner = this;
    }

    private void Round_Tank_Position()
    {
        Vector3 rounded_position = transform.position * 10f;
        rounded_position.x = Mathf.Round(rounded_position.x / 5f) * 5f;
        rounded_position.y = Mathf.Round(rounded_position.y / 5f) * 5f;
        transform.position = rounded_position / 10f;
    }

    public void Kill()
    {
        Died?.Invoke(this);
        Destroy(gameObject);
    }
}
