using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AbilityLoadout : MonoBehaviour
{
    public Ability EquippedAbility { get; private set; }

    public void EquipAbility(Ability ability)
    {
        //TODO add Equipping VFX, SFX
        RemoveCurrentAbilityObject();
        CreateNewAbilityObject(ability);
    }

    public void UseEquippedAbility(Transform target)
    {
            EquippedAbility.Use();
    }

    public void UseEquippedAbility()
    {
        EquippedAbility.Use();
    }

    public void RemoveCurrentAbilityObject()
    {
        //TODO replace with Object Pooling
        foreach(Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }


    }

    public void CreateNewAbilityObject(Ability ability)
    {
        EquippedAbility = Instantiate(ability, 
           transform.position, Quaternion.identity);
        //Ensure new ability is the child of loadout
        EquippedAbility.transform.SetParent(this.transform);

        GameObject.Destroy(GameObject.Find("Invisibility"));
    }
}
