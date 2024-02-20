using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject panel;
    public Animator waveAnimation;

    void Start()
    {
        panel.SetActive(false);
        
    }

    public void ActivatePanel()
    {
        panel.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.tag.Equals("Player"))
       {
            ActivatePanel();
            waveAnimation.SetTrigger("Wave");
       }
    }
}
