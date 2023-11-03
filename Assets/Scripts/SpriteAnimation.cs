using System;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    public bool Loop = false;
    [HideInInspector] public event Action Loop_end;

    [SerializeField] private Sprite[] anim_sprites;
    [SerializeField] private float anim_interval = 1f/50f;

    private int anim_step, anim_max;
    private float anim_count;
    private SpriteRenderer sprite_renderer;

    private void Awake()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        sprite_renderer.sprite = anim_sprites[0];
        anim_max = anim_sprites.Length;
    }

    private void Update()
    {
        if (!Loop)
            return;
        anim_count += Time.deltaTime;
        if (anim_count >= anim_interval)
        {
            anim_count -= anim_interval;
            Animation_Step();
        }
    }

    private void Animation_Step()
    {
        int sequence = anim_step = (anim_step + 1) % anim_max;
        bool sequence_ended = sequence == 0;
        sprite_renderer.sprite = anim_sprites[anim_step];
        if (sequence_ended)
        {
            Loop_end?.Invoke();
        }
    }
}
