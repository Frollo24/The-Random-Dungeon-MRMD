using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicBarBehaviour : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    int maxMagic, magic;

    public void setMaxMagic(int maxMagic)
    {
        slider.maxValue = maxMagic;
        slider.value = maxMagic;

        fill.color = Color.magenta;
    }

    public void setMagic(int magic)
    {
        slider.value = magic;

        fill.color = Color.magenta;
    }
}
