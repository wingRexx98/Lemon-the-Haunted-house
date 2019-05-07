using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;// in stead gameobject, transform is use to determine whether or not the player is in view or whther the gargoyle can see the player
    bool isInRange;
    public GameEnding gameEnd;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == player)
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            isInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);// the origin of the ray(where it start) and the direnction that it will go
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit))
            {
                if(raycastHit.collider.transform == player)
                {
                    gameEnd.Caught();
                }
            }
        }
    }
}
