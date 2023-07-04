using UnityEngine;

namespace RPG.Character
{
    public class AiAttackState : AiBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.movementCmp.StopMovingAgent();
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.distanceFromPlayer > enemy.attackRange)
            {
                enemy.SwitchState(enemy.chaseState);
                return;
            }
            Debug.Log("attacking");
        }
    }
}