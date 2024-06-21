using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerUpController : MonoBehaviour
{

    [SerializeField] private AxisFinder xFinder;
    [SerializeField] private AxisFinder yFinder;
    [SerializeField] private Rigidbody2D powerup;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D enemyBody;

    [SerializeField] private Sprite[] sprites;

    private float timeStart;
    private float totalTimeAllowed=15f;
    private int type;// 1: chained; 2: scarer; 3: speed; 4: flying
    private bool didntTouchYet = true;


    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(0);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
       if(Summon())
        {
            int randomInt = (int)Random.Range(1, 9);
            if(randomInt < 3) 
            { 
                type = 1;

            }
            else if (randomInt < 5) 
            { 
                type = 2; 
            }
            else if (randomInt < 7) 
            {
                type = 3; 
            }
            else
            {
                type = 4;
            }
            spriteRenderer.sprite = sprites[type-1];
            didntTouchYet = true;
            powerup.position=new Vector2(xFinder.GetAxisValue("x"), yFinder.GetAxisValue("y"));
        }
       if(!ActivePowerUp()&&!didntTouchYet)
        {
            type = 0;
            didntTouchYet=true;
        }
    }

    private bool Summon()
    {
        if (type!=0)
        {
            return false;
        }
        return ((int)Time.timeSinceLevelLoad) % 30 == 8;
    }

    public int GetCurrentType()
    {
        if(didntTouchYet)
        {
            return 0;
        }
        return type;
    }

    public bool ActivePowerUp()
    {
        if(type==0)
        {
            return false;
        }
        return TimeRemaining() != 0;
    }

    public float TimeRemaining()
    {
        float time = totalTimeAllowed - (Time.timeSinceLevelLoad - timeStart);
        return time<0 ? 0: time;
    }

    public void TeleportAway()
    {
        powerup.position = new Vector2(yFinder.GetAxisValue("x"), xFinder.GetAxisValue("y"));
    }

    public void SetUp()
    {
        timeStart = Time.timeSinceLevelLoad;
        didntTouchYet = false;
        if(type==1)
        {
            enemyBody.totalForce = new Vector2(0, 0);
            enemyBody.velocity = new Vector2(0, 0);
        }
    }

}
