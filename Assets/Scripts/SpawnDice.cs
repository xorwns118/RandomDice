using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnDice : MonoBehaviour
{
    public GameObject[] useableDice;
    //public static GameObject[] dice;
    public Transform[] myDeckTransform;
    public Transform[] diceSpawnPoints;
    public bool[] isHasDice;

    private void Start()
    {
        isHasDice = new bool[diceSpawnPoints.Length];

        for (int i = 0; i < myDeckTransform.Length; i++)
        {
            GameObject.Instantiate(useableDice[i], myDeckTransform[i]);
        }
    }

    public void SpawnDiceInRandomPoint()
    {
        int randomDice = Random.Range(0, useableDice.Length);
        int randomSpawnPoint = Random.Range(0, diceSpawnPoints.Length);

        if (!isHasDice.Contains(false))
        {
            Debug.Log("주사위를 소환할 공간이 없습니다.");
            return;
        }

        if(!isHasDice[randomSpawnPoint])
        {
            GameObject.Instantiate(useableDice[randomDice], diceSpawnPoints[randomSpawnPoint]);
            isHasDice[randomSpawnPoint] = true;
            return;
        }
        else
        {
            SpawnDiceInRandomPoint();
        }
    }
}
