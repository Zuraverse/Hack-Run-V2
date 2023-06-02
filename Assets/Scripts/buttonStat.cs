using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonStat : MonoBehaviour
{
    public static bool isActive = true; // Static variable to store the state of the button v
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(buttonStat.isActive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetButtonActiveState(bool state)
    {
        buttonStat.isActive = state; // Set the active state of the button
    }

    public void SetButtonActiveStateOne(bool state)
    {
        buttonStat.isActive = state; // Set the active state of the button
    }
}
