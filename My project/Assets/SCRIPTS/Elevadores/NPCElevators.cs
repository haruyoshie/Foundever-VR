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

    public Vector3 _Ascensor;
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
    
    public void ActiveAgent()
    {
        _Player.GetComponent<ThirdPersonController>().enabled = false;
        _Player.GetComponent<NavMeshAgent>().enabled = true;
        _PlayerAnim.SetFloat("Speed", 1);
        _NavMeshAgent.SetDestination(_Ascensor);
        _Doors.SetBool("elevatorOn", true);
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
    }
    public void AscensorLevel()
    {
        switch (levelFloor)
        {
            case 1:
                Debug.Log("Este piso no tiene acceso");
                numerInterface.text = "No access";
                Invoke("resetInterface", 1f);
                a = 0;
                c=0;
                break;
            case 2:
                Debug.Log("Este piso esta en construcción");
                numerInterface.text = "No access";
                Invoke("resetInterface", 1f);
                a = 0;
                c=0;
                break;
            case 3:
                Debug.Log("Este piso no tiene acceso");
                numerInterface.text = "No access";
                Invoke("resetInterface", 1f);
                a = 0;
                c=0;
                break;
            case 4:
                Debug.Log("ya esta en ese piso");
                numerInterface.text = "Same Floor";
                Invoke("resetInterface", 1f);
                a = 0;
                c=0;
                break;
            case 5:
                Debug.Log("Este piso no tiene acceso");
                numerInterface.text = "No access";
                Invoke("resetInterface", 1f);
                a = 0;
                c=0;
                break;
            case 6:
                Debug.Log("Este piso no tiene acceso");
                numerInterface.text = "No access";
                Invoke("resetInterface", 1f);
                a = 0;
                c=0;
                break;
            case 7:
                Debug.Log("Este piso no tiene acceso");
                numerInterface.text = "No access";
                Invoke("resetInterface", 1f);
                a = 0;
                c=0;
                break;
            case 8:
                Debug.Log("Este piso no tiene acceso");
                numerInterface.text = "No access";
                Invoke("resetInterface", 1f);
                a = 0;
                c=0;
                break;
            case 9:
                Debug.Log("Este piso no tiene acceso");
                numerInterface.text = "No access";
                Invoke("resetInterface", 1f);
                a = 0;
                c=0;
                break;
            case 10:
                Debug.Log("Este piso no tiene acceso");
                numerInterface.text = "No access";
                Invoke("resetInterface", 1f);
                a = 0;
                c=0;
                break;
            case 11:
                Debug.Log("Este piso no tiene acceso");
                numerInterface.text = "No access";
                Invoke("resetInterface", 1f);
                a = 0;
                c=0;
                break;
            case 12:
                Debug.Log("Este piso no tiene acceso");
                numerInterface.text = "No access";
                Invoke("resetInterface", 1f);
                a = 0;
                c=0;
                break;
            case 13:
               // Debug.Log("Este piso no tiene acceso");
                numerInterface.text = "B";
               ActiveAgent();
                //Invoke("resetInterface", 0);
                break;
            default:
                print ("Incorrect intelligence level.");
                Invoke("resetInterface", 1f);
                a = 0;
                c=0;
                break;
        }
    }
}
