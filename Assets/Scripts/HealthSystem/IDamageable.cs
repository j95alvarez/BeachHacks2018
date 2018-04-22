using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheSquad.HealthSystem
{
    public interface IDamageable
    {
        void TakeDamage(int amount);
    }
}