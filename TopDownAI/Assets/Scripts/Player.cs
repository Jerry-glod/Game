using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerWeaponType { KNIFE, PISTOL, NULL }

public class Player : MonoBehaviour
{
    //public Vector3 Direction=new Vector3(1,0,0);
    public float moveSpeed=5f;
    public Animator animator;
    public Transform gun_pivot,knife_pivot;
    public GameObject zidanPrefabs,mouse_pivot;
    Rigidbody _rigidbody;
    int HashSpeed;
    PlayerWeaponType currentweapon = PlayerWeaponType.NULL;//currentweaponType 当前武器的类型
    float attackTime = 0.4f;

    
    //Vector3 mousePositionOnScreen;//获取到点击屏幕的屏幕坐标

    /*
    Vector3 screenPosition;//将物体从世界坐标转换为屏幕坐标
    Vector3 mousePositionOnScreen;//获取到点击屏幕的屏幕坐标
    Vector3 mousePositionInWorld;//将点击屏幕的屏幕坐标转换为世界坐标
    */

    // Start is called before the first frame update
    void Start()
    {
        SetWeapon(PlayerWeaponType.PISTOL);
        _rigidbody = GetComponent<Rigidbody>();
        HashSpeed = Animator.StringToHash("Speed");
        //attackTime.StartTimer(0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat(HashSpeed, _rigidbody.velocity.magnitude);//钢体移动速度的大小
        float moveX = Input.GetAxis("Horizontal");//水平
        float moveY = Input.GetAxis("Vertical");//垂直

        //Vector3 Direction = new Vector3(moveY * moveSpeed,  moveX * -moveSpeed, 0f);
        //transform.Translate(new Vector3(  moveY, -moveX, 0) * Time.deltaTime * moveSpeed);
        Vector3 newVelocity = new Vector3(moveY * moveSpeed, 0.0f, moveX * -moveSpeed);
        _rigidbody.velocity = newVelocity;

        switch (currentweapon)
        {
            //当前攻击的武器类型
            case PlayerWeaponType.KNIFE: //刀
                if (Input.GetMouseButtonDown(0))//当鼠标按下时，武器刀进行攻击
                {
                    Attack();
                }
                break;
            case PlayerWeaponType.PISTOL:  //手枪
                if (Input.GetMouseButtonDown(0))//当鼠标按下时，武器手枪进行射击
                {
                    Attack();
                }
                break;
            default:
                break;
        }
        //切换武器
        //按下按键Q，切换武器刀
        if (Input.GetKeyDown (KeyCode.Q))
        {
            //设置到当前武器为刀
            SetWeapon(PlayerWeaponType.KNIFE);
        }
        //按下武器E，切换手枪
        if (Input.GetKeyDown(KeyCode.E))
        {
            //设置到当前武器为手枪
            SetWeapon(PlayerWeaponType.PISTOL);
        }
        UpdateMouse();
    }
    //鼠标移动到哪，光标移动到哪
    void UpdateMouse()
    {
        
        //点击鼠标的坐标转化为世界坐标
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        /*
        print(mousePos);
        mousePos.y = transform.position.y;//统一方向，鼠标的y轴位置等于世界坐标的y轴位置
        mouse_pivot.transform.position = mousePos;
        float deltaY = mousePos.z - transform.position.z;
        float deltaX = mousePos.x - transform.position.x;
        float angleInDegrees = Mathf.Atan2(deltaY, deltaX) * 180 / Mathf.PI; //Mathf.Atan2反正切
        transform.eulerAngles = new Vector3(0, -angleInDegrees, 0);//欧拉角（四元素）
        */
        

        /*
        screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        //获取鼠标在场景中坐标
        mousePositionOnScreen = Input.mousePosition;
        //让场景中的Z=鼠标坐标的Z
        mousePositionOnScreen.z = screenPosition.z;
        //将相机中的坐标转化为世界坐标
        mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
        //物体跟随鼠标移动
        //transform.position = mousePositionInWorld;
        //物体跟随鼠标X轴移动
        Vector3 mouse_pivot= new Vector3(mousePositionInWorld.x, mousePositionInWorld.y, mousePositionInWorld.z);
        */
    }
    //玩家受伤
    public void DamagePlayer()
    {
        animator.SetBool("Dead", true);
        animator.transform.parent = null;
        this.enabled = false;
        _rigidbody.isKinematic = true;
        gameObject.GetComponent<Collider>().enabled = false;
        Vector3 pos = animator.transform.position;
        pos.y = 0.2f;
        animator.transform.position = pos;
    }
    public void Attack()
    {
        //分两种武器，一种刀，近战，一种枪，远战
        //先确认使用的是什么类型的武器
        switch (currentweapon)
        {
            case PlayerWeaponType.KNIFE:

                break;
            case PlayerWeaponType.PISTOL:
                //如果是手枪，远程射击武器类型，那么子弹射击的方向是鼠标的点击的位置和人物朝向的位置的夹角
                GameObject Bullet = GameObject.Instantiate(zidanPrefabs, gun_pivot.position, gun_pivot.rotation);
                Bullet.transform.LookAt(mouse_pivot.transform);
                Bullet.transform.Rotate(0, Random.Range(-7.5f, 7.5f), 0);//(x,y,z)旋转的角度
                
                break;
                
            default:
                break;
        }
        animator.SetBool("Attack", true);
        CancelInvoke("AttackOver");
        Invoke("AttackOver", attackTime);
        //attackTime.
    }
    
    void AttackOver()
    {
        animator.SetBool("Attack", false);
    }
    private void SetWeapon(PlayerWeaponType weaponType)
    {
        //先判断当前的武器类型是什么，看是否与我要当前的武器相同
        if (weaponType != currentweapon)
        {
            currentweapon = weaponType;
            animator.SetTrigger("WeaponChange");
            switch (weaponType)
            {
                case PlayerWeaponType.PISTOL:
                    animator.SetInteger("WeaponType", 1);
                    break;
                case PlayerWeaponType.KNIFE:
                    animator.SetInteger("WeaponType", 0);
                    break;
                default:
                    break;
            }
        }
        
    }
}
