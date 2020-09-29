using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int minDamage, maxDamage;

    private void OnCollisionEnter(Collision collision)
    {
        Health _health = collision.gameObject.GetComponent<Health>();
        if (_health != null)
            _health.TakeDamage(Random.Range(minDamage, maxDamage));
    }
}
