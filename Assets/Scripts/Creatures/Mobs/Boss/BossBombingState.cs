using Platformer.Creatures.Mobs.Boss.Bombs;
using UnityEngine;

namespace Platformer.Creatures.Mobs.Boss
{
    public class BossBombingState : StateMachineBehaviour
    {

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var spawner = animator.GetComponent<BombsController>();
            spawner.StartBombing();
        }

    }
}
