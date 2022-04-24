using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterStats : MonoBehaviour
    {
        
        public int maxHelth;
        public int currentHealth;

        
        public float maxStamina;
        public float currentStamina;

       
        public float maxFocusPoints;
        public float currentFocusPoints;

        public int soulCount = 0;
        public bool isDead;

        [Header("Levels")]
        public int healthLevel = 10;
        public int staminaLevel = 10;
        public int focusLevel = 10;
        public int poiseLevel = 10;
        public int strengthLevel = 10;
        public int dexeterityLevel = 10;
        public int intelligenceLevel = 10;
        public int faithLevel = 10;


        [Header("Armor Absorptions")]
        public float physicalDamageAbsoptionHead;
        public float physicalDamageAbsoptionBody;
        public float physicalDamageAbsoptionLegs;
        public float physicalDamageAbsoptionHand;


        public virtual void TakeDamage(int physicalDamage, string damageAnimation = "TakeDamage")
        {
            if(isDead)
                return;
                float totalPhysicalDamageAbsorption = 1 - 
                (1 - physicalDamageAbsoptionHead / 100) * 
                (1 - physicalDamageAbsoptionBody / 100) * 
                (1 - physicalDamageAbsoptionLegs / 100) * 
                (1 - physicalDamageAbsoptionHand / 100);

                physicalDamage = Mathf.RoundToInt(physicalDamage - (physicalDamage * totalPhysicalDamageAbsorption));

                Debug.Log("Total Damage Absorption is" + totalPhysicalDamageAbsorption + "%");

                float finalDamage = physicalDamage;
                currentHealth = Mathf.RoundToInt(currentHealth - finalDamage);

                Debug.Log("Total Damage Deal is" + finalDamage);

                if(currentHealth <= 0)
                {
                    currentHealth = 0;
                    isDead = true;
                }
        }
    }
}
