using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPad : MonoBehaviour
{
    public int damage;
    public float damageRate;

    private List<IDamagable> things = new List<IDamagable>();

    void Start()
    {
        InvokeRepeating("DealDamage", 0, damageRate);
    }


    void DealDamage()
    {
        for(int i = 0; i < things.Count; i++) 
        {
            things[i].TakePhysicalDamage(damage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out IDamagable damagable))
        {
            things.Add(damagable);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out IDamagable damagable))
        {
            things.Remove(damagable);
        }

    }
}
