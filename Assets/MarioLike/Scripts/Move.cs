using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public Transform Head;
    public Transform FootLeft;
    public Transform FootRight;
    public Transform Eyes;
    public LayerMask JumpOn;
    public LayerMask JumOnMoving;

    public float Speed;
    public Rigidbody2D Body;
    public float JumForce;

    private Vector2 _move;
    private Vector2 _aditionalVelocity;
    private bool _touchTheGround;
    private bool _touchTheMovingGround;
    private bool _isFacingRight = true;

    private void Awake()
    {
        Body = this.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();

        if (_move.x > 0f && !_isFacingRight)
        {
            _isFacingRight = true;
            transform.Rotate(0f, 180f, 0f);
            Eyes.position = new Vector3(Eyes.position.x, Eyes.position.y, -1);
        }

        else if (_move.x < 0f && _isFacingRight)
        {
            _isFacingRight = false;
            transform.Rotate(0f, 180f, 0f);
            Eyes.position = new Vector3(Eyes.position.x, Eyes.position.y, -1);
        }

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        
        if(_touchTheGround && context.ReadValue<float>()==1)
            Body.AddForce(new Vector2(0f, context.ReadValue<float>()*JumForce), ForceMode2D.Impulse);
    }


    private void FixedUpdate()
    {

        Body.velocity = new Vector2(_move.x*Speed, Body.velocity.y);


        var footLeftPosition = new Vector2(FootLeft.position.x, FootLeft.position.y);
        var footRightPosition = new Vector2(FootRight.position.x, FootRight.position.y);
        
        _touchTheGround = (Physics2D.OverlapCircle(footLeftPosition, 0.1f, JumpOn) || Physics2D.OverlapCircle(footRightPosition, 0.1f, JumpOn));

        Collider2D colLeft = Physics2D.OverlapCircle(footLeftPosition, 0.1f, JumOnMoving);
        Collider2D colRight = Physics2D.OverlapCircle(footRightPosition, 0.1f, JumOnMoving);


        if(!_touchTheGround || (colRight || colLeft))
        {
            Body.velocity += _aditionalVelocity;
        }
        else
        {
            _aditionalVelocity = new Vector2(0, 0);
        }

        if(colLeft)
        {
            this.transform.SetParent(colLeft.transform);
            _aditionalVelocity = colLeft.GetComponent<Rigidbody2D>().velocity;
        }

        else if (colRight)
        {
            this.transform.SetParent(colRight.transform);
            _aditionalVelocity = colRight.GetComponent<Rigidbody2D>().velocity;

        }
        else
        {
            this.transform.SetParent(null);
        }


        
    }


}
