using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHelth;
        public int currentHealth;

        public int staminaLevel = 10;
        public float maxStamina;
        public float currentStamina;

        public int focusLevel = 10;
        public float maxFocusPoints;
        public float currentFocusPoints;

        public int currentSoulCount = 0;
        public bool isDead;

<<<<<<< Updated upstream
        public virtual void TakeDamage(int damage, string damageAnimation = "TakeDamage")
=======
        [Header("Character Level")]
        public int playerLevel = 1;

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


        public virtual void TakeDamage(int physicalDamage, string damageAnimation = "Damage_01")
>>>>>>> Stashed changes
        {

        }

        public int SetMaxHealthFromHealthLevel()
        {
            maxHelth = healthLevel * 10;
            return maxHelth;
        }

        public float SetMaxStaminaFromStaminaLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }

        public float SetMaxFocusPointsFromFocusLevel()
        {
            maxFocusPoints = focusLevel * 10;
            return maxFocusPoints;
        }
    }
}
