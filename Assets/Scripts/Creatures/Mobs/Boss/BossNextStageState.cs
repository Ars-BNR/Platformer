using Platformer.Components.GoBased;
using Platformer.Creatures.Mobs.Boss;
using UnityEngine;

public class BossNextStageState : StateMachineBehaviour
{
    [ColorUsage(true, true)]
    [SerializeField] private Color _stageColor;
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var spawner = animator.GetComponent<CircularProjectileSpawner>();
        spawner.Stage++;

        var changeLight = animator.GetComponent<ChangeLightsComponent>();
        changeLight.SetColor(_stageColor);
    }

}
