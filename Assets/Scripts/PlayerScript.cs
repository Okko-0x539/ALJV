using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public int health = 100;
    public int energy = 100;
    public WeaponTypes weaponInHand;
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
        if (weaponInHand == WeaponTypes.Bow)
        {
            return gameObject.transform.position.z - enemy.transform.position.z < 20;
        }
        else
        {
            return gameObject.transform.position.z - enemy.transform.position.z < 6;
        }
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
        if (weaponInHand == WeaponTypes.Bow)
        {
            int attackChance = Random.Range(1, 101);
            if (attackChance <= 50 && CanAttack())
            {
                enemy.GetComponent<PlayerScript>().TakeDamage(5);
            }

        }
        else
        {
            int attackChance = Random.Range(1, 101);
            if (attackChance <= 70 && CanAttack())
            {
                enemy.GetComponent<PlayerScript>().TakeDamage(5);
            }
        }
        player_turn = false;
        attackmode = false;
        attackbuttons.SetActive(false);

        energy -= 10;
        if (energy < 0)
            energy = 0;
    }
    public void AttackMedium()
    {
        if (weaponInHand == WeaponTypes.Bow)
        {
            int attackChance = Random.Range(1, 101);
            if (attackChance <= 35 && CanAttack())
            {
                enemy.GetComponent<PlayerScript>().TakeDamage(10);
            }
        }
        else
        {
            int attackChance = Random.Range(1, 101);
            if (attackChance <= 50 && CanAttack())
            {
                enemy.GetComponent<PlayerScript>().TakeDamage(20);
            }
        }
        player_turn = false;
        attackmode = false;
        attackbuttons.SetActive(false);

        energy -= 20;
        if (energy < 0)
            energy = 0;
    }
    public void AttackHard()
    {
        if (weaponInHand == WeaponTypes.Bow)
        {
            int attackChance = Random.Range(1, 101);
            if (attackChance <= 20 && CanAttack())
            {
                enemy.GetComponent<PlayerScript>().TakeDamage(15);
            }
        }
        else
        {
            int attackChance = Random.Range(1, 101);
            if (attackChance <= 30 && CanAttack())
            {
                enemy.GetComponent<PlayerScript>().TakeDamage(30);
            }
        }
        player_turn = false;
        attackmode = false;
        attackbuttons.SetActive(false);

        energy -= 30;
        if (energy < 0)
            energy = 0;
    }
    public void SwitchWeapon()
    {
        //to do weapon switch later
        player_turn = false;
    }
}
