
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyManager : CharacterManager
    {
        EnemyLocomotionManager enemyLocomotionManager;
        public bool isPreformingAction;

        [Header ("A.I. Settings")]
        public float detectionRadius = 20;        
        public float maximumDetectionAngle = 50;
        public float mininumDetectionAngle = -50;
        private void Awake() 
        {
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
        }

        private void Update() 
        {

        }

        private void FixedUpdate() 
        {
            HandleCurrentAction();
        }

        private void HandleCurrentAction()
        {
            if (enemyLocomotionManager.currentTarget == null)
            {
                enemyLocomotionManager.HandleDerection();
            }
            else
            {
                enemyLocomotionManager.HandleMoveToTarget();
            }
        }
    }
}