using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class WeaponPickUp : Interactable
    {
       public WeaponItem weapon;

       public override void Interact(PlayerManager playerManager)
       {
           base.Interact(playerManager);

           PickUpItem(playerManager);
       }

       private void PickUpItem(PlayerManager playerManager)
       {
           PlayerInventory playerInventory;
           PlayerLocomotion playerLocomotion;
           AnimatorHadler animatorHedler;

           playerInventory = playerManager.GetComponent<PlayerInventory>();
           playerLocomotion = playerManager.GetComponent<PlayerLocomotion>();
           animatorHedler = playerManager.GetComponentInChildren<AnimatorHadler>();

           playerLocomotion.rigidbody.velocity = Vector3.zero;
           animatorHedler.PlayTargetAnimation("Pick Up Item", true);
           playerInventory.weaponsInventory.Add(weapon);
           Destroy(gameObject);

       }
    }
}
