using UnityEngine;

public class Game : MonoBehaviour
{
    private Map map_gen;
    private int enemy_spawn_cycle = 0;
    private Nexus nexus;
    private Tank player; 

    private readonly Vector2 spawn = new Vector2(4, 0);
    private readonly Vector2[] enemy_spawns =
    {
        new Vector2(3,0)
    };

    private void Awake()
    {
        map_gen = GetComponent<Map>();
    }

    private void Start()
    {
        Setup_game();
        player = Instantiate(GameResources.Instance.Main_tank_prefab, spawn,Quaternion.identity).GetComponent<Tank>();
        Next_enemy(); // TEST
    }

    void Setup_game()
    {
        map_gen.Create_maze();
        nexus = Instantiate(GameResources.Instance.Nexus_prefab).GetComponent<Nexus>();
        nexus.Hit += Nexus_Hit;
    }

    private void Nexus_Hit()
    {
        player.GetComponent<HumanInput>().enabled = false;
    }

    private void Game_over()
    {

    }

    void Next_enemy()
    {
        GameObject test_enemy = Instantiate(GameResources.Instance.Enemy_tank_prefab, enemy_spawns[0], Quaternion.identity);
        test_enemy.GetComponent<ComputerInput>().Map = map_gen;
        test_enemy.GetComponent<ComputerInput>().Game = this;
    }
}

