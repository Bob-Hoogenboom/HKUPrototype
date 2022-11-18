using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputTest : MonoBehaviour
{
    [SerializeField] private float rollSpeed = 3f;

    private Rigidbody _rigidbody;
    private PlayerInputActions _playerInputActions;

    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();

        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Movement.Enable();
    }

    private void FixedUpdate()
    {
        OnMove();
    }

    private void OnMove()
    {
        Vector2 inputVector = _playerInputActions.Movement.Move.ReadValue<Vector2>();
        _rigidbody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * rollSpeed, ForceMode.Force);
    }
}
