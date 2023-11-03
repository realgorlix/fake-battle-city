using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [HideInInspector] public int[,] fill_grid = new int[13,13], maze_grid = new int[7, 7];

    //private int score = 0, lives = 3;
    private List<GameObject> map_contents = new List<GameObject>();

    [HideInInspector] public const int maze_size = 7, fill_size = 13, wall_depth = 2;

    public void Create_maze()
    {
        Cleanup();
        Recursive_maze_generation(-1, -1, 0, 0);
        Fill_maze();
    }

    private void Fill_maze()
    {
        map_contents.Add(Instantiate(GameResources.Instance.Nexus_wall_prefab));

        for (int x = 0; x < fill_size; x++)
            for (int y = 0; y < fill_size; y++)
                if (fill_grid[x, y] == 0)
                    map_contents.Add(
                        Instantiate(Random.Range(0, 5) == 0 ?
                            GameResources.Instance.Indestructible_wall_prefab :
                            GameResources.Instance.Brick_wall_prefab,
                            new Vector3(x, y, wall_depth), Quaternion.identity)
                        );
    }

    private void Recursive_maze_generation(int prev_x, int prev_y, int x, int y)
    {
        if (x < 0 || y < 0 || x >= maze_size || y >= maze_size || maze_grid[x, y] == 1)
            return;

        maze_grid[x, y] = 1;
        fill_grid[x * 2, y * 2] = 1;

        if(prev_x != -1 && prev_y != -1)
            fill_grid[x * 2 + (prev_x - x), y * 2 + (prev_y - y)] = 1;

        List<int> directions = new List<int>
        {
            0,1,2,3
        };

        while (directions.Count > 0)
        {
            int random_index = Random.Range(0, directions.Count);
            int random_direction = directions[random_index];
            directions.RemoveAt(random_index);

            switch(random_direction)
            {
                case 0:
                    Recursive_maze_generation(x, y, x + 1, y);
                    break;
                case 1:
                    Recursive_maze_generation(x, y, x - 1, y);
                    break;
                case 2:
                    Recursive_maze_generation(x, y, x, y + 1);
                    break;
                case 3:
                    Recursive_maze_generation(x, y, x, y - 1);
                    break;
            }
        }
    }

    public void Cleanup()
    {
        foreach(GameObject obj in map_contents)
            Destroy(obj);

        map_contents.Clear();

        maze_grid[3, 0] = 1; // preserving nexus area
        maze_grid[2, 0] = 1; // preserving player spawn area

        for (int pre_x = 3; pre_x < 8; pre_x++)
            for (int pre_y = 0; pre_y < 2; pre_y++)
                fill_grid[pre_x, pre_y] = 1; // preserving area for nexus and player spawn
    }
}
