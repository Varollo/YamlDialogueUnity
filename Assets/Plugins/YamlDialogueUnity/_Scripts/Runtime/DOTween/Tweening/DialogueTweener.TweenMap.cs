using DG.Tweening;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

namespace YamlDialogueUnity.DOTween
{
    public partial class DialogueTweener
    {
        private static readonly Dictionary<TweenType, Func<Transform, float, bool, Tween>> _tweenMap = new()
        {
            { TweenType.None,       (_,_,_)  => null },
            { TweenType.SlideUp,    (t, d, e) => TweenPosition(Vector2.up,    t, d, e) },
            { TweenType.SlideDown,  (t, d, e) => TweenPosition(Vector2.down,  t, d, e) },
            { TweenType.SlideLeft,  (t, d, e) => TweenPosition(Vector2.left,  t, d, e) },
            { TweenType.SlideRight, (t, d, e) => TweenPosition(Vector2.right, t, d, e) },
            { TweenType.ScaleUp,    (t, d, e) => TweenScale(+1, t, d, e) },
            { TweenType.ScaleDown,  (t, d, e) => TweenScale(-1, t, d, e) },
            { TweenType.RotateCW,   (t, d, e) => TweenRotation(+1, t, d, e) },
            { TweenType.RotateCCW,  (t, d, e) => TweenRotation(-1, t, d, e) },
        };

        private static Tween TweenPosition(Vector2 direction, Transform target, float duration, bool reverse)
        {
            RectTransform rectTarget = target as RectTransform;

            if (!rectTarget)
                throw new NotImplementedException("Missing implementation for non-recttransform tweens.");

            Vector2 startValue = rectTarget.rect.size * -direction;
            Vector2 endValue = Vector2.zero;

            if (direction.x == 0)
                startValue.x = rectTarget.anchoredPosition.x;
            if (direction.y == 0)
                startValue.y = rectTarget.anchoredPosition.y;

            if (reverse)
                (startValue, endValue) = (endValue, startValue);

            rectTarget.anchoredPosition = startValue;
            return rectTarget.DOAnchorPos(endValue, duration);
        }

        private static Tween TweenScale(float direction, Transform target, float duration, bool reverse)
        {
            Vector2 startValue = new Vector3(.5f, .5f, 1f);
            Vector2 endValue = Vector3.one;

            if (reverse)
                (startValue, endValue) = (endValue, startValue);

            target.localScale = startValue;
            return target.DOScale(endValue, duration);
        }

        private static Tween TweenRotation(float direction, Transform target, float duration, bool reverse)
        {
            Vector3 startValue = new(0, 0, direction * 15);
            Vector3 endValue = Vector3.zero;

            if (reverse)
                (startValue, endValue) = (endValue, startValue);

            target.eulerAngles = startValue;
            return target.DORotate(endValue, duration);
        }
    }
}