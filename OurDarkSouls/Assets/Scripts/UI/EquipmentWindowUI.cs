using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    public class EquipmentWindowUI : MonoBehaviour
    {
        public bool rightHandSlot01Selected;
        public bool rightHandSlot02Selected;
        public bool leftHandSlot01Selected;
        public bool leftHandSlot02Selected;

        public HandEquipmentSlotUI[] handEquipmentSlotUI;

        private void Start() 
        {
            handEquipmentSlotUI = GetComponentsInChildren<HandEquipmentSlotUI>();
        }

        public void LoadWeaponOnEquipmentScreen(PlayerInventoryManager playerInventoryManager)
        {
            for( int i = 0; i < handEquipmentSlotUI.Length; i++)
            {
                if(handEquipmentSlotUI[i].rightHandSLot01)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventoryManager.weaponsInRightHandSlots[0]);
                }
                else if (handEquipmentSlotUI[i].rightHandSLot02)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventoryManager.weaponsInRightHandSlots[1]);
                }
                else if(handEquipmentSlotUI[i].leftHandSLot01)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventoryManager.weaponsInLeftHandSlots[0]);
                }
                else
                {
                    handEquipmentSlotUI[i].AddItem(playerInventoryManager.weaponsInLeftHandSlots[1]);
                }
            }
        }

        public void SelectRightHandSlot01()
        {
            rightHandSlot01Selected = true;
        }

        public void SelectRightHandSlot02()
        {
            rightHandSlot02Selected = true;
        }

        public void SelectLeftHandSlot01()
        {
            leftHandSlot01Selected = true;
        }

        public void SelectLeftHandSlot02()
        {
            leftHandSlot02Selected = true;
        }
    }
}

