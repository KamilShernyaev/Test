using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerManager : CharacterManager
    {
        InputHandler inputHandler;
        Animator anim;
        CameraHandler cameraHandler;
        PlayerStats playerStats;
        PlayerLocomotion playerLocomotion;

        CriticalDamageCollider criticalDamageCollider;
        PlayerAnimatorManager playerAnimatorManager;

        InteractableUI interactableUI;
        public GameObject interactableUIGameObject;
        public GameObject itemInteractableGameObject;

        public bool isInteracting;

        [Header("Player Flags")]
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool canDoCombo;
        public bool isUsingRightHand;
        public bool isUsingLeftHand;

        public bool isInvulnerable;

        private void Awake() 
        {
          inputHandler = GetComponent<InputHandler>();
          anim = GetComponentInChildren<Animator>();
          playerStats = GetComponent<PlayerStats>();
          playerLocomotion = GetComponent<PlayerLocomotion>();
          interactableUI = FindObjectOfType<InteractableUI>();
          cameraHandler = FindObjectOfType<CameraHandler>();
          criticalDamageCollider = GetComponentInChildren<CriticalDamageCollider>();
          playerAnimatorManager = GetComponentInChildren<PlayerAnimatorManager>();
        }

        void Update() 
        {
            float delta = Time.deltaTime;
            
            isInteracting = anim.GetBool("isInteracting");
            canDoCombo = anim.GetBool("canDoCombo");            
            isUsingRightHand = anim.GetBool("isUsingRightHand");
            isUsingLeftHand = anim.GetBool("isUsingLeftHand");
            isInvulnerable = anim.GetBool("isInvulnerable");   
            anim.SetBool("isInAir", isInAir);    
            anim.SetBool("isDead", playerStats.isDead);
            playerAnimatorManager.canRotate = anim.GetBool("canRotate");

            inputHandler.TickInput(delta);
            playerLocomotion.HandleRollingAndSprinting(delta);
            playerLocomotion.HandleJumping();
            playerStats.RegenerateStamina();

            CheckForInteractableObject();
        }

         private void FixedUpdate() 
        {
            float delta = Time.fixedDeltaTime;

            playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRotation(delta);
        }

        private void LateUpdate() 
        {            
            inputHandler.rollFlag = false;
            inputHandler.rt_input = false;
            inputHandler.rb_input = false;   
            inputHandler.lt_input = false;
            inputHandler.d_Pad_Up = false;
            inputHandler.d_Pad_Down = false;
            inputHandler.d_Pad_Left = false;
            inputHandler.d_Pad_Right = false;
            inputHandler.a_Input = false; 
            inputHandler.jump_Input = false;   
            inputHandler.inventory_Input = false;

            float delta = Time.deltaTime;

            if(cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }

            if (isInAir)
            {
                playerLocomotion.isAirTimer = playerLocomotion.isAirTimer + Time.deltaTime;
            }
        }

        #region Player Interactions

       

        public void CheckForInteractableObject()
        {
            RaycastHit hit;

            if (Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 1f, cameraHandler.ignoreLayers))
            {
                if (hit.collider.tag == "Interactable")
                {
                    Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                    if (interactableObject != null)
                    {
                        string interactableText = interactableObject.interactableText;
                        interactableUI.interactableText.text = interactableText;
                        interactableUIGameObject.SetActive(true);

                        if (inputHandler.a_Input)
                        {
                            hit.collider.GetComponent<Interactable>().Interact(this);
                        }
                    }
                }
            }
            else
            {
                if(interactableUIGameObject != null)
                {
                    interactableUIGameObject.SetActive(false);
                }

                if(itemInteractableGameObject != null && inputHandler.a_Input)
                {
                    itemInteractableGameObject.SetActive(false);
                }
            }
        }
        
        public void OpenChestInteraction(Transform playerStandHereWhenOpeningChest)
        {
            playerLocomotion.rigidbody.velocity = Vector3.zero;
            transform.position = playerStandHereWhenOpeningChest.transform.position;
            Debug.Log("PlayerManager");
           
            playerAnimatorManager.PlayTargetAnimation("Open Chest", true);
        }

        #endregion
    }
}