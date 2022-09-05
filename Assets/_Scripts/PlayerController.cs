using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private Transform collectibles;
   
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (_movementInput != Vector2.zero) //when moved 
        {
            bool yesMoved = CanMove(_movementInput);
            if (!yesMoved) 
            {
                yesMoved = CanMove(new Vector2(_movementInput.x,0)); //slide on direction x when forced to move diagonally
                if (!yesMoved ) 
                {
                    yesMoved = CanMove(new Vector2(_movementInput.y, 0));  //slide on direction y when forced to move diagonally
                }
            }
            _animator.SetBool("IsMoving", yesMoved);//do move_anim
        }
        else
        {
            _animator.SetBool("IsMoving",false);//dont move_anim
        }

        if (_movementInput.x < 0) { _spriteRenderer.flipX = true; }
        else if(_movementInput.x >0) { _spriteRenderer.flipX = false; }
    }

    private bool CanMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
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
        else
        {
            return false; //no move_anim when no direction
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Collectibles"))
        {
            print("trigger for collectibles");
        }
    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
    }
}
