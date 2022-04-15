using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyManager : CharacterManager
    {
        EnemyLocomotionManager enemyLocomotionManager;
        EnemyAnimatorManager enemyAnimationManager;
        EnemyStats enemyStats;
        public State currentState;   //Новое
        public CharacterStats currentTarget; //Новое

        public bool isPreformingAction;

        [Header ("A.I. Settings")]
        public float detectionRadius = 20;        
        public float maximumDetectionAngle = 50;
        public float mininumDetectionAngle = -50;

        public float currentRecoveryTime = 0;
        private void Awake() 
        {
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
            enemyStats = GetComponent<EnemyStats>();
            enemyAnimationManager = GetComponentInChildren<EnemyAnimatorManager>();
        }

        private void Update() 
        {
            HandleRecoveryTimer();
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
        #region Attacks

        private void AttackTarget()
        {
        //     if(isPreformingAction)
        //     return;

        //     if (currentAttack == null)
        //     {
        //         GetNewAttack();
        //     }
        //     else
        //     {
        //         isPreformingAction = true;
        //         currentRecoveryTime = currentAttack.recoveryTime;
        //         enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
        //         currentAttack = null;
        //     }
        }

        private void GetNewAttack()
        {
            // Vector3 tagretDirection = enemyLocomotionManager.currentTarget.transform.position - transform.position;
            // float viewableAngle = Vector3.Angle(tagretDirection, transform.forward);
            // enemyLocomotionManager.distanceFromTarget = Vector3.Distance(enemyLocomotionManager.currentTarget.transform.position, transform.position);

            // int maxScore = 0;

            // for (int i = 0; i < enemyAttacks.Length; i++)
            // {
            //     EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            //     if (enemyLocomotionManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack && enemyLocomotionManager.distanceFromTarget >= enemyAttackAction.mininumDistanceNeededToAttack)
            //     {
            //         if (viewableAngle <= enemyAttackAction.maximumAttackAngle 
            //         && viewableAngle >= enemyAttackAction.minimumAttackAngle)
            //         {
            //             maxScore += enemyAttackAction.attackScore;
            //         }
            //     }
            // }
            
            // int randomValue = Random.Range(0, maxScore);
            // int temporaryScore = 0;

            // for (int i = 0; i < enemyAttacks.Length; i++)
            // {
            //     EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            //     if (enemyLocomotionManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
            //     && enemyLocomotionManager.distanceFromTarget >= enemyAttackAction.mininumDistanceNeededToAttack)
            //     {
            //         if (viewableAngle <= enemyAttackAction.maximumAttackAngle 
            //         && viewableAngle >= enemyAttackAction.minimumAttackAngle)
            //         {
            //            if (currentAttack != null)
            //            return;

            //            temporaryScore += enemyAttackAction.attackScore;

            //            if (temporaryScore > randomValue)
            //            {
            //                currentAttack = enemyAttackAction;
            //            }
            //         }
            //     }
            // }
        }
        #endregion
    }
}

