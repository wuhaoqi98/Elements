using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour {

    public int index; // index of this Element
    [HideInInspector]
    public int moveDirection; // 0 for static, 1 up, 2 down, 3 left, 4 right
    [HideInInspector]
    public bool canMove = true; 

    protected float moveSpeed; // controlled by GameManager
    protected float moveTime; // controlled by GameManager
    protected int xPos, yPos; // current position
    protected GameManager gameManager;
    protected Animator animator;

    /// <summary>
    /// Is called when an Element is instantiated.
    /// </summary>
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        moveSpeed = gameManager.moveSpeed;
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Plays the animation when appearing.
    /// </summary>
    public void Appears()
    {
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }
        animator.SetTrigger("elementAppears");
    }

    /// <summary>
    /// Returns the x position.
    /// </summary>
    /// <returns></returns>
    public int GetX()
    {
        return xPos;
    }

    /// <summary>
    /// Returns the y position.
    /// </summary>
    /// <returns></returns>
    public int GetY()
    {
        return yPos;
    }

    /// <summary>
    /// Sets the x and y positions.
    /// </summary>
    /// <param name="y"></param>
    /// <param name="x"></param>
    public void SetPos(int y, int x)
    {
        yPos = y;
        xPos = x;
    }
    
    /// <summary>
    /// Starts a coroutine to move this Element.
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private IEnumerator MoveTo(Vector3 pos)
    {
        canMove = false;
        float sqrDis = (transform.position - pos).sqrMagnitude;
        while (sqrDis > float.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, moveSpeed * Time.deltaTime);

            sqrDis = (transform.position - pos).sqrMagnitude;
            yield return null;
        }
        canMove = true;
        moveDirection = 0;
    }
    
    /// <summary>
    /// Moves to a given postion.
    /// </summary>
    /// <param name="y"></param>
    /// <param name="x"></param>
    public virtual void Move(int y, int x)
    {
        moveTime = Mathf.Abs(y - yPos + x - xPos) / moveSpeed * 2;
        gameManager.elements[yPos, xPos] = null;
        StartCoroutine("MoveTo",gameManager.grids[y, x]);
        yPos = y;
        xPos = x;
        gameManager.elements[yPos, xPos] = this;
    }

    /// <summary>
    /// React with another Element.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual Element ReactWith(Element other)
    {
        if(other == null)
        {
            switch (moveDirection)
            {
                case 1: // up
                    Move(0, xPos);
                    break;
                case 2: // down
                    Move(gameManager.scale - 1, xPos);
                    break;
                case 3: // left
                    Move(yPos, 0);
                    break;
                case 4: // right
                    Move(yPos, gameManager.scale - 1);
                    break;
            }
            return this;
        }
        return null;
    }
    

    /// <summary>
    /// When reaction results in nothing, this method returns the possible 
    /// Element before the reaction position.
    /// </summary>
    /// <returns></returns>
    protected Element CheckPreviousElem()
    {
        int scale = gameManager.scale;
        switch (moveDirection)
        {
            case 1: // up
                if (yPos == 1)
                {
                    return gameManager.elements[0, xPos];
                }
                else if (yPos == 2)
                {
                    if (gameManager.elements[1, xPos] != null)
                        return gameManager.elements[1, xPos];
                    else
                        return gameManager.elements[0, xPos];
                }
                return null;

            case 2: // down
                if (yPos == scale - 2)
                {
                    return gameManager.elements[scale - 1, xPos];
                }
                else if (yPos == scale - 3)
                {
                    if (gameManager.elements[scale - 2, xPos] != null)
                        return gameManager.elements[scale - 2, xPos];
                    else
                        return gameManager.elements[scale - 1, xPos];
                }
                return null;

            case 3: // left
                if (xPos == 1)
                {
                    return gameManager.elements[yPos, 0];
                }
                else if (xPos == 2)
                {
                    if (gameManager.elements[yPos, 1] != null)
                        return gameManager.elements[yPos, 1];
                    else
                        return gameManager.elements[yPos, 0];
                }
                return null;

            case 4: // right
                if (xPos == scale - 2)
                {
                    return gameManager.elements[yPos, scale - 1];
                }
                else if (xPos == scale - 3)
                {
                    if (gameManager.elements[yPos, scale - 2] != null)
                        return gameManager.elements[yPos, scale - 2];
                    else
                        return gameManager.elements[yPos, scale - 1];
                }
                return null;
        }
        return null;
    }

   
}
