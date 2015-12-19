using UnityEngine;
using System.Collections;

public class RobotDeath : MonoBehaviour {
	public int StartingHealth;
	public int CurrentHealth;

	// Use this for initialization
	void Start () {
		CurrentHealth = StartingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (CurrentHealth <= 0) {
			Destroy(gameObject);
		}
	}

	void TakeDamage(int amount) {
		CurrentHealth -= amount;
	}
}
