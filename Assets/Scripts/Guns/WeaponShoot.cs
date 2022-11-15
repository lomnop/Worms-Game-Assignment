using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShoot : MonoBehaviour
{
    [SerializeField] Rigidbody _bulletPrefab;
    [SerializeField] Transform _spawnpoint;
    [SerializeField] float _speed;
    [SerializeField] Transform _player;
    [SerializeField] float _cooldown;
    [SerializeField] AudioSource _reloadSound;
    [SerializeField] float _reloadTime;
    [SerializeField] AudioSource _shootSound;

    public int ClipSize;
    public int CurrentAmmo;
    public Text CurrentAmmoText;
    public bool Reload;

    private float _cooldowntime=0;
    private float _reloadTimer;

    public void Awake()
    {
        CurrentAmmo=ClipSize;
    }
    public void Shoot()
    {
        if(_cooldowntime<=0 && CurrentAmmo>0 &&Reload==false)
        {
        var bullet = Instantiate<Rigidbody>(_bulletPrefab,_spawnpoint.position,Quaternion.identity);
        var angle = Mathf.Abs(90-transform.localEulerAngles.x)*1/90;
        var direction = Vector3.up*angle+Vector3.forward;
        var ShootDirection = _player.TransformDirection(direction);
        bullet.AddForce(ShootDirection*_speed,ForceMode.Impulse);
        _cooldowntime=_cooldown;
        CurrentAmmo--;
        _shootSound.Play();
        }
    }

    public void ReloadWeapon()
    {
        if(Reload==false)
        {
            Reload=true;
            _reloadTimer=_reloadTime;
            _reloadSound.Play();
        }
    }
    public void FixedUpdate()
    {
        _cooldowntime-=Time.deltaTime;
        _reloadTimer-=Time.deltaTime;
        CurrentAmmoText.text=CurrentAmmo.ToString();

        //reload
        if(Reload==true&&_reloadTimer<=0)
        {
            CurrentAmmo=ClipSize;
            Reload=false;
        }
        
    }
}
