using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HoverButtons : MonoBehaviour
{
    public TextMeshProUGUI instructions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HoverForwardOn()
    {
        instructions.gameObject.SetActive(true);
        instructions.text = "Move forward";
    }
    public void HoverBackwardOn()
    {
        instructions.gameObject.SetActive(true);
        instructions.text = "Move backward";
    }
    public void HoverAttackOn()
    {
        instructions.gameObject.SetActive(true);
        instructions.text = "Attack";
    }
    public void HoverAttackEasyOn()
    {
        instructions.gameObject.SetActive(true);
        instructions.text = "Easy Attack(70% chance)";
    }
    public void HoverAttackNormalOn()
    {
        instructions.gameObject.SetActive(true);
        instructions.text = "Normal Attack(50% chance)";
    }
    public void HoverAttackHardOn()
    {
        instructions.gameObject.SetActive(true);
        instructions.text = "Hard Attack(30% chance)";
    }
    public void HoverSwitchOn()
    {
        instructions.gameObject.SetActive(true);
        instructions.text = "Switch Weapons";
    }
    public void HoverRestOn()
    {
        instructions.gameObject.SetActive(true);
        instructions.text = "Rest(Restores 50 energy)";
    }
    public void HoverPotionOn()
    {
        instructions.gameObject.SetActive(true);
        instructions.text = "Use potion(Restores 50 hp)";
    }
    public void HoverBackOn()
    {
        instructions.gameObject.SetActive(true);
        instructions.text = "Go back to actions";
    }
    public void HoverOff()
    {
        instructions.gameObject.SetActive(false);
    }
}
