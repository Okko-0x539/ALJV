using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public int health = 100;
    public int energy = 100;
    public GameObject enemy;
    public GameObject potion;
    public GameObject buttons;
    public GameObject attackbuttons;
    public GameObject weapon;
    public GameObject terrain;
    public int player_attack_range;
    public bool player_turn = true;
    public bool attackmode = false;
    public TextMeshProUGUI instructions;
    public Slider Hp;
    public Slider Energy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Hp.value = health / 100.0f;
        Energy.value = energy / 100.0f;

        if (!player_turn)
        {
            buttons.SetActive(false);
            instructions.gameObject.SetActive(false);
        }
        else if (attackmode)
        {
            buttons.SetActive(false);
        }
        else
        {
            buttons.SetActive(true);
        }
    }

    public bool CanAttack()
    {
        return gameObject.transform.position.z - enemy.transform.position.z < player_attack_range;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {

    }

    public bool CanMoveForward()
    {
        return gameObject.transform.position.z - 4 > enemy.gameObject.transform.position.z;
    }

    public bool CanMoveBackward()
    {
        return gameObject.transform.position.z + 4 < terrain.GetComponent<TerrainAttributes>().ZAxisMaxValue;
    }

    public void MoveForward()
    {
        gameObject.transform.Translate(0, 0, 4);
        energy -= 5;
        player_turn = false;
    }

    public void MoveBackward()
    {
        gameObject.transform.Translate(0, 0, -4);
        energy -= 5;
        player_turn = false;
    }
    public void Rest()
    {
        energy += 50;
        if(energy > 100)
            energy = 100;
        player_turn = false;
    }
    public void Potion()
    {
        health += 50;
        if(health > 100) 
            health = 100;
        potion.SetActive(false);
        player_turn = false;
    }
    public void Attack()
    {
        attackbuttons.SetActive(true);
        attackmode = true;
    }
    public void Back()
    {
        attackbuttons.SetActive(false);
        attackmode = false;
    }
    public void AttackEasy()
    {
        if (CanAttack())
        {
            enemy.GetComponent<EnemyAI>().TakeDamage(10);
        }
        else
        {
            // miss
        }
        player_turn = false;
        attackmode = false;
        attackbuttons.SetActive(false);
    }
    public void AttackMedium()
    {
        if (CanAttack())
        {
            enemy.GetComponent<EnemyAI>().TakeDamage(20);
        }
        else
        {
            // miss 
        }
        player_turn = false;
        attackmode = false;
        attackbuttons.SetActive(false);
    }
    public void AttackHard()
    {
        if (CanAttack())
        {
            enemy.GetComponent<EnemyAI>().TakeDamage(40);
        }
        else
        {
            // miss
        }
        player_turn = false;
        attackmode = false;
        attackbuttons.SetActive(false);
    }
    public void SwitchWeapon()
    {
        //to do weapon switch later
        player_turn = false;
    }
}
