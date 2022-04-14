using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CameraHandler : MonoBehaviour
    {
        InputHandler inputHandler;
        public Transform targetTransform;
        public Transform cameraTransform;
        public Transform cameraPivotTransform;
        private Transform myTransform;
        private Vector3 cameraTransformPosition;
        public LayerMask ignoreLayers;

        public static CameraHandler singleton;

        public float lookspeed = 0.1f;
        public float followSpeed = 0.1f;
        public float pivotSpeed = 0.3f;

        private float defaultPosition;
        private float lookAngle;
        private float pivotAngle;
        public float minimupPivot = -35;
        public float maximumPivot = 35;

        public Transform currentLockOnTarget;
        List<CharacterManager> availableTargets = new List<CharacterManager>();
        public Transform nearestLockOnTarget;
        public float maximumLockOnDistance = 30;

        private void Awake() 
        {
            singleton = this;
            myTransform = transform;
            defaultPosition = cameraTransform.localPosition.z;
            ignoreLayers = ~(1 << 8 | 1 << 9 | 1 <<10);
            targetTransform = FindObjectOfType<PlayerManager>().transform;
            inputHandler = FindObjectOfType<InputHandler>();
        }

        public void FollowTarget(float delta) 
        {
            Vector3 targetPosition = Vector3.Lerp(myTransform.position, targetTransform.position, delta/followSpeed);
            myTransform.position = targetPosition;
        }

        public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
        {
            if (inputHandler.lockOnFlag == false && currentLockOnTarget == null)
            {
                lookAngle += (mouseXInput * lookspeed) / delta;
                pivotAngle -= (mouseYInput * pivotSpeed) / delta;
                pivotAngle = Mathf.Clamp(pivotAngle, minimupPivot, maximumPivot);

                Vector3 rotation = Vector3.zero;
                rotation.y = lookAngle;
                Quaternion targetRotation = Quaternion.Euler(rotation);
                myTransform.rotation = targetRotation;

                rotation = Vector3.zero;
                rotation.x = pivotAngle;

                targetRotation = Quaternion.Euler(rotation);
                cameraPivotTransform.localRotation = targetRotation;
            }
            else
            {
                float velocity = 0;

                Vector3 dir = currentLockOnTarget.position - transform.position;
                dir.Normalize();
                dir.y = 0;

                Quaternion targetRotation = Quaternion.LookRotation(dir);
                transform.rotation = targetRotation;

                dir = currentLockOnTarget.position - cameraPivotTransform.position;
                dir.Normalize();

                targetRotation = Quaternion.LookRotation(dir);
                Vector3 eulerAngle = targetRotation.eulerAngles;
                eulerAngle.y = 0;
                cameraPivotTransform.localEulerAngles = eulerAngle;
            }
        }

        public void HandleLockOn()
        {
            float shortestDistance = Mathf.Infinity;

            Collider[] colliders = Physics.OverlapSphere(targetTransform.position, 26);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterManager character = colliders[i].GetComponent<CharacterManager>();

                if (character != null)
                {
                    Vector3 lockTargetDirection = character.transform.position - targetTransform.position;
                    float distanceFromTarget = Vector3.Distance(targetTransform.position, character.transform.position);
                    float viewableAngle = Vector3.Angle(lockTargetDirection, cameraTransform.forward);

                    if(character.transform.root != targetTransform.transform.root 
                    && viewableAngle > -50 
                    && viewableAngle < 50 
                    && distanceFromTarget <= maximumLockOnDistance)
                    {
                        availableTargets.Add(character);
                    }
                }
            }

            for (int k = 0; k < availableTargets.Count; k++)
            {
                float distanceFromTarget = Vector3.Distance(targetTransform.position, availableTargets[k].transform.position);

                if (distanceFromTarget < shortestDistance)
                {
                    shortestDistance = distanceFromTarget;
                    nearestLockOnTarget = availableTargets[k].lickOnTransform;
                }
            }
        }

        
          public void ClearLockOnTarget()
          {
            availableTargets.Clear();
            nearestLockOnTarget = null;
            currentLockOnTarget = null;
          }
    }
}
