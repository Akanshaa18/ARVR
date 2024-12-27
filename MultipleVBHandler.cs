using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MultipleVBHandler : MonoBehaviour
{

    public GameObject apartment;
    public GameObject office;
    public GameObject warehouse;
    public GameObject store;

    VirtualButtonBehaviour[] virtualButtons;


    //public Material m_VirtualButtonDefault;
    // public Material m_VirtualButtonPressed;

    private void Awake()
    {
        virtualButtons = GetComponentsInChildren<VirtualButtonBehaviour>();

        for (int i = 0; i < virtualButtons.Length; ++i)
        {

            virtualButtons[i].RegisterOnButtonPressed(OnButtonPressed);
            //virtualButtons[i].RegisterOnButtonReleased(OnButtonReleased);
            //SetVirtualButtonMaterial(m_VirtualButtonDefault);

        }
    }


    void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        //SetVirtualButtonMaterial(m_VirtualButtonPressed);

        string vbName = vb.name;

        switch (vbName)
        {
            case "VirtualButton 1":
                Debug.Log(vbName + " Button Pressed");
                apartment.SetActive(true);
                office.SetActive(false);
                warehouse.SetActive(false);
                store.SetActive(false);
                break;

            case "VirtualButton 2":
                Debug.Log(vbName + " Button Pressed");
                apartment.SetActive(false);
                office.SetActive(true);
                warehouse.SetActive(false);
                store.SetActive(false);
                break;

            case "VirtualButton 3":
                Debug.Log(vbName + " Button Pressed");
                apartment.SetActive(false);
                office.SetActive(false);
                warehouse.SetActive(true);
                store.SetActive(false);
                break;

            case "VirtualButton 4":
                Debug.Log(vbName + " Button Pressed");
                apartment.SetActive(false);
                office.SetActive(false);
                warehouse.SetActive(false);
                store.SetActive(true);
                break;

            default:
                Debug.Log("No Button Pressed");
                break;
        }
    }

    void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        Debug.Log("OnButtonReleased: " + vb.VirtualButtonName);

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
