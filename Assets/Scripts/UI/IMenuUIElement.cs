using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMenuUIElement 
{
    void Highlight();
    void Unhighlight();
    void OnSelectDown();
    void OnSelectUp();
}
