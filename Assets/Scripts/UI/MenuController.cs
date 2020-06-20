using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuSelector : MonoBehaviour
{
    IMenuUIElement[] _elements;
    IPlayerInput _input;
    int currentElement;

    private void Awake()
    {

    }

    private void Start()
    {
        IMenuUIElement[] _elements = GetComponentsInChildren<IMenuUIElement>();
        if(_elements == null)
        {
            Destroy(this);
            return;
        }

        _elements[0].Highlight();
    }

    private void Update()
    {
        if (_input.Horizontal > Mathf.Epsilon)
            HighlightNextHorizontalElement();

        if (_input.Horizontal < Mathf.Epsilon)
            HighlightPreviousHorizontalElement();
    }

    void HighlightPreviousHorizontalElement()
    {
        if (currentElement <= 0)
            return;

        _elements[currentElement].Unhighlight();

        currentElement--;

        _elements[currentElement].Highlight();
    }

    void HighlightNextHorizontalElement()
    {
        if (currentElement >= _elements.Length - 1)
            return;

        _elements[currentElement].Unhighlight();

        currentElement++;

        _elements[currentElement].Highlight();
    }
}

public class ButtonElement : MonoBehaviour, IMenuUIElement
{
    UnityEngine.UIElements.VisualElement visualElement;

    public void Highlight()
    {
        throw new System.NotImplementedException();
    }

    public void OnSelectDown()
    {
        throw new System.NotImplementedException();
    }

    public void OnSelectUp()
    {
        throw new System.NotImplementedException();
    }

    public void Unhighlight()
    {
        throw new System.NotImplementedException();
    }
}

public class MenuController : MonoBehaviour
{
    [SerializeField] string firstLevelName;

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(firstLevelName);
    }
}
