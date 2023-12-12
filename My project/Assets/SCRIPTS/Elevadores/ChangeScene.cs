using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
   public string nameScene;
   public void OnTriggerEnter(Collider other)
   {
      if (other.tag.Equals("Player"))
      {
         SceneManager.LoadScene(nameScene);
      }
   }

}
