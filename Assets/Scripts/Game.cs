using UnityEngine;

public class Game : MonoBehaviour
{
    void Start()
    {
        Instantiate(GameResources.Instance.Main_tank_prefab);
    }
}

