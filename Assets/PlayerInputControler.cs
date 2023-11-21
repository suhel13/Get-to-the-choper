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
    InputAction number1;
    InputAction number2;
    InputAction number3;
    InputAction number4;
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

        number1 = _inputActionMap.FindAction("Number 1");
        number2 = _inputActionMap.FindAction("Number 2");
        number3 = _inputActionMap.FindAction("Number 3");
        number4 = _inputActionMap.FindAction("Number 4");

        number1.performed += ctx => onNumberActionPerf(ctx, 0);
        number2.performed += ctx => onNumberActionPerf(ctx, 1);
        number3.performed += ctx => onNumberActionPerf(ctx, 2);
        number4.performed += ctx => onNumberActionPerf(ctx, 3);

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
        _characterControler.lookAtTarget.position = new Vector3(_characterControler.lookAtTarget.position.x, _characterControler.lookAtTarget.position.y, 0);
    }

    void onShootActionPerf(InputAction.CallbackContext ctx)
    {
        _wepponManager.isAttacking = true;
    }
    void onShootActionCanc(InputAction.CallbackContext ctx)
    {
        _wepponManager.isAttacking = false;
    }

    void onRelodeActionPerf(InputAction.CallbackContext ctx)
    {
        _wepponManager.activeWeppon.StartRelode(true);
    }

    void onNumberActionPerf(InputAction.CallbackContext ctx, int id)
    {
        _wepponManager.SwapWeppon(id);
    }
}