
using Platformer.Model.Data;
using Platformer.Model.Definitions;
using Platformer.Utils;
using UnityEngine;

namespace Platformer.Components.Colletables
{
    public class InventoryAddComponent:MonoBehaviour
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private int _count;

        public void Add(GameObject go)
        {
            var hero = go.GetInterface<ICanAddInventory>();
            hero?.AddInInventory(_id,_count);
        }
    }
}
