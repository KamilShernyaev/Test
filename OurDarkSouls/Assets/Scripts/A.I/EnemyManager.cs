using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace SG
{
    public class EnemyManager : CharacterManager
    {
        EnemyLocomotionManager enemyLocomotionManager;
        EnemyAnimatorManager enemyAnimationManager;
        EnemyStats enemyStats;

        public State currentState;   //Новое
        public CharacterStats currentTarget; //Новое
        public NavMeshAgent navMeshAgent;
        public Rigidbody enemyRigidbody;

        public bool isPreformingAction; 
        public bool isInteracting;
        public float rotationSpeed = 15;
        public float maximumAttackRange = 1.5f;

        [Header ("A.I. Settings")]
        public float detectionRadius = 20;        
        public float maximumDetectionAngle = 50;
        public float mininumDetectionAngle = -50;

        public float currentRecoveryTime = 0;
        private void Awake() 
        {
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
            enemyStats = GetComponent<EnemyStats>();
            enemyRigidbody = GetComponent<Rigidbody>();
            enemyAnimationManager = GetComponentInChildren<EnemyAnimatorManager>();
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
            navMeshAgent.enabled = false;
        }

        private void Start() 
        {
            enemyRigidbody.isKinematic = false;    
        }

        private void Update() 
        {
            HandleRecoveryTimer();

            isInteracting = enemyAnimationManager.anim.GetBool("isInteracting");
        }

        private void FixedUpdate() 
        {
            HandleStateMachine();
        }

        private void HandleStateMachine()
        { 
            if(currentState != null)
            {
                State nextState = currentState.Tick(this, enemyStats, enemyAnimationManager);
            
                if(nextState != null)
                {
                    SwitchToNextState(nextState);
                }
            }
        }
            
        private void SwitchToNextState(State state)
        {
            currentState = state;
        }
        
        private void HandleRecoveryTimer()
        {
            if(currentRecoveryTime > 0)
            {                
                currentRecoveryTime -= Time.deltaTime;
            }

            if(isPreformingAction)
            {
               if (currentRecoveryTime <= 0)
               {
                   isPreformingAction = false;
               }
            }
        }
    }
}

