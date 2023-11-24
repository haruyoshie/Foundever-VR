using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChairInteraction : MonoBehaviour
{
    public GameObject chair, standUpPos,_UiChairInteraction,player,UI,_Halo;
    public Transform finalPosSit,finalPosGetup;
    public Controller _Controller;
    private static readonly int IsSitting = Animator.StringToHash("isSitting");


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            player = other.gameObject;
            _UiChairInteraction.SetActive(true);
            _Halo.SetActive(false);
            InteractionManager.Instance.SetInteractState(InteractionState.StillMouseInteracting);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("salio");
            player = other.gameObject;
            _UiChairInteraction.SetActive(false);
            _Halo.SetActive(true);
            InteractionManager.Instance.SetInteractState(InteractionState.Free);
            player.GetComponent<Animator>().SetLayerWeight(1, 0);
            player.GetComponent<CharacterController>().height = 1.8f;
        }
    }
    public void GetUPOffChair()
    {
        UI.SetActive(false);
        _UiChairInteraction.SetActive(false);
        InteractionManager.Instance.SetInteractState(InteractionState.Free);
        StartCoroutine(GetUpCoroutine());
        
    }
    public void SitDownOnChair()
    {
        if (!_Controller._isSit)
        {
            StartCoroutine(SitDownCoroutine());
        }
    }
    public void LockRotation(bool _CanMove)
    {
        if(_CanMove)
        {
            player.GetComponent<ThirdPersonController>()._canMove=true;
        }
        else
        {
            player.GetComponent<ThirdPersonController>()._canMove=false;
        }
    }

    public void MoveCamara(bool state)
    {
        player.GetComponent<InteractionManager>()
            .SetInteractState(state ? InteractionState.StillLoking : InteractionState.StillInteracting);
    }
    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator SitDownCoroutine()
    {
        UI.SetActive(true);
        _UiChairInteraction.SetActive(false);
        player.GetComponent<Animator>().SetLayerWeight(1,1f);
        player.GetComponent<Animator>().SetBool(IsSitting,true);
        _Controller._isSit = true;
        player.GetComponent<CharacterController>().height = 0.1f;
        player.transform.position = chair.transform.position;
        player.transform.rotation = chair.transform.rotation;
        player.GetComponent<CharacterController>().enabled = false;
        InteractionManager.Instance.SetInteractState(InteractionState.StillLoking);
        
        yield return new WaitForSeconds(2.2f); 
        player.transform.position = Vector3.Lerp(player.transform.position, finalPosSit.position,0.5f );
    }
    IEnumerator GetUpCoroutine()
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<Animator>().SetBool(IsSitting,false);
        player.transform.position = Vector3.Lerp(player.transform.position, finalPosGetup.position,0.5f );
        yield return new WaitForSeconds(2.2f);
        _UiChairInteraction.SetActive(true);
        player.GetComponent<Animator>().SetLayerWeight(1, 0);
        player.GetComponent<CharacterController>().height = 1.8f;
        player.transform.position = standUpPos.transform.position;
        player.transform.rotation = standUpPos.transform.rotation;
        _Controller._isSit = false;
        player.GetComponent<CharacterController>().enabled = true;
    }
    void Update()
    {    }
}
