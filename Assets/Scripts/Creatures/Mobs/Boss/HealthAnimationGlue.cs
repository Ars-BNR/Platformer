using Platformer.Components.Health;
using Platformer.Utils.Disposables;
using UnityEngine;

namespace Platformer.Creatures.Mobs.Boss
{
    public class HealthAnimationGlue : MonoBehaviour
    {
        [SerializeField] private HealthComponent _hp;
        [SerializeField] private Animator _animator;
        private static readonly int Health = Animator.StringToHash("Health");

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private void Awake()
        {
            _trash.Retain(_hp._onChange.Subsrcibe(OnHealthChanged));
        }

        private void OnHealthChanged(int health)
        {
            _animator.SetInteger(Health, health);
        }

        private void Dispose()
        {
            _trash.Dispose();
        }
    }
}
