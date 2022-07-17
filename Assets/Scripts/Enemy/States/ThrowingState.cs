using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingState : EnemyState
{
    [SerializeField] private float _attackDelay;
    [SerializeField] private ThrowableObject _throwable;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _throwForce;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(Attack());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator Attack()
    {
        while (enabled)
        {
            Animator.SetTrigger("attack");
            yield return new WaitForSeconds(_attackDelay);

            var projectile = Instantiate(
                _throwable, _firePoint.position, Quaternion.identity
            );

            projectile.Owner = Owner.Enemy;
            projectile.Rigidbody.velocity = 
                (Player.transform.position - _firePoint.position).normalized
                * _throwForce;
        }
    }
}
