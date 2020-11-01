using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Move : MonoBehaviour
{
    public float BulletSpeed;
    public float DestroyPos;
    public float damage = 10.0f;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * BulletSpeed * Time.deltaTime);
        if(transform.position.z > DestroyPos)
        {
            Destroy(gameObject);
        }
    }
}
