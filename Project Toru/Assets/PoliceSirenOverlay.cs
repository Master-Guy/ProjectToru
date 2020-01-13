using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoliceSirenOverlay : MonoBehaviour
{
    public Image policeSiren;

    public float r;
    public float g;
    public float b;
    public float a;

    public bool flipBool = false;

    private void Start()
    {
        r = policeSiren.color.r;
        g = policeSiren.color.g;
        b = policeSiren.color.b;
        a = policeSiren.color.a;
    }

    private void Update()
    {
        AutomateSiren();
    }

    public void AutomateSiren()
    {
        if ((int)Time.timeSinceLevelLoad % 2 == 0)
        {
            a -= 0.1f;
            flipImage();
        }
        else
        {
            a += 0.1f;
        }
        a = Mathf.Clamp(a, 0, 1f);
        AdjustColor();
    }

    public void flipImage()
    {
        if(flipBool == false)
        {
            policeSiren.transform.localRotation = Quaternion.Euler(0, 0, 0);
            flipBool = true;
        } else
        {
            policeSiren.transform.localRotation = Quaternion.Euler(0, 180, 0);
            flipBool = false;
        }

    }

    public void AdjustColor()
    {
        Color c = new Color(r, g, b, a);
        GetComponent<Image>().color = c;
    }
}
