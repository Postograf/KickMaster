using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickCollider : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private SphereCollider _collider;

    public void Init(float radius, float distance, float speed, Vector3 direction)
    {
        _collider.radius = radius;
        _rigidbody.velocity = direction * speed;
        StartCoroutine(DestroyAfterTime(distance / speed));
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
