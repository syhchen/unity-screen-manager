using UnityEngine;
using System;
using System.Collections.Generic;
using DG.Tweening;

public class UITransition : MonoBehaviour
{
    public const float TRANS_DELAY = 0.0f;
    public const float TRANS_DURATION = 0.24f;
    public const float TRANS_PARALLAX = 2.0f;
    public const Ease TRANS_EASE = Ease.InOutSine;

    public static void Transition(GameObject targetGameObject, ref RectTransform rectTransform, bool isHide, bool isReverse, Action onComplete)
    {
        targetGameObject.SetActive(true);

        // set up tween sequence
        Sequence seq = DOTween.Sequence();

        seq.SetEase(TRANS_EASE);
        seq.AppendInterval(TRANS_DELAY);

        // convert isReverse to +/- direction
        int direction = isReverse ? -1 : 1;

        // get screen dimension
        float deviceWidth = rectTransform.rect.width;

        // set up default start and end positions
        float positionStart, positionEnd = 0.0f;

        if (isHide)
        {
            positionEnd = -direction * deviceWidth;
        }
        else // (isShow)
        {
            positionStart = direction * deviceWidth;
            rectTransform.localPosition = new Vector3(positionStart, rectTransform.localPosition.y, 0);
        }
        
        seq.Append(targetGameObject.transform.DOLocalMoveX(positionEnd, TRANS_DURATION)).OnComplete(() =>
        {
            targetGameObject.SetActive(!isHide);

            onComplete();
        });
    }
}