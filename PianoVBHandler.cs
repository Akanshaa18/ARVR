using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PianoVBHandler : MonoBehaviour
{

    // To get and store all VirtualButtonBehaviours in an array
    VirtualButtonBehaviour[] virtualButtons;

    // to get all Virtual Button gameobjects from Hierarchy and store them in another array
    // C1, A, D, B, et.,
    GameObject[] pianoKeys;


    //public Material m_VirtualButtonDefault;
    // public Material m_VirtualButtonPressed;

    private void Awake()
    {
        // Store all Piano Keys from Hierarchy.
        pianoKeys = GameObject.FindGameObjectsWithTag("Keys");


        virtualButtons = GetComponentsInChildren<VirtualButtonBehaviour>();

        for (int i = 0; i < virtualButtons.Length; ++i)
        {

            virtualButtons[i].RegisterOnButtonPressed(OnButtonPressed);
            virtualButtons[i].RegisterOnButtonReleased(OnButtonReleased);
            //SetVirtualButtonMaterial(m_VirtualButtonDefault);

        }

       
       
    }

    void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        //SetVirtualButtonMaterial(m_VirtualButtonPressed);

        string vbName = vb.name;

        foreach(var v in virtualButtons)
        {
            for(int i=0; i<pianoKeys.Length; i++)
            {
                if(vbName == pianoKeys[i].name)
                {
                    pianoKeys[i].GetComponent<AudioSource>().Play();
                }
            }
        }
    }

    void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        Debug.Log("OnButtonReleased: " + vb.VirtualButtonName);

        string vbName = vb.name;

        foreach (var v in virtualButtons)
        {
            for (int i = 0; i < pianoKeys.Length; i++)
            {
                if (vbName == pianoKeys[i].name)
                {
                    pianoKeys[i].GetComponent<AudioSource>().Stop();
                }
            }
        }

        //SetVirtualButtonMaterial(m_VirtualButtonDefault);
    }

    void Destroy()
    {
        // Register with the virtual buttons TrackableBehaviour

        virtualButtons = GetComponentsInChildren<VirtualButtonBehaviour>();

        for (int i = 0; i < virtualButtons.Length; ++i)
        {
            virtualButtons[i].UnregisterOnButtonPressed(OnButtonPressed);
            // virtualButtons[i].UnregisterOnButtonReleased(OnButtonReleased);
        }

    }

    void SetVirtualButtonMaterial(Material material)
    {
        // Set the Virtual Button material

        for (int i = 0; i < virtualButtons.Length; ++i)
        {
            if (material != null)
            {
                virtualButtons[i].GetComponent<MeshRenderer>().sharedMaterial = material;
            }
        }


    }
}
