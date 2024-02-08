using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NPCElevators : MonoBehaviour
{
    public string _SceneName;

    public GameObject  _Player;

    public NavMeshAgent _NavMeshAgent;

    public Animator _PlayerAnim,_Doors;

    public Transform[] Ascensores;
    public Animator[] _DoorsAnimators;

    public int levelFloor, stateLevel;
    public bool firstNumber,secondNumber;
    public int a, c;
    public TextMeshProUGUI numerInterface;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _Player = other.gameObject;
            _NavMeshAgent =_Player.GetComponent<NavMeshAgent>();
            _PlayerAnim = _Player.GetComponent<Animator>();
        }
    }
    
    public void ActiveAgent(int Ascensor)
    {
        _Player.GetComponent<ThirdPersonController>().enabled = false;
        _Player.GetComponent<NavMeshAgent>().enabled = true;
        Animator doorSelected = _DoorsAnimators[Ascensor];
        Vector3 posAscensor = new Vector3(Ascensores[Ascensor].position.x, Ascensores[Ascensor].position.y, Ascensores[Ascensor].position.z);
        _Player.GetComponent<NavMeshAgent>().SetDestination(posAscensor);
        _PlayerAnim.SetFloat("Speed", 1.95f);
        doorSelected.SetBool("elevatorOn", true);
        //StartCoroutine(openDoors());
    }

    public void SelectFloor(int number)
    {
        if (!firstNumber)
        {
            a = number;
            firstNumber = true;
            levelFloor = a;
            numerInterface.text = levelFloor.ToString();
            Invoke("AscensorLevel", 3.0f);
        }
        else
        {
            CancelInvoke("AscensorLevel");
            SelectSecondNumber(number);
        }
    }

    public void SelectSecondNumber(int number2)
    {
        c = number2;
        levelFloor = int.Parse(a.ToString()+ c.ToString());
        firstNumber = false;
        secondNumber = false;
        numerInterface.text = levelFloor.ToString();
        Invoke("AscensorLevel", 0);
        Debug.Log(stateLevel);
    }

    public void resetInterface()
    {
        numerInterface.text = "";
        levelFloor = 0;
        a = 0;
        c=0;
        firstNumber = false;
    }
    public void AscensorLevel()
    {
        switch (levelFloor)
        {
            case 2:
                // Debug.Log("Este piso no tiene acceso");
                numerInterface.text = "E";
                ActiveAgent(0);
                //Invoke("resetInterface", 0);
                break;
            case 4:
               // Debug.Log("Este piso no tiene acceso");
                numerInterface.text = "B";
                ActiveAgent(1);
                //Invoke("resetInterface", 0);
                break;
            case 7:
                // Debug.Log("Este piso no tiene acceso");
                numerInterface.text = "C";
                ActiveAgent(2);
                //Invoke("resetInterface", 0);
                break;
            case 13:
               // Debug.Log("Este piso no tiene acceso");
                numerInterface.text = "D";
                ActiveAgent(3);
                //Invoke("resetInterface", 0);
                break;
            default:
                print ("Este piso no tiene acceso");
                numerInterface.text = "No access";
                Invoke("resetInterface", 1f);
                break;
        }
    }
}
