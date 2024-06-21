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
       if(summon())
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
            powerup.position=new Vector2(xFinder.getAxisValue("x"), yFinder.getAxisValue("y"));
        }
       if(!activePowerUp()&&!didntTouchYet)
        {
            type = 0;
            didntTouchYet=true;
        }
    }

    private bool summon()
    {
        if (type!=0)
        {
            return false;
        }
        return ((int)Time.timeSinceLevelLoad) % 30 == 8;
    }

    public int getCurrentType()
    {
        if(didntTouchYet)
        {
            return 0;
        }
        return type;
    }

    public bool activePowerUp()
    {
        if(type==0)
        {
            return false;
        }
        return timeRemaining() != 0;
    }

    public float timeRemaining()
    {
        float time = totalTimeAllowed - (Time.timeSinceLevelLoad - timeStart);
        return time<0 ? 0: time;
    }

    public void teleportAway()
    {
        powerup.position = new Vector2(yFinder.getAxisValue("x"), xFinder.getAxisValue("y"));
    }

    public void setUp()
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
