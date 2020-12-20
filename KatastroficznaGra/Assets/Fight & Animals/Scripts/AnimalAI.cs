using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAI : AIController
{
    void Start()
    {
        fc = GetComponent<FightController>();
        chm = GetComponent<CharacterMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if(aggresive && target == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, visibleRange);
            foreach(Collider c in colliders)
            {
                if (c.gameObject == gameObject)
                    continue;
                FightController f = c.GetComponent<FightController>();
                if(f!=null)
                {
                    target = f.transform;
                    break;
                }
            }
        }
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.position) < fc.attackRange)
            {
                fc.attack(target);
            }
            else
            {
                Vector3 direction = (target.position - Vector3.up - transform.position).normalized;
                chm.move(direction);
            }
            if (Vector3.Distance(transform.position, target.position) > visibleRange)
                target = null;
        }
    }
}
