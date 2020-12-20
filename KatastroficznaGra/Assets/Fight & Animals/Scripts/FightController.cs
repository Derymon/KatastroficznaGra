using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : MonoBehaviour, IDamageable
{
    public GameObject hitVFX;

    public float hp=1000; // actually
    [SerializeField] float damage=10; // weapon/statistics
    [SerializeField] float attackSpeed=0.3f; // weapon+animation
    public float attackRange = 2f; // weapon

    protected Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected float timeAfterAttack=0;
    void Update()
    {
        timeAfterAttack += Time.deltaTime;
    }

    public virtual void attack(Transform c)
    {
        if (timeAfterAttack < 1 / attackSpeed)
            return;
        timeAfterAttack = 0;
        if (animator != null)
            animator.SetTrigger("attack");

        StartCoroutine(waitForEndAttack(c));
    }

    protected virtual IEnumerator waitForEndAttack(Transform c)
    {
        yield return new WaitForSeconds(0.5f/attackSpeed);

        if (Vector3.Distance(c.transform.position, transform.position) > attackRange)
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
        target.getDamage(damage);
    }

    public virtual void getDamage(float damage)
    {
        hp -= damage;
        if (hitVFX != null)
        {
            GameObject g = Instantiate(hitVFX);
            hitVFX.transform.position = transform.position;
            Destroy(g, 1f);
        }
        Debug.Log("Actually hp: " + hp);
        Debug.Log("Received damage: " + damage);
        if (hp < 1)
            kill();
    }

    public void kill()
    {
        Destroy(gameObject);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
