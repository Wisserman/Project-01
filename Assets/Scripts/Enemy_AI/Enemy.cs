using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Enemy : MonoBehaviour, IPatrolable
{
    [SerializeField] ThirdPersonMovement playerMove;
    public float moveSpd;
    public Transform[] tempSpots;
    Transform[] patrolSpots;
    int patrolI = 1;

    CharacterController controller;

    Vector3 destination;

    bool playerDetected = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        patrolSpots = new Transform[tempSpots.Length];

        for (int i=0; i < tempSpots.Length; i++)
        {
            patrolSpots[i] = tempSpots[i];
        }
    }

    void Update()
    {
        
        GameObject player = GameObject.Find("Invisibility(Clone)");

        DetectPlayer(player);

        if (playerDetected)
            destination = player.GetComponentInParent<Transform>().position;
        else
            SetDestination(patrolSpots);

        if (Vector3.Distance(transform.position, destination) >= 0.2f)
            Move(destination, transform.position);
    }

    //TODO add targeting that is affected by line-of-sight ~Raycasting Maybe?~
    protected bool DetectPlayer(GameObject player)
    {
        if (player.GetComponent<Invisibility>().playerSkin.enabled 
            && Vector3.Distance(gameObject.transform.position, player.transform.position) <= playerMove.speed*2)
        {
            destination = player.transform.position;
            playerDetected = true;
        }
        else
            playerDetected = false;

        return playerDetected;
    }

    public void SetDestination(Transform[] dest)
    {
        if (Vector3.Distance(transform.position, patrolSpots[patrolI].position) <= 1)
        {
            Debug.Log("Reached Destination");
            patrolI++;
            if (patrolI >= dest.Length)
                patrolI = 0;
        }

        destination = dest[patrolI].position;
    }

    public void SetDestination(Transform dest)
    {
        Debug.LogError("SetDestination transform");
    }

    public void Move(Vector3 destination, Vector3 currentPos)
    {
        destination.y = 0;
        currentPos.y = 0;
        Vector3 dir = (destination - currentPos).normalized * moveSpd;
        controller.Move(dir * Time.deltaTime);
        transform.LookAt(destination);
    }
}
