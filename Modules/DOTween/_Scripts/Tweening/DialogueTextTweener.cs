using DG.Tweening;
using UnityEngine.UI;

namespace YamlDialogueUnity.DOTween
{
    public class DialogueTextTweener
    {
        private readonly ITweenableText _view;

        private Tween _actorTxtTween;
        private Tween _lineTxtTween;

        private string _currentActor;

        public DialogueTextTweener(ITweenableText view)
        {
            _view = view;
        }

        public bool SkipLine()
        {
            bool skip = false;

            if (skip |= _actorTxtTween != null && _actorTxtTween.active)
                _actorTxtTween.Complete(true);

            if (skip |= _lineTxtTween != null && _lineTxtTween.active)
                _lineTxtTween.Complete(true);

            return skip;
        }

        public Tween TweenLine(string line, float lineCharDuration, Graphic lineGraphic)
        {
            if (string.IsNullOrEmpty(line))
            {
                _view.UpdateLineTxt(string.Empty);
                return null;
            }

            return _lineTxtTween = lineGraphic.DOFade(1, (line.Length - 1) * lineCharDuration)
                    .OnUpdate(() => _view.UpdateLineTxt(line[..(int)((line.Length - 1f) * _lineTxtTween.ElapsedPercentage())]))
                    .OnComplete(() => _view.UpdateLineTxt(line));
        }

        public Tween TweenActorName(string actorName, float fadeDuration, Graphic actorNameGraphic)
        {
            if (string.IsNullOrEmpty(actorName))
            {
                _view.UpdateActorTxt(string.Empty);
                return null;
            }
            
            if (actorName == _currentActor)
            {
                return null;
            }

            _view.UpdateActorTxt(_currentActor = actorName);

            actorNameGraphic.color = new()
            {
                r = actorNameGraphic.color.r,
                g = actorNameGraphic.color.g,
                b = actorNameGraphic.color.b,
                a = 0
            };

            return _actorTxtTween = actorNameGraphic.DOFade(1f, fadeDuration);
        }
    }
}
