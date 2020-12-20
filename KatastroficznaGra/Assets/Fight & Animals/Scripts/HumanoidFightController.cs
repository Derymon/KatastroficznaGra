using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidFightController : FightController
{
    [HideInInspector] public bool block;
    public Weapon activeWeapon;
    [SerializeField] GameObject arrow;
    [SerializeField] Transform respawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timeAfterAttack += Time.deltaTime;
    }

    public override void attack(Transform c)
    {
        if (timeAfterAttack < 1 / activeWeapon.attackSpeed)
            return;
        timeAfterAttack = 0;
        if (animator != null)
        {
            animator.SetTrigger("attack0");
            animator.SetFloat("speed", activeWeapon.attackSpeed);
        }

        StartCoroutine(waitForEndAttack(c));
    }

    protected override IEnumerator waitForEndAttack(Transform c)
    {
        yield return new WaitForSeconds(1f / activeWeapon.attackSpeed);
        //animator.SetTrigger("breakAnimation");

        if (activeWeapon.rangedWeapon)
        {
            GameObject g = Instantiate(arrow);
            g.transform.rotation = Camera.main.transform.rotation;
            g.transform.position = respawnPosition.position;
            g.GetComponent<FlyingObject>().damage = activeWeapon.damage;
            Destroy(g , 100f);
        }
        else
        {
            if (c == null)
                yield break;
            if (Vector3.Distance(c.transform.position, transform.position) > activeWeapon.range)
            {
                Debug.Log("Too far.");
                Debug.Log(Vector3.Distance(c.transform.position, transform.position));
                yield break;
            }

            IDamageable target = c.GetComponent<IDamageable>();
            if (target == null)
            {
                Debug.Log("This object can't be damaged");
                yield break;
            }
            target.getDamage(activeWeapon.damage);
        }
    }

    public override void getDamage(float damage)
    {
        hp -= block ? damage / 4 : damage;
        if (hitVFX != null)
        {
            GameObject g = Instantiate(hitVFX);
            hitVFX.transform.position = transform.position;
            Destroy(g, 1f);
        }
        Debug.Log("Actually hp: " + hp);
        Debug.Log("Received damage: " + damage);
        if (hp < 1)
            Debug.Log("Killed");
    }

    private new void OnDrawGizmosSelected()
    {
        if (activeWeapon != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, activeWeapon.range);
        }
    }
}
