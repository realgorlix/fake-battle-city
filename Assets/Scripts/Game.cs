using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [HideInInspector] public Nexus Nexus;
    [HideInInspector] public Tank Player;

    [SerializeField] private HUD Hud;

    private Map map;
    private int player_lives, spawned_enemies, killed_enemies, score;
    private bool game_is_over = false;

    private const int max_enemies = 10, score_per_kill = 100;
    private readonly Vector2 spawn = new Vector2(4, 0);
    private readonly Vector2[] enemy_spawns =
    {
        new Vector2(0,12), new Vector2(6,12), new Vector2(12,12)
    };
    public readonly Vector2 Nexus_spawn = new Vector2(6, 0);

    private void Awake()
    {
        map = GetComponent<Map>();
    }

    private void Start()
    {
        Setup_game();
        Setup_player();
        StartCoroutine(Game_loop());
    }

    private void Setup_player()
    {
        Player = Instantiate(GameResources.Instance.Main_tank_prefab, spawn, Quaternion.identity).GetComponent<Tank>();
        Player.Died += Tank_died;
    }

    private void Setup_game()
    {
        player_lives = 3;
        Hud.Set_health_counter(player_lives);
        Hud.Set_enemies_counter(max_enemies - killed_enemies);
        map.Create_maze();
        Nexus = Instantiate(GameResources.Instance.Nexus_prefab, Nexus_spawn, Quaternion.identity)
            .GetComponent<Nexus>();
        Nexus.Hit += Nexus_hit;
    }

    private void Nexus_hit() => StartCoroutine(Game_over());

    private IEnumerator Game_over()
    {
        Unhook_game_events();
        Hud.Game_over();
        Player.GetComponent<HumanInput>().enabled = false;
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene(0);
        yield return null;
    }

    private IEnumerator Game_win()
    {
        Unhook_game_events();
        Hud.Game_win(score);
        Player.GetComponent<HumanInput>().enabled = false;
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene(0);
        yield return null;
    }

    private void Unhook_game_events()
    {
        if (Player is not null)
            Player.Died -= Tank_died;
        Nexus.Hit -= Nexus_hit;
        map.Cleanup();
    }

    private IEnumerator Game_loop()
    {
        while(!game_is_over && spawned_enemies < max_enemies)
        {
            Next_enemy();
            yield return new WaitForSecondsRealtime(2.5f);
        }
        yield return null;
    }

    private void Next_enemy()
    {
        spawned_enemies++;
        GameObject enemy_tank = Instantiate(GameResources.Instance.Enemy_tank_prefab, 
            enemy_spawns[Random.Range(0, enemy_spawns.Length)], Quaternion.identity);
        enemy_tank.GetComponent<ComputerInput>().Map = map;
        enemy_tank.GetComponent<ComputerInput>().Game = this;
        enemy_tank.GetComponent<Tank>().Died += Tank_died;
    }

    private void Tank_died(Tank tank)
    {
        tank.Died -= Tank_died;
        if(Player == tank)
        {
            player_lives--;
            Hud.Set_health_counter(player_lives);
            if (player_lives == 0)
            {
                StartCoroutine(Game_over());
                return;
            }
            Setup_player();
        }
        else
        {
            score += score_per_kill;
            killed_enemies++;
            Hud.Set_enemies_counter(max_enemies - killed_enemies);
            if (killed_enemies >= max_enemies)
                StartCoroutine(Game_win());
        }
    }
}

