using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ScreenPanel : MonoBehaviour {

    // TODO: move to UITransition
    public static readonly Ease EASE = Ease.InOutSine;
    public static readonly float PARALLAX_MULT = 2.0f;

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

    public virtual void ShowScreen(float delay, float duration, UITransition.AnimateMode mode, bool isAnimateForward) {
        gameObject.SetActive(true);
        _rectTransform.SetAsFirstSibling();

        _animateScreen(delay, duration, mode, isAnimateForward, true);
    }

    public virtual void HideScreen(float delay, float duration, UITransition.AnimateMode mode, bool isAnimateForward) {
        _animateScreen(delay, duration, mode, isAnimateForward, false, () => {
            gameObject.SetActive(false);
        });
    }

    public void NavigateBack() {
		ScreenManager.Instance.NavigateBack();
	}

    // TODO: prevent touch events when animating
    private void _animateScreen(float delay, float duration, UITransition.AnimateMode mode, bool isAnimateForward, bool isShow, TweenCallback cb = null) {
        if (mode == UITransition.AnimateMode.PAGE) {
            float widthOffset = _rectTransform.rect.width;

            Sequence seq = DOTween.Sequence();
            seq.SetEase(EASE);
            seq.AppendInterval(delay);

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