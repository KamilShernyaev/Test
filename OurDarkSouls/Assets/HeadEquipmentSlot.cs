using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{    
    public class HeadEquipmentSlot : MonoBehaviour
    {
        UIManager uIManager;
        public Image icon;
        HelmetEquipment hemletItem;

        private void Awake() 
        {
            uIManager = FindObjectOfType<UIManager>();
        }
        public void AddItem(HelmetEquipment hemletItem)
        {
            if (hemletItem != null)
            {
                this.hemletItem = hemletItem;
                icon.sprite = this.hemletItem.itemIcon;
                icon.enabled = true;
                this.gameObject.SetActive(true);
            }
            else
            {
                ClearItem();
            }
        }

        public void ClearItem()
        {
            hemletItem = null;
            icon.sprite = null;
            icon.enabled = false;
        }
    
        public void SelectThisSlot()
        {
            uIManager.headEquipmentSlotSelected = true;
        }
    }
}
