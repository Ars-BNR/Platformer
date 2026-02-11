using System;
using UnityEngine;

namespace Platformer.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/ThrowableItemsDef", fileName = "ThrowableItemsDef")]


    public class ThrowableItemsDef : ScriptableObject
    {
        [SerializeField] private ThrowableDef[] _items;
        public ThrowableDef Get(string id)
        {
            foreach (var ThrowableDef in _items)
            {
                if (ThrowableDef.Id == id)
                    return ThrowableDef;
            }

            return default;
        }
    }

    [Serializable]
    public struct ThrowableDef
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private GameObject _projectile;

        public string Id => _id;
        public GameObject Projectile => _projectile;

    }
}
