using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicBarBehaviour : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    public void SetMaxMagic(int maxMagic)
    {
        slider.maxValue = maxMagic;
        slider.value = maxMagic;
    }

    public void SetMagic(int magic)
    {
        slider.value = magic;

        fill.color = Color.magenta;
    }
}
