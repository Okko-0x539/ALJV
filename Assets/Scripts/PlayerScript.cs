using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveForward()
    {
        gameObject.transform.Translate(0, 0, 4);
    }

    public void MoveBackward()
    {
        gameObject.transform.Translate(0, 0, -4);
    }
}
