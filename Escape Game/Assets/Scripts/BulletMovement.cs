using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Rigidbody2D bullet;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private EnemyMovement enemyMovement;

    private float speed = 25;
    private bool canFire = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canFire && playerMovement.getBullets()>0&&Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    public void Fire()
    {
        playerMovement.changeBullets(-1);
        canFire = false;
        Vector2 mouse = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float px = playerRigidbody.position.x;
        float py = playerRigidbody.position.y;
        float mx = mouse.x;
        float my = mouse.y;
        float xdif = mx - px;
        float ydif = my - py;
        float distance = Mathf.Sqrt(Mathf.Pow(xdif, 2) + Mathf.Pow(ydif, 2));
        float sine = ydif / distance;
        float cosine = xdif / distance;
        float degrees = (float) ( Mathf.Rad2Deg*(ydif == 0 && xdif == 0 ? 0 : (xdif < 0 && ydif == 0 ? Math.PI : 2 * Math.Atan(ydif / (xdif + distance)))));
        bullet.position = new Vector2(px+ 1.5f*cosine, py+ 1.5f*sine);
        bullet.rotation = degrees; 
        bullet.velocity = new Vector2(cosine*speed, sine* speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        teleportAway();
        if (collision.gameObject.name.Contains("Enemy"))
        {
            enemyMovement.decreaseHealth();
        }
    }

    public void teleportAway()
    {
        bullet.position = new Vector2(playerRigidbody.position.x, playerRigidbody.position.y+200);
        canFire = true;
    }
}
