using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zidan : MonoBehaviour
{
    //public CollisionTarget collisionTarget;
    public float lifeTime = 3f;
    public float speed = 1.5f;

    bool moving;
    // Start is called before the first frame update
    void Start()
    {
        moving = true;
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            transform.Translate(transform.forward * speed, Space.World);
        }
    }
}
