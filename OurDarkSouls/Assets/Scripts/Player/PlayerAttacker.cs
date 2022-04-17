using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG 
{
    public class PlayerAttacker : MonoBehaviour
    {
        PlayerManager playerManager;
        AnimatorHadler animatorHadler;
        PlayerStats playerStats;
        PlayerInventory playerInventory;
        InputHandler inputHandler;
        WeaponSlotManager weaponSlotManager;
        public string lastAttack;
        LayerMask backStabLayer = 1 << 12;

        private void Awake() 
        {
            animatorHadler = GetComponent<AnimatorHadler>();
            playerManager = GetComponentInParent<PlayerManager>();
            playerStats = GetComponentInParent<PlayerStats>();
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
            if (playerManager.isInteracting)
                return;
                
            if(weapon.isFaithCaster)
            {
                if(playerInventory.currentSpell != null && playerInventory.currentSpell.isFaithSpell)
                {
                    if (playerStats.currentFocusPoints >= playerInventory.currentSpell.focusPointCost)
                    {
                        playerInventory.currentSpell.AttemptToCastSpell(animatorHadler, playerStats);
                    }
                    else
                    {
                        animatorHadler.PlayTargetAnimation("Shrug", true);
                    }
                }
            }
        }

        private void SuccessfullyCastSpell()
        {
            playerInventory.currentSpell.SuccessfullyCastSpell(animatorHadler, playerStats);
        }        

        #endregion
        public void AttemptBackStabOrRiposte()
        {
            RaycastHit hit;

            if (Physics.Raycast(inputHandler.criticalAttackRayCastStartPoint.position, 
            transform.TransformDirection(Vector3.forward), out hit, 0.5f, backStabLayer))
            {
                CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();

                if (enemyCharacterManager != null)
                {
                    playerManager.transform.position = enemyCharacterManager.backStabCollider.backStabberStandPoint.position;
                    
                    Vector3 rotarionDirection = playerManager.transform.root.eulerAngles;
                    rotarionDirection = hit.transform.position - playerManager.transform.position; 
                    rotarionDirection.y = 0;
                    rotarionDirection.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotarionDirection);
                    Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                    playerManager.transform.rotation = targetRotation;

                    animatorHadler.PlayTargetAnimation("Back Stab", true);
                    enemyCharacterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Back Stabbed", true);
                }
            }
        }
    }
}

            