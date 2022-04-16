using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG 
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHadler animatorHadler;
        PlayerManager playerManager;
        PlayerInventory playerInventory;
        InputHandler inputHandler;
        WeaponSlotManager weaponSlotManager;
        public string lastAttack;
        private void Awake() 
        {
            animatorHadler = GetComponent<AnimatorHadler>();
            playerManager = GetComponentInParent<PlayerManager>();
            playerInventory = GetComponentInParent<PlayerInventory>();
            weaponSlotManager = GetComponent<WeaponSlotManager>();
            inputHandler = GetComponentInParent<InputHandler>();
        }

        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if(inputHandler.comboFlag)
            {
                animatorHadler.anim.SetBool("canDoCombo", false);

                if(lastAttack == weapon.OH_Light_Attack_1)
                {
                    animatorHadler.PlayTargetAnimation(weapon.OH_Light_Attack_2, true);
                }
                else if(lastAttack == weapon.TH_light_attack_01)
                {
                    animatorHadler.PlayTargetAnimation(weapon.TH_light_attack_02, true);
                }
                
            }
        }
        
        public void HandleLightAttack(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon = weapon;

            if(inputHandler.twoHandFlag)
            {
                animatorHadler.PlayTargetAnimation(weapon.TH_light_attack_01, true);
                lastAttack = weapon.TH_light_attack_01;
            }
            else
            {
                animatorHadler.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
                lastAttack = weapon.OH_Light_Attack_1;
            }
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon = weapon;

            if(inputHandler.twoHandFlag)
            {
                animatorHadler.PlayTargetAnimation(weapon.TH_Heavy_attack_01, true);
                lastAttack = weapon.TH_Heavy_attack_01;
            }
            else
            {
                animatorHadler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
                lastAttack = weapon.OH_Heavy_Attack_1;
            }
        }

        #region Input Actions 
        public void HandleRBAction()
        {
            if(playerInventory.rightWeapon.isMeleeWeapon)
            {
                PerformRBMeleeAction();
            }
            else if(playerInventory.rightWeapon.isSpellCaster || playerInventory.rightWeapon.isFaithCaster || playerInventory.rightWeapon.isPyroCaster)
            {
                PerformRBMagicAction(playerInventory.rightWeapon);
            }
        }
        #endregion

        #region Attack Actions
        private void PerformRBMeleeAction()
        {
            if(playerManager.canDoCombo)
            {
                inputHandler.comboFlag = true;
                HandleWeaponCombo(playerInventory.rightWeapon);
                inputHandler.comboFlag = false;
            }
            else 
            {
                if(playerManager.isInteracting)
                    return;

                if(playerManager.canDoCombo)
                    return;

                animatorHadler.anim.SetBool("isUsingRightHand", true);
                HandleLightAttack(playerInventory.rightWeapon);
            }
        }
        
        private void PerformRBMagicAction(WeaponItem weapon)
        {
            if(playerInventory.currentSpell != null && playerInventory.currentSpell.isFaithSpell)
            {
                
            }
        }
        

        #endregion
    }
}

            