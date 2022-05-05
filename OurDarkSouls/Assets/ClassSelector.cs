using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class ClassSelector : MonoBehaviour
    {
        PlayerManager player;

        [Header("Class Info UI")]
        public Text strenghtStat;
        public Text dexterityStat;
        public Text classDescription;


        [Header("Class Starting Statas")]
        public ClassStats[] classStats;


        [Header("Class Starting Gear")]
        public ClassGear[] classGears;

        private void Awake() 
        {
            player = FindObjectOfType<PlayerManager>();
        }

        private void AssignClassStats(int classChosen)
        {
            player.playerStatsManager.playerLevel = classStats[classChosen].classLevel;
            player.playerStatsManager.strengthLevel = classStats[classChosen].strenghtLevel;
            player.playerStatsManager.dexeterityLevel = classStats[classChosen].dexterityLevel;
            classDescription.text = classStats[classChosen].classDescription;
        }

        public void AssignKnightClass()
        {
            AssignClassStats(0);
            player.playerInventoryManager.currentHelmetEquipment = classGears[0].helmetEquipment;
            player.playerInventoryManager.currentBodyEquipment = classGears[0].bodyEquipment;
            player.playerInventoryManager.currentLegEquipment = classGears[0].legEquipment;
            player.playerInventoryManager.currentHandEquipment = classGears[0].handEquipment;

            player.playerInventoryManager.weaponsInRightHandSlots[0] = classGears[0].primaryWeapon;
            //player.playerInventoryManager.weaponsInRightHandSlots[1] = classGears[0].secondaryWeapon; - Secondary slot
            player.playerInventoryManager.weaponsInLeftHandSlots[0] = classGears[0].offHandWeapon;

            player.playerEquipmentManager.EquipAllEquipmentModels();
            player.playerWeaponSlotManager.LoadBothWeaponOnSlots();

            strenghtStat.text = player.playerStatsManager.strengthLevel.ToString();
            dexterityStat.text = player.playerStatsManager.dexeterityLevel.ToString();
        }
        public void AssignNakedClass()
        {
            AssignClassStats(1);
            player.playerInventoryManager.currentHelmetEquipment = classGears[1].helmetEquipment;
            player.playerInventoryManager.currentBodyEquipment = classGears[1].bodyEquipment;
            player.playerInventoryManager.currentLegEquipment = classGears[1].legEquipment;
            player.playerInventoryManager.currentHandEquipment = classGears[1].handEquipment;

            player.playerInventoryManager.weaponsInRightHandSlots[0] = classGears[1].primaryWeapon;
            //player.playerInventoryManager.weaponsInRightHandSlots[1] = classGears[0].secondaryWeapon; - Secondary slot
            player.playerInventoryManager.weaponsInLeftHandSlots[0] = classGears[1].offHandWeapon;

            player.playerEquipmentManager.EquipAllEquipmentModels();
            player.playerWeaponSlotManager.LoadBothWeaponOnSlots();
            strenghtStat.text = player.playerStatsManager.strengthLevel.ToString();
            dexterityStat.text = player.playerStatsManager.dexeterityLevel.ToString();
        }
    }
}