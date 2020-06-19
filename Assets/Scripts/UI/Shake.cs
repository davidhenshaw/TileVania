using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shake : MonoBehaviour, IOneShotTween
{
    private Transform myTransform;
    [SerializeField] float duration = 0.5f;
    [SerializeField] float intensity = 5;
    [SerializeField] int vibratoAmount = 15;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
    }

    public void Activate()
    {
        myTransform.DOShakePosition(duration, intensity, vibratoAmount, 0, false, true);
    }

}
