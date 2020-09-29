using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    [SerializeField] ParticleSystem invisEffect;
    [SerializeField] ParticleSystem hitEffect;
    public void SummonParticles(string name)
    {/*
        if (name == "Hit")
            
        else if (name == "Landing")
       */
        if (name == "Invisible")
            invisEffect.Play();
        else if (name == "Hit")
            hitEffect.Play();
        else
            Debug.LogError("Particle not found");
    }
}
