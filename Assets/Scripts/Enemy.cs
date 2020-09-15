using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Enemy : MonoBehaviour
{
    float detectDist = 10f;
    //int health = 1;

    void Start()
    {

    }

    void Update()
    {
        GameObject player = GameObject.Find("Invisibility(Clone)");
        if (player.GetComponent<Invisibility>().playerSkin.enabled)
            DetectPlayer(player);
    }

    //TODO add targeting that is affected by line-of-sight ~Raycasting Maybe?~
    protected virtual void DetectPlayer(GameObject player)
    {
        if(Vector3.Distance(gameObject.transform.position, player.transform.position) <= detectDist)
        {
            Vector3 targetPostition = new Vector3(player.transform.position.x,
                                       this.transform.position.y,
                                       player.transform.position.z);
            this.transform.LookAt(targetPostition);
        }
    }

}
