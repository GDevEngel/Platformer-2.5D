using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField] private Renderer _elevatorLight;
    [SerializeField] private int _reqCollectable = 8;

    [SerializeField] private Elevator _elevator;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                if (Input.GetKeyDown(KeyCode.E) && player.CollectableCollected() >= _reqCollectable)
                {
                    //turn light to green
                    _elevatorLight.material.color = Color.green;
                    _elevator.GetComponent<Elevator>().CallElevator();
                }
            }
        }
    }
}
