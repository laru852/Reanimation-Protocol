using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Image FillImage;
    public float CurrentHealth;

    public void UpdateHP()
    {
        FillImage.fillAmount = CurrentHealth / 100;
    }

}
