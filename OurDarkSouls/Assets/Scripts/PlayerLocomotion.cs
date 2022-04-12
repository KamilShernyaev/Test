using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    public class PlayerLocomotion : MonoBehaviour
    {
        PlayerManager playerManager;
        Transform cameraObject;
        InputHandler inputHandler;
        //Новый скрипт
        public Vector3 moveDirection;
        //конец Новый скрипт

        [HideInInspector]
        public Transform myTransform;
        [HideInInspector]
        public AnimatorHadler animatorHadler;
        public new Rigidbody rigidbody;
        public GameObject normalCamera;

//Новый скрипт
        [Header("Ground & Air Detection Stats")]
        [SerializeField] float groundDetectiondRayStartPoint = 0.5f;
        [SerializeField] float minimumDistanceNeededToBeginFall = 1f;
        [SerializeField] float groundDetectiondRayDistance = 0.2f;
        LayerMask ignoreForGroundCheck;
        public float isAirTimer;

//конец Новый скрипт
        [Header("Movement Stats")]
        [SerializeField]
        float movementSpeed = 5;
        [SerializeField]
        float walkingSpeed = 1;
        [SerializeField]
        float sprintSpeed = 7;

        [SerializeField]
        float rotationSpeed = 10;

//Новый скрипт
        [SerializeField] float fallingSpeed = 45;
//конец Новый скрипт


        void Start()
        {
            playerManager = GetComponent<PlayerManager>();
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            animatorHadler = GetComponentInChildren<AnimatorHadler>();
            cameraObject = Camera.main.transform;
            myTransform = transform;
            animatorHadler.Initialize();

            playerManager.isGrounded = true;
            ignoreForGroundCheck = ~(1 << 8 | 1 << 11);
        }

       
        #region Movement
        Vector3 normalVector;
        Vector3 targetPosition;

        private void HandleRotation(float delta)
        {
            Vector3 targetDir = Vector3.zero;
            float moveOverride = inputHandler.moveAmount;

            targetDir = cameraObject.forward * inputHandler.vertical;
            targetDir += cameraObject.right * inputHandler.horizontal;

            targetDir.Normalize();
            targetDir.y = 0;

            if(targetDir == Vector3.zero)
                targetDir = myTransform.forward;

            float rs = rotationSpeed;

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr,rs * delta);

            myTransform.rotation = targetRotation;
        }

        public void HandleMovement(float delta) 
        {
            if(inputHandler.rollFlag)
                return;

            if(playerManager.isInteracting) 
                return;

            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;
            moveDirection.Normalize();
            moveDirection.y = 0;

            float speed = movementSpeed;

            if(inputHandler.sprintFlag && inputHandler.moveAmount > 0.5)
            {
                speed = sprintSpeed;
                playerManager.isSprinting = true;
                moveDirection *= speed;
            }
            else
            {
                if(inputHandler.moveAmount < 0.5)
                {
                    moveDirection *= walkingSpeed;
                    playerManager.isSprinting = false;
                }
                else
                {
                    moveDirection *= speed;
                    playerManager.isSprinting = false;

                }
            }

            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            rigidbody.velocity = projectedVelocity;

            animatorHadler.UpdateAnimatorValues(inputHandler.moveAmount, 0, playerManager.isSprinting);

            if(animatorHadler.canRotate)
            {
                HandleRotation(delta);
            }
        }

        public void HandleRollingAndSprinting(float delta)
        {
            if(animatorHadler.anim.GetBool("isInteracting"))
                return;
            
            if(inputHandler.rollFlag)
            {
                moveDirection = cameraObject.forward * inputHandler.vertical;
                moveDirection += cameraObject.right * inputHandler.horizontal;

                if(inputHandler.moveAmount > 0)
                {
                    animatorHadler.PlayTargetAnimation("Rolling", true);
                    moveDirection.y = 0;
                    Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                }
                else
                {
                    animatorHadler.PlayTargetAnimation("Backstep", true);
                }
            }
        }

        public void HandleFalling(float delta, Vector3 moveDirection)
        {
            playerManager.isGrounded = false;
            RaycastHit hit;
            Vector3 origin = myTransform.position;
            origin.y += groundDetectiondRayStartPoint;

            if (Physics.Raycast(origin, myTransform.forward, out hit, 0.4f))
            {
                moveDirection = Vector3.zero;
            }

            if (playerManager.isInAir)
            {
                rigidbody.AddForce(-Vector3.up * fallingSpeed);
                rigidbody.AddForce(moveDirection * fallingSpeed / 10f);
            }

            Vector3 dir = moveDirection;
            dir.Normalize();
            origin = origin + dir * groundDetectiondRayDistance;

            targetPosition = myTransform.position;

            Debug.DrawRay(origin, - Vector3.up * minimumDistanceNeededToBeginFall, Color.red, 0.1f, false);

            if (Physics.Raycast(origin, - Vector3.up, out hit, minimumDistanceNeededToBeginFall, ignoreForGroundCheck))
            {
                normalVector = hit.normal;
                Vector3 tp = hit.point;
                playerManager.isGrounded = true;
                targetPosition.y = tp.y;

                if(playerManager.isInAir)
                {
                    if(isAirTimer > 0.5f)
                    {
                        Debug.Log("You were in the air for " + isAirTimer);
                        animatorHadler.PlayTargetAnimation("Land", true);
                        isAirTimer = 0;
                    }
                    else
                    {                        
                        animatorHadler.PlayTargetAnimation("Empty", false);
                        isAirTimer = 0;
                    }

                    playerManager.isInAir = false;
                }
            }
            else
            {
                if(playerManager.isGrounded)
                {
                    playerManager.isGrounded = false;
                }

                if(playerManager.isInAir == false)
                {
                    if(playerManager.isInteracting == false)
                    {
                        animatorHadler.PlayTargetAnimation("Falling", true);
                    }

                    Vector3 vel = rigidbody.velocity;
                    vel.Normalize();
                    rigidbody.velocity = vel * (movementSpeed / 2);
                    playerManager.isInAir = true;
                }
            }

            if(playerManager.isInteracting || inputHandler.moveAmount > 0)
            {
                myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime / 0.1f);
            }
            else
            {
                myTransform.position = targetPosition;
            }

            // if(playerManager.isGrounded)
            // {
            //     if(playerManager.isInteracting || inputHandler.moveAmount > 0)
            //     {
            //         myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime);
            //     }
            //     else
            //     {
            //         myTransform.position = targetPosition;
            //     }
            // }
        }

        #endregion
    
    
    }
}

