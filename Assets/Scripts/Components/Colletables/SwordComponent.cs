using UnityEngine;

namespace Platformer.Components.Colletables
{

    public class SwordComponent : MonoBehaviour
    {
        [SerializeField] private int _maxCount;
        [SerializeField] private int _currentCount;

        public void SetMaxCount(int maxCount)
        {
            _maxCount = maxCount;
        }
        public void SetCurrentCount(int currentCount)
        {
            _currentCount = currentCount;
        }

        


    }

}