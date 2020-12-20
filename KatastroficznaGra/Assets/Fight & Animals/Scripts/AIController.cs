using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FightController))]
public class AIController : MonoBehaviour
{
    public bool aggresive;
    public float visibleRange = 5f;

    protected Transform target;
    protected FightController fc;
    protected CharacterMotor chm;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visibleRange);
    }
}
