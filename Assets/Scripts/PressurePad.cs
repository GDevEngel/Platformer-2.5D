using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    // handle
    [SerializeField] private GameObject _button;

    // config
    [SerializeField] private float _pressureDistance = 0.05f;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Pushable")
        {
            if (Vector3.Distance(transform.position, other.transform.position) < _pressureDistance)
            {
                //then set box to iskinematic = not movable
                other.attachedRigidbody.isKinematic = true;
                //then change color of pressure pad to green
                _button.GetComponent<Renderer>().material.color = Color.green;
                Destroy(this);
            }
        }
    }    
}
