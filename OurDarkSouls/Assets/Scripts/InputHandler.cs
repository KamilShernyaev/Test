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
        public bool b_Input;

        public bool rb_input;
        public bool rt_input;
        public bool d_Pad_Up;
        public bool d_Pad_Down;
        public bool d_Pad_Left;
        public bool d_Pad_Right;

        public bool rollFlag;
        public bool sprintFlag;
        public bool comboFlag;
        public float rollInputTimer;
        
        PlayesControls inputActions;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        PlayerManager playerManager;

        Vector2 movementInput;
        Vector2 cameraInput;
      
        private void Awake() 
          {
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
          }
        public void OnEnable() 
          {
            if(inputActions == null)
              {
                inputActions = new PlayesControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
                b_Input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;
                inputActions.PlayerActions.RB.performed += i => rb_input = true;
                inputActions.PlayerActions.RT.performed += i => rt_input = true;
                inputActions.PlayerQuickSlots.DPadRight.performed += i => d_Pad_Right = true;
                inputActions.PlayerQuickSlots.DPadLeft.performed += i => d_Pad_Left = true;
              }
              inputActions.Enable();
          }
          private void OnDisable() 
          {
              inputActions.Disable();
          }
          public void TickInput(float delta) 
          {
              MoveInput(delta);
              HandleRollInput(delta);
              HandleAttackInput(delta);
              HandleQuickSlotsInput();
          }
          private void MoveInput(float delta)

          {
              horizontal = movementInput.x;
              vertical = movementInput.y;
              moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
              mouseX = cameraInput.x;
              mouseY = cameraInput.y;
          }

          private void HandleRollInput(float delta)
          {


            if(b_Input)
            {
              rollInputTimer += delta;
              sprintFlag = true;

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
              if(playerManager.canDoCombo)
              {
                comboFlag = true;
                playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
                comboFlag = false;
              }
              else 
              {
                if(playerManager.isInteracting)
                  return;

                if(playerManager.canDoCombo)
                  return;

                playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
              }
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
    }
}

