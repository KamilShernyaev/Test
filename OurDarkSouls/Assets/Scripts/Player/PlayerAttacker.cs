using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG 
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHadler animatorHadler;
        InputHandler inputHandler;
        WeaponSlotManager weaponSlotManager;
        public string lastAttack;
        private void Awake() 
        {
            animatorHadler = GetComponentInChildren<AnimatorHadler>();
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
            inputHandler = GetComponent<InputHandler>();
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
    }
}