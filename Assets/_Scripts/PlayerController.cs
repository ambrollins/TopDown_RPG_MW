using System;
using System.Collections.Generic;
using _Scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private ContactFilter2D moveFilter;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float collisionOffset = .5f;
    
    //[SerializeField] private float xPos = .5f;
    //[SerializeField] private float yPos = .5f;
    
    private Rigidbody2D _rigidbody2D;
    private Vector2 _movementInput;

    private List<RaycastHit2D> _collision2Ds = new List<RaycastHit2D>();
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    public int rocksCollectedCount = 0;

    public Inventory Inventory;
    public GameObject _image;
    public static PlayerController Instance;
    public Inventory_UI InventoryUI;

    public float dropDistance;
    private void Awake()
    {
        Inventory = new Inventory(6);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

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

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
    }


    private Vector3 mousePosInWorld;
    public void DropItemOnMousePos()
    {
        IsItemPicked = false;
        Vector3 mousePosition = Input.mousePosition;
        mousePosInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosInWorld.z = 0; 
        Instantiate(itemPicked,mousePosInWorld,Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, .3f);
    }


    private void Update()
    {
        if ( IsItemPicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 gg = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
                Debug.DrawLine(transform.position,gg);
                float _distance = Vector3.Distance(transform.position,gg);
                if(_distance<.3f)
                 DropItemOnMousePos();
            }
        }
    }

    private bool IsItemPicked;
    private Collectibles_ itemPicked;
    public void DropItem(Collectibles_ item)
    {
        IsItemPicked = true;
        InventoryUI.ToggleInventory();
        // Vector3 spawnLocation = transform.position;
        // Vector3 spawnOffset = new Vector3(xPos,yPos,0);
        itemPicked = item;
    }
}
