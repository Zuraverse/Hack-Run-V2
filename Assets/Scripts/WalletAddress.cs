using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalletAddress : MonoBehaviour
{
    public Text WalletAddresss;
    // Start is called before the first frame update
    void Start()
    {
        WalletAddresss.text = PlayerPrefs.GetString("WalletAddress");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
