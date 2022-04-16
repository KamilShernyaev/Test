using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyStats : CharacterStats
    {
        Animator animator;

        private void Awake() 
        {
            animator = GetComponentInChildren<Animator>();
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

            animator.Play("Damage_01");

            if(currentHealth <= 0)
            {
                currentHealth = 0;
                animator.Play("Death");
                isDead = true;
            }
        }
    }
}

