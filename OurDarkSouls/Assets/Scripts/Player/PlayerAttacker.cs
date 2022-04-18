using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG 
{
    public class PlayerAttacker : MonoBehaviour
    {
        PlayerManager playerManager;
        PlayerAnimatorManager playerAnimatorManager;
        PlayerStats playerStats;
        PlayerInventory playerInventory;
        InputHandler inputHandler;
        WeaponSlotManager weaponSlotManager;
        public string lastAttack;

        LayerMask backStabLayer = 1 << 12;
        LayerMask riposteLayer = 1 << 13;

        private void Awake() 
        {
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            playerManager = GetComponentInParent<PlayerManager>();
            playerStats = GetComponentInParent<PlayerStats>();
            playerInventory = GetComponentInParent<PlayerInventory>();
            weaponSlotManager = GetComponent<WeaponSlotManager>();
            inputHandler = GetComponentInParent<InputHandler>();
        }

        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if(playerStats.currentStamina <= 0)
                return;

            if(inputHandler.comboFlag)
            {
                playerAnimatorManager.anim.SetBool("canDoCombo", false);

                if(lastAttack == weapon.OH_Light_Attack_1)
                {
                    playerAnimatorManager.PlayTargetAnimation(weapon.OH_Light_Attack_2, true);
                }
                else if(lastAttack == weapon.TH_light_attack_01)
                {
                    playerAnimatorManager.PlayTargetAnimation(weapon.TH_light_attack_02, true);
                }
                
            }
        }
        
        public void HandleLightAttack(WeaponItem weapon)
        {
            if(playerStats.currentStamina <= 0)
                return;

            weaponSlotManager.attackingWeapon = weapon;

            if(inputHandler.twoHandFlag)
            {
                playerAnimatorManager.PlayTargetAnimation(weapon.TH_light_attack_01, true);
                lastAttack = weapon.TH_light_attack_01;
            }
            else
            {
                playerAnimatorManager.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
                lastAttack = weapon.OH_Light_Attack_1;
            }
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            if(playerStats.currentStamina <= 0)
                return;

            weaponSlotManager.attackingWeapon = weapon;

            if(inputHandler.twoHandFlag)
            {
                playerAnimatorManager.PlayTargetAnimation(weapon.TH_Heavy_attack_01, true);
                lastAttack = weapon.TH_Heavy_attack_01;
            }
            else
            {
                playerAnimatorManager.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
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

                playerAnimatorManager.anim.SetBool("isUsingRightHand", true);
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
                        playerInventory.currentSpell.AttemptToCastSpell(playerAnimatorManager, playerStats);
                    }
                    else
                    {
                        playerAnimatorManager.PlayTargetAnimation("Shrug", true);
                    }
                }
            }
        }

        private void SuccessfullyCastSpell()
        {
            playerInventory.currentSpell.SuccessfullyCastSpell(playerAnimatorManager, playerStats);
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
                    playerManager.transform.position = enemyCharacterManager.backStabCollider.criticalDamageStandPosition.position;
                    
                    Vector3 rotarionDirection = playerManager.transform.root.eulerAngles;
                    rotarionDirection = hit.transform.position - playerManager.transform.position; 
                    rotarionDirection.y = 0;
                    rotarionDirection.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotarionDirection);
                    Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                    playerManager.transform.rotation = targetRotation;

                    playerAnimatorManager.PlayTargetAnimation("Back_Stab", true);
                    enemyCharacterManager.GetComponentInChildren<EnemyAnimatorManager>().PlayTargetAnimation("Back_Stabbed", true);
                }
            }
            else if (Physics.Raycast(inputHandler.criticalAttackRayCastStartPoint.position, 
            transform.TransformDirection(Vector3.forward), out hit, 0.7f, riposteLayer))
            {
                CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
                
                if (enemyCharacterManager != null && enemyCharacterManager.canBeRiposted)
                {                
                    playerManager.transform.position = enemyCharacterManager.riposteCollider.criticalDamageStandPosition.position;

                    Vector3 rotationDirection = playerManager.transform.root.eulerAngles;
                    rotationDirection = hit.transform.position - playerManager.transform.position;
                    rotationDirection.y = 0;
                    rotationDirection.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotationDirection);
                    Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                    playerManager.transform.rotation = targetRotation;

                    playerAnimatorManager.PlayTargetAnimation("Riposte", true);
                    enemyCharacterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Riposted", true);
                }
            }
        }
    }
}

            