using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiratoryDoor : MonoBehaviour
{
    public Animator doorAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            doorAnimator.SetTrigger("Rotate");
        }
    }
}