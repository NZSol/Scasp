using System;
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
    GameObject target;
    [SerializeField]
    Transform[] spawnPoints;
    [SerializeField]
    GameObject SmallHostile, BigHostile;
    RoundHandler handler;
    MultiplayerHandler MHandler;
    int smallEnemiesWave;
    int largeEnemiesWave;

    int enemies;
    wave currentWave = wave.Wave0;


    float spawnTime;
    

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Tank");
        handler = GameObject.Find("God").GetComponent<RoundHandler>();
        MHandler = handler.gameObject.GetComponent<MultiplayerHandler>();
        spawnPoints = gameObject.GetComponentsInChildren<Transform>();
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
        if (handler.currentTime % spawnTime == 0 && MHandler.gameStarted && !PauseSpawn)
        {
            // print($"scene timer = {handler.currentTime} % (60 / spawnTime));
            PauseSpawn = true;
            Spawn();
        }
        else if (handler.currentTime % spawnTime != 0 && PauseSpawn)
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

    Vector3 GetSpawnPoint()
    {
        float distance;
        Vector3 targetSpawn;
        do
        {
            targetSpawn = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].position;
            distance = Vector3.Distance(targetSpawn, target.transform.position);

        } while (distance < 50);
        return targetSpawn;
    }

    void Spawn()
    {
        for (int i = 0; i < 2; i++)
        {
            Instantiate(SmallHostile, GetSpawnPoint(), transform.rotation);
            Instantiate(BigHostile, GetSpawnPoint(), transform.rotation);
        }
    }
}
