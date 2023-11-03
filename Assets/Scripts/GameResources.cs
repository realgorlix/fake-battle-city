using UnityEngine;

public class GameResources : MonoBehaviour
{
    private static GameResources instance;

    [HideInInspector] public static GameResources Instance { get { return instance; } }

    public Sprite 
        Main_tank_1, 
        Main_tank_2,
        Enemy_tank_1,
        Enemy_tank_2,
        Bullet,
        Brick_wall;

    public GameObject 
        Bullet_prefab, 
        Main_tank_prefab, 
        Brick_wall_prefab, 
        Indestructible_wall_prefab,
        Nexus_wall_prefab,
        Nexus_prefab,
        Enemy_tank_prefab;

    private void Awake() => instance = this;
}
