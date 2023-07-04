namespace RPG.Character
{
    public abstract class AiBaseState
    {
        public abstract void EnterState(EnemyController enemy);

        public abstract void UpdateState(EnemyController enemy);
    }
}