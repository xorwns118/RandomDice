using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawnManager_GameMode2 : MonoBehaviour
{
    public static MonsterSpawnManager_GameMode2 instance;

    public GameObject[] monsters;
    public GameObject[] bosses;
    public Transform myMonsterSpawnPosition;
    public Transform partnerMonsterSpawnPosition;
    public List<Monster> spawnMonsterList;

    public int monsterCount;
    public float spawnTime;
    public int speedMonsterOrder;
    public int bigMonsterOrder;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    //private void Start()
    //{
    //    monsterCount = 0;
    //    spawnTime = 3f;
    //    spawnMonsterList = new List<Monster>();
    //}

    //private void Update()
    //{
    //    spawnTime -= Time.deltaTime;

    //    if (spawnTime <= 0)
    //    {
    //        MonsterSpawnOrder();
    //        monsterCount++;
    //    }
    //}

    /// <summary>
    /// 몬스터 좌표에 따라 순서가 바뀌어야함.
    /// </summary>

    void MonsterSpawnOrder()
    {
        spawnTime = 3f;
        if (monsterCount != 0 && monsterCount % speedMonsterOrder == 0)
        {
            SpawnMonster(1);
            return;
        }
        else if (monsterCount != 0 && monsterCount % bigMonsterOrder == 0)
        {
            SpawnMonster(2);
            return;
        }
        SpawnMonster(0);
    }

    void SpawnMonster(int monsterIndex)
    {
        GameObject newMonster = GameObject.Instantiate(monsters[monsterIndex], myMonsterSpawnPosition);
        spawnMonsterList.Add(newMonster.GetComponent<Monster>());
    }

    public Monster GetFrontMonster()
    {
        if (spawnMonsterList.Count == 0) return null;

        Monster front = spawnMonsterList
        .OrderByDescending(m => {
            RectTransform rt = m.GetComponent<RectTransform>();
            Monster monster = m.GetComponent<Monster>();

            float positionValue = 0f;

            if (monster.isMovingY)
            {
                positionValue = rt.anchoredPosition.y;
            }
            else
            {
                positionValue = rt.anchoredPosition.x;
            }

            return positionValue;
        })
        .FirstOrDefault();

        Debug.Log(front.gameObject.name);

        return front?.GetComponent<Monster>();
    }
}
