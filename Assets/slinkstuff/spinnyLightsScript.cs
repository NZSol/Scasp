using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinnyLightsScript : MonoBehaviour
{
    [SerializeField] float spinSpeed;
    [SerializeField] Color[] playerLightColours;
    [SerializeField] Color redAlertColour;
    [SerializeField] Light[] spinnyLights;
    [SerializeField] GameObject normalLight, spinnyLightObject;
    // Update is called once per frame
    void Update()
    {
        spinnyLightObject.transform.RotateAround(spinnyLightObject.transform.position, Vector3.up, spinSpeed * Time.deltaTime);
    }

    public void activateSpinnyLights(CharColours character, int seconds)
    {
        StartCoroutine(flashSpinnyLights(character, seconds));
    }

    IEnumerator flashSpinnyLights(CharColours character, int seconds)
    {
        Debug.Log("PLAYER " + (int)character + " FIRED THE GUN");
        normalLight.SetActive(false);
        spinnyLightObject.SetActive(true);
        foreach(Light currentLight in spinnyLights)
        {
            currentLight.color = playerLightColours[(int)character - 1];
        }
        yield return new WaitForSeconds(seconds);
        spinnyLightObject.SetActive(false);
        yield return new WaitForSeconds(Random.Range(.5f, 1.5f));
        normalLight.SetActive(true);
    }
}
