using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class Interactible : MonoBehaviour
    {
        public float radius = 0.6f;
        public string interactibleText;

        private void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        public virtual void Interact(PlayerManager playerManager)
        {
            Debug.Log("You interacted with an object");
        }
    }
}

