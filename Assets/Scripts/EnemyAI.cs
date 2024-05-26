using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject sword;
    public GameObject bow;
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
        if(PlayerPrefs.GetInt("AILevel") == 1)
        {
            PlayerPrefs.SetInt("AILevel", 2);
            SceneManager.LoadScene("Arena");
        }
        if (PlayerPrefs.GetInt("AILevel") == 2)
        {
            PlayerPrefs.SetInt("AILevel", 3);
            SceneManager.LoadScene("Arena");
        }
        if (PlayerPrefs.GetInt("AILevel") == 3)
        {
            PlayerPrefs.SetInt("AILevel", 1);
            SceneManager.LoadScene("Main");
        }
    }

    public void DrinkPotion()
    {
        health += 50;
        if (health > 100)
            health = 100;
        hasPotion = false;
    }

    public void Rest()
    {
        energy += 50;
        if (energy > 100)
            energy = 100;
    }

    public void Attack()
    {
        int attackChoice = Random.Range(1, 4);

        switch (attackChoice)
        {
            case 1:
                AttackEasy();
                break;
            case 2:
                AttackMedium();
                break;
            case 3:
                AttackHard();
                break;
            default:
                return;
        }
    }
    public void AttackEasy()
    {
        if(weaponInHand == WeaponTypes.Bow)
        {
            int attackChance = Random.Range(1, 101);
            if(attackChance <= 50 && CanAttack())
            {
                player.GetComponent<PlayerScript>().TakeDamage(5);
            }
            
        }
        else
        {
            int attackChance = Random.Range(1, 101);
            if (attackChance <= 70 && CanAttack())
            {
                player.GetComponent<PlayerScript>().TakeDamage(5);
            }
        }

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
                player.GetComponent<PlayerScript>().TakeDamage(10);
            }
        }
        else
        {
            int attackChance = Random.Range(1, 101);
            if (attackChance <= 50 && CanAttack())
            {
                player.GetComponent<PlayerScript>().TakeDamage(20);
            }
        }

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
                player.GetComponent<PlayerScript>().TakeDamage(15);
            }
        }
        else
        {
            int attackChance = Random.Range(1, 101);
            if (attackChance <= 30 && CanAttack())
            {
                player.GetComponent<PlayerScript>().TakeDamage(30);
            }
        }

        energy -= 30;
        if (energy < 0)
            energy = 0;
    }

    public bool CanAttack()
    {
        if (weaponInHand == WeaponTypes.Bow)
        {
            return player.transform.position.z - gameObject.transform.position.z <= 50;
        }
        else
        {
            return player.transform.position.z - gameObject.transform.position.z <= 10;
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

    public void MoveForward()
    {
        if (!CanMoveForward()) return;
        gameObject.transform.Translate(0, 0, 4);
        energy -= 5;
    }

    public void MoveBackward()
    {
        if (!CanMoveBackward()) return;
        gameObject.transform.Translate(0, 0, -4);
        energy -= 5;
    }

    public void SwitchWeapons()
    {
        if(canSwitchWeapons)
        {
            if (weaponInHand == WeaponTypes.Bow)
            {
                weaponInHand = WeaponTypes.Sword;
                bow.SetActive(false);
                sword.SetActive(true);
            }
            else
            {
                weaponInHand = WeaponTypes.Bow;
                bow.SetActive(true);
                sword.SetActive(false);
            }   
        }
        energy -= 5;
    }

    public void DecideAction()
    {
        if (health <= 0) // Not alive
        {
            Die(); // Die
            Debug.Log("Enemy decided to Die");
            return;
        }
        else if (health < 50 && hasPotion) // HP under 50
        {
            DrinkPotion(); // Drink potion
            Debug.Log("Enemy decided to DrinkPotion");
        }
        else // HP 50 or above or less than 50 and doesn't have potion
        {
            if (energy >= 5) // Has enough energy
            {
                if (weaponInHand == WeaponTypes.Bow) // Has a bow
                {
                    if (CanMoveBackward()) // Can move backwards
                    {
                        if (CanAttack()) // Player in attack range
                        {
                            if(player.GetComponent<PlayerScript>().CanAttack()) // Player can attack
                            {
                                Attack(); // Attack
                                Debug.Log("Enemy decided to Attack (Bow)");
                            }
                            else // Player cannot attack
                            {
                                MoveBackward(); // Move backwards
                                Debug.Log("Enemy decided to MoveBackward (Bow)");
                            }
                        }
                        else
                        {
                            MoveForward(); // Move forward
                            Debug.Log("Enemy decided to MoveForward (Bow)");
                        }
                    }
                    else // Cannot move backwards
                    {
                        if (canSwitchWeapons) // Can switch weapons
                        {
                            SwitchWeapons();
                            Debug.Log("Enemy decided to SwitchWeapons (Bow)");
                        }
                        else
                        {
                            Attack();
                            Debug.Log("Enemy decided to Attack (Bow)");
                        }
                    }
                }
                else // Has a sword
                {
                    if (CanAttack()) // Player in attack range
                    {
                        if (player.GetComponent<PlayerScript>().CanAttack()) // Player can attack
                        {
                            Attack(); // Move forward
                            Debug.Log("Enemy decided to Attack (Sword)");
                        }
                        else // Player cannot attack
                        {
                            MoveBackward(); // Move backward
                            Debug.Log("Enemy decided to MoveBackward (Sword)");
                        }
                    }
                    else if (CanMoveForward())
                    {
                        MoveForward();
                        Debug.Log("Enemy decided to MoveForward (Sword)");
                    }
                }
            }
            else // Does not have enough energy
            {
                Rest();
                Debug.Log("Enemy decided to Rest");
            }
        }
        player.GetComponent<PlayerScript>().player_turn = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        //if AILevel = 1 - no switch, sword weapon start
        if (PlayerPrefs.GetInt("AILevel") == 1)
        {
            canSwitchWeapons = false;
        }
        else if (PlayerPrefs.GetInt("AILevel") == 2) //if AILevel = 2 - no switch, bow weapon start
        {
            weaponInHand = WeaponTypes.Bow;
            bow.SetActive(true);
            sword.SetActive(false);
            canSwitchWeapons = false;
        }
        else if (PlayerPrefs.GetInt("AILevel") == 3) //if AILevel = 3 - can switch, bow weapon start
        {
            weaponInHand = WeaponTypes.Bow;
            bow.SetActive(true);
            sword.SetActive(false);
            canSwitchWeapons = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Hp.value = health / 100.0f;
        Energy.value = energy / 100.0f;
    }
}
