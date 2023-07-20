using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] enemyPrefabs;
    public GameObject leftSpawner, rightSpawner;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    private bool roundSending = false;

    private int waves = 0;
    public int currentRound = 0;
    private int difficulty = 0;
    public int enemies = 0;
    

    // Update is called once per frame
    void Update()
    {
        while (roundSending)
        {
            if (countdown > 0)
            {
                SpawnWave();
                countdown = timeBetweenWaves;
            }

            countdown -= Time.deltaTime;
        }
    }

    void SpawnWave()
    {
        Debug.Log("Spawn a wave");

        switch (difficulty)
        {
            case 0:
                Debug.Log("Easy number of enemies (1-3)");
                break;
            case 1:
                Debug.Log("Medium number of enemies (3-5)");
                break;
            case 2:
                Debug.Log("Hard number of enemies (6-10)");
                break;

        }
        waves++;

        if (waves == 5)
            difficulty++;
        
    }

    void SpawnRound()
    {
        roundSending = true;
    }
}
