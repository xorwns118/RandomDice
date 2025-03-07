using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public string diceName; // 이름
    public int currentValue; // 현재 눈금 값
    public int currentUpgradeLevel; // 현재 업그레이드 레벨
    public int maxUpgradeLevel; // 최대 업그레이드 레벨
    public int upgradeCost; // 업그레이드 비용
    public int attackRange; // 공격 범위
    public int attackTarget; // 공격 대상 수
    public int attackDamage; // 공격 데미지
    public float attckSpeed; // 공격 속도
    public string description; // 설명
}
