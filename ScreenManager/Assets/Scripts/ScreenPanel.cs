using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ScreenPanel : MonoBehaviour {

    public string Name { get; private set; }

    protected RectTransform _rectTransform;

    public ScreenPanel(string name) : base() {
        Name = name;
    }

    public virtual void Awake() {
        _rectTransform = gameObject.GetComponent<RectTransform>();
        if (_rectTransform == null) {
            Debug.LogError("No Rect Transform found on GameObject '" + gameObject.name + "'");
        }
    }

    public void NavigateTo(string nextScreenName) {
        ScreenManager.Instance.NavigateTo(nextScreenName);
    }
     public void NavigateBack() {
		ScreenManager.Instance.NavigateBack();
	}

    public virtual void ShowScreen(UITransition.AnimateMode mode, bool isAnimateForward, Action cb = null, float delay = UITransition.DELAY, float duration = UITransition.DURATION) {
        gameObject.SetActive(true);
        _rectTransform.SetAsLastSibling();

        _animateScreen(mode, isAnimateForward, () => {
            cb();
        }, delay, duration, true);
    }

    public virtual void HideScreen(UITransition.AnimateMode mode, bool isAnimateForward, Action cb = null, float delay = UITransition.DELAY, float duration = UITransition.DURATION*UITransition.PARALLAX_MULT) {
        _animateScreen(mode, isAnimateForward, () => {
            gameObject.SetActive(false);
            cb();
        }, delay, duration, false);
    }

    // TODO: prevent touch events when animating
    private void _animateScreen(UITransition.AnimateMode mode, bool isAnimateForward, TweenCallback cb, float delay, float duration, bool isShow) {
        if (mode == UITransition.AnimateMode.PAGE) {
            float widthOffset = _rectTransform.rect.width;

            Sequence seq = DOTween.Sequence();
            seq.SetEase(UITransition.EASE);
            seq.AppendInterval(delay);

            // manage starting position based on isShow and isAnimateForward
            float startPositionOffset = 0.0f;
            if (isShow && isAnimateForward) {
                _rectTransform.localPosition = new Vector3(widthOffset, 0, 0);
            } else if (isShow && !isAnimateForward) {
                _rectTransform.localPosition = new Vector3(-widthOffset, 0, 0);
            } else { // otherwise, keep position, is existing screen (to hide)
                startPositionOffset = widthOffset;
            }

            // manage ending position based on isAnimateForward
            if (isAnimateForward) {
                seq.Append(transform.DOLocalMoveX(-startPositionOffset, duration)).OnComplete(cb);
            } else { // animateBackward
                seq.Append(transform.DOLocalMoveX(startPositionOffset, duration)).OnComplete(cb);
            }
        } else {
            Debug.LogError("Unrecognized AnimateMode '" + mode + "'");

            return;
        }
    }
}