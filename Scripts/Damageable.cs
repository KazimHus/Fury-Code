using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit; 
    Animator animator; 

    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth
    {
        get{
            return _maxHealth;
        }
        set
        {
        _maxHealth = value; 
        }
    }

    [SerializeField]
    private int _health = 100; 

    public int Health
    {
        get
        {
            return _health; 
        }
        set
        {
            _health = value; 
            if(_health <= 0)
            {
                IsAlive = false; 
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private bool isInvincible = false;

    private float timeSinceHit = 0;
    public float invicibilityTimer = 0.25f;

    public bool IsAlive 
    {
        get
        {
            return _isAlive; 
        }

        set{
            _isAlive = value; 
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("isAlive set" + value);
        }
    }

    public bool LockVelocity { get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set{
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }   
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component is missing from this GameObject.");
        }

    }

    private void Update()
    {
        if(isInvincible)
        {
            if(timeSinceHit > invicibilityTimer)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime; 
        }

    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if(IsAlive && !isInvincible)
        {
            if (CharacterEvents.characterHealed == null)
            {
                Debug.LogError("CharacterEvents.characterHealed event is not initialized.");
            }

            Health -= damage;
            isInvincible = true;

            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;

            // Scale knockback based on current health
            float knockbackScale = Health > 0 ? (float)MaxHealth / Health : MaxHealth;
            Vector2 scaledKnockback = knockback * knockbackScale;

            damageableHit?.Invoke(damage, scaledKnockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
            
            return true;
        }

        return false;
    }

    public bool Heal(int healthRestore)
    {
        if(IsAlive && Health < MaxHealth)
        {
            if (CharacterEvents.characterDamaged == null)
            {
                Debug.LogError("CharacterEvents.characterDamaged event is not initialized.");
            }
            int maxHeal =  Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            Health += actualHeal;

            CharacterEvents.characterHealed(gameObject, actualHeal);
            return true; 
        }

        return false;
    }

}
