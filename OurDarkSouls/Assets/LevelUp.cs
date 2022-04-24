using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class LevelUp : MonoBehaviour
    {
        [Header("Player Level")]
        public int currentPlayerLevel;
        public int projectedPlayerLevel;
        public Text currentPlayerLevelText;
        public Text projectedPlayerLevelText;

        [Header("Souls")]
        public Text currentSouls;
        public Text soulsRequiredToLevelUp;

        [Header("Health")]
        public Slider healthSlider;
        public Text currentHealthLevelText;
        public Text projectedHealthLevelText;

        [Header("Stamina")]
        public Slider staminaSloder;
        public Text currentStaminaLevelText;
        public Text projectedStaminaLevelText;

        [Header("Focus")]
        public Slider focusSlider;
        public Text currentFocusLevelText;
        public Text projectedFocusLevelText;

        [Header("Poise")]
        public Slider poiseSlider;
        public Text currentPoiseLevelText;
        public Text projectedPoiseLevelText;

        [Header("Strength")]
        public Slider strengthSlider;
        public Text currentStrengthLevelText;
        public Text projectedStrengthLevelText;

        [Header("Dexterity")]
        public Slider dexteritySlider;
        public Text currentDexterityLevelText;
        public Text projectedDexterityLevelText;

        [Header("Faith")]
        public Slider faithSlider;
        public Text currentFaithLevelText;
        public Text projectedFaithLevelText;

        [Header("Intelligence")]
        public Slider intelligencelider;
        public Text currentIntelligenceLevelText;
        public Text projectedIntelligenceLevelText;
    }
}
