using Platformer.Components.Dialogs;
using Platformer.Model.Definitions.Localization;
using Platformer.UI.Widgets;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Platformer.UI.Hud.Dialogs
{
    public class OptionDialogController : MonoBehaviour
    {
        [SerializeField] private GameObject _content;
        [SerializeField] private Text _contentText;
        [SerializeField] private Transform _optionsContainer;
        [SerializeField] private OptionItemWidget _prefab;

        private DataGroup<OptionData, OptionItemWidget> _dataGroup;

        private void Start()
        {
            _dataGroup = new DataGroup<OptionData, OptionItemWidget>(_prefab,_optionsContainer);
        }

        public void OnOptionsSelected(OptionData selectedOption)
        {
            selectedOption.OnSelect.Invoke();
            _content.SetActive(false);
        }

        public void Show(OptionDialogData data)
        {
            _content.SetActive(true);
            
            _contentText.text = LocalizationManager.I.Localize(data.DialogText);

            var localizedOptions = new OptionData[data.Options.Length];
            for (int i = 0; i < data.Options.Length; i++)
            {
                localizedOptions[i] = new OptionData
                {
                    Text = LocalizationManager.I.Localize(data.Options[i].Text),
                    OnSelect = data.Options[i].OnSelect
                };
            }

            _dataGroup.SetData(localizedOptions);
        }
    }

        [Serializable]
        public class OptionDialogData
        {
            public string DialogText;
            public OptionData[] Options;
        }

        [Serializable]
        public class OptionData
        {
            public string Text;
            public UnityEvent OnSelect;
        }

}
