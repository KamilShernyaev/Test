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

        [Header("Combat Flags")]
        public bool canDoCombo;

        [Header ("A.I. Settings")]
        public float detectionRadius = 20;        
        public float maximumDetectionAngle = 50;
        public float mininumDetectionAngle = -50;
        public float currentRecoveryTime = 0;

        [Header("A.I Cobat Settings")]
        public bool allowAIPerfomConbos;
        public float comboLikelyHood;


        
        


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
            HandleStateMachine();

            isInteracting = enemyAnimationManager.anim.GetBool("isInteracting");
            canDoCombo = enemyAnimationManager.anim.GetBool("canDoCombo");
            enemyAnimationManager.anim.SetBool("isDead", enemyStats.isDead);
        }

        private void LateUpdate()
        {            
            navMeshAgent.transform.localPosition = Vector3.zero;
            navMeshAgent.transform.localRotation = Quaternion.identity;
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

