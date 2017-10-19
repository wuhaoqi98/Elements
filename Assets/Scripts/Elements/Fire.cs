using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Element {

    public override Element ReactWith(Element other)
    {
        if (other != null)
        {
            switch (other.index)
            {
                case 0: // fire + fire = big fire
                    gameManager.AddScore(5);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return gameManager.InstantiateElem(yPos, xPos, 4, moveTime);

                case 1: // fire + water = gas
                    gameManager.AddScore(20);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return CheckPreviousElem();

                case 2: // fire + ice = water
                    gameManager.AddScore(10);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return gameManager.InstantiateElem(yPos, xPos, 1, moveTime);

                case 3: // fire + wood = coal
                    gameManager.AddScore(10);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return gameManager.InstantiateElem(yPos, xPos, 5, moveTime);

                case 4: // fire + big fire = big fire
                    gameManager.AddScore(5);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return gameManager.InstantiateElem(yPos, xPos, 4, moveTime);

                case 5: // fire + coal = big fire
                    gameManager.AddScore(10);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return gameManager.InstantiateElem(yPos, xPos, 4, moveTime);

                case 7: // fire + acid = gas
                    gameManager.AddScore(20);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return CheckPreviousElem();

                case 8: // fire + weak acid = gas
                    gameManager.AddScore(20);
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
