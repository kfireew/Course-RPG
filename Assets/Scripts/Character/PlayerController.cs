using UnityEngine;

namespace RPG.Character
{
    public class PlayerController : MonoBehaviour
    {
        private Health healthCmp;
        private Combat combatCmp;

        public CharacterStatsSO stats;

        protected void Awake()
        {
            if (stats == null)
                Debug.LogWarning($"{name} does not have stats.");

            healthCmp = GetComponent<Health>();
            combatCmp = GetComponent<Combat>();
        }

        protected void Start()
        {
            healthCmp.healthPoints = stats.health;
            combatCmp.damage = stats.damage;
        }
    }
}
