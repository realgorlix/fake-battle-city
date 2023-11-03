using UnityEngine;

public class DestructibleWall : MonoBehaviour
{
    private SpriteRenderer[,] spriteRenderers = new SpriteRenderer[4,4];

    private void Awake()
    {
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        int x = -1;
        int y = 0;
        foreach(var sprite in sprites)
        {
            if (sprite.gameObject.layer == 8) // layer 8: destructible wall
            {
                x++;
                if (x >= 4)
                {
                    y++;
                    x = 0;
                }
                spriteRenderers[x,y] = sprite;
            }
        }
    }

    public void Break_Wall_Piece(SpriteRenderer piece)
    {
        piece.color = Color.black;
        piece.gameObject.GetComponent<Collider2D>().enabled = false;
    }

    public void Break_Wall_Piece(int x, int y)
    {
        spriteRenderers[x, y].color = Color.black;
        spriteRenderers[x, y].gameObject.GetComponent<Collider2D>().enabled = false;
    }
}
