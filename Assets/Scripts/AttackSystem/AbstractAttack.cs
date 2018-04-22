using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

using TheSquad.Weapons;

namespace TheSquad.AttackSystem
{
    public abstract class AbstractAttack : NetworkBehaviour
    {
        public AbstractWeapon weapon;

        protected virtual void Attack()
        {
            if(weapon != null)
            {
                weapon.Fire();
            }
        }

        protected virtual void Start()
        {
            weapon = GetComponentInChildren<AbstractWeapon>();
        }
    }
}