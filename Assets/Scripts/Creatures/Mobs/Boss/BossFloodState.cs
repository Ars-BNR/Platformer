using UnityEngine;

namespace Platformer.Creatures.Mobs.Boss
{
    internal class BossFloodState : StateMachineBehaviour
    {

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var spawner = animator.GetComponent<FloodController>();
            spawner.StartFlooding();
        }

    }
}
