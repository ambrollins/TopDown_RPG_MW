using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private ContactFilter2D moveFilter;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float collisionOffset = .5f;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _movementInput;

    private List<RaycastHit2D> _collision2Ds = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_movementInput != Vector2.zero) //when moved 
        {
            bool yesMoved = CanMove(_movementInput);
            if (!yesMoved)
            {
                yesMoved = CanMove(new Vector2(_movementInput.x,0)); //slide on direction x when forced to move diagonally
                if (!yesMoved)
                {
                    yesMoved = CanMove(new Vector2(_movementInput.y, 0));  //slide on direction y when forced to move diagonally
                }
            }
        }
    }

    private bool CanMove(Vector2 direction)
    {
        //collision check using raycast
        int count = _rigidbody2D.Cast(direction, moveFilter, _collision2Ds,
            moveSpeed * Time.deltaTime + collisionOffset);
        if (count == 0)
        {
            _rigidbody2D.MovePosition(_rigidbody2D.position +
                                      direction *
                                      (Time.fixedDeltaTime *
                                       moveSpeed)); //amount of time between frames * movement speed  = how far its supposed to move
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
    }
}
