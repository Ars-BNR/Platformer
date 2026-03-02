using Platformer.Model.Definitions;
using Platformer.Model.Definitions.Repositories;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.UI.Widgets
{
    public class ItemWidgets:MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Text _value;

        public void SetData(ItemWithCount price)
        {
            var def = DefsFacade.I.Items.Get(price.ItemId);
            _icon.sprite = def.Icon;

            _value.text = price.Count.ToString();
        }
    }
}
