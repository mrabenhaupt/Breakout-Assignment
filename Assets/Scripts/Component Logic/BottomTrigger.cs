using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BottomTrigger : MonoBehaviour, IPassiveContact
{
    public void Hit()
    {
        EventManager.Instance.BallDroppedDown();
    }
}