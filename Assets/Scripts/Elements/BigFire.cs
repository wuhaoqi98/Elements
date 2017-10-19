using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFire : Element {

    
    private int lifeTime = 2;
    
    public void IncrementLifeTime()
    {
        lifeTime++;
    }

    public override Element ReactWith(Element other)
    {
        if (other != null)
        {
            switch (other.index)
            {
                case 0: // big fire + fire = big fire
                    gameManager.AddScore(5);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return gameManager.InstantiateElem(yPos, xPos, 4, moveTime);

                case 1: // big fire + water = fire
                    gameManager.AddScore(10);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return gameManager.InstantiateElem(yPos, xPos, 0, moveTime);

                case 2: // big fire + ice = gas
                    gameManager.AddScore(50);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return CheckPreviousElem();

                case 3: // big fire + wood = coal
                    gameManager.AddScore(10);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return gameManager.InstantiateElem(yPos, xPos, 5, moveTime);

                case 4: // big fire + big fire = big fire
                    gameManager.AddScore(5);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return gameManager.InstantiateElem(yPos, xPos, 4, moveTime);

                case 5: // big fire + coal = big fire
                    gameManager.AddScore(5);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return gameManager.InstantiateElem(yPos, xPos, 4, moveTime);

                case 7: // big fire + acid = fire
                    gameManager.AddScore(10);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return gameManager.InstantiateElem(yPos, xPos, 0, moveTime);

                case 8: // big fire + weak acid = fire
                    gameManager.AddScore(10);
                    Move(other.GetY(), other.GetX());
                    Destroy(gameObject, moveTime);
                    Destroy(other.gameObject, moveTime);
                    return gameManager.InstantiateElem(yPos, xPos, 0, moveTime);

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

   /*
    public override void Move(int y, int x)
    {
        base.Move(y, x);
        lifeTime--;
        if(lifeTime <= 0)
        {
            gameManager.InstantiateElem(y, x, 0, moveTime);
            Destroy(gameObject, moveTime);
            return;
        }
    }*/
}
