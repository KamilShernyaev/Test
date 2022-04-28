using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class ItemStatsWindowUI : MonoBehaviour
    {
        public Text itemNameText;
        public Image itemIconImage;

        public void UpdateWeaponItemStats(WeaponItem weapon)
        {
            itemNameText.text = weapon.itemName;
            itemIconImage.sprite = weapon.itemIcon;
        }
    }
}
