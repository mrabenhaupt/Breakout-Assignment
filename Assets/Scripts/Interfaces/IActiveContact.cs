using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActiveContact
{
    Vector2 Hit(Collision2D other);
}