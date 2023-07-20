using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesLeft = 0;

    public Wave[] waves;
    public List<Transform> spawners;
    public float timeBetweenWaves = 5f;
    [SerializeField] private TMP_Text roundText;

    [Header("Enemy Prefabs")]
    public GameObject enemy1_black;
    public GameObject enemy2_red, enemy3_bigBlack, enemy4_bigRed, enemy5_fire, enemy6_Tank;

    
    private float countdown = 2f;

    private int waveIdx = 0;
    private int spawnIdx = 0;
    

    // Update is called once per frame
    void Update()
    {
        if(EnemiesLeft > 0)
        {
            return;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        
        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
    }

    IEnumerator SpawnWave()
    {
        Debug.Log("Spawn a Round");
        roundText.text = ("ROUND " + (waveIdx+1)).ToString();
        Wave wave = waves[waveIdx];

        //Spawn Black Tanks
        for (int i = 0; i < wave.count1; i++)
        {
            SpawnEnemy(enemy1_black);
            yield return new WaitForSeconds(1f / wave.rate1);
        }

        //Spawn Red Tanks
        for (int i = 0; i < wave.count2; i++)
        {
            SpawnEnemy(enemy2_red);
            yield return new WaitForSeconds(1f / wave.rate2);
        }

        //Spawn Big Black Tanks
        for (int i = 0; i < wave.count3; i++)
        {
            SpawnEnemy(enemy3_bigBlack);
            yield return new WaitForSeconds(1f / wave.rate3);
        }

        //Spawn Big Red Tanks
        for (int i = 0; i < wave.count4; i++)
        {
            SpawnEnemy(enemy4_bigRed);
            yield return new WaitForSeconds(1f / wave.rate4);
        }

        //Spawn Fire Tanks
        for (int i = 0; i < wave.count5; i++)
        {
            SpawnEnemy(enemy5_fire);
            yield return new WaitForSeconds(1f / wave.rate5);
        }

        //Spawn Boss Tank
        for (int i = 0; i < wave.count6; i++)
        {
            SpawnEnemy(enemy6_Tank);
            yield return new WaitForSeconds(1f / wave.rate6);
        }

        waveIdx++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        if(waveIdx < 10)
        {
            Instantiate(enemy, spawners[spawnIdx].position, spawners[spawnIdx].rotation);
        }
        else
        {
            Instantiate(enemy, spawners[spawnIdx].position, spawners[spawnIdx].rotation);
            spawnIdx++;
            if (spawnIdx > spawners.Count - 1)
                spawnIdx = 0;
        }
        EnemiesLeft++;
    }
}
