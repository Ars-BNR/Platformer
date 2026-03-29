using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Platformer.InputReader
{
    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] private Hero _hero;

        public void OnVectorMovement(InputAction.CallbackContext context)
        {
            var directionVector = context.ReadValue<Vector2>();
            _hero.SetDirection(directionVector);
        }



        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Interact();
            }
        }
        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Attack();
            }
        }
        public void OnThrow(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _hero.StartThrowing();
            }

            if (context.canceled)
            {
                _hero.UseInventory();
            }
        }
        public void OnUse(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.useHill();
            }
        }
        public void OnNextItem(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.NextItem();
            }
        }

        public void OnUsePerk(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.UsePerk();
            }
        }

        public void OnToggleFlashLight(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.ToggleFlashLight();
            }
        }
    }
}