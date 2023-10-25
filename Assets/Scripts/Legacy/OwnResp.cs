﻿using System.Collections;
using System.Collections.Generic;
using Services.Factory;
using UnityEngine;
using UnityEngine.UI;

public class OwnResp : MonoBehaviour, IFactorizable
{
	public int healthPoint = 50;
	private float currentHealth;
	public Image healthBar;
	public GameObject explosionFX;
	public GameObject brokenOwnResp;

	public void Start()
	{
		currentHealth = healthPoint;
	}

	void Update()
	{
		healthBar.fillAmount = healthPoint / currentHealth;
		if (healthPoint <= 0)
		{
			Termination();
		}
	}

	public void TakingDamage(int damage)
	{
		healthPoint -= damage;
	}

	void Termination()
	{
		Instantiate(explosionFX, transform.position, transform.rotation);
		Instantiate(brokenOwnResp, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}