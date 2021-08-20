﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform _pointA, _pointB;
    private Transform _target; 
    private float _speed = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        _target = _pointA;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _pointA.position) < 0.001f)
        {
            _target = _pointB;
        }
        else if (Vector3.Distance(transform.position, _pointB.position) < 0.001f)
        {
            _target = _pointA;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.SetParent(null);
        }
    }
}
