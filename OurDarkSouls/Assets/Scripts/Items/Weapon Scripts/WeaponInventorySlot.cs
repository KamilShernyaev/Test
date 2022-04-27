using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class WeaponInventorySlot : MonoBehaviour
    {
        PlayerInventoryManager playerInventoryManager;
        PlayerWeaponSlotManager playerWeaponSlotManager;
        UIManager uIManager;
        public Image icon;
        WeaponItem item;

        public void Awake()
        {
            playerInventoryManager = FindObjectOfType<PlayerInventoryManager>();
            playerWeaponSlotManager = FindObjectOfType<PlayerWeaponSlotManager>();
            uIManager = FindObjectOfType<UIManager>();
        }

        public void AddItem(WeaponItem newItem)
        {
            item = newItem;
            icon.sprite = item.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
            icon.gameObject.SetActive(true);
        }

        public void ClearInventorySlot()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
            icon.gameObject.SetActive(false);

        }

        public void EquipThisItem()
        {
            if(uIManager.rightHandSlot01Selected)
            {
                playerInventoryManager.weaponsInventory.Add(playerInventoryManager.weaponsInRightHandSlots[0]);
                playerInventoryManager.weaponsInRightHandSlots[0] = item;
                playerInventoryManager.weaponsInventory.Remove(item);
            }
            else if(uIManager.rightHandSlot02Selected)
            {
                playerInventoryManager.weaponsInventory.Add(playerInventoryManager.weaponsInRightHandSlots[1]);
                playerInventoryManager.weaponsInRightHandSlots[1] = item;
                playerInventoryManager.weaponsInventory.Remove(item);
            }
            else if(uIManager.leftHandSlot01Selected)
            {
                playerInventoryManager.weaponsInventory.Add(playerInventoryManager.weaponsInLeftHandSlots[0]);
                playerInventoryManager.weaponsInLeftHandSlots[0] = item;
                playerInventoryManager.weaponsInventory.Remove(item);
            }
            else if(uIManager.leftHandSlot02Selected)
            {
                playerInventoryManager.weaponsInventory.Add(playerInventoryManager.weaponsInLeftHandSlots[1]);
                playerInventoryManager.weaponsInLeftHandSlots[1] = item;
                playerInventoryManager.weaponsInventory.Remove(item);
            }
            else
            {
                return;
            }

            playerWeaponSlotManager.LoadWeaponOnSlot(playerInventoryManager.rightWeapon,false);
            playerWeaponSlotManager.LoadWeaponOnSlot(playerInventoryManager.leftWeapon,true);  

            uIManager.equipmentWindowUI.LoadWeaponOnEquipmentScreen(playerInventoryManager);       
            uIManager.ResetAllSelectedSlot();
        }
    }
}