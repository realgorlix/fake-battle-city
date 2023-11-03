using UnityEngine;

public class ComputerInput : MonoBehaviour
{
    private Tank tank;

    private void Awake()
    {
        tank = GetComponent<Tank>();
    }
}
