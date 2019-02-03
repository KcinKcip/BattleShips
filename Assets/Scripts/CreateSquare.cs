using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSquare : MonoBehaviour
{
    public Sprite destroyedSquare;
    public Sprite shipSquare;
    public Sprite missSquare;
    void OnMouseDown()
    {
        if (!gameObject.GetComponent<SpriteRenderer>().sprite.Equals(missSquare) && !gameObject.GetComponent<SpriteRenderer>().sprite.Equals(destroyedSquare))
        {
            if (gameObject.GetComponent<SpriteRenderer>().sprite.Equals(shipSquare))
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = destroyedSquare;
                CreateField.playerHits++;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = missSquare;
                CreateField.playerTurn = false;
            }
            gameObject.transform.Translate(0, 0, -3);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
