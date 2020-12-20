using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    public float speed;
    public float damage;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable idmg = other.gameObject.GetComponent<IDamageable>();
        if(idmg!=null)
            idmg.getDamage(damage);
        Destroy(gameObject);
    }
}
