using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Element
{

    public override Element ReactWith(Element other)
    {
        if(other != null)
        {
            switch (other.index)
            {
                case 0: // water + fire = gas
                    gameManager.AddScore(20);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return CheckPreviousElem();

                case 2: // water + ice = 2 x ice
                    gameManager.AddScore(5);
                    Move(other.GetY(), other.GetX());
                    gameManager.elements[yPos, xPos] = other;
                    Destroy(gameObject, moveTime / 1.5f);
                    switch (moveDirection)
                    {
                        case 1: // up
                            return gameManager.InstantiateElem(yPos + 1, xPos, 2, moveTime / 1.5f);
                        case 2: // down
                            return gameManager.InstantiateElem(yPos - 1, xPos, 2, moveTime / 1.5f);
                        case 3: // left
                            return gameManager.InstantiateElem(yPos, xPos + 1, 2, moveTime / 1.5f);
                        case 4: // right
                            return gameManager.InstantiateElem(yPos, xPos - 1, 2, moveTime / 1.5f);
                    }
                    break;

                case 4: // water + big fire = fire
                    gameManager.AddScore(10);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return gameManager.InstantiateElem(yPos, xPos, 0, moveTime);

                case 7: // water + acid = weak acid
                    gameManager.AddScore(10);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return gameManager.InstantiateElem(yPos, xPos, 8, moveTime);

                case 8: // water + weak acid = water
                    gameManager.AddScore(5);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return gameManager.InstantiateElem(yPos, xPos, 1, moveTime);

                default:
                    switch(moveDirection)
                    {
                        case 1: // up
                            Move(other.GetY() + 1, xPos);
                            return this;
                        case 2: // down
                            Move(other.GetY() - 1, xPos);
                            return this;
                        case 3: // left
                            Move(yPos, other.GetX() + 1);
                            return this;
                        case 4: // right
                            Move(yPos, other.GetX() - 1);
                            return this;
                    }
                    break;
            }
        }
        return base.ReactWith(other);
    }

}
