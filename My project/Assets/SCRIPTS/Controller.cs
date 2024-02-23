using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;
public class Controller : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    public GameObject player,_doorPos,camaraMinimap,minimapPoint;
    public bool _isSit,_hasBatch;
    public GameObject Menu,uiMenu;
    public Animator door;
    private bool t;

    private static readonly int IsOpening = Animator.StringToHash("isOpening");
    private static readonly int Opening = Animator.StringToHash("Opening");

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(searchPlayer());
    }

    IEnumerator searchPlayer()
    {
        yield return new WaitForSeconds(0.1f);
        player = GameObject.FindWithTag("Player");
        camaraMinimap.transform.SetParent(player.transform);
        minimapPoint.transform.SetParent(player.transform);
        cam.Follow = GameObject.FindWithTag("CinemachineTarget").transform;
        player.GetComponent<StarterAssetsInputs>().openMenu.AddListener(OpenMenu);
        Debug.Log(camaraMinimap.transform.position + "camara");
        Debug.Log(camaraMinimap.transform.position + "Point");
    }
    public void OpenMenu()
    {
        t = !t;
        if (t)
        {
            Debug.Log("minimap activate");
            uiMenu.SetActive(false);
            player.GetComponent<ThirdPersonController>()._canMove = false;
            Menu.SetActive(true);
            InteractionManager.Instance.SetInteractState(InteractionState.StillMouseInteracting);
        }
        else
        {
            CloseMenu();
        }
    }

    public void CloseMenu()
    {
        t = false;
        Menu.SetActive(false);
        uiMenu.SetActive(true);
        if (_isSit)
        {
            Debug.Log("nada");
        }
        else
        {
            player.GetComponent<ThirdPersonController>()._canMove = true;
            InteractionManager.Instance.SetInteractState(InteractionState.Free);
        }
        
    }

    public void OpenDoor()
    {
        if (_hasBatch)
        {
            StartCoroutine(OpenDoorRoutine());
        }
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator OpenDoorRoutine()
    {
        player.GetComponent<Animator>().SetLayerWeight(2,1f);
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<Animator>().SetBool(IsOpening,true);
        player.GetComponent<ThirdPersonController>()._canMove=false;
        player.transform.position = _doorPos.transform.position;
        player.transform.rotation = _doorPos.transform.rotation;
        door.SetBool(Opening, true);
        yield return new WaitForSeconds(5f);
        //door.SetBool(Opening, false);
        player.GetComponent<ThirdPersonController>()._canMove=true;
        player.GetComponent<Animator>().SetBool(IsOpening,false);
        player.GetComponent<Animator>().SetLayerWeight(1, 0);
        
        player.GetComponent<CharacterController>().enabled = true;
    }
}
