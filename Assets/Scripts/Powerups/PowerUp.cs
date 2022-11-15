using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool AmmoClipPickup;
    public bool HpIncreasePickup;
    public float HpIncrease;
    public bool HealPickup;
    public float HealPercent;

    private GameObject _turnControllerObject;
    private TurnController _turnController;
    private PowerupSpawner _powerupSpawner;
    private AudioSource _pickupSound;

    void Start()
    {
        _turnControllerObject = GameObject.Find("TurnController");
        _turnController=_turnControllerObject.GetComponent<TurnController>();

        _pickupSound=GameObject.Find("ItemPickup").GetComponent<AudioSource>();

        _powerupSpawner=GameObject.Find("Pickup spawner").GetComponent<PowerupSpawner>();
    }
     void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerObject")
        {
            if(AmmoClipPickup)
            {

            }
            if(HpIncreasePickup)
            {
                _turnController.increaseMaxHP(HpIncrease);
            }
            if(HealPickup)
            {
                _turnController.Heal(HealPercent);
            }
            _pickupSound.Play(0);
            Destroy(gameObject);
            _powerupSpawner.CurrentAmount--;
        }
    }
}
