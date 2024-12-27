using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // the cube will move 1 meter in Forward direction   
        //transform.Translate(new Vector3(0f, 0f, 1));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0f, 0f, 1) * Time.deltaTime);
    }
}
