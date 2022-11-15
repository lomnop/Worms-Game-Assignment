using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarScript : MonoBehaviour
{
    [SerializeField] TurnController turnController;
    public Slider HealthSlider;
    
    private void Awake()
        {
            HealthSlider.maxValue=turnController.Player1Hp;
            HealthSlider.value=HealthSlider.maxValue;
        }
 
    public void SetHealth(float health)
    {
        HealthSlider.value=health;
    }

    public void SetMaxHealth(float health)
    {
        HealthSlider.maxValue=health;
    }
}
