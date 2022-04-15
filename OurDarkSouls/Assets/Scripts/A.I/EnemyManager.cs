
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyManager : CharacterManager
    {
        EnemyLocomotionManager enemyLocomotionManager;
        
        EnemyStats enemyStats; //Новое
        public State currentState;   //Новое
        public CharacterStats currentTarget; //Новое

        public bool isPreformingAction;

        [Header ("A.I. Settings")]
        public float detectionRadius = 20;        
        public float maximumDetectionAngle = 50;
        public float mininumDetectionAngle = -50;
        private void Awake() 
        {
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
            enemyStats = GetComponent<EnemyStats>(); //новое
        }

        private void Update() 
        {

        }

        private void FixedUpdate() 
        {
            HandleStateMachine();
        }

        private void HandleStateMachine()
        { //Удалил скрипт вставил новый
            if(currentState != null)
            {
                State nextState = currentState.Tick(this, enemyStats, enemyAnimatorManager);

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
            // Конец нового скрипта
    }
}