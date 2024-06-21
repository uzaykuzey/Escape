using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDisplay : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PowerUpController powerUpController;
    [SerializeField] private Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int type=powerUpController.getCurrentType();
        spriteRenderer.sprite = sprites[type];
        if(powerUpController.timeRemaining()<=4 && powerUpController.timeRemaining()>0 && (((int)(powerUpController.timeRemaining() * 1.5)) % 2 == 1))
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            spriteRenderer.color = new Color(1, 1, 1, 1f);
        }
    }
}
