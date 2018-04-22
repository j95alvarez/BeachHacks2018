using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Update is called once per frame
	private void OnCollisionEnter (Collision col) {
        var hit = col.gameObject;
        var health = hit.GetComponent<HealthSystem>();

        if (health != null) {
            health.TakeDamage(10);
        }

        Destroy(gameObject);
    }
}
