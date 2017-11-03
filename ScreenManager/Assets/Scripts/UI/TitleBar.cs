using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class TitleBar : MonoBehaviour
{
    public Text TitleText;
    public Button BackButton;

    public bool IsTransitioning { get; private set; }

    private RectTransform _rectTransform;

    protected virtual void Awake()
    {
        _rectTransform = gameObject.GetComponent<RectTransform>();
    }

    public void Show(string titleText, bool isReverse)
    {
        TitleText.text = titleText;

        if (!gameObject.activeSelf) // do the screen transition if hidden
        {
            IsTransitioning = true;

            Transition(false, isReverse, () =>
            {
                IsTransitioning = false;
            });
        }
    }

    public void Hide(bool isReverse)
    {
        if (gameObject.activeSelf) // do the screen transition if active
        {
            IsTransitioning = true;

            Transition(true, isReverse, () =>
            {
                IsTransitioning = false;
            });
        }
    }

    private void Transition(bool isHide, bool isReverse, Action onComplete)
    {
        UITransition.Transition(gameObject, ref _rectTransform, isHide, isReverse, onComplete);
    }
}