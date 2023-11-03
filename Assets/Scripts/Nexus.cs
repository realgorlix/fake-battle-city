using System;
using UnityEngine;

public class Nexus : MonoBehaviour
{
    public event Action Hit;

    private SpriteAnimation sprite_animation;

    private void Awake()
    {
        sprite_animation = GetComponent<SpriteAnimation>();
    }

    public void Register_hit()
    {
        sprite_animation.Set_frame(1);
        Hit?.Invoke();
    }
}
