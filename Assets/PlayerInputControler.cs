using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Character2dTopDownControler))]
[RequireComponent(typeof(WepponManager))]
public class PlayerInputControler : MonoBehaviour
{
    InputAction movement;
    InputAction aim;
    InputAction shoot;
    InputAction relode;
    Camera mainCam;

    [SerializeField] private InputActionAsset controls;
    private InputActionMap _inputActionMap;

    Character2dTopDownControler _characterControler;
    WepponManager _wepponManager;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;

        _characterControler = GetComponent<Character2dTopDownControler>();
        _wepponManager = GetComponent<WepponManager>();
        _inputActionMap = controls.FindActionMap("Player");

        movement = _inputActionMap.FindAction("Movement");
        movement.performed += onMovementActionPerf;
        movement.canceled += onMovementActionCanc;

        aim = _inputActionMap.FindAction("Aim");
        aim.performed += onAimActionPerf;

        shoot = _inputActionMap.FindAction("Shoot");
        shoot.performed += onShootActionPerf;
        shoot.canceled += onShootActionCanc;

        relode = _inputActionMap.FindAction("Relode");
        relode.performed += onRelodeActionPerf;

    }

    // Update is called once per frame
    void Update()
    {

    }
    void onMovementActionPerf(InputAction.CallbackContext ctx)
    {
        _characterControler.movementVector = ctx.ReadValue<Vector2>();
    }
    void onMovementActionCanc(InputAction.CallbackContext ctx)
    {
        _characterControler.movementVector = Vector2.zero;
    }
    void onAimActionPerf(InputAction.CallbackContext ctx)
    {
        _characterControler.lookAtTarget.position = mainCam.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
    }

    void onShootActionPerf(InputAction.CallbackContext ctx)
    {
        _wepponManager.isShooting = true;
    }
    void onShootActionCanc(InputAction.CallbackContext ctx)
    {
        _wepponManager.isShooting = false;
    }
    void onRelodeActionPerf(InputAction.CallbackContext ctx)
    {
        _wepponManager.activeGun.startRelode();
    }
}