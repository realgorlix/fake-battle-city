using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Timeline;
using UnityEngine;

public class ComputerInput : MonoBehaviour
{
    [HideInInspector] public Map Map;
    [HideInInspector] public Game Game;

    private Tank tank;
    private bool driving = false;
    
    private float last_shooting_time = 0, shoot_wait_interval = 0, last_driving_time = 0;
    private int target_x = 0, target_y = 0;

    private const float position_error = 0.15f, error_timeout = 2f;

    private void Awake() => tank = GetComponent<Tank>();

    private void Update()
    {
        Vector3 current_pos = tank.gameObject.transform.position;
        if (driving)
        {
            if (Check_if_arrived(current_pos))
            {
                driving = false;
                tank.Move = Vector2.zero;
            }
        }
        else
        {
            Calculate_next_move(Mathf.RoundToInt(current_pos.x), Mathf.RoundToInt(current_pos.y));
        }
        float time = Time.time;
        if(time - last_driving_time >= error_timeout)
            driving = false;
        if(time - last_shooting_time >= shoot_wait_interval)
        {
            last_shooting_time = time;
            shoot_wait_interval = Random.Range(0.2f, 2f);
            tank.Shoot = true;
        }
        else
        {
            tank.Shoot = false;
        }
    }

    private bool Check_if_arrived(Vector3 current_position)
    {
        return (float)target_x - position_error <= current_position.x &&
            current_position.x <= (float)target_x + position_error &&
            (float)target_y - position_error <= current_position.y &&
            current_position.y <= (float)target_y + position_error;
    }

    private void Calculate_next_move(int pos_x, int pos_y)
    {
        List<Vector2> directions = new List<Vector2>
        {
            new Vector2(1,0),new Vector2(-1, 0),new Vector2(0, 1),new Vector2(0, -1)
        };
        while (directions.Count > 0)
        {
            int random_index = Random.Range(0, directions.Count);
            Vector2 rnd_dir = directions[random_index];
            directions.RemoveAt(random_index);
            target_x = pos_x + (int)rnd_dir.x;
            target_y = pos_y + (int)rnd_dir.y;
            if (target_x < 0 || target_y < 0 || target_x >= Map.fill_size||
                target_y >= Map.fill_size)
                continue;
            if (Map.fill_grid[target_x, target_y] == 0)
                continue;
            tank.Move = rnd_dir;
            driving = true;
            last_driving_time = Time.time;
            break;
        }
    }
}
