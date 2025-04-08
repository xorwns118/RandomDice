using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    public static InGameManager instance;

    [Header("My Field Info")]
    public Transform[] myDeckTransform;
    public int spawnCost = 10;
    public int sp = 100;

    [Header("UI")]
    public Text spText;
    public Text spawnCostText;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        Application.targetFrameRate = 60;

        for (int i = 0; i < myDeckTransform.Length; i++)
        {
            myDeckTransform[i].GetComponent<Image>().sprite = RandomSpawnManager.instance.useableDice[i].GetComponent<Image>().sprite;
        }
    }

    public void SpawnDiceButton()
    {
        sp -= spawnCost;
        spawnCost += 10;

        spText.text = sp.ToString();
        spawnCostText.text = spawnCost.ToString();
    }

    public void DestroyedMonster(int cost)
    {
        sp += cost;
        spText.text = InGameManager.instance.sp.ToString();
    }    

    public void OnGameOver()
    {
        Debug.Log("[Game Over]");
    }
}
