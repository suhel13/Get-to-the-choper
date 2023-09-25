using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Character2dTopDownControler))]
public class PlayerInputControler : MonoBehaviour
{
    public InputAction movement;
    public InputAction aim;
    Camera mainCam;

    [SerializeField] private InputActionAsset controls;
    private InputActionMap _inputActionMap;

    Character2dTopDownControler _characterControler;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;

        _characterControler = GetComponent<Character2dTopDownControler>();
        _inputActionMap = controls.FindActionMap("Player");

        movement = _inputActionMap.FindAction("Movement");
        movement.performed += onMovementActionPerf;
        movement.canceled += onMovementActionCans;

        aim = _inputActionMap.FindAction("Aim");
        aim.performed += onAimActionPerf;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void onMovementActionPerf(InputAction.CallbackContext ctx)
    {
        _characterControler.movementVector = ctx.ReadValue<Vector2>();
    }
    void onMovementActionCans(InputAction.CallbackContext ctx)
    {
        _characterControler.movementVector = Vector2.zero;
    }
    void onAimActionPerf(InputAction.CallbackContext ctx)
    {
        _characterControler.lookAtTarget.position = mainCam.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
    }
}