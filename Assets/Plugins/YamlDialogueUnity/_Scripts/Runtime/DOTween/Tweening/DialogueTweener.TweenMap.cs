using DG.Tweening;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

namespace YamlDialogueUnity.DOTween
{
    public partial class DialogueTweener
    {
        private static readonly Dictionary<TweenType, Func<Transform, float, float, bool, Tween>> _tweenMap = new()
        {
            { TweenType.None,       (_,_,_,_)  => null },
            { TweenType.SlideUp,    (t, i, d, e) => TweenPosition(Vector2.up * i,    t, d, e) },
            { TweenType.SlideDown,  (t, i, d, e) => TweenPosition(Vector2.down * i,  t, d, e) },
            { TweenType.SlideLeft,  (t, i, d, e) => TweenPosition(Vector2.left * i,  t, d, e) },
            { TweenType.SlideRight, (t, i, d, e) => TweenPosition(Vector2.right * i, t, d, e) },
            { TweenType.ScaleUp,    (t, i, d, e) => TweenScale(+i, t, d, e) },
            { TweenType.ScaleDown,  (t, i, d, e) => TweenScale(-i, t, d, e) },
            { TweenType.RotateCW,   (t, i, d, e) => TweenRotation(+i, t, d, e) },
            { TweenType.RotateCCW,  (t, i, d, e) => TweenRotation(-i, t, d, e) },
            { TweenType.Fade,       (t, i, d, e) => TweenFade(i, t, d, e) },
        };

        private static Tween TweenPosition(Vector2 direction, Transform target, float duration, bool reverse)
        {
            RectTransform rectTarget = target as RectTransform;

            if (!rectTarget)
                throw new NotImplementedException("Missing implementation for non-recttransform tweens.");
            
            Vector2 fromCenter = new()
            {
                x = Mathf.Sign(target.parent.position.x - target.position.x),
                y = Mathf.Sign(target.parent.position.y - target.position.y)
            };

            Vector2 startValue = rectTarget.rect.size * -direction * fromCenter;
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

        private static Tween TweenFade(float direction, Transform target, float duration, bool reverse)
        {
            if (target is not RectTransform)
                return null;

            float startValue = 1 - direction;
            float endValue = direction;

            if (reverse)
                (startValue, endValue) = (endValue, startValue);

            if (target.TryGetComponent<Graphic>(out var graphic))
            {
                graphic.color = new(graphic.color.r, graphic.color.g, graphic.color.b, startValue);
                return graphic.DOFade(endValue, duration);
            }
            else if (target.TryGetComponent<CanvasGroup>(out var canvasGroup)
                 || (canvasGroup = target.gameObject.AddComponent<CanvasGroup>()))
            {
                canvasGroup.alpha = startValue;
                return canvasGroup.DOFade(endValue, duration);
            }

            return null;
        }
    }
}