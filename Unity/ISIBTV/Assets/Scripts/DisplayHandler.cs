using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayHandler : MonoBehaviour
{
    public GameObject screen;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, true, 60);
        screen.transform.Rotate(0f,0f,-90f, Space.Self);
    }
}
