using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitMyDice : MonoBehaviour
{
    public GameObject diceBoxPref;
    public GameObject[] dicePrefs;
    public Transform contentTransform;

    void Start()
    {
        StartCoroutine(InitDicesInfo());
    }

    // 가지고 있는 주사위를 화면에 표시
    IEnumerator InitDicesInfo()
    {
        for (int i = 0; i < dicePrefs.Length; i++)
        {
            GameObject newPrefs = GameObject.Instantiate(diceBoxPref);
            GameObject dice = GameObject.Instantiate(dicePrefs[i]);

            newPrefs.transform.SetParent(contentTransform);
            newPrefs.transform.localScale = new Vector3(1, 1, 1);

            dice.transform.SetParent(newPrefs.transform);
            dice.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 55);
            dice.transform.localScale = new Vector3(1, 1, 1);
            dice.transform.localPosition = new Vector3(0, 5, 0);

            // 일반적인 position 사용 시 부모객체의 영향 없이 월드 좌표계 기준으로 설정되어 원치 않은 좌표로 설정 될 수 있음.
            // 부모 객체를 기준으로 위치를 설정하려면 localPosition 이 적절함.
        }

        yield return null;
    }
}
