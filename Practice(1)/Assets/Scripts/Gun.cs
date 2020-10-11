using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject PlayerBullet;
    public Transform Bulletlocation;
    public float BulletSpeed;
    private bool BulletState;
    
    void Start()
    {
        BulletState = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void playerfireclick()
    {
        if(BulletState)
        {
            StartCoroutine(playerfirecontroll());
            Instantiate(PlayerBullet, Bulletlocation.position, Bulletlocation.rotation);
        }
    }
    IEnumerator playerfirecontroll()
    {
        BulletState = false;
        yield return new WaitForSeconds(BulletSpeed);
        BulletState = true;
    }
}
