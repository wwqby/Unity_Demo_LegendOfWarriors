using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControls inputControls;
    public Vector2 inputDirection;

    private void Awake()
    {
        inputControls = new PlayerInputControls();
    }

    private void OnEnable() {
        inputControls.Enable();
    }

    private void OnDisable() {
        inputControls.Disable();
    }

    private void Update() {
        inputDirection = inputControls.GamePlay.Move.ReadValue<Vector2>();
    }

}
