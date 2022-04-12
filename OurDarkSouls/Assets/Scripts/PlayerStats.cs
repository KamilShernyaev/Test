using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHelth;
        public int currentHealth;

        public HealthBar healthBar;

        AnimatorHadler animatorHadler;

        private void Awake() 
        {
            animatorHadler = GetComponentInChildren<AnimatorHadler>();
        }

        void Start()
        {
            maxHelth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHelth;
            healthBar.SetMaxHealth(maxHelth);
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHelth = healthLevel * 10;
            return maxHelth;
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
    }
}
