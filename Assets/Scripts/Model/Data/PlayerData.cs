using Platformer.Model.Data.Properties;
using System;
using UnityEngine;

namespace Platformer.Model.Data
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData _inventory;

        public IntProperty HP = new IntProperty();
        public PerksData Perks = new PerksData();

        public InventoryData Inventory => _inventory;

        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }

    }
}