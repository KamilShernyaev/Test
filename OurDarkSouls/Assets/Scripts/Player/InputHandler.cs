using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool a_Input;
        public bool b_Input;
        public bool y_Input;
        public bool rb_input;
        public bool rt_input;
        public bool jump_Input;
        public bool inventory_Input;
        public bool lockOnInput;
        public bool right_Stick_Right_Input;
        public bool right_Stick_Left_Input;

        public bool d_Pad_Up;
        public bool d_Pad_Down;
        public bool d_Pad_Left;
        public bool d_Pad_Right;

        public bool rollFlag;
        public bool twoHandFlag;
        public bool sprintFlag;
        public bool comboFlag;
        public bool lockOnFlag;
        public bool inventoryFlag;
        public float rollInputTimer;
        
        PlayesControls inputActions;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        PlayerManager playerManager;
        WeaponSlotManager weaponSlotManager;
        CameraHandler cameraManager;
        AnimatorHadler animatorHadler;
        UIManager uIManager;

        Vector2 movementInput;
        Vector2 cameraInput;
      
        private void Awake() 
          {
            playerAttacker = GetComponentInChildren<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
            uIManager = FindObjectOfType<UIManager>();
            cameraManager = FindObjectOfType<CameraHandler>();
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
            animatorHadler = GetComponentInChildren<AnimatorHadler>();
          }
        public void OnEnable() 
          {
            if(inputActions == null)
              {
                inputActions = new PlayesControls();

                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
                inputActions.PlayerQuickSlots.DPadRight.performed += i => d_Pad_Right = true;
                inputActions.PlayerQuickSlots.DPadLeft.performed += i => d_Pad_Left = true;
                inputActions.PlayerActions.RB.performed += i => rb_input = true;
                inputActions.PlayerActions.RT.performed += i => rt_input = true;
                inputActions.PlayerActions.A.performed += i => a_Input = true;
                inputActions.PlayerActions.Jump.performed += i => jump_Input = true;
                inputActions.PlayerActions.Inventory.performed += i => inventory_Input = true;
                inputActions.PlayerActions.LockOn.performed += i => lockOnInput = true;
                inputActions.PlayerActions.Y.performed += i => y_Input = true;
                inputActions.PlayerMovement.LockOnTargetRight.performed += i  => right_Stick_Right_Input = true;
                inputActions.PlayerMovement.LockOnTargetLeft.performed += i  => right_Stick_Left_Input = true; 
              }
              inputActions.Enable();
          }
          private void OnDisable() 
          {
              inputActions.Disable();
          }
          public void TickInput(float delta) 
          {
              HandleMoveInput(delta);
              HandleRollInput(delta);
              HandleAttackInput(delta);
              HandleQuickSlotsInput();
              HandleInventoryInput();
              HandleLockOnInput();
              HandleTwoHandInput();
          }
          private void HandleMoveInput(float delta)

          {
              horizontal = movementInput.x;
              vertical = movementInput.y;
              moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
              mouseX = cameraInput.x;
              mouseY = cameraInput.y;
          }

          private void HandleRollInput(float delta)
          {
            b_Input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            sprintFlag = b_Input;

            if(b_Input)
            {
              rollInputTimer += delta;

            }
            else
            {
              if(rollInputTimer > 0 && rollInputTimer < 0.5f)
              {
                sprintFlag = false;
                rollFlag = true;
              }

              rollInputTimer = 0;
            }
          }

          private void HandleAttackInput(float delta) 
          {

            if(rb_input)
            {
                playerAttacker.HandleRBAction();
            }

            if(rt_input)
            {
              playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);

            }
          }

           private void HandleQuickSlotsInput()
          {
            if(d_Pad_Right)
            {
              playerInventory.ChangeRightWeapon();
            }
            else if(d_Pad_Left)
            {
              playerInventory.ChangeLeftWeapon();
            }
          }

          private void HandleInventoryInput()
          {
            if(inventory_Input)
            {
              inventoryFlag = !inventoryFlag;

              if(inventoryFlag)
              {
                uIManager.OpenSelectWindow();
                uIManager.UpdateUI();
                uIManager.hudWindow.SetActive(false);
              }
              else
              {
                uIManager.CloseSelectWindow();
                uIManager.CloseAllInventoryWindow();
                uIManager.hudWindow.SetActive(true);
              }
            }
          }

          private void HandleLockOnInput()
          {
            if (lockOnInput && lockOnFlag == false)
            {             
              lockOnInput = false;
              cameraManager.HandleLockOn();
              if (cameraManager.nearestLockOnTarget != null)
              {
                cameraManager.currentLockOnTarget = cameraManager.nearestLockOnTarget;
                lockOnFlag = true;
              }
            }
            else if (lockOnInput && lockOnFlag)
            {
              lockOnInput = false;
              lockOnFlag = false;
              cameraManager.ClearLockOnTarget();
            }

            if (lockOnFlag && right_Stick_Left_Input)
            {
              right_Stick_Left_Input = false;
              cameraManager.HandleLockOn();
              if (cameraManager.leftLockTarget != null)
              {
                cameraManager.currentLockOnTarget = cameraManager.leftLockTarget;
              }
            }

            if (lockOnFlag && right_Stick_Right_Input)
            {
              right_Stick_Left_Input = false;
              cameraManager.HandleLockOn();
              
              if(cameraManager.RightLockTarget != null)
              {
                cameraManager.currentLockOnTarget = cameraManager.RightLockTarget;
              }
            }

            cameraManager.SetCameraHeigh();
          }

          private void HandleTwoHandInput()
          {
            if(y_Input)
            {
              y_Input = false;

              twoHandFlag = !twoHandFlag;

              if(twoHandFlag)
              {
                weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon, false);
              }
              else
              {
                weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon, false);
                weaponSlotManager.LoadWeaponOnSlot(playerInventory.leftWeapon, true);
              }
            }
          }
    }
}

