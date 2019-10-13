using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A simple script that determines whether the city is alive or not. 
/// </summary>
public class CityController : MonoBehaviour
{
   
    Animator animator = null;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>(); //Gets the animator of the object that this script is attached to. 
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public bool CityDead()
        //Function for checking whether the city is still alive or not. 
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("cityaliveidle")) //Checks the animation state for alive/dead calculations.
        {
            return false;
        }
        else
            return true;
    }
}
