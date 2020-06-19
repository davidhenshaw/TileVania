using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameUIElement : MonoBehaviour
{
    [SerializeField] TMP_Text _textField;
    [SerializeField] IOneShotTween _tween;


    private void Awake()
    {
        _tween = GetComponentInChildren<IOneShotTween>();
    }

    public void UpdateValue(object newValue)
    {
        _textField.text = newValue.ToString();
    }

    public void ActivateOneShot()
    {
        _tween.Activate();
    }
}
