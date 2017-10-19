using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : Element {

    public override Element ReactWith(Element other)
    {
        if (other != null)
        {
            switch (other.index)
            {
                case 0: // ice + fire = water
                    gameManager.AddScore(10);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return gameManager.InstantiateElem(yPos, xPos, 1, moveTime);

                case 1: // water + ice = 2 x ice
                    gameManager.AddScore(5);
                    switch (moveDirection)
                    {
                        case 1: // up
                            Move(other.GetY() + 1, xPos);
                            break;
                        case 2: // down
                            Move(other.GetY() - 1, xPos);
                            break;
                        case 3: // left
                            Move(yPos, other.GetX() + 1);
                            break;
                        case 4: // right
                            Move(yPos, other.GetX() - 1);
                            break;
                    }
                    Destroy(other.gameObject, moveTime);
                    gameManager.InstantiateElem(other.GetY(), other.GetX(), 2, moveTime);
                    return this;

                case 4: // ice + big fire = gas
                    gameManager.AddScore(50);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return CheckPreviousElem();

                case 7: // ice + acid = weak acid
                    gameManager.AddScore(10);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return gameManager.InstantiateElem(yPos, xPos, 8, moveTime);

                case 8: // ice + weak acid = water
                    gameManager.AddScore(10);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return gameManager.InstantiateElem(yPos, xPos, 1, moveTime);

                default:
                    switch (moveDirection)
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
