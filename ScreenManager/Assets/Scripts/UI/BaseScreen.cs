using UnityEngine;
using System;
using System.Collections.Generic;

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

        int siblingCount = _rectTransform.parent.childCount;
        int navigatorOffset = HasNavigator ? 1 : 0;

        if (!isHide && !isReverse) // which "card" should be on top for isReverse?
        {
            _rectTransform.SetSiblingIndex(siblingCount - navigatorOffset);
        }

        UITransition.Transition(gameObject, ref _rectTransform, isHide, isReverse, onComplete);
    }
}