using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
//Turn control
public float TurnTime;
public float MinTurnTime;
public float TimeReductionPerTurn;
public float CurrentTurn;
public bool TurnOver=false;
public Text CurrentTurnTimeText;
public GameObject RoundOverText;

//Player info
public CharacterController CharacterController;
public GameObject Player;
public GameObject PlayerEmptyBody;
public GameObject EmptybodyWeaponDestroy;
public GameObject EmptybodyWeaponBuild;

private bool _player1Turn;
private bool _player2Turn;
private Vector3 _tempPos;
private Quaternion _tempRot;
private Vector3 _tempPos2;
private Quaternion _tempRot2;


//Weapon Stats

public int Player1Ammo=4;
public int Player1Ammo2=5;
public int Player2Ammo=4;
public int Player2Ammo2=5;
public int Player1Activegun=1;
public int Player2Activegun=1;

//Player health 
public float MaxHp1=100f;
public float MaxHp2=100f;
public float Player1Hp;
public float Player2Hp;
public GameObject Player1HealthBar;
public GameObject Player2HealthBar;
public HealthBarScript Player1HealthBarScript;
public HealthBarScript Player2HealthBarScript;
public GameObject Player1HealthBarBackground;
public GameObject Player2HealthBarBackground;

//GameOver
public ChangeScene ChangeSceneScript;
public static int WinningPlayer;


    void Awake()
    {
        Player1Hp=MaxHp1;
        Player2Hp=MaxHp2;
        _player1Turn=true;
        _player2Turn=false;
        CurrentTurn=TurnTime;
    }
    

    public void SwapPlayer(int ammo, int ammo2, int activeWeapon, out int currentAmmo, out int currentAmmo2, out int currentWeapon)
    {
        if(TurnTime<=MinTurnTime)
        {

        }
        else
        {
            TurnTime-=TimeReductionPerTurn;
        }
        

        CurrentTurn=TurnTime;
        TurnOver=false;
        RoundOverText.SetActive(false);

        CharacterController.enabled = true;
        currentAmmo=-10;
        currentAmmo2=-10;
        currentWeapon=-10;

        if(activeWeapon==1)
        {
            EmptybodyWeaponDestroy.SetActive(false);
            EmptybodyWeaponBuild.SetActive(true);
        }
        else if(activeWeapon==2)
        {
            EmptybodyWeaponDestroy.SetActive(true);
            EmptybodyWeaponBuild.SetActive(false);
        }

        if(_player1Turn==true && _player2Turn==false)
        {

            _player1Turn=false;
            _player2Turn=true;
            //Swap healthbar pos
            Vector3 _tempPos;
            _tempPos=Player1HealthBar.transform.position;
            Player1HealthBar.transform.position=Player2HealthBar.transform.position;
            Player2HealthBar.transform.position=_tempPos;
            //Swap pos with dummy player
            _tempPos=Player.transform.position;
            _tempRot=Player.transform.rotation;

            _tempPos2=PlayerEmptyBody.transform.position;
            _tempRot2=PlayerEmptyBody.transform.rotation;

            CharacterController.enabled = false;
            Player.transform.position=_tempPos2;
            Player.transform.rotation=_tempRot2;
            CharacterController.enabled = true;

            PlayerEmptyBody.transform.position=_tempPos;
            PlayerEmptyBody.transform.rotation=_tempRot;


            //set ammo
            Player1Ammo=ammo;
            Player1Ammo2=ammo2;
            Player1Activegun=activeWeapon;
            
            currentAmmo=Player2Ammo;
            currentAmmo2=Player2Ammo2;
            currentWeapon=Player2Activegun;
        }
        else if(_player1Turn==false && _player2Turn==true)
        {
            
            _player1Turn=true;
            _player2Turn=false;
            //Swap healthbar pos
            Vector3 _tempPos;
            _tempPos=Player1HealthBar.transform.position;
            Player1HealthBar.transform.position=Player2HealthBar.transform.position;
            Player2HealthBar.transform.position=_tempPos;
            //Swap pos with dummy player
            _tempPos=Player.transform.position;
            _tempRot=Player.transform.rotation;

            _tempPos2=PlayerEmptyBody.transform.position;
            _tempRot2=PlayerEmptyBody.transform.rotation;

            CharacterController.enabled = false;
            Player.transform.position=_tempPos2;
            Player.transform.rotation=_tempRot2;
            CharacterController.enabled = true;

            PlayerEmptyBody.transform.position=_tempPos;
            PlayerEmptyBody.transform.rotation=_tempRot;

            //set ammo
            Player2Ammo=ammo;
            Player2Ammo2=ammo2;
            Player2Activegun=activeWeapon;

            currentAmmo=Player1Ammo;
            currentAmmo2=Player1Ammo2;
            currentWeapon=Player1Activegun;
        }
    }

    public void DamagePlayer(float damage)
    {
        if(_player1Turn==true)
        {
            Player1Hp-=damage;
            Player1HealthBarScript.SetHealth(Player1Hp);
        }
        else if(_player2Turn==true)
        {
            Player2Hp-=damage;
            Player2HealthBarScript.SetHealth(Player2Hp);
        }
        DamageCheck();
    }

    public void DamageInactivePlayer(float damage)
    {
        if(_player1Turn==false)
        {
            Player1Hp-=damage;
            Player1HealthBarScript.SetHealth(Player1Hp);
        }
        else if(_player2Turn==false)
        {
            Player2Hp-=damage;
            Player2HealthBarScript.SetHealth(Player2Hp);
        }
        DamageCheck();
    }
    public void increaseMaxHP(float hpIncrease)
    {
        if(_player1Turn==true)
        {
            MaxHp1+=hpIncrease;
            Player1Hp=MaxHp1;
            Player1HealthBarScript.SetMaxHealth(Player1Hp);
            Player1HealthBarScript.SetHealth(Player1Hp);
            var RectTransform = Player1HealthBarBackground.transform as RectTransform;
            RectTransform.sizeDelta = new Vector2 (RectTransform.sizeDelta.x+hpIncrease*1.5f, RectTransform.sizeDelta.y);
        }
        else if(_player2Turn==true)
        {
            MaxHp2+=hpIncrease;
            Player2Hp=MaxHp2;
            Player2HealthBarScript.SetMaxHealth(Player2Hp);
            Player2HealthBarScript.SetHealth(Player2Hp);
            var RectTransform = Player2HealthBarBackground.transform as RectTransform;
            RectTransform.sizeDelta = new Vector2 (RectTransform.sizeDelta.x+hpIncrease*1.5f, RectTransform.sizeDelta.y);
        }
    }

    public void Heal(float heal)
    {
        if(_player1Turn==true)
        {
            Player1Hp+=heal*MaxHp1;
            if(Player1Hp>MaxHp1)
            {
                Player1Hp=MaxHp1;
            }
            Player1HealthBarScript.SetHealth(Player1Hp);
        }
        else if(_player2Turn==true)
        {
            Player2Hp+=heal*MaxHp2;
            if(Player1Hp>MaxHp1)
            {
                Player2Hp=MaxHp2;
            }
            Player2HealthBarScript.SetHealth(Player2Hp);
        }
    }

    private void FreezePlayer()
    {
        CharacterController.enabled = false;
        TurnOver=true;
    }

    private void DamageCheck()
    {
        if(Player1Hp<=0)
        {
            GameOver(2);
        }
        else if(Player2Hp<=0)
        {
            GameOver(1);
        }
    }

    private void GameOver(int winner)
    {
        WinningPlayer=winner;
        ChangeSceneScript.MoveToScene(2);
    }

    void FixedUpdate()
    {
        if(CurrentTurn>0)
        {
            CurrentTurn-=Time.deltaTime;
        }
        else{
            CurrentTurn=0;
        }
        CurrentTurnTimeText.text=CurrentTurn.ToString("0.0");
        if(CurrentTurn<=0)
        {
            RoundOverText.SetActive(true);
            FreezePlayer();
        }
    }
}
