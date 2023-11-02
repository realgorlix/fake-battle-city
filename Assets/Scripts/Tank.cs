using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [HideInInspector] public Vector2 Move;
    [HideInInspector] public bool Shoot;

    private SpriteAnimation sprite_animation;

    private const float move_step = 1f / 16f;

    private void Awake()
    {
        sprite_animation = GetComponent<SpriteAnimation>();
    }

    private void FixedUpdate()
    {
        if (Move.magnitude > 0f)
        {
            gameObject.transform.rotation = 
                Quaternion.Euler(0,0, Vector2.SignedAngle(Vector2.up, Move));
            sprite_animation.Animation_Step();
        }

        transform.position += (Vector3)(Move * move_step);
    }
}
