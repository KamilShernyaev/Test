using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyStats : CharacterStats
    {
        EnemyAnimatorManager enemyAnimatorManager;
        public int soulsAwardedOnDeath = 50;

        private void Awake() 
        {
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        }

        void Start()
        {
            maxHelth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHelth;
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHelth = healthLevel * 10;
            return maxHelth;
        }

        public void TakeDamage(int damage)
        {
            if(isDead)
                return;
                
            currentHealth = currentHealth - damage;

            enemyAnimatorManager.PlayTargetAnimation("Damage_01", true);

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

