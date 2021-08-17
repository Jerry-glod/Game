using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform trackedObject, trackedObjectZoom, targetCamera; //相机跟随的目标
    public float smootspeed = 5f; //平滑速度
    Vector3 offset;       //相机对物体的偏移
    static MainCamera myself;
    //public Animation animator;
    private void Awake()
    {
        myself = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //计算物体的相对位置
        //offset = transform.position - Player.position;
    }

    // Update is called once per frame
    void Update()
    {
        //相机目标的位置=当前角色位置+偏移量
        Vector3 PlayerCameraPos = transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, PlayerCameraPos, smootspeed * Time.deltaTime);
    }
}
