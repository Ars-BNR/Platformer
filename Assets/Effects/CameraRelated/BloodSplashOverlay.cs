using Platformer.Model;
using Platformer.Model.Definitions.Player;
using Platformer.Utils.Disposables;
using UnityEngine;

namespace Platformer.Effects.CameraRelated
{
    [RequireComponent(typeof(Animator))]
    public class BloodSplashOverlay : MonoBehaviour
    {
        [SerializeField] private Transform _overlay;
        private static readonly int Health = Animator.StringToHash("Health");

        private Animator _animator;
        private Vector3 _overScale;
        private GameSession _session;

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _overScale = _overlay.localScale - Vector3.one;

            _session = FindObjectOfType<GameSession>();
            _session.Data.HP.SubscribeAndInvoke(OnHPChanged);
        }

        private void OnHPChanged(int newValue, int _)
        {
            var maxHP = _session.StatsModel.GetValue(StatId.Hp);
            var hpNormalized = newValue / maxHP;
            _animator.SetFloat(Health, hpNormalized);

            var overlayModifier = Mathf.Max(hpNormalized -0.3f, 0f);
            _overlay.localScale = Vector3.one + _overScale * overlayModifier;
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}
