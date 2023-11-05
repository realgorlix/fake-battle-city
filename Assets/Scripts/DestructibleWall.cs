using UnityEngine;

public class DestructibleWall : MonoBehaviour
{
    public Map Map;

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
        Destroyed_check();
    }

    private void Destroyed_check()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (spriteRenderers[x,y].gameObject.GetComponent<Collider2D>().enabled)
                    return;
            }
        }
        Vector3 position = transform.position;
        int pos_x = Mathf.RoundToInt(position.x), pos_y = Mathf.RoundToInt(position.y);
        if(Map is not null)
            Map.fill_grid[pos_x, pos_y] = 1;
    }
}
