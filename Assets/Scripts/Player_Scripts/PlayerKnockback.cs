using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    float mass = 3f;
    public float knockback;
    private Vector3 impact = Vector3.zero;
    private CharacterController character;
 
    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    void AddImpact(Vector3 force)
    {
        var dir = force.normalized;

        impact += dir.normalized * force.magnitude / mass;
    }

    void Update()
    {
        if (impact.magnitude > 0.2)
        {
            character.Move(impact * Time.deltaTime);
        }

        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        Vector3 dir = (transform.position - collision.transform.position).normalized;
        dir.y = 0;
        AddImpact(dir * knockback);
    }

}
