using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CubeEventHandler : MonoBehaviour
{
    // Gameobject that you want to control using Virtual Button
    public GameObject cube;

    #region PUBLIC_MEMBERS
   // 
   public Material m_VirtualButtonDefault; // Opaque
   public Material m_VirtualButtonPressed; // Transparent

    public float m_ButtonReleaseTimeDelay; // 
    #endregion // PUBLIC_MEMBERS

    #region PRIVATE_MEMBERS
    // Array to store all VirtualButtonBehaviours
    VirtualButtonBehaviour[] virtualButtonBehaviours;
    #endregion // PRIVATE_MEMBERS

    #region MONOBEHAVIOUR_METHODS
    void Awake()
    {
        // Register with the virtual buttons TrackableBehaviour
        virtualButtonBehaviours = GetComponentsInChildren<VirtualButtonBehaviour>();

        for (int i = 0; i < virtualButtonBehaviours.Length; ++i)
        {
            virtualButtonBehaviours[i].RegisterOnButtonPressed(OnButtonPressed);
            virtualButtonBehaviours[i].RegisterOnButtonReleased(OnButtonReleased);
        }
    }

    void Destroy()
    {
        // Register with the virtual buttons TrackableBehaviour
        virtualButtonBehaviours = GetComponentsInChildren<VirtualButtonBehaviour>();

        for (int i = 0; i < virtualButtonBehaviours.Length; ++i)
        {
            virtualButtonBehaviours[i].UnregisterOnButtonPressed(OnButtonPressed);
            virtualButtonBehaviours[i].UnregisterOnButtonReleased(OnButtonReleased);
        }
    }



    #endregion // MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS
    /// <summary>
    /// Called when the virtual button has just been pressed:
    /// </summary>
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        //cube.SetActive(true);
        cube.GetComponent<Renderer>().material.color = Color.red;
        cube.GetComponent<Transform>().transform.Rotate(0, 45f, 0);

        Debug.Log("OnButtonPressed: " + vb.VirtualButtonName);

        SetVirtualButtonMaterial(m_VirtualButtonPressed);

        StopAllCoroutines();

        BroadcastMessage("HandleVirtualButtonPressed", SendMessageOptions.DontRequireReceiver);
    }

    /// <summary>
    /// Called when the virtual button has just been released:
    /// </summary>
    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        cube.GetComponent<Renderer>().material.color = Color.green;
        cube.GetComponent<Transform>().transform.Rotate(0, 90f, 0);

        Debug.Log("OnButtonReleased: " + vb.VirtualButtonName);

        SetVirtualButtonMaterial(m_VirtualButtonDefault);

        StartCoroutine(DelayOnButtonReleasedEvent(m_ButtonReleaseTimeDelay, vb.VirtualButtonName));
    }
    #endregion //PUBLIC_METHODS


    #region PRIVATE_METHODS
    void SetVirtualButtonMaterial(Material material)
    {
        // Set the Virtual Button material
        for (int i = 0; i < virtualButtonBehaviours.Length; ++i)
        {
            if (material != null)
            {
                virtualButtonBehaviours[i].GetComponent<MeshRenderer>().sharedMaterial = material;
            }
        }
    }

    IEnumerator DelayOnButtonReleasedEvent(float waitTime, string buttonName)
    {
        yield return new WaitForSeconds(waitTime);

        BroadcastMessage("HandleVirtualButtonReleased", SendMessageOptions.DontRequireReceiver);
    }
    #endregion // PRIVATE METHODS
}


