using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthSystem : MonoBehaviour {

    public int maxHealth;

    private int currentHealth;

    [SerializeField]
    private RectTransform healthbar;

    public int Health {
        get { return currentHealth; }
        set { currentHealth += value; }
    }

    void Start() {
        currentHealth = maxHealth;
    }

    public void Heal(int amount) {
        currentHealth += amount;
    }

    public void TakeDamage(int amount) {
        currentHealth -= amount;

        if (isDead()) {
            currentHealth = 0;
            Debug.Log("Player is Dead!");
        }

        healthbar.sizeDelta = new Vector2(currentHealth, healthbar.sizeDelta.y);
    }

    public bool isDead() {
        if (currentHealth <= 0) {
            return true;
        }
        return false;
    }
}