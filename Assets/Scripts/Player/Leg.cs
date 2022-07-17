using DG.Tweening;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour
{
    [SerializeField] private Transform _start;
    [SerializeField] private Transform _end;
    [SerializeField] private float _kickDuration;
    [SerializeField] private float _hideDelay;
    [SerializeField] private Vector3 _rotationToHide;
    [SerializeField] private float _hideDuration;

    public async void KickAnimation()
    {
        if (gameObject.activeSelf) return;

        transform.position = _start.position;
        gameObject.SetActive(true);

        await transform.DOLocalMove(_end.localPosition, _kickDuration).AsyncWaitForCompletion();

        var oldRotation = transform.rotation;
        await transform.DORotateQuaternion(Quaternion.Euler(_rotationToHide), _hideDuration)
            .SetDelay(_hideDelay)
            .AsyncWaitForCompletion();

        gameObject.SetActive(false);
        transform.rotation = oldRotation;
        transform.position = _start.position;
    }
}
