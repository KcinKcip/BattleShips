using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public Text textScore;
    public Text textWin;
    // Start is called before the first frame update
    void Start()
    {
        textScore.text = "Score: Player 0 - 0 AI";
        textWin.text = "";
    }

        // Update is called once per frame
        void Update()
    {
        textScore.text = "Score: Player " + CreateField.playerHits + " - " + CreateField.aiHits + " AI";
        if (CreateField.playerHits == 20) { textWin.text = "Player Win!"; }
        if (CreateField.aiHits == 20) { textWin.text = "AI Win!"; }
    }
}
