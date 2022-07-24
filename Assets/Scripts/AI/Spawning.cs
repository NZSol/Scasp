using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum wave
{
    Wave0 = 20,
    Wave1 = 28,
    Wave2 = 36,
    Wave3 = 44,
    Wave4 = 52
}

public class Spawning : MonoBehaviour
{
    [SerializeField]
    GameObject SmallHostile, BigHostile;
    int smallEnemiesWave;
    int largeEnemiesWave;

    int enemies;
    wave currentWave = wave.Wave0;

    float timer = 0;
    float minute = 60;

    float spawnTime;
    

    private void Start()
    {
        SetEnemiesToSpawn();
    }
    void SetEnemiesToSpawn()
    {
        enemies = largeEnemiesWave + smallEnemiesWave;
        spawnTime = (enemies / 4) / minute;
        smallEnemiesWave = (int)currentWave/2;
        largeEnemiesWave = (int)currentWave/2;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > spawnTime)
        {
            Spawn();
        }
        if(timer > 60)
        {
            timer -= timer;
            currentWave = (wave)((int)currentWave + 8);
        }
    }


    void Spawn()
    {

    }



}
