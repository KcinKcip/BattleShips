using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateField : MonoBehaviour
{
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
    //    List<Coordinate> coordinates;
    List<GameObject> nearGameObjects = new List<GameObject>();
    List<GameObject> emptyObjects = new List<GameObject>();
    List<GameObject> emptyObjectsForAI = new List<GameObject>();
    List<GameObject> emptyObjectsForAIHit = new List<GameObject>();

    void Start() 
    {
        CreateMap();  
        createClassicShips();
        CreateMapForAI();
        createClassicShipsForAI();
        //GameObject prefab = Instantiate(prefabSquare);
        //GameObject square = new GameObject("NEWTESTSQUARE");
        //square.AddComponent<SpriteRenderer>().sprite = emptySquare;
        //square.transform.Translate(2.15f, 3.15f, -1);
        //square.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        //gameObjects[2-1, 2-1].GetComponent<SpriteRenderer>().sprite = shipSquare;
        //createCoordinates(coordinates);
        //nearGameObjects = checkNear(gameObjects[1, 1]);
        //for (int i = 0; i < nearGameObjects.Count; i++) {
        //    nearGameObjects[i].GetComponent<SpriteRenderer>().sprite = missSquare;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHits > 0) { Debug.Log("Player Win!"); }
        if (aiHits > 0) { Debug.Log("AI Win!"); }
        if (playerTurn == false) {
            countAIHit = random.Next(emptyObjectsForAIHit.Count);
            if (emptyObjectsForAIHit[countAIHit].GetComponent<SpriteRenderer>().sprite.Equals(shipSquare))
            {
                emptyObjectsForAIHit[countAIHit].GetComponent<SpriteRenderer>().sprite = destroyedSquare;
                aiHits++;
                emptyObjectsForAIHit.Remove(emptyObjectsForAIHit[countAIHit]);
            }
            else {
                emptyObjectsForAIHit[countAIHit].GetComponent<SpriteRenderer>().sprite = missSquare;
                playerTurn = true;
                emptyObjectsForAIHit.Remove(emptyObjectsForAIHit[countAIHit]);
            }
        }
        
    }

    void CreateMap()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                //GameObject square = new GameObject();
                //square.AddComponent<SpriteRenderer>().sprite = emptySquare;
                //square.AddComponent<BoxCollider2D>();
                //square.AddComponent<CreateSquare>();
                GameObject square = Instantiate(prefabSquare);
                square.name = "square(" + (i + 1) + ":" + (j + 1) + ")";
                square.transform.Translate(i * 0.60f, -j * 0.60f, 3);
                gameObjects[i, j] = square;
                emptyObjects.Add(square);
            }
        }
    }
    public Vector2 findObjectOnField(GameObject gameObject) {
        int x = -1;
        int y = -1;
        for (int i = 0; i < 10; i++) {
            for (int j = 0; j < 10; j++) {
                if (gameObject.Equals(gameObjects[i, j])) {
                    x = i;
                    y = j;
                }
            }
        }
        return new Vector2(x, y);
    }
    public List<GameObject> checkNear(GameObject gameObject) {
        List<GameObject> list = new List<GameObject>();
        Vector2 vector = findObjectOnField(gameObject);
        int x = (int)vector.x;
        int y = (int)vector.y;
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
        } else if (x == 9 && y == 0 ) {
            list.Add(gameObjects[x - 1, y]);
            list.Add(gameObjects[x, y + 1]);
        } else if (x == 9 && y == 9) {
            list.Add(gameObjects[x - 1, y]);
            list.Add(gameObjects[x, y - 1]);
        } else if (x == 0 && y == 0) {
            list.Add(gameObjects[x + 1, y]);
            list.Add(gameObjects[x, y + 1]);
        }
        return list;
    }
    public void createShip(int shipValue) {
        int value = shipValue;
        List<GameObject> nearObjects = new List<GameObject>();
        List<GameObject> shipList = new List<GameObject>();
        
        int count = random.Next(emptyObjects.Count);
        shipList.Add(emptyObjects[count]);
        emptyObjects[count].GetComponent<SpriteRenderer>().sprite = shipSquare;
        nearObjects = checkNear(emptyObjects[count]);
        emptyObjects.Remove(emptyObjects[count]);
        //Vector2 vector = findObjectOnField(emptyObjects[count]);
        //int x = (int)vector.x;
        //int y = (int)vector.y;
        value--;
        while (value != 0) {
            count = random.Next(nearObjects.Count);

            if (nearObjects[count].GetComponent<SpriteRenderer>().sprite.Equals(emptySquare)) {
                shipList.Add(nearObjects[count]);
                nearObjects[count].GetComponent<SpriteRenderer>().sprite = shipSquare;
                emptyObjects.Remove(nearObjects[count]);
                nearObjects = checkNear(nearObjects[count]);
                value--;
            }
        }
        for (int i = 0; i < shipList.Count; i++) {
            nearObjects = checkNear(shipList[i]);
            for (int j = 0; j < nearObjects.Count; j++) {
                if (nearObjects[j].GetComponent<SpriteRenderer>().sprite.Equals(emptySquare)) {
                    nearObjects[j].GetComponent<SpriteRenderer>().sprite = notAllowedSquare;
                    emptyObjects.Remove(nearObjects[j]);
                }
            }
            
        }
    }
    public void createClassicShips() {
        createShip(4);
        createShip(3);
        createShip(3);
        createShip(2);
        createShip(2);
        createShip(2);
        createShip(1);
        createShip(1);
        createShip(1);
        createShip(1);
    }
    public Sprite getDestroyedSquare() {
        return destroyedSquare;
    }
    void CreateMapForAI()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                //GameObject square = new GameObject();
                //square.AddComponent<SpriteRenderer>().sprite = emptySquare;
                //square.AddComponent<BoxCollider2D>();
                //square.AddComponent<CreateSquare>();
                GameObject square = Instantiate(prefabSquareForAI);
                square.name = "squareAI(" + (i + 1) + ":" + (j + 1) + ")";
                square.transform.Translate(i * 0.30f, -j * 0.30f, -1);
                gameObjectsForAI[i, j] = square;
                emptyObjectsForAI.Add(square);
                emptyObjectsForAIHit.Add(square);
            }
        }
    }
    public Vector2 findObjectOnFieldForAI(GameObject gameObject)
    {
        int x = -1;
        int y = -1;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (gameObject.Equals(gameObjectsForAI[i, j]))
                {
                    x = i;
                    y = j;
                }
            }
        }
        return new Vector2(x, y);
    }
    public List<GameObject> checkNearForAI(GameObject gameObject)
    {
        List<GameObject> list = new List<GameObject>();
        Vector2 vector = findObjectOnFieldForAI(gameObject);
        int x = (int)vector.x;
        int y = (int)vector.y;
        if (x > 0 && x < 9 && y > 0 && y < 9)
        {
            list.Add(gameObjectsForAI[x - 1, y]);
            list.Add(gameObjectsForAI[x + 1, y]);
            list.Add(gameObjectsForAI[x, y - 1]);
            list.Add(gameObjectsForAI[x, y + 1]);
        }
        else if (x > 0 && x < 9 && y == 9)
        {
            list.Add(gameObjectsForAI[x - 1, y]);
            list.Add(gameObjectsForAI[x + 1, y]);
            list.Add(gameObjectsForAI[x, y - 1]);
        }
        else if (x > 0 && x < 9 && y == 0)
        {
            list.Add(gameObjectsForAI[x - 1, y]);
            list.Add(gameObjectsForAI[x + 1, y]);
            list.Add(gameObjectsForAI[x, y + 1]);
        }
        else if (x == 0 && y > 0 && y < 9)
        {
            list.Add(gameObjectsForAI[x + 1, y]);
            list.Add(gameObjectsForAI[x, y - 1]);
            list.Add(gameObjectsForAI[x, y + 1]);
        }
        else if (x == 9 && y > 0 && y < 9)
        {
            list.Add(gameObjectsForAI[x - 1, y]);
            list.Add(gameObjectsForAI[x, y - 1]);
            list.Add(gameObjectsForAI[x, y + 1]);
        }
        else if (x == 0 && y == 9)
        {
            list.Add(gameObjectsForAI[x + 1, y]);
            list.Add(gameObjectsForAI[x, y - 1]);
        }
        else if (x == 9 && y == 0)
        {
            list.Add(gameObjectsForAI[x - 1, y]);
            list.Add(gameObjectsForAI[x, y + 1]);
        }
        else if (x == 9 && y == 9)
        {
            list.Add(gameObjectsForAI[x - 1, y]);
            list.Add(gameObjectsForAI[x, y - 1]);
        }
        else if (x == 0 && y == 0)
        {
            list.Add(gameObjectsForAI[x + 1, y]);
            list.Add(gameObjectsForAI[x, y + 1]);
        }
        return list;
    }
    public void createShipForAI(int shipValue)
    {
        int value = shipValue;
        List<GameObject> nearObjects = new List<GameObject>();
        List<GameObject> shipList = new List<GameObject>();
        System.Random random = new System.Random();
        int count = random.Next(emptyObjectsForAI.Count);
        shipList.Add(emptyObjectsForAI[count]);
        emptyObjectsForAI[count].GetComponent<SpriteRenderer>().sprite = shipSquare;
        nearObjects = checkNearForAI(emptyObjectsForAI[count]);
        emptyObjectsForAI.Remove(emptyObjectsForAI[count]);
        //Vector2 vector = findObjectOnField(emptyObjects[count]);
        //int x = (int)vector.x;
        //int y = (int)vector.y;
        value--;
        while (value != 0)
        {
            count = random.Next(nearObjects.Count);

            if (nearObjects[count].GetComponent<SpriteRenderer>().sprite.Equals(emptySquare))
            {
                shipList.Add(nearObjects[count]);
                nearObjects[count].GetComponent<SpriteRenderer>().sprite = shipSquare;
                emptyObjectsForAI.Remove(nearObjects[count]);
                nearObjects = checkNearForAI(nearObjects[count]);
                value--;
            }
        }
        for (int i = 0; i < shipList.Count; i++)
        {
            nearObjects = checkNearForAI(shipList[i]);
            for (int j = 0; j < nearObjects.Count; j++)
            {
                if (nearObjects[j].GetComponent<SpriteRenderer>().sprite.Equals(emptySquare))
                {
                    nearObjects[j].GetComponent<SpriteRenderer>().sprite = notAllowedSquare;
                    emptyObjectsForAI.Remove(nearObjects[j]);
                }
            }

        }
    }
    public void createClassicShipsForAI()
    {
        createShipForAI(4);
        createShipForAI(3);
        createShipForAI(3);
        createShipForAI(2);
        createShipForAI(2);
        createShipForAI(2);
        createShipForAI(1);
        createShipForAI(1);
        createShipForAI(1);
        createShipForAI(1);
    }









    /* private void createCoordinates(List<Coordinate> list) {
        for (int i = 0; i < 10; i++)
            for (int j = 0; j < 10; i++) {
                string name = "coordinate(" + (i + 1) + ":" + (j + 1) + ")";
                Coordinate name =  
                list.Add(new Coordinate(i, j) coordinate);
            }
    }

    public class Coordinate {
        private int x;
        private int y;
        public Coordinate(int x, int y) {
            this.x = x;
            this.y = y;
        }
        public void SetCoordinate(int x, int y) {
            this.x = x;
            this.y = y;
        }
        public Coordinate GetCoordinate() {
            return new Coordinate(this.x, this.y);
        }
    } */
}
