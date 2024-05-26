using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject sword;
    public GameObject bow;
    public GameObject messageBox;
    public bool player_turn = true;
    public bool attackmode = false;
    public TextMeshProUGUI instructions;
    public Slider Hp;
    public Slider Energy;

    private Coroutine nextTurnCoroutine;


    // Coroutine to hide the message box after a delay
    private IEnumerator GoToEnemyTurn()
    {
        yield return new WaitForSeconds(1f);
        enemy.GetComponent<EnemyAI>().DecideAction();
        nextTurnCoroutine = null;
    }

    private void StartTimerForEnemyTurn()
    {
        if (nextTurnCoroutine != null)
        {
            return;
        }
        nextTurnCoroutine = StartCoroutine(GoToEnemyTurn());
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
            return gameObject.transform.position.z - enemy.transform.position.z <= 60;
        }
        else
        {
            return gameObject.transform.position.z - enemy.transform.position.z <= 10;
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
        SceneManager.LoadScene("Main");
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
        if(energy != 0)
        {
            gameObject.transform.Translate(0, 0, 4);
            energy -= 5;
            player_turn = false;
            StartTimerForEnemyTurn();
        }
        else
        {
            Rest();
        }
    }

    public void MoveBackward()
    {
        if(energy != 0 )
        {
            gameObject.transform.Translate(0, 0, -4);
            energy -= 5;
            player_turn = false;
            StartTimerForEnemyTurn();
        }
        else
        {
            Rest();
        }
    }
    public void Rest()
    {
        energy += 50;
        if(energy > 100)
            energy = 100;
        messageBox.GetComponent<MessageBox>().ShowMessage("The player rested and restored energy");
        player_turn = false;
        StartTimerForEnemyTurn();
    }
    public void Potion()
    {
        health += 50;
        if(health > 100) 
            health = 100;
        potion.SetActive(false);
        messageBox.GetComponent<MessageBox>().ShowMessage("The player drank a potion and restored health");
        player_turn = false;
        StartTimerForEnemyTurn();
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
        if (energy != 0)
        {
            if (weaponInHand == WeaponTypes.Bow)
            {
                int attackChance = Random.Range(1, 101);
                if (attackChance <= 70 && CanAttack())
                {
                    enemy.GetComponent<EnemyAI>().TakeDamage(5);
                    messageBox.GetComponent<MessageBox>().ShowMessage("The player successfully hit the enemy");
                }
                else
                {
                    messageBox.GetComponent<MessageBox>().ShowMessage("Player missed!");
                }
            }
            else
            {
                int attackChance = Random.Range(1, 101);
                if (attackChance <= 70 && CanAttack())
                {
                    enemy.GetComponent<EnemyAI>().TakeDamage(5);
                    messageBox.GetComponent<MessageBox>().ShowMessage("The player successfully hit the enemy");
                }
                else
                {
                    messageBox.GetComponent<MessageBox>().ShowMessage("Player missed!");
                }
            }
            player_turn = false;
            StartTimerForEnemyTurn();
            attackmode = false;
            attackbuttons.SetActive(false);

            energy -= 10;
            if (energy < 0)
                energy = 0;
        }
        else
        {
            attackmode = false;
            attackbuttons.SetActive(false);
            Rest();
        }
    }
    public void AttackMedium()
    {
        if (energy != 0)
        {
            if (weaponInHand == WeaponTypes.Bow)
            {
                int attackChance = Random.Range(1, 101);
                if (attackChance <= 50 && CanAttack())
                {
                    enemy.GetComponent<EnemyAI>().TakeDamage(10);
                    messageBox.GetComponent<MessageBox>().ShowMessage("The player successfully hit the enemy");
                }
                else
                {
                    messageBox.GetComponent<MessageBox>().ShowMessage("Player missed!");
                }
            }
            else
            {
                int attackChance = Random.Range(1, 101);
                if (attackChance <= 50 && CanAttack())
                {
                    enemy.GetComponent<EnemyAI>().TakeDamage(20);
                    messageBox.GetComponent<MessageBox>().ShowMessage("The player successfully hit the enemy");
                }
                else
                {
                    messageBox.GetComponent<MessageBox>().ShowMessage("Player missed!");
                }
            }
            player_turn = false;
            StartTimerForEnemyTurn();
            attackmode = false;
            attackbuttons.SetActive(false);

            energy -= 20;
            if (energy < 0)
                energy = 0;
        }
        else
        {
            attackmode = false;
            attackbuttons.SetActive(false);
            Rest();
        }
    }
    public void AttackHard()
    {
        if (energy != 0)
        {
            if (weaponInHand == WeaponTypes.Bow)
            {
                int attackChance = Random.Range(1, 101);
                if (attackChance <= 30 && CanAttack())
                {
                    enemy.GetComponent<EnemyAI>().TakeDamage(15);
                    messageBox.GetComponent<MessageBox>().ShowMessage("The player successfully hit the enemy");
                }
                else
                {
                    messageBox.GetComponent<MessageBox>().ShowMessage("Player missed!");
                }
            }
            else
            {
                int attackChance = Random.Range(1, 101);
                if (attackChance <= 30 && CanAttack())
                {
                    enemy.GetComponent<EnemyAI>().TakeDamage(30);
                    messageBox.GetComponent<MessageBox>().ShowMessage("The player successfully hit the enemy");
                }
                else
                {
                    messageBox.GetComponent<MessageBox>().ShowMessage("Player missed!");
                }
            }
            player_turn = false;
            StartTimerForEnemyTurn();
            attackmode = false;
            attackbuttons.SetActive(false);

            energy -= 30;
            if (energy < 0)
                energy = 0;
        }
        else
        {
            attackmode = false;
            attackbuttons.SetActive(false);
            Rest();
        }
    }
    public void SwitchWeapon()
    {
        if (energy != 0)
        {
            //to do weapon switch later
            if(weaponInHand == WeaponTypes.Sword)
            {
                weaponInHand = WeaponTypes.Bow;
                bow.SetActive(true);
                sword.SetActive(false);
            }
            else
            {
                weaponInHand = WeaponTypes.Sword;
                bow.SetActive(false);
                sword.SetActive(true);
            }
            energy -= 5;
            messageBox.GetComponent<MessageBox>().ShowMessage("Player switched weapons");
            player_turn = false;
            StartTimerForEnemyTurn();
        }
        else
        {
            Rest();
        }
    }
}
