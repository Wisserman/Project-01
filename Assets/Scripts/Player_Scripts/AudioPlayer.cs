using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip[] audioClips; //enter elements in order of priority
    public AudioSource test;

    //public AudioClip[]

    public void PlayClip(string clipName)
    {
        if (clipName == "Hit")
            test.PlayOneShot(audioClips[3], 5f);
        else if (clipName == "Idle")
            test.loop = false;
        else if (clipName == "Walking")
        {
            test.clip = audioClips[0];
            test.loop = true;
            test.PlayDelayed(.25f);
        }
        else if (clipName == "Sprinting")
        {
            test.clip = audioClips[0];
            test.loop = true;
            test.Play();
        }
        else if (clipName == "Landing")
            test.PlayOneShot(audioClips[0], 5f);
        else if (clipName == "Jumping")
            test.PlayOneShot(audioClips[1]);
        else if (clipName == "Invisible")
            test.PlayOneShot(audioClips[2]);
        else if (clipName == "Dead")
        {
            test.loop = false;
            test.PlayOneShot(audioClips[4]);
        }
        else
            Debug.LogError("AudioClip not found");
    }
}