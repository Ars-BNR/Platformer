using UnityEngine;
using System;

namespace Platformer.Components.Health
{

public class ModifyHealthComponent : MonoBehaviour
{
        [SerializeField] private int _hpDelta = -1;


        private void Awake()
        {
            Debug.Log($"[ModifyHealth] Awake() вызван! gameObject={gameObject.name}, Hp Delta = {_hpDelta}");
        }

        public void SetDelta(int delta)
        {
            Debug.LogWarning($"[ModifyHealth] SetDelta() вызван! Старое={_hpDelta}, Новое={delta}");
            Debug.LogWarning($"[ModifyHealth] Вызов из: {Environment.StackTrace}");
            _hpDelta = delta;
        }

        public void Apply(GameObject target)
        {
            Debug.Log($"ModifyHealthComponent.Apply вызван! Hp Delta = {_hpDelta}");
            Debug.Log($"Этот компонент на объекте: {gameObject.name}");

            var healthComponent = target.GetComponent<HealthComponent>();
            if(healthComponent != null)
            {
                Debug.Log($"Наносим урон: {_hpDelta}");
                healthComponent.ModifyHealth(_hpDelta);
            }


            if (healthComponent == null)
            {
                Debug.LogError($"HealthComponent НЕ найден на {target.name}!");
                return;
            }

        }
}

}