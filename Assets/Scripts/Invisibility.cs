using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : Ability
{
    public int invisTimer;
    public bool isInvis;
    public SkinnedMeshRenderer playerSkin;

    public override void Use()
    {
        if (playerSkin.enabled == true)
        {
            StartCoroutine("InvisTimer");
        }
    }

    IEnumerator InvisTimer()
    {
        playerSkin.enabled = false;
        yield return new WaitForSeconds(invisTimer);
        playerSkin.enabled = true;
    }
}
