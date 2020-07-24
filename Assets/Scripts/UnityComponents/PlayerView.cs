using Leopotam.Ecs;
using System.Collections;
using UnityEngine;

namespace RunnerTT
{
    public class PlayerView : MonoBehaviour
    {
        public EcsEntity Entity;
        public Animator Animator;

        public IEnumerator EndGameAfterDeathAnimation(float animationTime, GameState gameState)
        {
            Animator.SetTrigger("Death");
            yield return new WaitForSeconds(animationTime);
            gameState.State = State.End;
        }
    }
}