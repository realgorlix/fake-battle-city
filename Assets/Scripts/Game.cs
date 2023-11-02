using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject main_tank;

    void Start()
    {
        Instantiate(main_tank);
    }
}

