using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class GameResources : MonoBehaviour
{
    private static GameResources instance;

    [HideInInspector] public static GameResources Instance { get { return instance; } }

    public Sprite Main_tank_1, 
        Main_tank_2,
        Enemy_tank_1,
        Enemy_tank_2,
        Bullet,
        Brick_wall;

    public GameObject Bullet_prefab, Main_tank_prefab;

    private void Awake()
    {
        instance = this;
    }
}
