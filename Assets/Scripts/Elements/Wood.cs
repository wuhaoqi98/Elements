using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : Element {

    public override Element ReactWith(Element other)
    {
        if (other != null)
        {
            switch (other.index)
            {
                case 0: // wood + fire = coal
                    gameManager.AddScore(10);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return gameManager.InstantiateElem(yPos, xPos, 5, moveTime);

                case 4: // wood + big fire = coal
                    gameManager.AddScore(10);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return gameManager.InstantiateElem(yPos, xPos, 5, moveTime);

                case 7: // wood + acid = coal
                    gameManager.AddScore(10);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return gameManager.InstantiateElem(yPos, xPos, 5, moveTime);

                case 8: // wood + weak acid = coal
                    gameManager.AddScore(10);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return gameManager.InstantiateElem(yPos, xPos, 5, moveTime);

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
