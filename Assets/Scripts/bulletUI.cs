using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class bulletUI : MonoBehaviour
{
    private GameObject player;
    public Image[] bulletImages;

    void Start()
    {
        for(int i = 0; i < bulletImages.Length; i++)
        {
            bulletImages[i].enabled = false;
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void UpdateBullet(int currentBullet)
    {
        for (int i = 0; i < bulletImages.Length; i++)
        {
            Debug.Log(currentBullet);
            if (i < currentBullet)
            {
                bulletImages[i].enabled = true;
            }
            if(i >= currentBullet)
            {
                bulletImages[i].enabled = false;
            }
        }
    }

}

