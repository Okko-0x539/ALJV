using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int health = 100;
    public int energy = 100;
    public GameObject enemy;
    public GameObject potion;
    public GameObject buttons;
    public int player_attack_range;
    public bool player_turn = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!player_turn)
            buttons.SetActive(false);
        else
            buttons.SetActive(true);
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
    public void AttackEasy()
    {
        if (enemy.transform.position.z - gameObject.transform.position.z < player_attack_range)
        {
            //miss
        }
        else
        {
            //configure attacks
        }
        player_turn = false;
    }
    public void AttackMedium()
    {
        if (enemy.transform.position.z - gameObject.transform.position.z < player_attack_range)
        {
            //miss
        }
        else
        {
            //configure attacks
        }
        player_turn = false;
    }
    public void AttackHard()
    {
        if (enemy.transform.position.z - gameObject.transform.position.z < player_attack_range)
        {
            //miss
        }
        else
        {
            //configure attacks
        }
        player_turn = false;
    }
    public void SwitchWeapon()
    {
        //to do weapon switch later
        player_turn = false;
    }
}
