using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundHandler : MonoBehaviour
{

    [SerializeField] int timeLimit;
    [HideInInspector] public int currentTime;
    [SerializeField] Text timerText;
    [SerializeField] GameObject congratulation, timerObject, helpText;
    MultiplayerHandler MPHandler;

    private void Start()
    {
        MPHandler = GetComponent<MultiplayerHandler>();
    }

    public void startGame()
    {
        if(MPHandler.Players.Count > 0)
        {
            StartCoroutine(timerCoroutine());
            GetComponent<PlayerInputManager>().DisableJoining();
            MPHandler.gameStarted = true;
            helpText.SetActive(false);
            timerObject.SetActive(true);
        }
    }

    IEnumerator timerCoroutine()
    {
        currentTime = timeLimit;
        while(currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            currentTime--;
            timerText.text = Mathf.FloorToInt(currentTime / 60) + ":" + Mathf.Floor(currentTime % 60);
        }
        endGame();
    }

    void endGame()
    {
        congratulation.SetActive(true);
        Debug.Log("CONGRATULATION");
        Invoke("reloadScene", 3);
    }

    void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
