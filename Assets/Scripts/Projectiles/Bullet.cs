using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TheSquad.HealthSystem;

namespace TheSquad.Projectiles
{
    public class Bullet : AbstractProjectile
    {
        [SerializeField] float projectileSpeed;

        Rigidbody rb;
        protected override void Start()
        {
            base.Start();
            rb = GetComponent<Rigidbody>();
            SetVelocity();
        }

        void SetVelocity()
        {
            rb.velocity = transform.forward * projectileSpeed;
        }

        protected override void SendDamage(GameObject damagedObject)
        {
            IDamageable damageable = damagedObject.GetComponent<IDamageable>();
            damageable.TakeDamage(damage);
        }
    }
}