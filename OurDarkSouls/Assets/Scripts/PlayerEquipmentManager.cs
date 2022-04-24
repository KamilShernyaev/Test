using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerEquipmentManager : MonoBehaviour
    {
        InputHandler inputHandler;
        PlayerInventory playerInventory;
        PlayerStats playerStats;

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
            playerStats = GetComponentInParent<PlayerStats>();

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
             //HELMET EQUIPMENT
            helmetModelChanger.UnEquipAllHelmetModels();
            if(playerInventory.currentHelmetEquipment != null)
            {
                nakedHeadModel.SetActive(false);
                helmetModelChanger.EquipHelmetModelByName(playerInventory.currentHelmetEquipment.helmetModelName);
                playerStats.physicalDamageAbsoptionHead = playerInventory.currentHelmetEquipment.physicalDefense;
                Debug.Log("Head Absorption is" + playerStats.physicalDamageAbsoptionHead + "%");
            }
            else
            {
                nakedHeadModel.SetActive(true);
                playerStats.physicalDamageAbsoptionHead = 0;
            }

             //TORSO EQUIPMENT
            torsoModelChanger.UnEquipAllTorsoModels();
            if(playerInventory.currentTorsoEquipment != null)
            {
                torsoModelChanger.EquipTorsoModelByName(playerInventory.currentTorsoEquipment.torsoModelName);
                playerStats.physicalDamageAbsoptionBody = playerInventory.currentTorsoEquipment.physicalDefense;
                Debug.Log("Torso Absorption is" + playerStats.physicalDamageAbsoptionBody + "%");
            }
            else
            {
                torsoModelChanger.EquipTorsoModelByName(nakedTorsoModel);
                playerStats.physicalDamageAbsoptionBody = 0;
            }

             //LEG EQUIPMENT
            hipModelChanger.UnEquipAllHipModels();
            leftLegModelChanger.UnEquipAllLegModels();
            rightLegModelChanger.UnEquipAllLegModels();
            if(playerInventory.currentLegEquipment != null)
            {
                hipModelChanger.EquipHipModelByName(playerInventory.currentLegEquipment.hipModelName);
                leftLegModelChanger.EquipLegModelByName(playerInventory.currentLegEquipment.leftLegName);
                rightLegModelChanger.EquipLegModelByName(playerInventory.currentLegEquipment.rightLegName);
                playerStats.physicalDamageAbsoptionLegs = playerInventory.currentLegEquipment.physicalDefense;
                Debug.Log("Hip Absorption is" + playerStats.physicalDamageAbsoptionLegs + "%");
            } 
            else
            {
                hipModelChanger.EquipHipModelByName(nakedHipModel);
                leftLegModelChanger.EquipLegModelByName(nakedLeftLeg);
                rightLegModelChanger.EquipLegModelByName(nakedRightLeg);
                playerStats.physicalDamageAbsoptionLegs = 0;
            }
            //HAND EQUIPMENT

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
