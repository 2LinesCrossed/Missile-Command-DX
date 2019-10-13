﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Simple self-destroy script for explosion objects.
/// </summary>
public class SelfDestruct : MonoBehaviour
{
    [SerializeField] private float destroyTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //Destroys object after a second.
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
