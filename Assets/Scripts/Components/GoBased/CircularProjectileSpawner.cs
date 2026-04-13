using Platformer.Creatures.Weapons;
using Platformer.Utils;
using System;
using System.Collections;
using UnityEngine;

namespace Platformer.Components.GoBased
{
    public class CircularProjectileSpawner : MonoBehaviour
    {
        [SerializeField] private ProjectileSequence[] _settings;

        public int Stage { get; set; }

        [ContextMenu("Launch!")]
        public void LaunchProjectile()
        {
            StartCoroutine(SpawnProjectile());
        }

        private IEnumerator SpawnProjectile()
        {
            var sequence = _settings[Stage];

            foreach (var setting in sequence.Sequence)
            {
                var sectorStep = 2 * Mathf.PI / setting.BurstCount;
                for (int i = 0, burstCount = 1; i < setting.BurstCount; i++, burstCount++)
                {
                    var angle = sectorStep * i;
                    var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                    var instance = SpawnUtils.Spawn(setting.Prefab.gameObject, transform.position);
                    var projectile = instance.GetComponent<DirectionalProjectile>();
                    projectile.Launch(direction);

                    if (burstCount < setting.ItemPerBurst) continue;

                    burstCount = 0;
                    yield return new WaitForSeconds(setting.Delay);
                }
            }
        }

        [Serializable]
        public struct ProjectileSequence
        {
            [SerializeField] private CircularProjectileSettings[] _sequence;

            public CircularProjectileSettings[] Sequence => _sequence;
        }


        [Serializable]
        public struct CircularProjectileSettings
        {
            [SerializeField] private DirectionalProjectile _prefab;
            [SerializeField] private int _burstCount;
            [SerializeField] private int _itemPerBurst;
            [SerializeField] private float _delay;

            public DirectionalProjectile Prefab => _prefab;
            public int BurstCount => _burstCount;
            public int ItemPerBurst => _itemPerBurst;
            public float Delay => _delay;
        }

    }
}
