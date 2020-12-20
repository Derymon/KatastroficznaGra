using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon" )]
public class Weapon : ScriptableObject
{
    public bool onehanded;
    public bool rangedWeapon;

    public float damage;
    public float range;
    public float attackSpeed;

    public GameObject weaponPrefab;
}
