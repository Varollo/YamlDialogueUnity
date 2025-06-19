using DG.Tweening;
using System;
using UnityEngine;

namespace YamlDialogueUnity.DOTween
{
    public partial class DialogueTweener
    {
        public Sequence Tween(Transform target, TweenSettings settings)
        {
            var seq = DG.Tweening.DOTween.Sequence(target);
            
            foreach (TweenType tweenType in Enum.GetValues(typeof(TweenType)))
            {
                if (settings.TweenType.HasFlag(tweenType))
                {
                    var tween = _tweenMap[tweenType]?.Invoke(target, settings.Duration, settings.Reverse);

                    if (tween != null)
                        seq.Join(tween.SetEase(settings.Ease));
                }
            }

            return seq;
        }

        [System.Serializable]
        public class TweenSettings
        {
            public TweenType TweenType;
            public bool Reverse;
            public float Duration;
            public Ease Ease;

            public static TweenSettings GetDefault()
            {
                return new TweenSettings()
                {
                    TweenType = TweenType.SlideUp,
                    Reverse = false,
                    Duration = .25f,
                    Ease = Ease.InOutQuad
                };
            }
        }

        [Serializable, Flags]
        public enum TweenType
        {
            None       = 0x00,
            SlideUp    = 0x01, 
            SlideDown  = 0x02,
            SlideLeft  = 0x04,
            SlideRight = 0x08,
            ScaleUp    = 0x10,
            ScaleDown  = 0x20,
            RotateCW   = 0x40, 
            RotateCCW  = 0x80
        }
    }
}