using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MySolarSystem : MonoBehaviour
{
    public GameObject[] planets; // array to store all objects (A-Z)

    // Buttons
    public Button prevBtn;
    public Button nextBtn;

    // Gameobject states
    GameObject currentPlanet;
    GameObject nextPlanet;
    GameObject previousPlanet;

    //TextMesh
    public TextMeshProUGUI currentPlanetName;

    int i = 0;

    void Start()
    {

        currentPlanet = planets[i];  // planets[0] --> Apple
        currentPlanet.SetActive(true); // Apple will be enabled
        currentPlanetName.text = currentPlanet.name; // Apple's name
        prevBtn.interactable = false; // Previous is not active
    }

    private void Update()
    {

    }

    // NextButton Method
    public void NextButton()
    {
        prevBtn.interactable = true;
        i = i + 1;
        if (i == planets.Length - 1)
        {
            nextBtn.interactable = false;
        }
        nextPlanet = planets[i]; // store next alphabet in the arry to nextPlanet
        currentPlanet.SetActive(false); // Disable currentPlanet in the Scene
        currentPlanet = nextPlanet; // Assign nextPlanet to currentPlanet
        currentPlanet.SetActive(true);  // Enable currentPlanet in the Scene   
        currentPlanetName.text = currentPlanet.name;

    }

    // PreviousButton Method
    public void PrevButton()
    {
        nextBtn.interactable = true;
        i = i - 1;
        if (i == 0)
        {
            prevBtn.interactable = false;
        }
        previousPlanet = planets[i];
        currentPlanet.SetActive(false);
        currentPlanet = previousPlanet; // Assign previousPlanet to currentPlanet
        currentPlanet.SetActive(true);  // Enable currentPlanet in the Scene   
        currentPlanetName.text = currentPlanet.name;

    }

  

}
