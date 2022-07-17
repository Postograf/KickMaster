using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _appearanceDuration;
    
    private CanvasGroup _canvasGroup;
    private float _deltaAlpha;

    private void Awake()
    {
        _player.Died += OnPlayerDeth;
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _deltaAlpha = 1 / _appearanceDuration;
    }

    private void OnPlayerDeth()
    {
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
        StartCoroutine(SmothShow());
    }

    private IEnumerator SmothShow()
    {
        while (_canvasGroup.alpha < 1)
        {
            _canvasGroup.alpha += _deltaAlpha * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
