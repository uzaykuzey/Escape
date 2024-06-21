using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    private float basespeed = 25f;
    private bool stop = false;
    private float randomFactor = 0.1f;
    private int health = 5;
    [SerializeField] private Rigidbody2D enemy;
    [SerializeField] private Rigidbody2D player;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PowerUpController powerUpController;
    [SerializeField] private Sprite trophy;
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!stop)
        {
            float px = player.position.x;
            float py = player.position.y;
            float ex = enemy.position.x;
            float ey = enemy.position.y;
            float distance = Mathf.Sqrt(Mathf.Pow(ex - px, 2) + Mathf.Pow(ey - py, 2));
            float sine = (py - ey) / distance+Random.value*2*randomFactor-randomFactor;
            float cosine = (px - ex) / distance + Random.value * 2 * randomFactor - randomFactor;
            float speed = basespeed + 5*Random.value;

            if(powerUpController.getCurrentType()== 2)
            {
                if(distance<7)
                {
                    speed *= -2;
                }
            }
            else if (powerUpController.getCurrentType() == 1)
            {
                speed = 0.01f;
            }
            if(!stop)
            {
                enemy.AddForce(new Vector2(cosine * speed, sine * speed));
            }

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!stop&&playerMovement.getHealth()<1&&collision.gameObject.name.Equals(player.gameObject.name))
        {
            enemy.gravityScale =1;
            GameObject.Destroy(collision.gameObject);
            stop = true;
        }
        if(health<1)
        {
            enemy.gravityScale = 2;
            spriteRenderer.sprite = trophy;
            enemy.gameObject.name = "Trophy";
            stop = true;
        }
    }

    public void decreaseHealth()
    {
        health--;
        print(health);
    }
}
