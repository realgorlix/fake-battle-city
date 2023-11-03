using UnityEngine;

public class HumanInput : MonoBehaviour
{
    private Tank tank;
    private KeyCode last_pressed;
    private const KeyCode shoot_key = KeyCode.Space;
    private readonly KeyCode[] movement_keys =
    {
        KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D
    };
    private readonly Vector2[] movement_directions =
    {
        new Vector2(0,1), new Vector2(-1,0), new Vector2(0,-1), new Vector2(1,0)
    };

    private void Awake() => tank = GetComponent<Tank>();

    private void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            KeyCode key = movement_keys[i];
            if (Input.GetKeyUp(key))
            {
                if(key == last_pressed)
                {
                    tank.Move = Vector2.zero;
                }
            }
            if (Input.GetKeyDown(key))
            {
                tank.Move = movement_directions[i];
                last_pressed = key;
                break;
            }
        }
        tank.Shoot = Input.GetKeyDown(shoot_key);
    }
}