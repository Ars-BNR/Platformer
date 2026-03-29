using Platformer.Model;
using UnityEngine;

namespace Platformer.Components.Colletables
{
    public class RefilFuelComponent:MonoBehaviour
    {
        [SerializeField] private int _refillFuel;
        private GameSession _session;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
        }

        public void Refill()
        {
            _session.Data.Fuel.Value = _refillFuel;
        }

    }
}
