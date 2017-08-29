using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UITransition : MonoBehaviour {
    public enum AnimateMode {
        PAGE,
    }

    public const float DELAY = 0.0f;
    public const float DURATION = 0.24f;
    public const Ease EASE = Ease.InOutSine;
    public const float PARALLAX_MULT = 2.0f;
}