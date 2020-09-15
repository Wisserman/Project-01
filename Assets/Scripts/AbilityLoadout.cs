using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityLoadout : MonoBehaviour
{
    public Ability EquippedAbility { get; private set; }

    public void EquipAbility(Ability ability)
    {
        //TODO add Equipping VFX, SFX
        RemoveCurrentAbilityObject();
        CreateNewAbilityObject(ability);
        Debug.Log("Ability Changed");
    }

    public void UseEquippedAbility(Transform target)
    {
        EquippedAbility.Use();
    }

    public void UseEquippedAbility()
    {
        Debug.Log("UseEquippedAbility");
        EquippedAbility.Use();
    }

    public void RemoveCurrentAbilityObject()
    {
        //TODO replace with Obect Pooling
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
    }
}
