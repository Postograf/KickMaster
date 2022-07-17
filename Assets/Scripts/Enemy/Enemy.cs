using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class Enemy : StateMachine
{
    [SerializeField] private ThrowableObject _ragdoll;

    protected override void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _player = FindObjectOfType<Player>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (
            collision.gameObject.TryGetComponent(out ThrowableObject obj) 
            && obj.Owner == Owner.Player
        )
        {
            Die();
        }
    }

    public ThrowableObject Die()
    {
        enabled = false;
        Destroy(gameObject);
        return Instantiate(_ragdoll, transform.position, transform.rotation);
    }
}
