using UnityEngine;
using System;
using System.Collections.Generic;
using DG.Tweening;

// TODO: status bar color, PlayerSettings.iOS.statusBarStyle?

public class BaseScreen : MonoBehaviour
{
    public string Name { get; private set; }
    public bool HasNavigator { get; private set; }
    public bool IsTransitioning { get; private set; }
    
    protected ScreenManager _screenManager;
    private RectTransform _rectTransform;

    public BaseScreen(string name, bool navigator) : base()
    {
        Name = name;
        HasNavigator = navigator;
    }

    public void Initialize(ScreenManager screenManager)
    {
        _screenManager = screenManager;
    }

    protected virtual void Awake()
    {
        _rectTransform = gameObject.GetComponent<RectTransform>();
    }

    public void Show(bool isReverse)
    {
        StageShow();

        Transition(false, isReverse, () =>
        {
            DeStageShow();
        });
    }

    protected virtual void WillShow() {}

    protected virtual void OnShow() {}

    private void StageShow()
    {
        IsTransitioning = true;

        WillShow();
    }

    private void DeStageShow()
    {
        IsTransitioning = false;

        OnShow();
    }

    public void Hide(bool isReverse)
    {
        StageHide();

        Transition(true, isReverse, () =>
        {
            DeStageHide();
        });
    }

    protected virtual void WillHide() {}

    protected virtual void OnHide() {}

    private void StageHide()
    {
        IsTransitioning = true;

        WillHide();
    }

    private void DeStageHide()
    {
        IsTransitioning = false;

        OnHide();
    }

    private void Transition(bool isHide, bool isReverse, Action onComplete)
    {
        gameObject.SetActive(true);

        // set up DOTween sequence
        Sequence seq = DOTween.Sequence();
        seq.SetEase(ScreenManager.TRANS_EASE);
        seq.AppendInterval(ScreenManager.TRANS_DELAY);

        // convert isReverse to +/- direction
        int direction = isReverse ? -1 : 1;

        // get screen dimension, set up default start and end positions
        float deviceWidth = _rectTransform.rect.width;
        float positionStart = 0.0f;
        float positionEnd = 0.0f;

        if (isHide)
        {
            positionEnd = -direction * deviceWidth; // already onscreen, no need to adjust start position
        }
        else // (isShow)
        {
            positionStart = direction * deviceWidth; // then position offscreen, in preparation for transition
            _rectTransform.localPosition = new Vector3(positionStart, 0, 0);
        }
        
        seq.Append(transform.DOLocalMoveX(positionEnd, ScreenManager.TRANS_DURATION)).OnComplete(() =>
        {
            gameObject.SetActive(!isHide);

            onComplete();
        });
    }
}