using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public int health;
    public int energy;
    public bool hasPotion;
    public bool canSwitchWeapons;
    public WeaponTypes weaponInHand;
    public GameObject player;
    public GameObject terrain;
    public Slider Hp;
    public Slider Energy;

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

    void DrinkPotion()
    {
        health += 50;
        if (health > 100)
            health = 100;
        hasPotion = false;
    }

    void Rest()
    {
        energy += 50;
        if (energy > 100)
            energy = 100;
    }

    void Attack()
    {
        if(weaponInHand == WeaponTypes.Bow)
        {

        }
        else
        {

        }
    }

    bool CanMoveForward()
    {
        return gameObject.transform.position.z + 4 < player.gameObject.transform.position.z; 
    }

    bool CanMoveBackward()
    {
        return gameObject.transform.position.z - 4 > terrain.GetComponent<TerrainAttributes>().ZAxisMinValue;
    }

    void MoveForward()
    {
        if (!CanMoveForward()) return;
        gameObject.transform.Translate(0, 0, -4);
        energy -= 5;
    }

    void MoveBackward()
    {
        if (!CanMoveBackward()) return;
        gameObject.transform.Translate(0, 0, 4);
        energy -= 5;
    }

    void SwitchWeapons()
    {
        if(canSwitchWeapons)
        {
            if (weaponInHand == WeaponTypes.Bow)
                weaponInHand = WeaponTypes.Sword;
            else
                weaponInHand = WeaponTypes.Bow;
        }
    }

    bool IsPlayerInAttackRange()
    {
        if (weaponInHand == WeaponTypes.Bow)
        {

        }
        else
        {

        }
        return true;
    }

    public void DecideAction()
    {
        if (health <= 0) // Not alive
        {
            Die(); // Die
        }
        else if (health < 50 && hasPotion) // HP under 50
        {
            DrinkPotion(); // Drink potion
        }
        else // HP 50 or above or less than 50 and doesn't have potion
        {
            if (energy >= 5) // Has enough energy
            {
                if (weaponInHand == WeaponTypes.Bow) // Has a bow
                {
                    if (CanMoveBackward()) // Can move backwards
                    {
                        if (IsPlayerInAttackRange()) // Player in attack range
                        {
                            if(player.GetComponent<PlayerScript>().CanAttack()) // Player can attack
                            {
                                MoveBackward(); // Move backwards
                            }
                            else // Player cannot attack
                            {
                                Attack(); // Attack
                            }
                        }
                        else
                        {
                            MoveBackward(); // Move backward
                        }
                    }
                    else // Cannot move backwards
                    {
                        if (canSwitchWeapons) // Can switch weapons
                        {
                            SwitchWeapons();
                        }
                        else
                        {
                            Attack();
                        }
                    }
                }
                else // Has a sword
                {
                    if (IsPlayerInAttackRange()) // Player in attack range
                    {
                        if (player.GetComponent<PlayerScript>().CanAttack()) // Player can attack
                        {
                            MoveBackward(); // Move backward
                        }
                        else // Player cannot attack
                        {
                            Attack(); // Move forward
                        }
                    }
                    else if (CanMoveForward())
                    {
                        MoveForward();
                    }
                }
            }
            else // Does not have enough energy
            {
                Rest();
            }
        }
        player.GetComponent<PlayerScript>().player_turn = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Hp.value = health / 100.0f;
        Energy.value = energy / 100.0f;
    }
}
