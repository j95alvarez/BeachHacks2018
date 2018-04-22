using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TheSquad.HealthSystem;

namespace TheSquad.Projectiles
{
    public abstract class AbstractProjectile : MonoBehaviour
    {
        [SerializeField] protected int damage;
        [SerializeField] float lifeTime;

        protected virtual void Start()
        {
            StartCoroutine(CoDestroyOverTime());
        }

        protected virtual void OnCollisionEnter(Collision coll)
        {
            if (coll.transform.root.GetComponent(typeof(IDamageable)))
            {
                Debug.Log("Health");
                GameObject damagedObject = coll.transform.root.gameObject;
                SendDamage(damagedObject);
            }

            Destroy(gameObject);
        }

        protected abstract void SendDamage(GameObject damagedObject);

        IEnumerator CoDestroyOverTime()
        {
            yield return new WaitForSeconds(lifeTime);
            Destroy(gameObject);
        }
    }
}
