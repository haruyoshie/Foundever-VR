using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

[RequireComponent(typeof(ThirdPersonController))]
[DisallowMultipleComponent]
public class InteractionManager : MonoBehaviour
{
    [SerializeField]
    private InteractionState playerStartingState;

    public static InteractionManager Instance;
    private InteractionState interactState = InteractionState.Free;
    private ThirdPersonController controller;

    private void Awake()
    {
        if (!Instance)
        {
            SetInstance();
        }
    }

    private void Start()
    {
        controller = GetComponent<ThirdPersonController>();
        SetInteractState(playerStartingState);
    }

    public void SetInstance()
    {
        Instance = this;
    }

    public void SetInteractState(InteractionState state)
    {
        interactState = state;

        if (!controller)
            return;

        switch (interactState)
        {
            case InteractionState.Free:
                //controller.SwitchMove(true);
                controller.LockCameraPosition = false;
                SetCursorState(false);
                break;

            case InteractionState.StillMouseInteracting:
                //controller.SwitchMove(false);
                controller.LockCameraPosition = true;
                SetCursorState(true);
                break;

            case InteractionState.StillInteracting:
                //controller.SwitchMove(false);
                controller.LockCameraPosition = true;
                break;
            case InteractionState.StillLoking:
                controller.LockCameraPosition = false;
                SetCursorState(true);
                break;
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            switch (interactState)
            {
                case InteractionState.Free:
                    SetCursorState(false);
                    break;
                case InteractionState.StillMouseInteracting:
                    SetCursorState(true);
                    break;
                case InteractionState.StillInteracting:
                    break;
            }
        }
    }

   

    public static void SetCursorState(bool state)
    {
        if (state)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

public enum InteractionState { Free, StillMouseInteracting, StillInteracting,StillLoking }