﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    Camera fpsCamera;

    public float fireRate = 0.1f;
    float fireTimer;

    // Start is called before the first frame update
    //Hit Scan Yöntemi

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime;
        }

        if (Input.GetButton("Fire1") && fireTimer > fireRate)
        {
            fireTimer = 0.0f; // FireTimer 0'la

            RaycastHit _hit; 
            Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f,0.5f));
            
            if (Physics.Raycast(ray,out _hit,100))
            {
                Debug.Log(_hit.collider.gameObject.name);

            }
        }
    }
}
