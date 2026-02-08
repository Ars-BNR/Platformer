using UnityEngine;
using UnityEngine.UI;

namespace Platformer.UI.Widgets
{

    public class ProgressBarWidgets : MonoBehaviour
    {
        [SerializeField] private Image _bar;

        public void SetProgress(float progress)
        {
            _bar.fillAmount = progress;
          
        }
       
    }

}