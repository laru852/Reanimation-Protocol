using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Image FillImage;
    public Slider slider;
    public CatKnight player;
    void Awake()
    {

    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = player.maxHealth;
        slider.value = player.currentHealth;
    }

    public void SetHealth(int currentHealth)
    {
        slider.value = player.currentHealth;
    }
}
