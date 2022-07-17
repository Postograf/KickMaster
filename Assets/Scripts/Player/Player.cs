using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private VariableJoystick _joystick;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Kicker _kicker;
    [SerializeField] private int _health;

    public event Action Died;
    public event Action Damaged;

    private void Awake()
    {
        _joystick.MoveEnded += _kicker.Kick;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (
            collision.gameObject.TryGetComponent(out ThrowableObject obj) 
            && obj.Owner == Owner.Enemy
        )
        {
            TakeDamage();
        }
    }

    public void FixedUpdate()
    {
        Vector3 direction = 
            Vector3.forward * -_joystick.Horizontal 
            + Vector3.right * _joystick.Vertical;

        _rb.velocity = direction * _speed;
    }

    public void TakeDamage()
    {
        _health--;
        Damaged?.Invoke();

        if (_health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Died?.Invoke();
    }
}
