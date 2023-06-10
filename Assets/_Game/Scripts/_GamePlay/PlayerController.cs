using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private Character character;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Rigidbody rigibody;
    [SerializeField] private bool EnableMove = true;
    public Vector3 offsetCameraBeforeCollectGift;
    public Vector3 offsetCameraBeforeDead;
    public bool AttackEnd = true;

    public int numberKillBot = 0;

    public int rankPlayer;

    public int coin = 0;

    public int startingCoin; // Số vàng khởi đầu

    public const string goldKey = "Gold"; // Khóa lưu trữ vàng

    private Vector3 moveVector;

    private void Awake() 
    {
        for(int i = 0; i < 9; i++)
        {
            if(weaponData.GetWeapon((WeaponType)i).IsEquipped)
            {
                LevelManager.Ins.player.weaponType = (WeaponType)i;
                SetUpWeaponAndHairIndicator();
                break;
            }
        }
        for(int i = 0; i < 11; i ++)
        {
            if(hairData.GetHair((HairsType)i).IsEquipped)
            {
                LevelManager.Ins.player.hairsType = (HairsType)i;
                SetUpWeaponAndHairIndicator();
                break;
            }
        }
        for(int i = 0; i < 10; i ++)
        {
            if(pantsData.GetPants((PantsType)i).IsEquipped)
            {
                LevelManager.Ins.player.pantsType = (PantsType)i;
                SetUpPantIndicator();
                break;
            }
        }
        for(int i = 0; i < 2; i ++)
        {
            if(supportItemData.GetSupportItem((SupportsType)i).IsEquipped)
            {
                LevelManager.Ins.player.supportsType = (SupportsType)i;
                SetUpSupportItemIndicator();
                break;
            }
        }
        for(int i = 0; i < 3; i ++)
        {
            if(suitData.GetSuit((SuitType)i).IsEquipped)
            {
                LevelManager.Ins.player.suitType = (SuitType)i;
                break;
            }
        }
    }
    
    public override void Start() 
    {
        base.Start();
        // Kiểm tra xem vàng đã được lưu trữ trước đó chưa
        if (PlayerPrefs.HasKey(goldKey))
        {
            // Nếu có, lấy giá trị vàng đã lưu trữ
            coin = PlayerPrefs.GetInt(goldKey);
        }
        else
        {
            // Nếu chưa, sử dụng giá trị vàng khởi đầu
            coin = startingCoin;
        }
    }

    private void Update() 
    {
        if(character.currentState != null)
        {
            if(GameManager.Ins.IsState(GameState.Pause))
            {
                ChangeState(new IdleState());
            }
            character.currentState.OnExecute(this);
        }  
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if(isDead)
        {
            rigibody.velocity = Vector3.zero;
            return;
        } 
        
        Move();
    }

    private void Move()
    {
        if(!GameManager.Ins.IsState(GameState.Gameplay)) return;
        moveVector = Vector3.zero;
        moveVector.x = joystick.Horizontal * moveSpeed * Time.fixedDeltaTime;
        moveVector.z = joystick.Vertical * moveSpeed * Time.fixedDeltaTime;

        if((joystick.Horizontal != 0 || joystick.Vertical != 0) && !isThrow)
        {
            Vector3 direction = Vector3.RotateTowards(TF.forward, moveVector, rotateSpeed * Time.fixedDeltaTime, 0.0f);
            TF.rotation = Quaternion.LookRotation(direction);
            rigibody.velocity = TF.forward * moveSpeed;
            ChangeState(new PatrolState());
        }
        else if(joystick.Horizontal == 0 && joystick.Vertical == 0 && AttackEnd)
        {
            rigibody.velocity = Vector3.zero;
            ChangeState(new IdleState());
        }
        
    }

    public override void OnInit()
    {
        LevelCharacter = 1;
        sizeCharacter = 1;
        sizeRing = 1;
        numberKillBot = 0;
        base.OnInit();
        EnableMove = true;
        AttackEnd = true;
        TF.position = Vector3.up * 1.5f;
        TF.rotation = Quaternion.Euler(0f,0f,0f);
        offsetCameraBeforeCollectGift = Vector3.zero;
        offsetCameraBeforeDead = Vector3.zero;
    }

    public void OnInitRevive()
    {
        if(isUlti)
        {
            attackRange.TF.localScale = sizeRingBeforeCollectGift;
            CameraFollow.Ins.offset = offsetCameraBeforeCollectGift;
        }
        else
        {
            CameraFollow.Ins.offset = offsetCameraBeforeDead;
        }
        currentAnimName = "Idle";
        isIdle = false;
        isPatrol = false;
        isAttack = false;
        isDead = false;
        isDance = false;
        isThrow = false;
        isUlti = false;
        timerAttack = 0f;
        characterList.Clear();
        SetMissionWayPoint();
        SetColorMissionWayPoint();
        boxCollider.enabled = true;
        EnableMove = true;
        AttackEnd = true;
        LevelManager.Ins.maxBot++;
        LevelManager.Ins.currentBot++;
        offsetCameraBeforeCollectGift = Vector3.zero;
        offsetCameraBeforeDead = Vector3.zero;
    }

    //IDLE
    public override void OnIdleEnter()
    {
        base.OnIdleEnter();
    }

    public override void OnIdleExecute()
    {
        base.OnIdleExecute();
        if (characterList.Count > 0)
        {
            if(!isUlti) ChangeState(new AttackState());
            else ChangeState(new Ulti());
        }
    }

    public override void OnIdleExit()
    {
        base.OnIdleExit();
    }


    //PATROL
    public override void OnPatrolEnter()
    {
        base.OnPatrolEnter();
    }

    public override void OnPatrolExecute()
    {
        base.OnPatrolExecute();
    }

    public override void OnPatrolExit()
    {
        base.OnPatrolExit();
        
    }


    //ATTACK
    public override void OnAttackEnter()
    {
        base.OnAttackEnter();
        WeaponModel.gameObject.SetActive(false);
        AttackEnd = false;
        CountThrow = 0;
    }

    public override void OnAttackExecute()
    {
        if (CountThrow == 0)
        {
            base.OnAttackExecute();
            ChangeAnim(Constant.ANIM_ATTACK);
            CountThrow++;
        }

        if(timerAttack >= (0.3f - (float)LevelManager.Ins.player.weaponData.GetWeapon(LevelManager.Ins.player.weaponType).attackSpeed / 10) * durationAttack && CountThrow == 1)
        {
            isThrow = true;
            ThrowWeapon();
            isThrow = false;
            CountThrow++;
        }

        if (timerAttack >= durationAttack) 
        {
            ChangeState(new IdleState());
        }

        timerAttack += Time.deltaTime; // cộng thêm thời gian đã trôi qua
    }

    public override void OnAttackExit()
    {
        base.OnAttackExit();
        isThrow = false;
        AttackEnd = true;
        timerAttack = 0f;
        WeaponModel.gameObject.SetActive(true);
    }

    public override void OnUltiEnter()
    {
        base.OnUltiEnter();
        WeaponModel.gameObject.SetActive(false);
        AttackEnd = false;
        CountThrow = 0;
    }

    public override void OnUltiExecute()
    {
        if (CountThrow == 0)
        {
            base.OnUltiExecute();
            ChangeAnim(Constant.ANIM_ULTI);
            CountThrow++;
        }

        if(timerAttack >= (0.3f - (float)LevelManager.Ins.player.weaponData.GetWeapon(LevelManager.Ins.player.weaponType).attackSpeed / 10) * durationUlti && CountThrow == 1)
        {
            isThrow = true;
            ThrowWeapon();
            isUlti = false;
            
            CountThrow++;
            isThrow = false;
        }

        if (timerAttack >= durationUlti) 
        {
            
            ChangeState(new IdleState());
        }

        timerAttack += Time.deltaTime; // cộng thêm thời gian đã trôi qua
    }

    public override void OnUltiExit()
    {
        base.OnUltiExit();
        isThrow = false;
        AttackEnd = true;
        timerAttack = 0f;
        WeaponModel.gameObject.SetActive(true);
    }

    public override void OnDeadEnter()
    {
        base.OnDeadEnter();
        rankPlayer = LevelManager.Ins.maxBot;
        Invoke("OpenUIWhenDead", 2f);
        if(!isUlti) offsetCameraBeforeDead = CameraFollow.Ins.offset;
    }


    public void UpCoin(int number)
    {
        coin += number;
        PlayerPrefs.SetInt(goldKey, coin);
    }

    public void BuyItem(int price)
    {
        coin -= price;
        PlayerPrefs.SetInt(goldKey, coin);
    }

    public void BuyItemRevive(int price)
    {
        coin -= price;
        PlayerPrefs.SetInt(goldKey, coin);
    }

    public void SetNumberKillBot(int number)
    {
        numberKillBot += number;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag(Constant.TAG_GIFT))
        {
            
            Gift gift = Cache.GetGift(other);
            SimplePool.Despawn(gift);
            if(isUlti) return;
            isUlti = true;
            sizeRingBeforeCollectGift = attackRange.TF.localScale;
            SetSizeRingWhenCollectGift(1.8f,sizeRingBeforeCollectGift);
            offsetCameraBeforeCollectGift = CameraFollow.Ins.offset;
            CameraFollow.Ins.SetUpWhenCollectGift();
        }    
    }

    private void OpenUIWhenDead()
    {
        UIManager.Ins.OpenUI(UIID.UIRevive);
    }

}