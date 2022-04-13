using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHelth;
        public int currentHealth;

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
            currentHealth = currentHealth - damage;


            animator.Play("TakeDamage");

            if(currentHealth <= 0)
            {
                currentHealth = 0;
                animator.Play("Death");
            }
        }
    }
}

