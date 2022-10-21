using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class snake : MonoBehaviour
{
    public string textValue;
    public Text textElement;
    
    private int _length = 7;
    private int _high = 7;
    private int[,] _theField;

    private int _snake1;
    private int _XofSnakeHead = 0;
    private int _YofSnakeHead = 0;

    bool snakeMovingUP = false;
    bool snakeMovingDOWN = false;
    bool snakeMovingLEFT = false;
    bool snakeMovingRIGHT = false;

    private void ChooseApple()
    {
        int XofApple;
        int YofApple;
        do
        {
            YofApple = Random.Range(0, _high);
            XofApple = Random.Range(0, _length);
        } while (_theField[XofApple, YofApple] != 0);

        _theField[XofApple, YofApple] = -1;
    }

    private (int centerLength, int centerHigh) FindCenter()
    {
        int centerLength;
        if (_length % 2 != 0)
        {
            centerLength = ((_length - 1) / 2);
        }
        else
        {
            centerLength = (_length / 2) - 1;
        }

        int centerHigh;
        if (_high % 2 != 0)
        {
            centerHigh = ((_high - 1) / 2);
        }
        else
        {
            centerHigh = (_high / 2) - 1;
        }

        return (centerLength, centerHigh);
    }

    private void ChooseDirectionMove()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            snakeMovingUP = true;
            snakeMovingDOWN = false;
            snakeMovingLEFT = false;
            snakeMovingRIGHT = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            snakeMovingDOWN = true;
            snakeMovingUP = false;
            snakeMovingLEFT = false;
            snakeMovingRIGHT = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            snakeMovingLEFT = true;
            snakeMovingUP = false;
            snakeMovingDOWN = false;
            snakeMovingRIGHT = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            snakeMovingRIGHT = true;
            snakeMovingUP = false;
            snakeMovingDOWN = false;
            snakeMovingLEFT = false;
        }
    }


    private void SnakeMoving()
    {
        // moving
        if (snakeMovingUP)
        {
            _YofSnakeHead++;
        }

        if (snakeMovingDOWN)
        {
            _YofSnakeHead--;
        }

        if (snakeMovingLEFT)
        {
            _XofSnakeHead--;
        }

        if (snakeMovingRIGHT)
        {
            _XofSnakeHead++;
        }
    }


    private float howMuchTimePassed = 0;

    private void Update()
    {
        ChooseDirectionMove();
        howMuchTimePassed += Time.deltaTime;

        if (howMuchTimePassed > 2)
        {
            SnakeMoving();
            Debug.Log(_YofSnakeHead);
            howMuchTimePassed = 0;
        }
    }

    private void Start()
    {
        _theField = new int[,]
        {
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 }
        };
        
        (_YofSnakeHead, _XofSnakeHead) = FindCenter();
        _theField[_YofSnakeHead, _XofSnakeHead] = 1;
        
       
    }
}