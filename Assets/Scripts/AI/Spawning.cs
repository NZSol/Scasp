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
    RoundHandler handler;
    int smallEnemiesWave;
    int largeEnemiesWave;

    int enemies;
    wave currentWave = wave.Wave0;


    float spawnTime;
    float waveCounter = 300;
    

    private void Start()
    {
        handler = GameObject.Find("God").GetComponent<RoundHandler>();
        SetEnemiesToSpawn();
    }
    void SetEnemiesToSpawn()
    {
        enemies = (int)currentWave;
        spawnTime = 60/ (enemies/4);
        smallEnemiesWave = (int)currentWave/2;
        largeEnemiesWave = (int)currentWave/2;
    }

    bool PauseSpawn = false;
    bool RoundEnd = false;

    private void Update()
    {
        if (handler.currentTime % 60 / spawnTime == 0)
        {
            PauseSpawn = true;
            Spawn();
        }
        else if (handler.currentTime % 60 / spawnTime != 0 && PauseSpawn)
            PauseSpawn = false;


        if(handler.currentTime % 60 == 0)
        {
            RoundEnd = true;
            currentWave = (wave)((int)currentWave + 8);
        }
        else if (handler.currentTime % 60 != 0 && PauseSpawn)
        {
            RoundEnd = false;
        }
    }


    void Spawn()
    {

    }



}
