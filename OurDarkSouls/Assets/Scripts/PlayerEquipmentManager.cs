using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerEquipmentManager : MonoBehaviour
    {
        InputHandler inputHandler;
        PlayerInventoryManager playerInventoryManager;
        PlayerStatsManager playerStatsManager;

        [Header("Equipment Model Changers")]
        //Head Equipment
        HelmetModelChanger helmetModelChanger;
        //Torso Equipment
        TorsoModelChanger torsoModelChanger;
        //Leg Equipment
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
            inputHandler = GetComponent<InputHandler>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();   
            playerStatsManager = GetComponent<PlayerStatsManager>();

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
            if(playerInventoryManager.currentHelmetEquipment != null)
            {
                nakedHeadModel.SetActive(false);
                helmetModelChanger.EquipHelmetModelByName(playerInventoryManager.currentHelmetEquipment.helmetModelName);
                playerStatsManager.physicalDamageAbsoptionHead = playerInventoryManager.currentHelmetEquipment.physicalDefense;
                Debug.Log("Head Absorption is" + playerStatsManager.physicalDamageAbsoptionHead + "%");
            }
            else
            {
                nakedHeadModel.SetActive(true);
                playerStatsManager.physicalDamageAbsoptionHead = 0;
            }

             //TORSO EQUIPMENT
            torsoModelChanger.UnEquipAllTorsoModels();
            if(playerInventoryManager.currentTorsoEquipment != null)
            {
                torsoModelChanger.EquipTorsoModelByName(playerInventoryManager.currentTorsoEquipment.torsoModelName);
                playerStatsManager.physicalDamageAbsoptionBody = playerInventoryManager.currentTorsoEquipment.physicalDefense;
                Debug.Log("Torso Absorption is" + playerStatsManager.physicalDamageAbsoptionBody + "%");
            }
            else
            {
                torsoModelChanger.EquipTorsoModelByName(nakedTorsoModel);
                playerStatsManager.physicalDamageAbsoptionBody = 0;
            }

             //LEG EQUIPMENT
            hipModelChanger.UnEquipAllHipModels();
            leftLegModelChanger.UnEquipAllLegModels();
            rightLegModelChanger.UnEquipAllLegModels();
            if(playerInventoryManager.currentLegEquipment != null)
            {
                hipModelChanger.EquipHipModelByName(playerInventoryManager.currentLegEquipment.hipModelName);
                leftLegModelChanger.EquipLegModelByName(playerInventoryManager.currentLegEquipment.leftLegName);
                rightLegModelChanger.EquipLegModelByName(playerInventoryManager.currentLegEquipment.rightLegName);
                playerStatsManager.physicalDamageAbsoptionLegs = playerInventoryManager.currentLegEquipment.physicalDefense;
                Debug.Log("Hip Absorption is" + playerStatsManager.physicalDamageAbsoptionLegs + "%");
            } 
            else
            {
                hipModelChanger.EquipHipModelByName(nakedHipModel);
                leftLegModelChanger.EquipLegModelByName(nakedLeftLeg);
                rightLegModelChanger.EquipLegModelByName(nakedRightLeg);
                playerStatsManager.physicalDamageAbsoptionLegs = 0;
            }
            //HAND EQUIPMENT

        }

        public void OpenBlockindCollider()
        {
            if (inputHandler.twoHandFlag)
            {
                blockingCollider.SetColliderDamageAbsorption(playerInventoryManager.rightWeapon);
            }
            else
            {
                blockingCollider.SetColliderDamageAbsorption(playerInventoryManager.leftWeapon);

            }

            blockingCollider.EnableBlockCollider();
        }

        public void CloseBlockingCollider()
        {
            blockingCollider.DisableBlockCollider();
        }
    }

}
