using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Collectable();
            }
            else { Debug.LogError("Collectable.player is null"); }

            Destroy(this.gameObject);
        }
    }
}
