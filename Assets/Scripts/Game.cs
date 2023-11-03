using UnityEngine;

public class Game : MonoBehaviour
{
    private readonly Vector2 spawn = new Vector2(4, 0);
    void Start()
    {
        Instantiate(GameResources.Instance.Main_tank_prefab, spawn,Quaternion.identity);
    }
}

