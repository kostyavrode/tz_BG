using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPart : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Danger"))
        {
            GetComponentInParent<PlayerController>().RemovePlayerPart(this);
        }
        if (other.CompareTag("Bonus"))
        {
            Destroy(other.gameObject);
            GetComponentInParent<PlayerController>().AddPlayerPart();
        }
    }
}
