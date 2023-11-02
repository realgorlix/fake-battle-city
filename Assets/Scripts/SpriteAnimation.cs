using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] anim_sprites;

    private int anim_step, anim_max;
    private SpriteRenderer sprite_renderer;

    private void Awake()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        sprite_renderer.sprite = anim_sprites[0];
        anim_max = anim_sprites.Length;
    }

    public void Animation_Step()
    {
        anim_step = (anim_step + 1) % anim_max;
        sprite_renderer.sprite = anim_sprites[anim_step];
    }
}
