using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager_GameMode2 : MonoBehaviour
{
    public GameObject[] monsters;
    public GameObject[] bosses;
    public Transform myMonsterSpawnPosition;
    public Transform partnerMonsterSpawnPosition;

    public float spawnTime = 3f;

    void Update()
    {
        spawnTime -= Time.deltaTime;

        if(spawnTime <= 0)
        {
            spawnTime = 3f;
            SpawnMonster();
        }
    }

    void SpawnMonster()
    {
        int randomMonster = Random.Range(0, monsters.Length);

        GameObject.Instantiate(monsters[randomMonster], myMonsterSpawnPosition);
    }
}
