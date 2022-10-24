using System;
using JetBrains.Annotations;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Snake : MonoBehaviour
{
    private int _length = 7;
    private int _high = 7;
    private int[,] _field;

    private int _snake1;
    private int _xOfSnakeHead = 0;
    private int _yOfSnakeHead = 0;

    private bool _upMoving = true;
    private bool _downMoving;
    private bool _leftMoving;
    private bool _rightMoving;

    private const int _snakeHead = 1;

    private const string _appleSymbol = "$";
    private const string _snakeSymbol = "S";
    private const string _emptySymbol = "0";

    private const int _positionUpdateSeconds = 1;

    private bool _isSnakeDeath;

    private int _lengthSnake = 1;

    private void ChooseApple()
    {
        int XofApple;
        int YofApple;
        do
        {
            YofApple = Random.Range(0, _high);
            XofApple = Random.Range(0, _length);
        } while (_field[XofApple, YofApple] != 0);


        _field[XofApple, YofApple] = -1;
    }

    private (int highCenter, int lengthCenter) FindCenter()
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
            centerHigh = ((_high - 1) / 2) + 1;
        }
        else
        {
            centerHigh = (_high / 2) - 1;
        }

        return (centerHigh, centerLength);
    }

    private void ChooseDirectionMove()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _upMoving = true;
            _downMoving = false;
            _leftMoving = false;
            _rightMoving = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _downMoving = true;
            _upMoving = false;
            _leftMoving = false;
            _rightMoving = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            _leftMoving = true;
            _upMoving = false;
            _downMoving = false;
            _rightMoving = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _rightMoving = true;
            _upMoving = false;
            _downMoving = false;
            _leftMoving = false;
        }
    }


    private void MoveSnake()
    {
        // moving
        if (_upMoving)
        {
            _yOfSnakeHead--;
        }

        if (_downMoving)
        {
            _yOfSnakeHead++;
        }

        if (_leftMoving)
        {
            _xOfSnakeHead++;
        }

        if (_rightMoving)
        {
            _xOfSnakeHead--;
        }
    }

    [SerializeField] private TextMeshProUGUI _text;

    private void ShowGame()
    {
        string a = null;
        string mapCondition = "";

        for (int y = 0; y < _high; y++)
        {
            if (y != 0)
            {
                mapCondition += "\n";
            }

            for (int x = 0; x < _length; x++)
            {
                if (_field[y, x] > 0)
                {
                    a = _snakeSymbol;
                }

                if (_field[y, x] == -1)
                {
                    a = _appleSymbol;
                }

                if (_field[y, x] == 0)
                {
                    a = _emptySymbol;
                }

                mapCondition += a;
            }
        }

        _text.text = mapCondition;
    }

    private void MakeLonger()
    {
        for (int y = 0; y < _high; y++)
        {
            for (int x = 0; x < _length; x++)
            {
                if (_field[y, x] > 0)
                {
                    _field[y, x]++;
                }
            }
        }
    }

    private void CheckIfDeathFirst()
    {
        if (_yOfSnakeHead > _high - 1 || _xOfSnakeHead > _length - 1|| _yOfSnakeHead<0|| _xOfSnakeHead<0)
        {
            _isSnakeDeath = true;
        }

        if (_isSnakeDeath)
        {
            die();
        }
    }
    private void CheckIfDeathSecond()
    {
        int currentSnakeLength = 0;
        for (int y = 0; y < _high; y++)
        {
            for (int x = 0; x < _length; x++)
            {
                if (_field[y, x] > 0)
                {
                    currentSnakeLength++;
                }
            }
        }

        if (currentSnakeLength < _lengthSnake)
        {
            _isSnakeDeath = true;
        }
        
        die();
        currentSnakeLength = 0;
        
    }

    private void die()
    {
        if (_isSnakeDeath)
        {
            _isSnakeDeath = false;
            Start();
        }
    }
    private void MakeShorter()
    {
        for (int y = 0; y < _high; y++)
        {
            for (int x = 0; x < _length; x++)
            {
                if (_field[y, x] > _lengthSnake)
                {
                    _field[y, x] = 0;
                }
            }
        }
    }

    private void EatApple()
    {
        bool isAplleExist = false;
        for (int y = 0; y < _high; y++)
        {
            for (int x = 0; x < _length; x++)
            {
                if (_field[y, x] == -1)
                {
                    isAplleExist = true;
                }
            }
        }

        if (isAplleExist == false)
        {
            ChooseApple();
            _lengthSnake++;
        }
    }


    private float _howMuchTimePassed = 0;

    private void Update()
    {
        ChooseDirectionMove();
        _howMuchTimePassed += Time.deltaTime;
        if (_howMuchTimePassed > _positionUpdateSeconds)
        {
            _howMuchTimePassed = 0;
            MoveSnake();
            CheckIfDeathFirst();
            MakeLonger();
            if (_field[_yOfSnakeHead,_xOfSnakeHead]==-1)
            {
                _field[_yOfSnakeHead, _xOfSnakeHead]=1;
            }
            else
            {
                _field[_yOfSnakeHead, _xOfSnakeHead]++;
            }
            MakeShorter();
            CheckIfDeathSecond();
            EatApple();
            ShowGame();
        }
    }

    private void Start()
    {
        _lengthSnake = 1;
        _field = new int[7, 7]
        {
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 }
        };
        (_yOfSnakeHead, _xOfSnakeHead) = FindCenter();
        ChooseApple();
    }
}