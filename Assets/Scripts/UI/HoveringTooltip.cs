using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringTooltip : MonoBehaviour, IContextDisplay
{
    [SerializeField] GameObject toDisplay;

    // Start is called before the first frame update
    void Start()
    {
        toDisplay.SetActive(false);   
    }

    public void Activate()
    {
        toDisplay.SetActive(true);
    }

    public void Deactivate()
    {
        toDisplay.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if(player)
        {
            Activate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player)
        {
            Deactivate();
        }
    }
}
