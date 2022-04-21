using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyStats : CharacterStats
    {
        EnemyAnimatorManager enemyAnimatorManager;

        public UIEnemyHealthBar enemyHealthBar;
        public int soulsAwardedOnDeath = 50;

        private void Awake() 
        {
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        }

        void Start()
        {
            maxHelth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHelth;
            enemyHealthBar.SetMaxHealth(maxHelth);
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHelth = healthLevel * 10;
            return maxHelth;
        }

        public void TakeDamageNoAnimation(int damage)
        {
            currentHealth = currentHealth - damage;
            enemyHealthBar.SetHealth(currentHealth);

            if(currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
            }
        }

        public void TakeDamage(int damage, string damageAnimation = "TakeDamage")
        {
            if(isDead)
                return;
                
            currentHealth = currentHealth - damage;
            enemyHealthBar.SetHealth(currentHealth);

            enemyAnimatorManager.PlayTargetAnimation(damageAnimation, true);

            if(currentHealth <= 0)
            {
                HandleDeath();
            }
        }
    
        private void HandleDeath()
        {
            currentHealth = 0;
            enemyAnimatorManager.PlayTargetAnimation("Death", true);
            isDead = true;
        }
    }
}

