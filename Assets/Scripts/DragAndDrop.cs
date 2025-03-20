using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Vector3 defaultPos;
    public Transform parentPanel;
    private static List<Dice> activeTargets = new(); // 합칠 수 있는 대상
    private static List<Dice> nonTargets = new(); // 합칠 수 없는 대상

    // 드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        Dice currentDice = GetComponentInChildren<Dice>();
        defaultPos = transform.position;

        currentDice.transform.parent.SetAsLastSibling();
        currentDice.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        // 배경을 어둡게 하고, 같은 주사위만 활성화
        HighlightMergeTargets(currentDice);

        // 드래그 중, 레이캐스트 타겟 해제 (다른 UI 요소 방해 방지)
        this.GetComponentInChildren<Image>().raycastTarget = false;
    }

    // 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 currentPos = Camera.main.ScreenToWorldPoint(eventData.position);
        currentPos.z = 0;
        transform.position = currentPos;
    }

    // 드래그 종료
    public void OnEndDrag(PointerEventData eventData)
    {
        Dice currentDice = GetComponentInChildren<Dice>();
        Dice mergeTarget = GetMergeTarget(currentDice);

        if (mergeTarget != null)
        {
            // 같은 주사위와 합쳐짐
            RandomSpawnManager.instance.MergeDice(mergeTarget, currentDice);
            transform.position = defaultPos;
        }
        else
        {
            // 합칠 주사위 없을 때, 원래 자리로 복귀
            transform.position = defaultPos;
        }

        // 배경 밝기 복원, 레이캐스트 다시 활성화
        ResetMergeTargets();
        this.GetComponentInChildren<Image>().raycastTarget = true;

        currentDice.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    // 같은 이름의 주사위만 활성화, 배경 어둡게 처리
    private void HighlightMergeTargets(Dice currentDice)
    {
        activeTargets.Clear();
        nonTargets.Clear();
        Dice[] allDices = parentPanel.transform.GetComponentsInChildren<Dice>();

        foreach (Dice dice in allDices)
        {
            Image diceImage = dice.GetComponent<Image>();

            if (dice != currentDice && dice.diceName == currentDice.diceName && dice.currentValue == currentDice.currentValue)
            {
                activeTargets.Add(dice);
                diceImage.color = new Color(1f, 1f, 1f, 1f); // 밝게 표시
                continue;
            }

            nonTargets.Add(dice);

            if(dice == currentDice)
            {
                diceImage.color = new Color(1f, 1f, 1f, 1f);
                continue;
            }

            diceImage.color = new Color(1f, 1f, 1f, 0.5f); // 반투명 표시
        }
    }

    // 같은 이름의 주사위 중 가장 가까운 것 찾기
    private Dice GetMergeTarget(Dice currentDice)
    {
        Dice closest = null;
        float minDistance = float.MaxValue;

        foreach (Dice dice in activeTargets)
        {
            float distance = Vector3.Distance(dice.transform.parent.TransformPoint(dice.transform.localPosition), 
                                              currentDice.transform.parent.TransformPoint(currentDice.transform.localPosition));

            if (distance < minDistance)
            {
                minDistance = distance;
                closest = dice;
            }
        }

        if(90f <= minDistance && minDistance < 90.00005f)
        {
            return closest;
        }
        return null;
    }


    // 배경 색상 원래대로 복원
    private void ResetMergeTargets()
    {
        foreach (Dice dice in nonTargets)
        {
            dice.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        nonTargets.Clear();
    }
}