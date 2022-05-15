﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.SimpleLUT;

/*
 * Noah Trillizio
 * Project 5 
 * Controls what happens when the enemie takes there turn
 */

public class EnemiesTurn : MonoBehaviour
{
    private AttackAction playerAction;
    private PlayerStats playerSanity;
    private ClickedAttack clickedAttack;

    public Button Attack1;
    public Button Attack2;
    public Button Attack3;
    public Button Attack4;

    private Text description;
    // Start is called before the first frame update

    //public bool enemyTurn;

    private OpenFightMenu fightMenu;
    private UseableAttackHandler useableAttacks;
    private SimpleLUT cameraLUT;

    void Start()
    {
        clickedAttack = GetComponent<ClickedAttack>();
        playerAction = GetComponent<AttackAction>();
        fightMenu = GameObject.FindGameObjectWithTag("Player").GetComponent<OpenFightMenu>();
        playerSanity = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        description = GameObject.FindGameObjectWithTag("DescriptionBox").GetComponentInChildren<Text>();
        useableAttacks = GameObject.FindGameObjectWithTag("Attack 1").GetComponent<UseableAttackHandler>();
        cameraLUT = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SimpleLUT>();
    }

    public void playerTurn(bool hurtEnemy)
    {
        StartCoroutine(StartPlayerTurn(hurtEnemy));
    }

    IEnumerator StartPlayerTurn(bool hurtEnemy)
    {
        Attack1.enabled = false;
        Attack1.GetComponentInChildren<Text>().enabled = false;
        Attack2.enabled = false;
        Attack2.GetComponentInChildren<Text>().enabled = false;
        Attack3.enabled = false;
        Attack3.GetComponentInChildren<Text>().enabled = false;
        Attack4.enabled = false;
        Attack4.GetComponentInChildren<Text>().enabled = false;
        if (hurtEnemy)
        {
            fightMenu.enemyPortrait.enabled = false;
            yield return new WaitForSeconds(.15f);
            fightMenu.enemyPortrait.enabled = true;
            yield return new WaitForSeconds(.15f);
            fightMenu.enemyPortrait.enabled = false;
            yield return new WaitForSeconds(.15f);
            fightMenu.enemyPortrait.enabled = true;
            yield return new WaitForSeconds(.15f);
            fightMenu.enemyPortrait.enabled = false;
            yield return new WaitForSeconds(.15f);
            fightMenu.enemyPortrait.enabled = true;
        }
    }

    public void enemyTurn(bool showButtons)
    {
        StartCoroutine(StartEnemiesTurn(showButtons));
    }

    IEnumerator StartEnemiesTurn(bool showButtons)
    {
        //text indicating what the enemy attack is
        int randomDescription = Random.Range(0, fightMenu.enemyEncountered.attackDescriptions.Length);
        description.text = fightMenu.enemyEncountered.attackDescriptions[randomDescription];
        //Enemy attack
        if (fightMenu.enemyEncountered.numAttacks == 1)
        {
            playerSanity.attributes[0].value.BaseValue = (playerSanity.attributes[0].value.BaseValue) + UnityEngine.Random.Range(fightMenu.enemyEncountered.minDamage, fightMenu.enemyEncountered.maxDamage);
            float timer = 0f;
            float red = .5f;
            float green = .5f;
            float blue = 1f;
            float attackTime = 1.5f;
            while (timer < attackTime)
            {
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
                if (timer < (attackTime / 2))
                {
                    cameraLUT.TintColor = new Color(1f - (red * (timer/(attackTime/2))), 1f - (green * (timer / (attackTime / 2))), 1f - (blue * (timer / (attackTime / 2))));
                    cameraLUT.Contrast = .5f * (timer / (attackTime / 2));
                    cameraLUT.Sharpness = timer / (attackTime / 2);
                }
                else
                {
                    cameraLUT.TintColor = new Color(1f - (red * (1f - ((timer - (attackTime / 2)) / (attackTime / 2)))), 1f - (green * (1f - ((timer - (attackTime / 2)) / (attackTime / 2)))), 1f - (blue * (1f - ((timer - (attackTime / 2)) / (attackTime / 2)))));
                    cameraLUT.Contrast = .5f - (.5f * ((timer- (attackTime / 2)) / (attackTime / 2)));
                    cameraLUT.Sharpness = 1f - ((timer - (attackTime / 2)) / (attackTime / 2));
                }
            }
        }
        else
        {
            for (int i = 0; i < fightMenu.enemyEncountered.numAttacks; i++)
            {
                playerSanity.attributes[0].value.BaseValue = (playerSanity.attributes[0].value.BaseValue) + UnityEngine.Random.Range(fightMenu.enemyEncountered.minDamage, fightMenu.enemyEncountered.maxDamage);
                yield return new WaitForSeconds(3f/ fightMenu.enemyEncountered.numAttacks);
            }
        }
        Debug.Log(playerSanity.attributes[0].value.BaseValue);
        
        if (showButtons)
        {
            Attack1.enabled = true;
            Attack1.GetComponentInChildren<Text>().enabled = true;
            Attack2.enabled = true;
            Attack2.GetComponentInChildren<Text>().enabled = true;
            Attack3.enabled = true;
            Attack3.GetComponentInChildren<Text>().enabled = true;
            Attack4.enabled = true;
            Attack4.GetComponentInChildren<Text>().enabled = true;
            clickedAttack.changeAttack = true;
            useableAttacks.CallSetUsableAttacks();
        }
    }

    /*IEnumerator EnemyFlash()
    {
        fightMenu.enemyPortrait.enabled = false;
        yield return new WaitForSeconds(.15f);
        fightMenu.enemyPortrait.enabled = true;
        yield return new WaitForSeconds(.15f);
        fightMenu.enemyPortrait.enabled = false;
        yield return new WaitForSeconds(.15f);
        fightMenu.enemyPortrait.enabled = true;
        yield return new WaitForSeconds(.15f);
        fightMenu.enemyPortrait.enabled = false;
        yield return new WaitForSeconds(.15f);
        fightMenu.enemyPortrait.enabled = true;
    }*/

    /*public void enemyTripleTurn()
    {
        StartCoroutine(StartEnemiesTripleTurn());
    }

    IEnumerator StartEnemiesTripleTurn()
    {
        Attack1.enabled = false;
        Attack2.enabled = false;
        Attack3.enabled = false;
        Attack4.enabled = false;
        playerTurn.enabled = false;
        //Debug.Log("Turn Started");

        for (int i = 0; i < 2; i++)
        {
            while (!Input.GetKey(KeyCode.Space))
            {
                yield return new WaitForFixedUpdate();
            }

            int randomDescription = Random.Range(0, fightMenu.enemyEncountered.attackDescriptions.Length);
            description.text = fightMenu.enemyEncountered.attackDescriptions[randomDescription];

            playerSanity.attributes[0].value.BaseValue = (playerSanity.attributes[0].value.BaseValue) + UnityEngine.Random.Range(10, 30); //enemy attack
        }

        playerTurn.enabled = true;
        Attack1.enabled = true;
        Attack2.enabled = true;
        Attack3.enabled = true;
        Attack4.enabled = true;
        clickedAttack.changeAttack = true;
    }*/
}