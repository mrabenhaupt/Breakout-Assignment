using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IPassiveContact
{
    public void Hit()
    {
        EventManager.Instance.WallBounce();
    }
}