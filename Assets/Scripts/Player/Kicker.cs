using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;

using UnityEngine;

public class Kicker : MonoBehaviour
{
    [SerializeField] private Leg _leg;
    [SerializeField] private KickCollider _kickColliderPrefab;
    [SerializeField] private float _distance;
    [SerializeField] private float _radius;
    [SerializeField] private float _force;

    private KickCollider _spawnedKickCollider = null;

    public void Kick()
    {
        if (_leg.gameObject.activeSelf) return;

        var collisions = Physics.SphereCastAll(
                transform.position,
                _radius,
                transform.forward,
                _distance
            )
            .Where(hit => hit.rigidbody != null)
            .ToList();

        foreach (var collision in collisions)
        {
            if (collision.rigidbody.TryGetComponent(out ThrowableObject obj))
            {
                obj.Owner = Owner.Player;
                collision.rigidbody.isKinematic = false;
            }
            else if (collision.rigidbody.TryGetComponent(out Enemy enemy))
            {
                var regdoll = enemy.Die();
                regdoll.Owner = Owner.Player;
            }
        }

        if (_spawnedKickCollider is null)
        {
            _spawnedKickCollider = Instantiate(_kickColliderPrefab);
        }
        _spawnedKickCollider.transform.position = transform.position;

        _spawnedKickCollider.gameObject.SetActive(true);
        _spawnedKickCollider.Init(_radius, _distance, _force, transform.forward);

        _leg.KickAnimation();
    }
}
