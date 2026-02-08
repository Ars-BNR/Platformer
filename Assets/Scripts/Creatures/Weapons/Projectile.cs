using UnityEngine;

namespace Platformer.Creatures.Weapons
{

    public class Projectile : BaseProjectile
    {

        protected override void Start ()
        {
            base.Start ();

            //если делаем падающий меч со временем
            var force = new Vector2(Direction * _speed, 0);
            Rigidbody.AddForce(force, ForceMode2D.Impulse);
        }
    }   

}