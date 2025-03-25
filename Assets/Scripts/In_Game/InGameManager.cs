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
    }

    public void OnGameOver()
    {
        Debug.Log("[Game Over]");
    }
}
