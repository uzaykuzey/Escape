using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisFinder : MonoBehaviour
{
    [SerializeField] private string axisName;
    [SerializeField] private Rigidbody2D rb;

    private float speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(axisName.ToLower().Equals("x"))
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(0, speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed = - Mathf.Sign(speed) * Random.Range(5,15)* 1.5f;
    }

    public float GetAxisValue(string str)
    {
        if(str.ToLower().Equals("x"))
        {
            return rb.position.x;
        }
        return rb.position.y;
    }
}
