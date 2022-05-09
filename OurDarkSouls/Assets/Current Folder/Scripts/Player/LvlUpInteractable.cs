using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class LvlUpInteractable : Interactable
    {
        public override void Interact (PlayerManager playerManager)
        {
            playerManager.uIManager.levelUpWindow.SetActive(true);
        }
    }
}
