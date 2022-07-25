using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialTogglerScript : MonoBehaviour
{
    MultiplayerHandler mpHandler;
    [SerializeField] GameObject tutorialText, toggleText;
    bool toggled;

    // Start is called before the first frame update
    void Start()
    {
        mpHandler = GetComponent<MultiplayerHandler>();
    }

    public void toggleTutorialTexts()
    {
        if (!mpHandler.gameStarted)
        {
            if (!toggled)
            {
                tutorialText.SetActive(true);
                toggleText.SetActive(false);
                toggled = true;
            }
            else
            {
                tutorialText.SetActive(false);
                toggleText.SetActive(true);
                toggled = false;
            }
        }
    }

    public void turnTutorialTextOff()
    {
            tutorialText.SetActive(false);
            toggleText.SetActive(false);
    }


}
