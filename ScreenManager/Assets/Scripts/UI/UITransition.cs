using UnityEngine;
using System;
using System.Collections.Generic;
using DG.Tweening;

public class UITransition : MonoBehaviour
{
    public const float DELAY = 0.0f;
    public const float DURATION = 1.24f;
    public const float PARALLAX = 1.0f;
    public const Ease EASE = Ease.InOutSine;
    // public const bool LINEAR = true; // override Ease function

    public static void Transition(ref RectTransform rectTransform, bool isHide, bool isReverse, Action onComplete)
    {
        // make sure GameObject is active
        GameObject targetGameObject = rectTransform.gameObject;
        targetGameObject.SetActive(true);

        // convert isReverse to +/- direction
        int direction = isReverse ? -1 : 1;

        // get screen width
        float deviceWidth = rectTransform.rect.width;

        // set up default start and end positions
        float positionStart, positionEnd = 0.0f;

        // set up duration of transition
        float duration = DURATION;

        if (isHide)
        {
            positionEnd = -direction * deviceWidth;

            duration *= PARALLAX;
        }
        else // (isShow)
        {
            positionStart = direction * deviceWidth;

            rectTransform.localPosition = new Vector3(positionStart, rectTransform.localPosition.y, 0);
        }

        // set up tween sequence
        Sequence seq = DOTween.Sequence();

        // only ease if linear override is false
        // if (!LINEAR)
        // {
        //     seq.SetEase(EASE);
        // }

        seq.AppendInterval(DELAY);
        
        seq.Append(targetGameObject.transform.DOLocalMoveX(positionEnd, duration)).OnComplete(() =>
        {
            targetGameObject.SetActive(!isHide);

            onComplete();
        });
    }
}