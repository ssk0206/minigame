using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Token
{   
    public static TokenMgr<Character> parent;
    private Animator _animator;
    private Rigidbody2D _rigidbody2d;
    private int _direction;
    private float _speed; 
    public void Start()
    {
        _direction = 1;
        _speed = 0.02f;
    }
    public void OnStart()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }
    public enum State
    {
        Moving = 0,
        Attacking,
    }

    private State _currentState = State.Moving;
    private State currentState
    {
        get => _currentState;
        set => _currentState = value;
    }

    public void OnUpdate()
    {
        switch (currentState)
        {
            case State.Moving:
                _animator.SetBool("isAttacking", false);
                Dash();
            break;
            case State.Attacking:
                _animator.SetBool("isAttacking", true);
                Attack();
            break;
        }
    }

    private void Dash()
    {
        AddPosition(_speed*_direction, 0);
        currentState = State.Moving;
    }

    private void Attack()
    {
    }
}
