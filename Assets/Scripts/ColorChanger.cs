using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public Image image;
    public Color startColor = Color.green;
    public Color endColor = Color.blue;
    public float duration = 1f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        float t = Mathf.PingPong(timer, duration) / duration;
        image.color = Color.Lerp(startColor, endColor, t);
    }
}
