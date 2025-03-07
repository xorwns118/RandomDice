using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationBar : MonoBehaviour
{
    public GameObject[] pannels;
    public const int STORE = 0, HOME = 1, MYDICE = 2;  
    public Toggle[] toggles;

    private void Start()
    {
        pannels[STORE].SetActive(false);
        pannels[HOME].SetActive(true);
        pannels[MYDICE].SetActive(false);

        toggles[STORE].isOn = false;
        toggles[HOME].isOn = true;
        toggles[MYDICE].isOn = false;
    }

    public void OnClickButton(int index)
    {
        for (int i = 0; i < pannels.Length; i++)
        {
            pannels[i].SetActive(i == index);
        }
    }
}
