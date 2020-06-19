using UnityEngine;
using DG.Tweening;

public class Transition : MonoBehaviour
{
    Sequence _tweenSequence;
    [SerializeField] CanvasGroup canvasGroup;

    public float duration { get; private set; }

    private void Start()
    {
        _tweenSequence = DOTween.Sequence();
    }

    public void PlayBeginning()
    {
        canvasGroup.DOFade(1, 1);
    }

    public void PlayEnd()
    {
        canvasGroup.DOFade(0, 1);
    }

}
