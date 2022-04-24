using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerEquipmentManager : MonoBehaviour
    {
        InputHandler inputHandler;
        PlayerInventory playerInventory;

        [Header("Equipment Model Changers")]
        HelmetModelChanger helmetModelChanger;
        TorsoModelChanger torsoModelChanger;
        HipModelChanger hipModelChanger;
        LeftLegModelChanger leftLegModelChanger;
        RightLegModelChanger rightLegModelChanger;

        [Header("Defalt Naked Models")]
        public GameObject nakedHeadModel;
        public string nakedTorsoModel;
        public string nakedHipModel;
        public string nakedLeftLeg;
        public string nakedRightLeg;

        public BlockingCollider blockingCollider;

        private void Awake() 
        {
            inputHandler = GetComponentInParent<InputHandler>();
            playerInventory = GetComponentInParent<PlayerInventory>();   
            helmetModelChanger = GetComponentInChildren<HelmetModelChanger>();
            torsoModelChanger = GetComponentInChildren<TorsoModelChanger>();
            hipModelChanger = GetComponentInChildren<HipModelChanger>();
            leftLegModelChanger = GetComponentInChildren<LeftLegModelChanger>();
            rightLegModelChanger = GetComponentInChildren<RightLegModelChanger>();
        }

        private void Start() 
        {
            EquipAllEquipmentModelsOnStart();
        }

        private void EquipAllEquipmentModelsOnStart()
        {
            helmetModelChanger.UnEquipAllHelmetModels();
            if(playerInventory.currentHelmetEquipment != null)
            {
                nakedHeadModel.SetActive(false);
                helmetModelChanger.EquipHelmetModelByName(playerInventory.currentHelmetEquipment.helmetModelName);
            }
            else
            {
                nakedHeadModel.SetActive(true);
            }
            
            torsoModelChanger.UnEquipAllTorsoModels();
            if(playerInventory.currentTorsoEquipment != null)
            {
                torsoModelChanger.EquipTorsoModelByName(playerInventory.currentTorsoEquipment.torsoModelName);
            }
            else
            {
                torsoModelChanger.EquipTorsoModelByName(nakedTorsoModel);
            }

            hipModelChanger.UnEquipAllHipModels();
            leftLegModelChanger.UnEquipAllLegModels();
            rightLegModelChanger.UnEquipAllLegModels();
            if(playerInventory.currentLegEquipment != null)
            {
                hipModelChanger.EquipHipModelByName(playerInventory.currentLegEquipment.hipModelName);
                leftLegModelChanger.EquipLegModelByName(playerInventory.currentLegEquipment.leftLegName);
                rightLegModelChanger.EquipLegModelByName(playerInventory.currentLegEquipment.rightLegName);
            } 
            else
            {
                hipModelChanger.EquipHipModelByName(nakedHipModel);
                leftLegModelChanger.EquipLegModelByName(nakedLeftLeg);
                rightLegModelChanger.EquipLegModelByName(nakedRightLeg);
            }
        }

        public void OpenBlockindCollider()
        {
            if (inputHandler.twoHandFlag)
            {
                blockingCollider.SetColliderDamageAbsorption(playerInventory.rightWeapon);
            }
            else
            {
                blockingCollider.SetColliderDamageAbsorption(playerInventory.leftWeapon);

            }

            blockingCollider.EnableBlockCollider();
        }

        public void CloseBlockingCollider()
        {
            blockingCollider.DisableBlockCollider();
        }
    }

}
