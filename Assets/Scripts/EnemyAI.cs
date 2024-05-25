using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int health;
    public int energy;
    public bool hasPotion;
    public bool canSwitchWeapons;
    public WeaponTypes weaponInHand;
    public GameObject player;
    public GameObject terrain;


    void Die()
    {

    }

    void DrinkPotion()
    {

    }

    void Rest()
    {
        
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

    void MoveForward()
    {

    }

    void MoveBackward()
    {

    }

    void SwitchWeapons()
    {

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
                    if (gameObject.transform.position.z - 4 >= terrain.GetComponent<TerrainAttributes>().ZAxisMinValue) // Can move backwards
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
                    else
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
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
