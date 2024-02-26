using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Serialization;

public class PointOfInterest : MonoBehaviour
{
    public Camera camToRender;
    public GameObject uiToDisplay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            uiToDisplay.SetActive(true);
            InteractionManager.Instance.SetInteractState(InteractionState.StillMouseInteracting);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            uiToDisplay.SetActive(false);
            InteractionManager.Instance.SetInteractState(InteractionState.Free);
        }
    }

    public void Interaction()
    {
        camToRender.gameObject.SetActive(true);
        FindObjectOfType<ThirdPersonController>()._canMove = false;
        InteractionManager.Instance.SetInteractState(InteractionState.StillInteracting);
    }

    public void EndInteraction()
    {
        FindObjectOfType<ThirdPersonController>()._canMove = true;
        camToRender.gameObject.SetActive(false);
        
    }
}
