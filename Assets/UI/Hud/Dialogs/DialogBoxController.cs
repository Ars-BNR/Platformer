using Platformer.Utils;
using Platformerl.Model.Data;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.UI.Hud.Dialogs
{
    public class DialogBoxController : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private GameObject _container;
        [SerializeField] private Animator _animator;

        [Space]
        [SerializeField] private float _textSpeed = 0.09f;

        [Header("Sounds")][SerializeField] private AudioClip _typing;
        [SerializeField] private AudioClip _open;
        [SerializeField] private AudioClip _close;

        private DialogData _data;
        private int _currentSentence;
        private AudioSource _sfxSource;
        private static readonly int IsOpen = Animator.StringToHash("IsOpen");
        private Coroutine _typingRoutine;
        private void Start()
        {
            _sfxSource = AudioUtils.FindSfxSource();
        }

        public void ShowDialog(DialogData data)
        {
            _data = data;
            _currentSentence = 0;
            _text.text = string.Empty;

            _container.SetActive(true);
            _sfxSource.PlayOneShot(_open);
            _animator.SetBool(IsOpen, true);
        }
        private IEnumerator TypeDialogText()
        {
            _text.text = string.Empty;
            var sentences = _data.Sentences[_currentSentence];

            foreach (var letter in sentences)
            {
                _text.text += letter;
                _sfxSource.PlayOneShot(_typing);
                yield return new WaitForSeconds(_textSpeed);
            }
            _typingRoutine = null;
        }

        public void OnSkip()
        {
            if (_typingRoutine == null) return;

            StopTypeAnimation();
            _text.text = _data.Sentences[_currentSentence];
        }

        private void StopTypeAnimation()
        {
            if (_typingRoutine != null)
                StopCoroutine(_typingRoutine);
            _typingRoutine = null;
        }

        public void OnContinue()
        {
            StopTypeAnimation();
            _currentSentence++;

            var isDialogCompleted = _currentSentence >= _data.Sentences.Length;
            if (isDialogCompleted)
            {
                HideDialogBox();
            }
            else
            {
                OnStartDialogAnimation();
            }
        }

        private void HideDialogBox()
        {
            _animator.SetBool(IsOpen,false);
            _sfxSource.PlayOneShot(_close);
        }

        private void OnStartDialogAnimation()
        {
            _typingRoutine = StartCoroutine(TypeDialogText());
        }


        private void OnCloseAnimationComplete()
        {

        }
    }
}
