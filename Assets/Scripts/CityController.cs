using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityController : MonoBehaviour
{
    //[SerializeField] GameObject minisplosion = null;
    Animator animator = null;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       // if (minisplosion )
    }
    public bool CityDead()
        //Function for checking whether the city is still alive or not. 
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("cityaliveidle"))
        {
            return false;
        }
        else
            return true;
    }
}
