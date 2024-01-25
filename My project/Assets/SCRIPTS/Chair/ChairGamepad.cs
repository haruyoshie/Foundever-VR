using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ChairGamepadInteraction : MonoBehaviour
{
    public Button _UISitInteraction;
    public Button _GetUpButton;
    public Controller _Controller;
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag.Equals("Player") && _Controller._isSit == false)
        {
            SelectSit();
        }
        else if (_Controller._isSit)
        {
            SelectStand();
        }
    }

    private void SelectSit()
    {
        _UISitInteraction.Select();
    }

    private void SelectStand()
    {
        _GetUpButton.Select();
    }
}