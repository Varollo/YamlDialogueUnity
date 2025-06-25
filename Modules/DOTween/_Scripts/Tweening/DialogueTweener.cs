using DG.Tweening;
using System;
using UnityEngine;

namespace YamlDialogueUnity.DOTween
{
    public partial class DialogueTweener
    {
        public Sequence Tween(Transform target, TweenSettings settings, bool killExisting = false)
        {
            var seq = DG.Tweening.DOTween.Sequence(target);
            
            foreach (TweenType tweenType in Enum.GetValues(typeof(TweenType)))
            {
                if (settings.TweenType.HasFlag(tweenType))
                {
                    var tween = _tweenMap[tweenType]?.Invoke(target, settings.Intensity, settings.Duration, settings.Reverse);

                    if (tween != null)
                        seq.Join(tween.SetEase(settings.Ease));
                }
            }

            return seq;
        }

        [Serializable]
        public class TweenSettings
        {
            public TweenType TweenType;
            public float Intensity;
            public bool Reverse;
            public float Duration;
            public Ease Ease;

            public static TweenSettings GetDefault()
            {
                return new TweenSettings()
                {
                    TweenType = TweenType.SlideUp,
                    Intensity = 1f,
                    Reverse = false,
                    Duration = .25f,
                    Ease = Ease.InOutQuad
                };
            }
        }

        [Serializable, Flags]
        public enum TweenType
        {
            None       = 0x000,
            SlideUp    = 0x001, 
            SlideDown  = 0x002,
            SlideLeft  = 0x004,
            SlideRight = 0x008,
            ScaleUp    = 0x010,
            ScaleDown  = 0x020,
            RotateCW   = 0x040, 
            RotateCCW  = 0x080,
            Fade       = 0x100
        }
    }
}