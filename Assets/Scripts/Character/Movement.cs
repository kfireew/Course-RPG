using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using RPG.Utility;

namespace RPG.Character
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour
    {
        [NonSerialized]
        public Vector3 originalForwardVector;

        private NavMeshAgent agent;

        public Vector3 movementVector;

        protected void Awake()
        {
            agent = GetComponent<NavMeshAgent>();

            originalForwardVector = transform.forward;
        }

        protected void Start()
        {
            agent.updateRotation = false;
        }

        protected void Update()
        {
            print(movementVector);
            MovePlayer();
            if (CompareTag(Constants.PLAYER_TAG)) Rotate(movementVector);
        }

        private void MovePlayer()
        {
            Vector3 offset = movementVector * Time.deltaTime * agent.speed;
            agent.Move(offset);
        }

        public void HandleMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            movementVector = new Vector3(input.x, 0, input.y);
        }

        public void Rotate(Vector3 newForwardVector)
        {
            if (newForwardVector == Vector3.zero) return;

            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.LookRotation(newForwardVector);

            transform.rotation = Quaternion.Lerp(
                startRotation,
                endRotation,
                Time.deltaTime * agent.angularSpeed
            );
        }

        public void MoveAgentByDestination(Vector3 destination)
        {
            agent.SetDestination(destination);
        }

        public void StopMovingAgent()
        {
            agent.ResetPath();
        }

        public bool ReachedDestination()
        {
            if (agent.pathPending) return false;

            if (agent.remainingDistance > agent.stoppingDistance) return false;

            if (agent.hasPath || agent.velocity.sqrMagnitude != 0f) return false;

            return true;
        }

        public void MoveAgentByOffset(Vector3 offset)
        {
            agent.Move(offset);
        }

        public void UpdateAgentSpeed(float newSpeed)
        {
            agent.speed = newSpeed;
        }
    }
}