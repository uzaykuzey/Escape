using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawning : MonoBehaviour
{
    [SerializeField] private AxisFinder xFinder;
    [SerializeField] private AxisFinder yFinder;
    [SerializeField] private Rigidbody2D bulletSpawner;

    private int thereIsABullet=0;
    private int times = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(summon())
        {
            int k=0;
            for(int i=0;i<5;i++)
            {
                k++;
            }
            thereIsABullet = (int) Mathf.Sign(k);
            bulletSpawner.position = new Vector2(xFinder.getAxisValue("x"), yFinder.getAxisValue("y"));
            bulletSpawner.rotation= Random.Range(0, 360);
        }
    }

    private bool summon()
    {
        if (thereIsABullet != 0)
        {
            if (thereIsABullet == 2) { times++; }
            if (times > 200) { thereIsABullet = 0; }
            return false;
        }
        return ((int)Time.timeSinceLevelLoad) % 40 == 12;
    }

    public void teleportAway()
    {
        bulletSpawner.position = new Vector2(yFinder.getAxisValue("x"), xFinder.getAxisValue("y")+15);
        thereIsABullet = 2;
        times = 0;
    }
}
