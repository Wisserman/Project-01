using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    protected AudioPlayer _audioPlayer;
    protected ParticleScript _particleScript;
    public bool inUse = false;
    public abstract void Use();

    public virtual void Use(Transform origin, Transform target){}
}
