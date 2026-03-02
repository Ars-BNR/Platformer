using Platformer.Components;
using Platformer.Components.ColliderBased;
using Platformer.Components.GoBased;
using Platformer.Components.Health;
using Platformer.Creatures;
using Platformer.Model;
using Platformer.Model.Data;
using Platformer.Model.Definitions;
using Platformer.Model.Definitions.Repositories;
using Platformer.Model.Definitions.Repositories.Items;
using Platformer.Utils;
using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

namespace Platformer
{

    public class Hero : Creature, ICanAddInventory
    {
        [SerializeField] private CheckCircleOverlap _interactionCheck;
        [SerializeField] private ColliderCheck _wallCheck;

        [SerializeField] private float _slamDownVelocity;
        [SerializeField] private float _slamDownVelocityAfterLongFalling;
        [SerializeField] private int _damageAfterFalling;

        [SerializeField] private Cooldown _throwCooldown;
        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _unarmed;

        [Header("Super throw")]
        [SerializeField] private Cooldown _superThrowCooldown;
        [SerializeField] private int _superThrowParticles;
        [SerializeField] private float _superThrowDelay;

        [Space]
        [SerializeField] private ProbabilityDropComponent _hitDrop;
        [SerializeField] private SpawnComponent _throwSpawner;

        private static readonly int Throw_trigger = Animator.StringToHash("throw");
        private static readonly int IsOnWall = Animator.StringToHash("is-on-wall");


        private bool _allowDoubleJump;
        private bool _isOnWall;
        private bool _superThrow;

        private GameSession _session;
        private HealthComponent _health;
        private float _defaultGravityScale;

        private const string SwordId = "Sword";
        private int numCoins => _session.Data.Inventory.Count("Coin");
        private int SwordCount => _session.Data.Inventory.Count(SwordId);

        private string SelectedItemId => _session.QuickInventory.SelectedItem.Id;

        private bool CanThrow
        {
            get
            {
                if (SelectedItemId == SwordId)
                    return SwordCount > 1;

                var def = DefsFacade.I.Items.Get(SelectedItemId);
                return def.HasTag(ItemTag.Throwable);
            }
        }

        private int PortionCountSmall => _session.Data.Inventory.Count("PortionSmall");

        protected override void Awake()
        {
            base.Awake();
            _defaultGravityScale = _rigidbody.gravityScale;
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _health = GetComponent<HealthComponent>();

            _session.Data.Inventory.OnChanged += OnInventoryChanged;


            _health.SetHealth(_session.Data.HP.Value);
            _session.Data.Inventory.OnChanged += OnInventoryChanged;

            UpdateHeroWeapon();
        }

        private void OnInventoryChanged(string id, int value)
        {
            if (id == SwordId)
                UpdateHeroWeapon();
        }

        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.HP.Value = currentHealth;
        }

        protected override void Update()
        {
            base.Update();

            var moveToSameDirection = Direction.x * transform.lossyScale.x > 0;

            if (_wallCheck.IsTouchingLayer && moveToSameDirection)
            {
                _isOnWall = true;
                _rigidbody.gravityScale = 0;
            }
            else
            {
                _isOnWall = false;
                _rigidbody.gravityScale = _defaultGravityScale;
            }

            Animator.SetBool(IsOnWall, _isOnWall);
        }

        protected override float CalculateYVelocity()
        {
            var yVelocity = _rigidbody.velocity.y;
            var isJumpPressing = Direction.y > 0;

            if (IsGrounded || _isOnWall)
            {
                _allowDoubleJump = true;
            }

            if (!isJumpPressing && _isOnWall)
            {
                return 0f;
            }

            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVelocity)
        {
            if (!IsGrounded && _allowDoubleJump && _session.PerksModel.IsDoubleJumpSupported && !_isOnWall)
            {
                _allowDoubleJump = false;
                DoJumpVfx();
                return _jumpSpeed;
            }

            return base.CalculateJumpVelocity(yVelocity);
        }

        public void AddInInventory(string id, int value)
        {
            _session.Data.Inventory.Add(id, value);
        }


        public override void TakeDamage()
        {
            base.TakeDamage();
            if (numCoins > 0)
            {
                SpawnCoins();
            }
        }

