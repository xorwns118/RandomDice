using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public string diceName;
    public int currentValue;
    public int currentUpgradeLevel;
    public int maxUpgradeLevel;
    public int upgradeCost;
    public int skillDamage;
    public int attackTarget;
    public int attackDamage;
    public float attackSpeed;
    public string description;
    public GameObject pip; // 1 ~ 6
    public GameObject star; // 7
    public List<GameObject> pips; // 1 ~ 7
}
