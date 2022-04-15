using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SG
{
    public class EnemyLocomotionManager : MonoBehaviour
    {
        EnemyManager enemyManager;
        EnemyAnimatorManager enemyAnimatorManager;
        public NavMeshAgent navMeshAgent;
        public Rigidbody enemyRigidbody;

        public CharacterStats currentTarget;
        public LayerMask detectionLayer;

        public float distanceFromTarget;
        public float stoppingDistance = 1f;

        public float rotationSpeed = 15;

        private void Awake() 
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
            enemyRigidbody = GetComponent<Rigidbody>();
        }

        private void Start() 
        {
            navMeshAgent.enabled = false;
            enemyRigidbody.isKinematic = false;
        }

        public void HandleDerection()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();

                if(characterStats != null)
                {
                    Vector3 targetDirection = characterStats.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                    if (viewableAngle > enemyManager.mininumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                    {
                        currentTarget = characterStats;
                    }                
                }
            }
        }

        public void HandleMoveToTarget()
        {
            if (enemyManager.isPreformingAction)
                return;

            Vector3 targetDirection = currentTarget.transform.position - transform.position;
            distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
            float viewableAngle = Vector3.Angle(targetDirection, transform.position);

            if(enemyManager.isPreformingAction)
            {
                enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                navMeshAgent.enabled = false;
            }
            else
            {
                if(distanceFromTarget > stoppingDistance)
                {
                    enemyAnimatorManager.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
                }
                else if(distanceFromTarget <= stoppingDistance)
                {
                    enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                }
            }

            HandleRotateTowardsTarget();
            navMeshAgent.transform.localPosition = Vector3.zero;
            navMeshAgent.transform.localRotation = Quaternion.identity;
        }
    
        public void HandleRotateTowardsTarget()
        {
            if(enemyManager.isPreformingAction)
            {
                Vector3 direction = currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if(direction == Vector3.zero)
                {
                    direction = transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
            }
            else
            {
                Vector3 relativeDirection = transform.InverseTransformDirection(navMeshAgent.desiredVelocity);
                Vector3 targetVelocity = enemyRigidbody.velocity;

                navMeshAgent.enabled = true;
                navMeshAgent.SetDestination(currentTarget.transform.position);
                enemyRigidbody.velocity = targetVelocity;
                transform.rotation = Quaternion.Slerp(transform.rotation, navMeshAgent.transform.rotation, rotationSpeed/Time.deltaTime);
            }
        }
    }
}
