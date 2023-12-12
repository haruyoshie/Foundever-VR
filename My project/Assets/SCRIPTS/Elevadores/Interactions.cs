using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
     public GameObject _UiElevator;
  
      private void OnTriggerEnter(Collider other)
      {
          if (other.tag.Equals("Player"))
          {
              _UiElevator.SetActive(true);
              InteractionManager.Instance.SetInteractState(InteractionState.StillMouseInteracting);
          }
      }
      private void OnTriggerExit(Collider other)
      {
          if (other.tag.Equals("Player"))
          {
              _UiElevator.SetActive(false);
              InteractionManager.Instance.SetInteractState(InteractionState.Free);
          }
      }

}
