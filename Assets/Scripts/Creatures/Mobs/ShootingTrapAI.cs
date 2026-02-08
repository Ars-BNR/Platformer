using Platformer.Components.ColliderBased;
using Platformer.Components.GoBased;
using Platformer.Utils;
using UnityEngine;

namespace Platformer.Creatures.Mobs
{

    public class ShootingTrapAI : MonoBehaviour
    {
        [SerializeField] public ColliderCheck _vision;

        [Header("Melee")]
        [SerializeField] private Cooldown _meleeCooldown;
        [SerializeField] private CheckCircleOverlap _meleeAttack;
        [SerializeField] private ColliderCheck _meleeCanAttack;

        [Header("Range")]
        [SerializeField] private Cooldown _rangeCooldown;
        [SerializeField] private SpawnComponent _rangeAttack;

        private Animator _animator;

        private static readonly int Melee_event = Animator.StringToHash("melee");
        private static readonly int Range_event = Animator.StringToHash("range");


        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_vision.IsTouchingLayer)
            {
                Shoot();
            }
        }
        public void Shoot()
        {
            if (_meleeCanAttack.IsTouchingLayer)
            {
                if (_meleeCooldown.IsReady)
                    MeleeAttack();
                return;
            }

            if (_rangeCooldown.IsReady)
                RangeAttack();
        }
        private void RangeAttack()
        {
            _rangeCooldown.Reset();
            _animator.SetTrigger(Range_event);
        }

        private void MeleeAttack()
        {
            _meleeCooldown.Reset();
            _animator.SetTrigger(Melee_event);
        }

        private void onMeleeAttack()
        {
            _meleeAttack.Check();
        }

        private void onRangeAttack()
        {
            _rangeAttack.Spawn();
        }
    }

}