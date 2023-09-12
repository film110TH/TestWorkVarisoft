using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    private Transform[] spawnpoint;
    public SpawnerClass spawnerClass; 
    public List<GameObject> CurrentEnemy = new List<GameObject>();

    private bool respawnable;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        getSpawnpoint();
        AllSpawn();

        respawnable = true;
    }

    private void Update()
    {
        Resport();
    }

    private void getSpawnpoint()
    {
        spawnpoint = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount;i++)
            spawnpoint[i] = transform.GetChild(i).transform;
    }

    private void AllSpawn()
    {
        for(int i = 0; i < spawnerClass.NumofEnemySpawn; i++)
        {
            GameObject _enemy = Instantiate(spawnerClass.Enemy[UnityEngine.Random.Range(0, spawnerClass.Enemy.Length)],
                spawnpoint[UnityEngine.Random.Range(0,spawnpoint.Length)].position,Quaternion.identity);
            CurrentEnemy.Add(_enemy);
        }
    }

    private async void Resport()
    {
        if (!respawnable)
            return;

        if (CurrentEnemy.Count < spawnerClass.NumofEnemySpawn)
        {
            Debug.Log("Respawn");
            GameObject _enemy = Instantiate(spawnerClass.Enemy[UnityEngine.Random.Range(0, spawnerClass.Enemy.Length)],
                    spawnpoint[UnityEngine.Random.Range(0, spawnpoint.Length)].position, Quaternion.identity);
            CurrentEnemy.Add(_enemy);
        }
        respawnable = false;
        respawnable = await Tool.Delaybool(respawnable, 5f);
    }

    public void removeEnemyObject(GameObject enemy)
    {
        if(CurrentEnemy.Contains(enemy))
            CurrentEnemy.Remove(enemy);
    }
}

[Serializable]
public class SpawnerClass
{
    public int NumofEnemySpawn;
    public GameObject[] Enemy;
} 
