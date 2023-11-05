using System.Collections.Generic;
using UnityEngine;

public class ComputerInput : MonoBehaviour
{
    [HideInInspector] public Map Map;
    [HideInInspector] public Game Game;

    private Tank tank;
    private bool driving = false;
    private float last_shooting_time = 0, shoot_wait_interval = 0, last_driving_time = 0;
    private int target_x = 0, target_y = 0;

    private const float position_error = 0.1f, error_timeout = 1f;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.TryGetComponent<Tank>(out Tank collided_with))
            if (driving)
                driving = false;
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
        int decrementer = 0;
        for (int i = 0; i < 4; i++)
        {
            Vector2 dir = directions[i - decrementer];
            target_x = pos_x + (int)dir.x;
            target_y = pos_y + (int)dir.y;
            if (target_x < 0 || target_y < 0 || target_x >= Map.Fill_size || target_y >= Map.Fill_size)
                goto remove_index;
            if (Map.fill_grid[target_x, target_y] == Map.Blank_tile || 
                Map.fill_grid[target_x, target_y] == Map.Destructible_wall_type)
                continue;
            remove_index:
            directions.RemoveAt(i - decrementer);
            decrementer++;
        }
        if (directions.Count <= 0)
            return;
        bool randomize_behaviour = Random.Range(0, 3) == 0;
        if(randomize_behaviour)
        {
            tank.Move = directions[Random.Range(0, directions.Count)];
            goto final_touch;
        }
        float best_dot = -1;
        Vector2 best_dir = (Game.Nexus_spawn - new Vector2(pos_x,pos_y)).normalized;
        foreach (Vector2 dir in directions)
        {
            float cur_dot = Vector2.Dot(dir, best_dir);
            if (cur_dot > best_dot)
            {
                best_dot = cur_dot;
                tank.Move = dir;
            }
        }
        final_touch:
        driving = true;
        last_driving_time = Time.time;
    }
}
