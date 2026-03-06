using Platformer.Model.Definitions.Player;
using Platformer.Model.Definitions.Repositories;
using Platformer.Model.Definitions.Repositories.Items;
using UnityEngine;

namespace Platformer.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/DefsFacade", fileName = "DefsFacade")]

    public class DefsFacade: ScriptableObject
    {
        [SerializeField] private ItemsRepository _items;
        [SerializeField] private ThrowableIRepository _trowableItems;
        [SerializeField] private PortionRepository _portions;
        [SerializeField] private PerkRepository _perks;
        [SerializeField] private PlayerDef _player;

        public ItemsRepository Items => _items ; 
        public ThrowableIRepository Trowable => _trowableItems; 
        public PortionRepository Portions => _portions; 
        public PerkRepository Perks => _perks; 
        public PlayerDef Player => _player; 

        private static DefsFacade _instance;
        public static DefsFacade I => _instance == null ? LoadDefs() : _instance;

        private static DefsFacade LoadDefs()
        {
            return _instance = Resources.Load<DefsFacade>("DefsFacade");
        }


    }
}
