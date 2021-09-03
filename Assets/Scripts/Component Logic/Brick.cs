using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour, IPassiveContact
{
    public void Hit()
    {
        EventManager.Instance.BrickDestruction();
        Destroy(this.gameObject);
    }
}