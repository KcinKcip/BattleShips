using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CreateField : MonoBehaviour
{
    enum Owner
    {
        AI = 1,
        Human = 2
    }

    public Sprite destroyedSquare;
    public Sprite emptySquare;
    public Sprite shipSquare;
    public Sprite missSquare;
    public Sprite notAllowedSquare;
    public GameObject prefabSquare;
    public GameObject prefabSquareForAI;
    public static bool playerTurn = true;
    public static int playerHits = 0;
    public static int aiHits = 0;
    int countAIHit;
    System.Random random = new System.Random();


    GameObject[,] gameObjects = new GameObject[10, 10];
    GameObject[,] gameObjectsForAI = new GameObject[10, 10];
    List<GameObject> nearGameObjects = new List<GameObject>();
    List<GameObject> emptyObjects = new List<GameObject>();
    List<GameObject> emptyObjectsForAI = new List<GameObject>();
    List<GameObject> emptyObjectsForAIHit = new List<GameObject>();

    void Start()
    {
        CreateMap();
        createClassicShips();
    }
    void Update()
    {
        if (playerHits > 0) { Debug.Log("Player Win!"); }
        if (aiHits > 0) { Debug.Log("AI Win!"); }
        if (playerTurn == false) {
            countAIHit = random.Next(emptyObjectsForAIHit.Count);
            if (emptyObjectsForAIHit[countAIHit].GetComponent<SpriteRenderer>().sprite.Equals(shipSquare)) {
                emptyObjectsForAIHit[countAIHit].GetComponent<SpriteRenderer>().sprite = destroyedSquare;
                aiHits++;
                emptyObjectsForAIHit.Remove(emptyObjectsForAIHit[countAIHit]);
            } else {
                emptyObjectsForAIHit[countAIHit].GetComponent<SpriteRenderer>().sprite = missSquare;
                playerTurn = true;
                emptyObjectsForAIHit.Remove(emptyObjectsForAIHit[countAIHit]);
            }
        }

    }

    void CreateMap()
    {
        for (int i = 0; i < 10; i++) {
            for (int j = 0; j < 10; j++) {
                GameObject square = Instantiate(prefabSquare);
                GameObject squareAI = Instantiate(prefabSquareForAI);
                square.name = "square(" + (i + 1) + ":" + (j + 1) + ")";
                squareAI.name = "squareAI(" + (i + 1) + ":" + (j + 1) + ")";
                square.transform.Translate(i * 0.60f, -j * 0.60f, 3);
                squareAI.transform.Translate(i * 0.30f, -j * 0.30f, -1);
                gameObjects[i, j] = square;
                gameObjectsForAI[i, j] = squareAI;
                emptyObjects.Add(square);
                emptyObjectsForAI.Add(squareAI);
                emptyObjectsForAIHit.Add(squareAI);
            }
        }
    }
    Vector2 findObjectOnField(GameObject gameObject)
    {
        int x = -1;
        int y = -1;
            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    if (gameObject.Equals(gameObjects[i, j]) || gameObject.Equals(gameObjectsForAI[i, j])) {
                        x = i;
                        y = j;
                    }
                }
            }
        return new Vector2(x, y);
    }
    List<GameObject> checkNear(GameObject gameObject, Owner owner)
    {
        List<GameObject> list = new List<GameObject>();
        if (owner.Equals(Owner.Human)) {
            Vector2 vector = findObjectOnField(gameObject);
            int x = (int) vector.x;
            int y = (int) vector.y;
            if (x > 0 && x < 9 && y > 0 && y < 9) {
                list.Add(gameObjects[x - 1, y]);
                list.Add(gameObjects[x + 1, y]);
                list.Add(gameObjects[x, y - 1]);
                list.Add(gameObjects[x, y + 1]);
            } else if (x > 0 && x < 9 && y == 9) {
                list.Add(gameObjects[x - 1, y]);
                list.Add(gameObjects[x + 1, y]);
                list.Add(gameObjects[x, y - 1]);
            } else if (x > 0 && x < 9 && y == 0) {
                list.Add(gameObjects[x - 1, y]);
                list.Add(gameObjects[x + 1, y]);
                list.Add(gameObjects[x, y + 1]);
            } else if (x == 0 && y > 0 && y < 9) {
                list.Add(gameObjects[x + 1, y]);
                list.Add(gameObjects[x, y - 1]);
                list.Add(gameObjects[x, y + 1]);
            } else if (x == 9 && y > 0 && y < 9) {
                list.Add(gameObjects[x - 1, y]);
                list.Add(gameObjects[x, y - 1]);
                list.Add(gameObjects[x, y + 1]);
            } else if (x == 0 && y == 9) {
                list.Add(gameObjects[x + 1, y]);
                list.Add(gameObjects[x, y - 1]);
            } else if (x == 9 && y == 0) {
                list.Add(gameObjects[x - 1, y]);
                list.Add(gameObjects[x, y + 1]);
            } else if (x == 9 && y == 9) {
                list.Add(gameObjects[x - 1, y]);
                list.Add(gameObjects[x, y - 1]);
            } else if (x == 0 && y == 0) {
                list.Add(gameObjects[x + 1, y]);
                list.Add(gameObjects[x, y + 1]);
            }
        } else {
            Vector2 vector = findObjectOnField(gameObject);
            int x = (int) vector.x;
            int y = (int) vector.y;
            if (x > 0 && x < 9 && y > 0 && y < 9) {
                list.Add(gameObjectsForAI[x - 1, y]);
                list.Add(gameObjectsForAI[x + 1, y]);
                list.Add(gameObjectsForAI[x, y - 1]);
                list.Add(gameObjectsForAI[x, y + 1]);
            } else if (x > 0 && x < 9 && y == 9) {
                list.Add(gameObjectsForAI[x - 1, y]);
                list.Add(gameObjectsForAI[x + 1, y]);
                list.Add(gameObjectsForAI[x, y - 1]);
            } else if (x > 0 && x < 9 && y == 0) {
                list.Add(gameObjectsForAI[x - 1, y]);
                list.Add(gameObjectsForAI[x + 1, y]);
                list.Add(gameObjectsForAI[x, y + 1]);
            } else if (x == 0 && y > 0 && y < 9) {
                list.Add(gameObjectsForAI[x + 1, y]);
                list.Add(gameObjectsForAI[x, y - 1]);
                list.Add(gameObjectsForAI[x, y + 1]);
            } else if (x == 9 && y > 0 && y < 9) {
                list.Add(gameObjectsForAI[x - 1, y]);
                list.Add(gameObjectsForAI[x, y - 1]);
                list.Add(gameObjectsForAI[x, y + 1]);
            } else if (x == 0 && y == 9) {
                list.Add(gameObjectsForAI[x + 1, y]);
                list.Add(gameObjectsForAI[x, y - 1]);
            } else if (x == 9 && y == 0) {
                list.Add(gameObjectsForAI[x - 1, y]);
                list.Add(gameObjectsForAI[x, y + 1]);
            } else if (x == 9 && y == 9) {
                list.Add(gameObjectsForAI[x - 1, y]);
                list.Add(gameObjectsForAI[x, y - 1]);
            } else if (x == 0 && y == 0) {
                list.Add(gameObjectsForAI[x + 1, y]);
                list.Add(gameObjectsForAI[x, y + 1]);
            }
        }
        return list;
    }
    void createShip(int shipValue, Owner owner)
    {
        int value = shipValue;
        List<GameObject> nearObjects = new List<GameObject>();
        List<GameObject> shipList = new List<GameObject>();

        if (owner.Equals(Owner.Human)) {
            int count = random.Next(emptyObjects.Count);
            shipList.Add(emptyObjects[count]);
            emptyObjects[count].GetComponent<SpriteRenderer>().sprite = shipSquare;
            nearObjects = checkNear(emptyObjects[count], owner);
            emptyObjects.Remove(emptyObjects[count]);
            value--;
            while (value != 0) {
                count = random.Next(nearObjects.Count);

                if (nearObjects[count].GetComponent<SpriteRenderer>().sprite.Equals(emptySquare)) {
                    shipList.Add(nearObjects[count]);
                    nearObjects[count].GetComponent<SpriteRenderer>().sprite = shipSquare;
                    emptyObjects.Remove(nearObjects[count]);
                    nearObjects = checkNear(nearObjects[count], owner);
                    value--;
                }
            }
            for (int i = 0; i < shipList.Count; i++) {
                nearObjects = checkNear(shipList[i], owner);
                for (int j = 0; j < nearObjects.Count; j++) {
                    if (nearObjects[j].GetComponent<SpriteRenderer>().sprite.Equals(emptySquare)) {
                        nearObjects[j].GetComponent<SpriteRenderer>().sprite = notAllowedSquare;
                        emptyObjects.Remove(nearObjects[j]);
                    }
                }

            }
        } else {
            System.Random random = new System.Random();
            int count = random.Next(emptyObjectsForAI.Count);
            shipList.Add(emptyObjectsForAI[count]);
            emptyObjectsForAI[count].GetComponent<SpriteRenderer>().sprite = shipSquare;
            nearObjects = checkNear(emptyObjectsForAI[count], owner);
            emptyObjectsForAI.Remove(emptyObjectsForAI[count]);
            value--;
            while (value != 0) {
                count = random.Next(nearObjects.Count);

                if (nearObjects[count].GetComponent<SpriteRenderer>().sprite.Equals(emptySquare)) {
                    shipList.Add(nearObjects[count]);
                    nearObjects[count].GetComponent<SpriteRenderer>().sprite = shipSquare;
                    emptyObjectsForAI.Remove(nearObjects[count]);
                    nearObjects = checkNear(nearObjects[count], owner);
                    value--;
                }
            }
            for (int i = 0; i < shipList.Count; i++) {
                nearObjects = checkNear(shipList[i], owner);
                for (int j = 0; j < nearObjects.Count; j++) {
                    if (nearObjects[j].GetComponent<SpriteRenderer>().sprite.Equals(emptySquare)) {
                        nearObjects[j].GetComponent<SpriteRenderer>().sprite = notAllowedSquare;
                        emptyObjectsForAI.Remove(nearObjects[j]);
                    }
                }

            }
        }

    }
    public void createClassicShips()
    {
        createShip(4, Owner.Human);
        createShip(3, Owner.Human);
        createShip(3, Owner.Human);
        createShip(2, Owner.Human);
        createShip(2, Owner.Human);
        createShip(2, Owner.Human);
        createShip(1, Owner.Human);
        createShip(1, Owner.Human);
        createShip(1, Owner.Human);
        createShip(1, Owner.Human);
        createShip(4, Owner.AI);
        createShip(3, Owner.AI);
        createShip(3, Owner.AI);
        createShip(2, Owner.AI);
        createShip(2, Owner.AI);
        createShip(2, Owner.AI);
        createShip(1, Owner.AI);
        createShip(1, Owner.AI);
        createShip(1, Owner.AI);
        createShip(1, Owner.AI);
    }
}