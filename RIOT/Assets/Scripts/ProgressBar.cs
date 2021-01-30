using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float max;
    public float curr;
    public Image fill;
    public Color color;

    // Start is called before the first frame update
    void Start()
    {
        fill.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        float fillAmount = curr / max;
        fill.fillAmount = fillAmount;
    }
}
