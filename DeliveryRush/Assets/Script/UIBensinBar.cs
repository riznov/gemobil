using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBensinBar : MonoBehaviour
{
    public Slider bensin_bar;
    public Gradient grad;
    public Image fill;

    private float maxHealt = 500f;

    public void SetHealth(float healt)
    {
        bensin_bar.value = healt/maxHealt;
        fill.color = grad.Evaluate(bensin_bar.normalizedValue);
    }
}
