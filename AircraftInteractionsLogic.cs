using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AircraftInteractionsLogic : MonoBehaviour
{
    // Get Reference for Button and Text
    string DefaultButtonName;  // Body Up
    public string ChangeButtonName;  // Body Down
    public Button button;
    bool ChangeState;
    public string UpTrigger;
    public string DownTrigger;


    // Start is called before the first frame update
    void Start()
    {
        DefaultButtonName = button.GetComponentInChildren<Text>().text;
    }

    public void ChangeOnClick()
    {
        if (ChangeState == false)
        {
            MoveUpAnimation();
        }
        else
        {
            MoveDownAnimation();
        }
    }

    void MoveUpAnimation()
    {
        button.GetComponentInChildren<Text>().text = ChangeButtonName;
        GetComponent<Animator>().SetTrigger(UpTrigger);
        ChangeState = true;
    }

    void MoveDownAnimation()
    {
        button.GetComponentInChildren<Text>().text = DefaultButtonName;
        GetComponent<Animator>().SetTrigger(DownTrigger);
        ChangeState = false;

    }
}
