using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Classic event manager, offers subscribale events ¯\_(ツ)_/¯
/// </summary>
public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public event Action onBrickDestruction;
    public event Action onWallBounce;
    public event Action onPaddleBounce;
    public event Action onBallDroppedDown;

    private void Awake()
    {
        Instance = this;
    }

    public void BrickDestruction()
    {
        if(onBrickDestruction != null)
        {
            onBrickDestruction();
        }
    }

    public void WallBounce()
    {
        if(onWallBounce != null)
        {
            onWallBounce();
        }
    }

    public void PaddleBounce()
    {
        if (onPaddleBounce != null)
        {
            onPaddleBounce();
        }
    }

    public void BallDroppedDown()
    {
        if (onBallDroppedDown != null)
        {
            onBallDroppedDown();
        }
    }
}