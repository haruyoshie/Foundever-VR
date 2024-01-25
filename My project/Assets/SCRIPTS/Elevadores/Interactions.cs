using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactions : MonoBehaviour
{
     public GameObject _UiElevator;
     public Button _FirstButtonOnPanel;
  
      private void OnTriggerEnter(Collider other)
      {
          if (other.tag.Equals("Player"))
          {
              _UiElevator.SetActive(true);
              InteractionManager.Instance.SetInteractState(InteractionState.StillMouseInteracting);
              _FirstButtonOnPanel.Select();
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
