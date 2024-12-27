using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateObjects : MonoBehaviour
{
    public float xAxis, yAxis, zAxis;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(xAxis, yAxis, zAxis) * Time.deltaTime);
    }
}