        private void SpawnCoins()
        {

            var numCoinsToDispose = Mathf.Min(numCoins, 5);
            _session.Data.Inventory.Remove("Coin", numCoinsToDispose);

            _hitDrop.SetCount(numCoinsToDispose);
            _hitDrop.CalculateDrop();
        }

        internal void Interact()
        {
            _interactionCheck.Check();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.IsInLayer(_groundLayer))
            {
                var contact = other.contacts[0];
                if (contact.relativeVelocity.y >= _slamDownVelocity)
                {
                    _particles.Spawn("SlamDown");
                }
                if (contact.relativeVelocity.y >= _slamDownVelocityAfterLongFalling)
                {
                    GetComponent<HealthComponent>().ModifyHealth(-_damageAfterFalling);
                }
            }
        }



        public override void Attack()
        {
            if (SwordCount <= 0) return;

            base.Attack();
            _particles.Spawn("SwordParticles");
        }

        private void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = SwordCount == 0 ? _unarmed : _armed;

        }
        public void useHill()
        {
            var portionCount = _session.Data.Inventory.Count("PortionSmall");
            if (portionCount > 0)
            {
                _health.ModifyHealth(5);
                _session.Data.Inventory.Remove("PortionSmall", 1);
            }
        }

        public void OnDoThrow()
        {
            if (_superThrow && _session.PerksModel.IsSuperThrowSupported)
            {
                var throwableCount = _session.Data.Inventory.Count(SelectedItemId);
                var posibleCount = SelectedItemId == SwordId ? throwableCount - 1 : throwableCount;

                var numThrows = Mathf.Min(_superThrowParticles, posibleCount);
                StartCoroutine(DoSuperThrow(numThrows));
            }
            else
            {
                ThrowAndRemoveFromInventory();
            }

            _superThrow = false;
        }

        private IEnumerator DoSuperThrow(int numThrows)
        {
            for (int i = 0; i < numThrows; i++)
            {
                ThrowAndRemoveFromInventory();
                yield return new WaitForSeconds(_superThrowDelay);
            }
        }

        private void ThrowAndRemoveFromInventory()
        {
            Sounds.Play("Range");

            var throwableId = _session.QuickInventory.SelectedItem.Id;
            var throwableDef = DefsFacade.I.Trowable.Get(throwableId);
            _throwSpawner.SetPrefab(throwableDef.Projectile);
            _throwSpawner.Spawn();

            _session.Data.Inventory.Remove(throwableId, 1);
        }

        public void StartThrowing()
        {
            _superThrowCooldown.Reset();
        }

        public void UseInventory()
        {
            if (IsSelectedItem(ItemTag.Throwable))
            {
                PerformTrowing();
            }
            else if (IsSelectedItem(ItemTag.Potion))
            {
                UsePotion();
            }

        }

        private void UsePotion()
        {
            var potion = DefsFacade.I.Portions.Get(SelectedItemId);

            switch (potion.Effect)
            {
                case Effect.AddHp:
                    _session.Data.HP.Value += (int)potion.Value;
                    break;
                case Effect.SpeedUp:
                    _speedUpCooldown.Value = _speedUpCooldown.TimeLasts + potion.Time;
                    _additionalSpeed = Mathf.Max(potion.Value, _additionalSpeed);
                    _speedUpCooldown.Reset();
                    break;
            }


            _session.Data.Inventory.Remove(potion.Id, 1);
        }

        private Cooldown _speedUpCooldown = new Cooldown();
        private float _additionalSpeed;

        protected override float CalculateSpeed()
        {
            if (_speedUpCooldown.IsReady)
                _additionalSpeed = 0f;
            return base.CalculateSpeed() + _additionalSpeed;
        }

        private bool IsSelectedItem(ItemTag tag)
        {
            return _session.QuickInventory.SelectedDef.HasTag(tag);
        }

        private void PerformTrowing()
        {
            if (!_throwCooldown.IsReady || !CanThrow) return;

            if (_superThrowCooldown.IsReady) _superThrow = true;

            Animator.SetTrigger(Throw_trigger);
            _throwCooldown.Reset();
        }

        public void NextItem()
        {
            _session.QuickInventory.SetNextItem();
        }

    }
}