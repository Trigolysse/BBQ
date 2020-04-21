using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HealthBar : MonoBehaviour
{
	
	public GameObject player;
	private int CurrentHealth;

	public Slider slider;
	public Gradient gradient;
	public Image fill;

	public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;

		fill.color = gradient.Evaluate(1f);
	}

    public void SetHealth(int health)
	{
		slider.value = health;

		fill.color = gradient.Evaluate(slider.normalizedValue);
	}

    private void Start()
    {
	    SetMaxHealth(CurrentHealth= player.GetComponent<Combatmanager>().currentHealth);
    }

    private void Update()
    {

	    CurrentHealth= player.GetComponent<Player>().currentHealth;
	    SetHealth(CurrentHealth);
	    Debug.Log(CurrentHealth*10);

    }
}
