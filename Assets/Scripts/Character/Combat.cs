using System;
using UnityEngine;
using UnityEngine.InputSystem;
using RPG.Utility;

namespace RPG.Character
{
    public class Combat : MonoBehaviour
    {
        [NonSerialized]
        public float damage = 0f;

        private Animator animatorCmp;

        protected void Awake()
        {
            animatorCmp = GetComponentInChildren<Animator>();
        }

        public void HandleAttack(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            StartAttack();
        }

        private void StartAttack()
        {
            animatorCmp.SetFloat(Constants.SPEED_ANIMATOR_PARAM, 0);
            animatorCmp.SetTrigger(Constants.ATTACK_ANIMATOR_PARAM);
        }
    }
}
