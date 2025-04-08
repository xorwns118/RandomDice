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

    public Transform[] myDeckTransform;

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

    public void OnGameOver()
    {
        Debug.Log("[Game Over]");
    }
}
