using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject destoryWhenStart;
    void Start()
    {
        Destroy(destoryWhenStart);
    }
}
