using Platformer.Model;
using Platformer.Model.Definitions;
using Platformer.Model.Definitions.Localization;
using Platformer.Model.Definitions.Player;
using Platformer.UI.Widgets;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.UI.Windows.PlayerStats
{
    public class StatWidget : MonoBehaviour, IItemRenderer<StatDef>
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Text _name;
        [SerializeField] private Text _currentValue;
        [SerializeField] private Text _increaseValue;
        [SerializeField] private ProgressBarWidgets _progress;
        [SerializeField] private GameObject _selector;

        private GameSession _session;
        private StatDef _data;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            UpdateView();
        }


        public void SetData(StatDef data, int index)
        {
            _data = data;
            if (_session != null)
                UpdateView();
        }

        private void UpdateView()
        {
            var statsModel = _session.StatsModel;

            _icon.sprite = _data.Icon;
            _name.text = LocalizationManager.I.Localize(_data.Name);
            var currentLevelValue = statsModel.GetValue(_data.ID);
            _currentValue.text = currentLevelValue.ToString(CultureInfo.InvariantCulture);

            var currentLevel = statsModel.GetCurrentLevel(_data.ID);
            var nextLevel = currentLevel + 1;
            var nextLevelValue = statsModel.GetValue(_data.ID, nextLevel);
            var increaseValue = nextLevelValue - currentLevelValue;
            _increaseValue.text = increaseValue.ToString(CultureInfo.InvariantCulture);
            _increaseValue.gameObject.SetActive(increaseValue > 0);

            var maxLevels = DefsFacade.I.Player.GetStat(_data.ID).Levels.Length - 1;
            _progress.SetProgress(currentLevel / (float)maxLevels);

            _selector.SetActive(statsModel.InterfaceSelectedStat.Value == _data.ID);
        }


        public void OnSelect()
        {
            _session.StatsModel.InterfaceSelectedStat.Value = _data.ID;
        }

    }
}
