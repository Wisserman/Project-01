using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPatrolable
{
    void SetDestination(Transform[] dest);
    void SetDestination(Transform dest);
    void Move(Vector3 destination, Vector3 currentPos);
}

public interface IDamageable
{
    void TakeDamage(int dmgTaken);
    void Kill();
}

public interface IControlable
{
    void Control(GameObject newUI);
    void Release();
}