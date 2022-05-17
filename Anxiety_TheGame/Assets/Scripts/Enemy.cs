﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Anna Breuker
 * Project 6
 * Contains information on individual enemies.
 */
public class Enemy : MonoBehaviour
{
   // public Attacks attack;
    public Sprite enemySprite;
    public int health;
    public string enemyName;
    public string[] attackDescriptions;
    public int minDamage;
    public int maxDamage;
    public bool attackFirst = false;
    public int numAttacks = 1;
    public float attackTime;
    public float red;
    public float green;
    public float blue;
}
