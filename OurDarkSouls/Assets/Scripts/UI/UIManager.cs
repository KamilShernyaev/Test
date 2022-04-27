using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class UIManager : MonoBehaviour
    {
        public PlayerManager playerManager;
        public EquipmentWindowUI equipmentWindowUI;

        [Header("HUD")]
        public Text currentSoulCount;

        [Header("UI Windows")]
        public GameObject hudWindow;
        public GameObject selectWindow;
        public GameObject equipmentScreenWindow;
        public GameObject weaponInventoryWindow;
        public GameObject levelUpWindow;

        [Header("Equipment Window Slot Selected")]
        public bool rightHandSlot01Selected;
        public bool rightHandSlot02Selected;
        public bool leftHandSlot01Selected;
        public bool leftHandSlot02Selected;

        [Header("Weapon Inventory")]
        public GameObject weaponInventorySlotPrefab;
        public Transform weaponInventorySlotsParent;
        WeaponInventorySlot[] weaponInventorySlots;

        private void Awake() 
        {
            playerManager = FindObjectOfType<PlayerManager>();
        }

        private void Start() 
        {
            weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>(); 
            equipmentWindowUI.LoadWeaponOnEquipmentScreen(playerManager.playerInventoryManager);
            currentSoulCount.text = playerManager.playerStatsManager.currentSoulCount.ToString();
        }

        public void UpdateUI()
        {
            #region Weapon Inventory Slots;
            for (int i = 0; i < weaponInventorySlots.Length; i++)
            {
                if (i < playerManager.playerInventoryManager.weaponsInventory.Count)
                {
                    if (weaponInventorySlots.Length < playerManager.playerInventoryManager.weaponsInventory.Count)
                    {
                        Instantiate(weaponInventorySlotPrefab, weaponInventorySlotsParent);
                        weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
                    }
                    weaponInventorySlots[i].AddItem(playerManager.playerInventoryManager.weaponsInventory[i]);
                }
                else
                {
                    weaponInventorySlots[i].ClearInventorySlot();
                }
            }

            #endregion
        }

        public void OpenSelectWindow()
        {
            selectWindow.SetActive(true);
        }

        public void CloseSelectWindow()
        {
            selectWindow.SetActive(false);
        }

        public void CloseAllInventoryWindow()
            {
                ResetAllSelectedSlot();
                weaponInventoryWindow.SetActive(false);
                equipmentScreenWindow.SetActive(false);
            }

        public void ResetAllSelectedSlot()
        {
            rightHandSlot01Selected = false;
            rightHandSlot02Selected = false;
            leftHandSlot01Selected = false;
            leftHandSlot02Selected = false;
        }
    }
}