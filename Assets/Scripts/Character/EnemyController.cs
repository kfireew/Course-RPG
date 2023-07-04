using System;
using UnityEngine;
using RPG.Utility;

namespace RPG.Character
{
    public class EnemyController : MonoBehaviour
    {
        [NonSerialized]
        public float distanceFromPlayer;
        [NonSerialized]
        public Vector3 originalPosition;
        [NonSerialized]
        public Movement movementCmp;
        [NonSerialized]
        public GameObject player;
        [NonSerialized]
        public Patrol patrolCmp;
        private Health healthCmp;
        private Combat combatCmp;

        public CharacterStatsSO stats;

        public float chaseRange = 2.5f;
        public float attackRange = 1f;

        private AiBaseState currentState;
        public AiReturnState returnState = new();
        public AiChaseState chaseState = new();
        public AiAttackState attackState = new();
        public AiPatrolState patrolState = new();

        protected void Awake()
        {
            if (stats == null)
                Debug.LogWarning($"{name} does not have stats.");

            currentState = returnState;

            player = GameObject.FindWithTag(Constants.PLAYER_TAG);
            movementCmp = GetComponent<Movement>();
            patrolCmp = GetComponent<Patrol>();
            healthCmp = GetComponent<Health>();
            combatCmp = GetComponent<Combat>();

            originalPosition = transform.position;
        }

        protected void Start()
        {
            currentState.EnterState(this);

            healthCmp.healthPoints = stats.health;
            combatCmp.damage = stats.damage;
        }

        protected void Update()
        {
            CalculateDistanceFromPlayer();

            currentState.UpdateState(this);
        }

        public void SwitchState(AiBaseState newState)
        {
            currentState = newState;
            currentState.EnterState(this);
        }

        private void CalculateDistanceFromPlayer()
        {
            if (player == null) return;

            Vector3 enemyPosition = transform.position;
            Vector3 playerPosition = player.transform.position;

            distanceFromPlayer = Vector3.Distance(enemyPosition, playerPosition);
        }

        protected void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(
                transform.position,
                chaseRange
            );
        }
    }
}
