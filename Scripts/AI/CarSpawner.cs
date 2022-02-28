using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefabs;

    private void Start()
    {
        Instantiate(carPrefabs, transform);
    }
}
