﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : MonoBehaviour
{
    private PlayerStats playerStats;
    private ClickedAttack clickedAttack;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        clickedAttack = GetComponent<ClickedAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Meditate();
        }
    }

    public void Meditate()
    {
        for (int i = 0; i < playerStats.attributes.Length; i++)
        {
            if (playerStats.attributes[0].type == clickedAttack.attack.data.buffs[0].attribute)
            {
                Debug.Log(string.Concat(playerStats.attributes[0].type, " was updated! Value is now ", playerStats.attributes[0].value.ModifiedValue));
                //playerStats.attributes[0].value.AddModifier(clickedAttack.attack.data.buffs[0]);
            }
        }
        //Debug.Log(string.Concat(playerStats.attributes[0].type, " was updated! Value is now ", playerStats.attributes[0].value.ModifiedValue));
    }
}