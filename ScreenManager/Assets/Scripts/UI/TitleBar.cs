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
        Initialize();
    }

    public void Initialize()
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

    public void SetZ()
    {
        if (!_rectTransform)
        {
            Initialize();
        }

        if (!IsTransitioning)
        {
            _rectTransform.parent.SetAsLastSibling();
        }
        else
        {
            Invoke("SetZ", 0.02f); // keep checking until screen is no longer transitioning
        }
    }

    private void Transition(bool isHide, bool isReverse, Action onComplete)
    {
        gameObject.SetActive(true);

        Transform overlayContainerTransform = _rectTransform.parent;

        int siblingCount = overlayContainerTransform.parent.childCount;
        int navigatorOffset = gameObject.activeSelf ? 0 : 1;

        if (!isHide && !isReverse) // right-most screen is always on top
        {
            overlayContainerTransform.SetSiblingIndex(siblingCount - navigatorOffset);
        }

        UITransition.Transition(ref _rectTransform, isHide, isReverse, onComplete);
    }
}