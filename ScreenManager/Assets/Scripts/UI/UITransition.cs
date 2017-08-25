using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UITransition : MonoBehaviour {

    public enum AnimateDirection {
        UP,
        DOWN,
        LEFT,
        RIGHT,
    }

    public enum AnimateMode {
        PAGE,
    }

    public static readonly float DELAY = 0.0f;
    public static readonly float DURATION = 0.24f;
    public static readonly Ease EASE = Ease.InOutSine;
    public static readonly float PARALLAX_MULT = 2.0f;
}