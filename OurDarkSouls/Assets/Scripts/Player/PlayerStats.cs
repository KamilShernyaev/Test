using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerStats : CharacterStats
    {
        PlayerManager playerManager;
        HealthBar healthBar;
        StaminaBar staminaBar;
        AnimatorHadler animatorHadler;

        public float staminaRegenerationAmount = 30;
        public float staminaRegenTimer = 0;

        private void Awake() 
        {
            playerManager = GetComponent<PlayerManager>();
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

        private float SetMaxStaminaFromStaminaLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }

        public void TakeDamage(int damage)
        {
            if (isDead)
                return;

            currentHealth = currentHealth - damage;
            if(playerManager.isInvulnerable)
                return;

            currentHealth = currentHealth - damage;
            healthBar.SetCurrentHealth(currentHealth);

            animatorHadler.PlayTargetAnimation("TakeDamage", true);

            if(currentHealth <= 0)
            {
                currentHealth = 0;
                animatorHadler.PlayTargetAnimation("Death", true);
                isDead = true;
            }
        }

        public void TakeStaminaDamage(int damage)
        {
            currentStamina = currentStamina - damage;
            staminaBar.SetCurrentStamina(currentStamina);
        }
    
        public void RegenerateStamina()
        {
            if(playerManager.isInteracting)
            {
                staminaRegenTimer = 0;
            }
            else
            {
                staminaRegenTimer += Time.deltaTime;
                if(currentStamina <= maxStamina && staminaRegenTimer > 1f)
                {
                    currentStamina += staminaRegenerationAmount * Time.deltaTime;
                    staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
                }
            }
        }
    }
}
