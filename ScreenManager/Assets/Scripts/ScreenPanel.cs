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

    public virtual void ShowScreen(float delay, float duration, UITransition.AnimateMode mode, bool isAnimateForward, Action cb = null) {
        gameObject.SetActive(true);
        _rectTransform.SetAsLastSibling();

        _animateScreen(delay, duration, mode, isAnimateForward, true, () => {
            cb();
        });
    }

    public virtual void HideScreen(float delay, float duration, UITransition.AnimateMode mode, bool isAnimateForward, Action cb = null) {
        _animateScreen(delay, duration*UITransition.PARALLAX_MULT, mode, isAnimateForward, false, () => {
            gameObject.SetActive(false);
            cb();
        });
    }

    // TODO: prevent touch events when animating
    private void _animateScreen(float delay, float duration, UITransition.AnimateMode mode, bool isAnimateForward, bool isShow, TweenCallback cb = null) {
        if (mode == UITransition.AnimateMode.PAGE) {
            float widthOffset = _rectTransform.rect.width;

            Sequence seq = DOTween.Sequence();
            seq.SetEase(UITransition.EASE);
            seq.AppendInterval(delay);

            // TODO: make sure this is used;
            // manage AnimateDirection based on mode
            UITransition.AnimateDirection direction = isAnimateForward ? UITransition.AnimateDirection.LEFT : UITransition.AnimateDirection.RIGHT;

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