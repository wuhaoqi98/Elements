﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int scale = 4; // the scale of board, should be 4 or 5
    public Element[,] elements; // Elements on the board
    public Vector3[,] grids; // positions on the board
    public GameObject[] newElems; // the prefabs of Elements
    public int moveSpeed = 15;
    public float moveTime = 0.55f; // estimated time between movements
    public float spaceBetweenElements = 2; 
    // the score required to enter different stages
    public int forrestScore = 3000, iceAgeScore = 10000; 
    public Sprite forrest, iceAge; // boards for different stages

    private int score;
    private bool canMove = true;
    private bool isGameOver = false;
    private bool isStarted = false; 
    private Text scoreText;
    private GameObject messageText;
    private GameObject mainCamera;
    // percentage of different Elements
    private int firePercentage, waterPercentage, icePercentage, woodPercentage,
                bigFirePercentage, stonePercentage, acidPercentage;
    private bool inForrest, inIceAge;


    // Use this for initialization
    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        messageText = GameObject.Find("MessageText");
        messageText.SetActive(false);
        mainCamera = GameObject.Find("Main Camera");
        InitializeBoard();

        firePercentage = 50;
        waterPercentage = 70;
        icePercentage = 85;
        woodPercentage = 100;
        bigFirePercentage = 0;
        stonePercentage = 0;
        acidPercentage = 0;

        //GenerateElemAtRandomPos(newElems[2]);
        //GenerateElemAtRandomPos(newElems[1]);
        //GenerateElemAtRandomPos(newElems[2]);
        //InstantiateElem(4, 0, 1);
        //InstantiateElem(0, 0, 0);
        //InstantiateElem(4, 1, 0);
        //InstantiateElem(0, 4, 3);

        // initial Elements on the board
        for (int i = 0; i < 4; i++)
        {
            GenerateRandomElement();
        }
    }


    /// <summary>
    /// Initializes arrays and Adjusts camera.
    /// </summary>
    private void InitializeBoard()
    {
        elements = new Element[scale, scale];
        grids = new Vector3[scale, scale];

        for (int y = 0; y < scale; y++)
        {
            for (int x = 0; x < scale; x++)
            {
                grids[y, x] = new Vector3(-2 * spaceBetweenElements + spaceBetweenElements * x,
                                          2 * spaceBetweenElements - spaceBetweenElements * y, 0);
            }
        }

        if(scale == 4)
        {
            mainCamera.transform.position = new Vector3(-1, 1, -10);
            mainCamera.GetComponent<Camera>().orthographicSize = 4.3f;
            transform.position = new Vector3(-1, 1, 0);
            transform.localScale = new Vector3(0.84f, 0.84f, 0);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (canMove && !isGameOver)
        {
            // touches control
            if (Input.touchCount > 0)
            {
                float dx = Input.GetTouch(0).deltaPosition.x;
                float dy = Input.GetTouch(0).deltaPosition.y;
                if (Mathf.Abs(dx) >= Mathf.Abs(dy))
                {
                    if (Input.GetTouch(0).deltaPosition.x < 0)
                    {
                        StartCoroutine(CanMoveAfterDelay(moveTime));
                        MoveLeft();
                    }
                    else if (Input.GetTouch(0).deltaPosition.x > 0)
                    {
                        StartCoroutine(CanMoveAfterDelay(moveTime));
                        MoveRight();
                    }
                }
                else
                {
                    if (Input.GetTouch(0).deltaPosition.y > 0)
                    {
                        StartCoroutine(CanMoveAfterDelay(moveTime));
                        MoveUp();
                    }
                    else if (Input.GetTouch(0).deltaPosition.y < 0)
                    {
                        StartCoroutine(CanMoveAfterDelay(moveTime));
                        MoveDown();
                    }
                }
            }

            // keyboard control
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StartCoroutine(CanMoveAfterDelay(moveTime));
                MoveLeft();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartCoroutine(CanMoveAfterDelay(moveTime));
                MoveRight();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                StartCoroutine(CanMoveAfterDelay(moveTime));
                MoveUp();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                StartCoroutine(CanMoveAfterDelay(moveTime));
                MoveDown();
            }
        }

    }


    /// <summary>
    /// Prevents controls for a given amount of time.
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    private IEnumerator CanMoveAfterDelay(float delay)
    {
        canMove = false;
        yield return new WaitForSeconds(delay);
        canMove = true;

        if (inIceAge)
        {
            for (int y = 0; y < scale; y++)
            {
                for (int x = 0; x < scale; x++)
                {
                    if (elements[y, x] != null && elements[y, x].index == 1)
                    {
                        Destroy(elements[y, x].gameObject);
                        InstantiateElem(y, x, 2);
                    }
                }
            }
        }

        GenerateRandomElement();
    }


    /// <summary>
    /// Instantiates an Element at given position.
    /// </summary>
    /// <param name="y"></param>
    /// <param name="x"></param>
    /// <param name="elemId"></param>
    /// <returns></returns>
    public Element InstantiateElem(int y, int x, int elemId)
    {
        elements[y, x] = Instantiate(newElems[elemId], grids[y, x], Quaternion.identity).GetComponent<Element>();
        elements[y, x].SetPos(y, x);
        elements[y, x].index = elemId;
        elements[y, x].Appears();
        return elements[y, x];
    }


    /// <summary>
    /// Instantiates an Element at given position after delay.
    /// </summary>
    /// <param name="y"></param>
    /// <param name="x"></param>
    /// <param name="elemId"></param>
    /// <param name="delay"></param>
    /// <returns></returns>
    public Element InstantiateElem(int y, int x, int elemId, float delay)
    {
        Element elem = InstantiateElem(y, x, elemId);
        elem.gameObject.SetActive(false);
        StartCoroutine(SetActiveAfterDelay(elem, delay));
        return elements[y, x];
    }


    /// <summary>
    /// Waits for delay and sets the Element active.
    /// </summary>
    /// <param name="elem"></param>
    /// <param name="delay"></param>
    /// <returns></returns>
    private IEnumerator SetActiveAfterDelay(Element elem, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (elem != null)
        {
            elem.gameObject.SetActive(true);
        }
    }


    /// <summary>
    /// Adds score.
    /// </summary>
    /// <param name="num"></param>
    public void AddScore(int num)
    {
        StartCoroutine(AddScoreAfterDelay(num, moveTime - 0.1f));
    }


    /// <summary>
    /// Coroutine of adding score.
    /// </summary>
    /// <param name="num"></param>
    /// <param name="delay"></param>
    /// <returns></returns>
    private IEnumerator AddScoreAfterDelay(int num, float delay)
    {
        yield return new WaitForSeconds(delay);
        score += num;
        scoreText.text = "Score: " + score;
        if(!inForrest && score >= forrestScore)
        {
            ToForrest();
            inForrest = true;
        }
        if(!inIceAge && score >= iceAgeScore)
        {
            ToIceAge();
            inIceAge = true;
        }
    }


    /// <summary>
    /// Changes the stage to forrest.
    /// </summary>
    private void ToForrest()
    {
        GetComponent<SpriteRenderer>().sprite = forrest;
        StartCoroutine(ShowMessage("Forrest", 1f));
        firePercentage = 45; // 45%
        waterPercentage = 65; // 20%
        icePercentage = 75; // 10%
        woodPercentage = 88; // 13%
        bigFirePercentage = 88; // 0%
        stonePercentage = 90; // 2%
        acidPercentage = 100; // 10%
    }


    /// <summary>
    /// Changes the stage to ice age.
    /// </summary>
    private void ToIceAge()
    {
        GetComponent<SpriteRenderer>().sprite = iceAge;
        StartCoroutine(ShowMessage("Ice Age", 1f));
        firePercentage = 45; // 45%
        waterPercentage = 70; // 25%
        icePercentage = 70; // 0%
        woodPercentage = 83; // 13%
        bigFirePercentage = 88; // 5%
        stonePercentage = 90; // 2%
        acidPercentage = 100; // 10%
    }
 
    
    /// <summary>
    /// Generates an Element at random position.
    /// </summary>
    /// <param name="elemId"></param>
    private void GenerateElemAtRandomPos(int elemId)
    {
        List<Vector3> emptyPos = new List<Vector3>();
        int x = 0, y = 0;

        for (int i = 0; i < scale; i++)
        {
            for (int j = 0; j < scale; j++)
            {
                if (elements[i, j] == null)
                {
                    emptyPos.Add(grids[i, j]);
                }
            }
        }
        if(emptyPos.Count == 0)
        {
            GameOver();
            return;
        }

        else if (emptyPos.Count == scale * scale && isStarted)
        {
            AllClear();
        }

        if(!isStarted)
        {
            isStarted = true;
        }

        Vector3 randomPos = emptyPos[Random.Range(0, emptyPos.Count)];
        
        x = (int)((randomPos.x + 2 * spaceBetweenElements) / spaceBetweenElements);
        y = (int)((randomPos.y - 2 * spaceBetweenElements) / -spaceBetweenElements);

        InstantiateElem(y, x, elemId, 0);
    }


    /// <summary>
    /// Generates a random Element id and calls GenerateElemAtRandomPos().
    /// </summary>
    private void GenerateRandomElement()
    {
        int random = Random.Range(0, 100);
        int id = 0;
        if (random < firePercentage) // fire
        {
            id = 0;
        }
        else if (random < waterPercentage) // water
        {
            id = 1;
        }
        else if (random < icePercentage) // ice
        {
            id = 2;
        }
        else if (random < woodPercentage) // wood
        {
            id = 3;
        }
        else if (random < bigFirePercentage) // big fire
        {
            id = 4;
        }
        else if(random < stonePercentage) // stone
        {
            id = 6;
        }
        else // acid
        {
            id = 7;
        }
        GenerateElemAtRandomPos(id);
    }


    /// <summary>
    /// Ends the game.
    /// </summary>
    private void GameOver()
    {
        isGameOver = true;
        messageText.GetComponent<Text>().text = "Game Over";
        messageText.SetActive(true);
    }


    /// <summary>
    /// Is called when all Elements on board are cleared.
    /// </summary>
    private void AllClear()
    {
        StartCoroutine(ShowMessage("All Clear!", 0.8f));
        score += 100;
        scoreText.text = "Score: " + score;
    }


    /// <summary>
    /// Shows message after delay.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="delay"></param>
    /// <returns></returns>
    private IEnumerator ShowMessage(string message, float delay)
    {
        messageText.GetComponent<Text>().text = message;
        messageText.SetActive(true);
        yield return new WaitForSeconds(delay);
        messageText.SetActive(false);
    }


    /// <summary>
    /// Moves all Elements left and triggers reactions.
    /// </summary>
    private void MoveLeft()
    {
        for (int y = 0; y < scale; y++)
        {
            Element lastElem = elements[y, 0];
            for(int x = 1; x < scale; x++)
            {
                if(elements[y, x] != null)
                {
                    elements[y, x].moveDirection = 3;
                    lastElem = elements[y, x].ReactWith(lastElem);
                }
            }
        }
    }


    /// <summary>
    /// Moves all Elements right and triggers reactions.
    /// </summary>
    private void MoveRight()
    {
        for (int y = 0; y < scale; y++)
        {
            Element lastElem = elements[y, scale - 1];
            for (int x = scale - 2; x >= 0; x--)
            {
                if (elements[y, x] != null)
                {
                    elements[y, x].moveDirection = 4;
                    lastElem = elements[y, x].ReactWith(lastElem);
                }
            }
        }
    }


    /// <summary>
    /// Moves all Elements up and triggers reactions.
    /// </summary>
    private void MoveUp()
    {
        for(int x = 0; x < scale; x++)
        {
            Element lastElem = elements[0, x];
            for(int y = 1; y < scale; y++)
            {
                if (elements[y, x] != null)
                {
                    elements[y, x].moveDirection = 1;
                    lastElem = elements[y, x].ReactWith(lastElem);
                }
            }
        }
    }


    /// <summary>
    /// Moves all Elements down and triggers reactions.
    /// </summary>
    private void MoveDown()
    {
        for (int x = 0; x < scale; x++)
        {
            Element lastElem = elements[scale - 1, x];
            for (int y = scale - 2; y >= 0; y--)
            {
                if (elements[y, x] != null)
                {
                    elements[y, x].moveDirection = 2;
                    lastElem = elements[y, x].ReactWith(lastElem);
                }
            }
        }
    }


}
