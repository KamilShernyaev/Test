using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyStatsManager : CharacterStatsManager
    {
        public EnemyManager enemy;
        public UIEnemyHealthBar enemyHealthBar;

        // public delegate void EnemyKilled();
        // public static event EnemyKilled OnEnemyKilled;

        private void Awake() 
        {
            enemy = GetComponent<EnemyManager>();
            enemy.enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
        }

        void Start()
        {
            
            maxHelth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHelth;
            enemyHealthBar.SetMaxHealth(maxHelth);
        }

        public override void TakeDamageNoAnimation(int damage)
        {
            base.TakeDamageNoAnimation(damage);
            enemyHealthBar.SetHealth(currentHealth);
        }

        public override void TakeDamage(int physicalDamage, string damageAnimation = "TakeDamage")
        {
            if(isDead == false)
            {
                base.TakeDamage(physicalDamage, damageAnimation = "TakeDamage");
                enemyHealthBar.SetHealth(currentHealth);
                enemy.enemyAnimatorManager.PlayTargetAnimation(damageAnimation, true);
                if(currentHealth <= 0)
                {
                    HandleDeath();
                }
            }       
        }
    
        private void HandleDeath()
        {
            currentHealth = 0;
            enemy.enemyAnimatorManager.PlayTargetAnimation("Death", true);
            isDead = true;

            if (isDead == true)
            {
                Destroy(gameObject, 3f);
            }

            // if (OnEnemyKilled != null)
            // {
            //     OnEnemyKilled();
            // }
        }
    }
}

