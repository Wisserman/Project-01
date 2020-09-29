using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    Invisibility _invisibility;

    [SerializeField] AbilityLoadout _abilityLoadout;
    [SerializeField] Ability _startingAbility;
    [SerializeField] Ability _newAbilityToTest;

    public Transform CurrentTarget { get; private set; }

    private void Awake()
    {
        if (_startingAbility != null)
            _abilityLoadout?.EquipAbility(_startingAbility);
        _invisibility = gameObject.GetComponent<Invisibility>();
    }

    //TODO consider expanding into own script
    public void SetTarget(Transform newTarget)
    {
        CurrentTarget = newTarget;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _abilityLoadout.UseEquippedAbility();
        }

        //equip new ability
        if (Input.GetKeyDown(KeyCode.Tab) && _abilityLoadout.EquippedAbility.inUse != true)
        {
            _abilityLoadout.EquipAbility(_newAbilityToTest);
        }

        // set target, for testing   ~Lockon for future ability?~
        if (Input.GetKeyDown(KeyCode.F))
        {
            // temp self-target
            // TODO add targeting function: targets closest
            SetTarget(transform);
        }
    }
}
