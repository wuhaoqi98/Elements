using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coal : Element {

    public override Element ReactWith(Element other)
    {
        if (other != null)
        {
            switch (other.index)
            {
                case 0: // coal + fire = big fire
                    gameManager.AddScore(10);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return gameManager.InstantiateElem(yPos, xPos, 4, moveTime);

                case 4: // coal + big fire = big fire
                    gameManager.AddScore(5);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return gameManager.InstantiateElem(yPos, xPos, 4, moveTime);

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
