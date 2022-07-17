using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Owner 
{
    None,
    Player,
    Enemy
}

[RequireComponent(typeof(Rigidbody))]
public class ThrowableObject : MonoBehaviour
{
    private Owner _owner;
    private Rigidbody _rigidbody;

    public Rigidbody Rigidbody  => _rigidbody;

    public Owner Owner 
    { 
        get => _owner;
        set
        {
            _rigidbody.useGravity = value != Owner.Enemy;
            _owner = value;
        }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Owner != Owner.None && _rigidbody.velocity == Physics.gravity)
            Owner = Owner.None;
    }
}
