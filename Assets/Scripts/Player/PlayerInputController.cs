using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerInputController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] WeaponShoot weapon;
    [SerializeField] GameObject weaponDestroy;
    [SerializeField] WeaponShoot weaponBuildScript;
    [SerializeField] GameObject weaponBuild;
    [SerializeField] TurnController turnController;

    public float MoveSpeed;
    public float SprintSpeed;
    public Vector2 MoveValue;
    public float JumpPower;
    public AudioSource WeaponSwapSound;
    public float RotateSpeedX;
    public float RotateSpeedY;

    private CharacterController _characterController;
    private bool _isMoving;
    private float _walkspeed;
    private bool _sprint=false;
    private int _activeWeapon = 1;
    private int _weaponamount = 2;
    private float gravity = 0;
    private bool _jumping;
    private GameObject _changeSceneObject;
    private ChangeScene _changeSceneScript;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _walkspeed=MoveSpeed;
        weaponDestroy.SetActive(true);
        weaponBuild.SetActive(false);
        weaponDestroy.SetActive(false);
        weaponBuild.SetActive(true);

        _changeSceneObject = GameObject.Find("ChangeScene");
        _changeSceneScript=_changeSceneObject.GetComponent<ChangeScene>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        //Debug.Log("Move");
        MoveValue = context.ReadValue<Vector2>();

        //Animation Logic

        if(MoveValue.x>=0.8f)
        {
            anim.Play("StrafeRight");
        }
        else if(MoveValue.x<=-0.8f)
        {
            anim.Play("StrafeLeft");
        }
        else if(MoveValue.y>=0.8f)
        {
            anim.Play("RunForward");
        }
        else if(MoveValue.y<=-0.8f)
        {
            anim.Play("RunBackward");
        }
        else if(MoveValue.x>=0.5f && MoveValue.y>=0.5f)
        {
            anim.Play("RunLeft");
        }
        else if(MoveValue.x>=0.5f && MoveValue.y<=-0.5f)
        {
            anim.Play("RunBackwardRight");
        }
        else if(MoveValue.x<=-0.5f && MoveValue.y>=0.5f)
        {
            anim.Play("RunRight");
        }
        else if(MoveValue.x<=-0.5f && MoveValue.y<=-0.5f)
        {
            anim.Play("RunBackwardLeft");
        }
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            return;
        }
        if(turnController.TurnOver==false)
        {
            if(_activeWeapon==1)
        {
            weaponBuildScript.Shoot();
        }
        else if(_activeWeapon==2)
        {
            weapon.Shoot();
        }
        
        }
        
    }

    public void Reload(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            return;
        }

        if(turnController.TurnOver==false)
        {
        if(_activeWeapon==1)
        {
            weaponBuildScript.ReloadWeapon();
        }
        else if(_activeWeapon==2)
        {
            weapon.ReloadWeapon();
        }
        }
        
    }

    public void CameraMove(InputAction.CallbackContext context)
    {
        Vector2 _lookInput=context.ReadValue<Vector2>();
        //followCamera.CameraRotate(_lookInput.x, _lookInput.y);
        float RotationX = RotateSpeedX * _lookInput.x * Time.deltaTime;
        float RotationY = RotateSpeedY * _lookInput.y * Time.deltaTime;

        Vector3 CameraRotation = transform.rotation.eulerAngles;
        CameraRotation.x -= RotationY;
        CameraRotation.y += RotationX;


        transform.rotation = Quaternion.Euler(CameraRotation);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        _jumping=true;
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        _sprint=true;
    }

    public void ChangeWeaponPlus(InputAction.CallbackContext context)
    {
        if(weapon.Reload==false&&weaponBuildScript.Reload==false&&turnController.TurnOver==false)
        {
            _activeWeapon++;
            if(_activeWeapon>_weaponamount)
            {
                _activeWeapon=1;
            }
                WeaponCheck();
                WeaponSwapSound.Play();
        }
        
    }

    public void ChangeWeaponMinus(InputAction.CallbackContext context)
    {
        if(weapon.Reload==false&&weaponBuildScript.Reload==false&&turnController.TurnOver==false)
        {
            _activeWeapon--;
            if(_activeWeapon<_weaponamount)
            {
                _activeWeapon=1;
            }
            WeaponCheck();
            WeaponSwapSound.Play();
        }

    }

    private void WeaponCheck()
    {
        if(_activeWeapon==1)
        {
            weaponDestroy.SetActive(false);
            weaponBuild.SetActive(true);
        }
        else if(_activeWeapon==2)
        {
            weaponDestroy.SetActive(true);
            weaponBuild.SetActive(false);
        }
    }

    public void EndTurn(InputAction.CallbackContext context)
    {
        turnController.SwapPlayer(weapon.CurrentAmmo,weaponBuildScript.CurrentAmmo, _activeWeapon, out int currentAmmo, out int currentAmmo2, out int currentWeapon);
        weapon.CurrentAmmo=currentAmmo;
        weaponBuildScript.CurrentAmmo=currentAmmo2;
        _activeWeapon=currentWeapon;
        WeaponCheck();
    }

    public void BackToMenu(InputAction.CallbackContext context)
    {
        Cursor.lockState = CursorLockMode.None;
        _changeSceneScript.MoveToScene(0);
    }

   

    private void FixedUpdate()
    {
        //Gravity 
        
        gravity -= 2 * Time.deltaTime;
        if ( _characterController.isGrounded )
        {
            
            if(_jumping==true)
            {
                gravity=0;
                gravity+=JumpPower;
                anim.Play("Jump");
            }
        }
        _jumping=false;

        if(_sprint==true)
        {
            MoveSpeed=SprintSpeed;
        }
        else
        {
            MoveSpeed=_walkspeed;
        }

        var moveVector = new Vector3(MoveValue.x*MoveSpeed,0,MoveValue.y*MoveSpeed);

        if(MoveValue.x==0 && MoveValue.y==0)
        {
            anim.SetBool("IsMoving",false);
            _sprint=false;
        }
        else
        {
            anim.SetBool("IsMoving",true);
        }

        var moveDirection = transform.TransformDirection(moveVector);
        moveDirection.y=gravity;
        _characterController.Move(moveDirection);
        
    }
}
