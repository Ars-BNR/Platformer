using UnityEngine;

namespace Platformer.Components.ColliderBased
{

    public class EnterCollisionComponentMany : MonoBehaviour
    {
        [SerializeField] private string[] _tags;
        [SerializeField] private EnterEvent _action;

        private void OnCollisionEnter2D(Collision2D other)
        {
            foreach (var tag in _tags)
            {
                if (other.gameObject.CompareTag(tag))
                {
                    _action?.Invoke(other.gameObject);
                    return;
                }
            }
        }

    }
}