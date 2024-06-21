using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthDisplay : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int hearthIndex;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Sprite activeHeart;
    [SerializeField] Sprite deactiveHeart;
    [SerializeField] SpriteRenderer spriteRenderer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerMovement.getHealth() < hearthIndex)
        {
            spriteRenderer.sprite = deactiveHeart;
        }
        else
        {
            spriteRenderer.sprite= activeHeart;
        }
    }
}
