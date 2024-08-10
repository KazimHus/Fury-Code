using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f; 
    public float airWalkSpeed = 3f;
    public float jumpImpulse = 10f;
    Vector2 moveInput;
    TouchingDirections touchingDirections;
    Damageable damageable; 

    public float CurrentMoveSpeed { get
        {
            if(CanMove)
            {
                 if(IsMoving && !touchingDirections.IsOnWall)
            {
           
                if(touchingDirections.IsGrounded)
                {
                    if(IsRolling)
                    {
                        return runSpeed;
                    }
                    else
                    {
                        return walkSpeed;
                    }
                }else
                {

                    return airWalkSpeed;

                }

            }           
            else
            {
                return 0;
            }

            } 
            else
            {
                return 0;
            }
        
        }
    }

    [SerializeField]
    private bool _isMoving = false; 

    public bool IsMoving {get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value; 
            animator.SetBool(AnimationStrings.isMoving, value);

        }
    }

    [SerializeField]
    private bool _isRunning = false; 

    public bool IsRolling
    {
        get
        {
            return _isRunning;
        }

        set
        {
            _isRunning = value; 
            animator.SetBool(AnimationStrings.isRolling, value);
        }

    }

    public bool _isFacingRight = true; 

    public bool IsFacingRight { 
        get 
        { 
            return _isFacingRight; 
        } 
        private set
        {
            if(_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1,1);
            }

            _isFacingRight = value; 

        } 
    }
    Rigidbody2D rb;
    Animator animator;

    public bool CanMove { get
        {
            return animator.GetBool(AnimationStrings.canMove); 
        }
    }

    public bool IsAlive {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
    }

    public void FixedUpdate()
    {
        if(!damageable.LockVelocity)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if(IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);

        }
        else
        {
            IsMoving = false; 
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;

        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false; 

        }
    }

    public void onRoll(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            IsRolling = true; 
        }

        else if(context.canceled)
        {
            IsRolling = false; 

        }
    }

    public void onJump(InputAction.CallbackContext context)
    {
        if(context.started && touchingDirections.IsGrounded && CanMove || touchingDirections._isOnWall)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);

        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);

    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            animator.SetTrigger(AnimationStrings.rangedattackTrigger);
        }
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetBool(AnimationStrings.attackBlock, true);
        }
        else if (context.canceled)
        {
            animator.SetBool(AnimationStrings.attackBlock, false);
        }
    }

}
