﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDispley : MonoBehaviour
{
    TextMeshProUGUI healthText;
    Player player;
    void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = player.GetHealth().ToString();
    }

    public void ResetHealth()
    {
        Destroy(gameObject);
    }
}
