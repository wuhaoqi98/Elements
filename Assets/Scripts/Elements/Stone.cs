using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Element {

    public override Element ReactWith(Element other)
    {
        if (other != null)
        {
            switch (other.index)
            {
                case 7: // stone + acid = gas
                    gameManager.AddScore(50);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return CheckPreviousElem();

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
