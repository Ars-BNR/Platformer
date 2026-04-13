using Platformer.Components.GoBased;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Creatures.Mobs.Boss.Bombs
{
    public class BombsController : MonoBehaviour
    {
        [SerializeField] List<GameObject> _platforms;
        [SerializeField] BombSequence[] _sequences;
        private Coroutine _coroutine;

        [ContextMenu("Start boombing")]
        public void StartBombing()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(BombingSequence());
        }

        private IEnumerator BombingSequence()
        {
            _platforms.ForEach(x => x.SetActive(false));
            foreach (var bombSequence in _sequences)
            {
                foreach (var spawnComponent in bombSequence.Bombs)
                {
                    spawnComponent.Spawn();
                }

                yield return new WaitForSeconds(bombSequence.Delay);
            }

            _platforms.ForEach(x => x.SetActive(true));
            
            _coroutine = null;
        }

        [Serializable]
        public class BombSequence
        {
            [SerializeField] private SpawnComponent[] _bombs;
            [SerializeField] private float _delay;

            public SpawnComponent[] Bombs => _bombs;

            public float Delay => _delay;
        }
    }
}
