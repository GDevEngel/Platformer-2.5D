using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    //config
    [SerializeField] private Transform _pointStart, _pointEnd;
    private float _speed = 3f;

    private bool _goingDown = false;

    public void CallElevator()
    {
        _goingDown = !_goingDown;
    }

    private void FixedUpdate()
    {
        if (_goingDown == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _pointEnd.position, _speed * Time.deltaTime);
        }
        else if (_goingDown == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _pointStart.position, _speed * Time.deltaTime);            
        }
    }    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CallElevator();
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
