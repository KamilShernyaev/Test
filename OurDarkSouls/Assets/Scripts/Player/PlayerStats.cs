using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerStats : CharacterStats
    {

        HealthBar healthBar;
        StaminaBar staminaBar;

        AnimatorHadler animatorHadler;

        private void Awake() 
        {
            healthBar = FindObjectOfType<HealthBar>();
            staminaBar = FindObjectOfType<StaminaBar>();
            animatorHadler = GetComponentInChildren<AnimatorHadler>();
        }

        void Start()
        {
            maxHelth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHelth;
            healthBar.SetMaxHealth(maxHelth);
            healthBar.SetCurrentHealth(currentHealth);

            maxStamina = SetMaxStaminaFromStaminaLevel();
            currentStamina = maxStamina;
            staminaBar.SetMaxStamina(maxStamina);
            staminaBar.SetCurrentStamina(currentStamina);

        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHelth = healthLevel * 10;
            return maxHelth;
        }

        private int SetMaxStaminaFromStaminaLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }

        public void TakeDamage(int damage)
        {
            currentHealth = currentHealth - damage;

            healthBar.SetCurrentHealth(currentHealth);

            animatorHadler.PlayTargetAnimation("TakeDamage", true);

            if(currentHealth <= 0)
            {
                currentHealth = 0;
                animatorHadler.PlayTargetAnimation("Death", true);
            }
        }

        public void TakeStaminaDamage(int damage)
        {
            currentStamina = currentStamina - damage;
            staminaBar.SetCurrentStamina(currentStamina);
        }
    }
}
