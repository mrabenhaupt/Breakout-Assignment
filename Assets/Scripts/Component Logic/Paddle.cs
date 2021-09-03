using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controls player/paddle movement and ball reflection angles
/// </summary>
public class Paddle : MonoBehaviour, IActiveContact
{
    private Camera cam;
    private float fixedYPosition;

    [SerializeField]
    private float minBounds = -5.76f, maxBounds = 5.76f;

    private void Start()
    {
        fixedYPosition = transform.position.y;
        cam = GameController.Instance.mainCamera;
    }


    // What: Gets mouse position and calculates world coordinates to set Paddle position based within min/maxBounds
    // Why: Main Input method, physics independant
    private void Update()
    {
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.x, cam.nearClipPlane));
        
        if(point.x > minBounds && point.x < maxBounds)
        {
            transform.position = new Vector2(point.x, fixedYPosition);
        }
        else if(point.x < minBounds)
        {
            transform.position = new Vector2(minBounds, fixedYPosition);
        }
        else if (point.x > maxBounds)
        {
            transform.position = new Vector2(maxBounds, fixedYPosition);
        }
    }

    //What: Calculates exit angle after collision with the ball
    //Note: Gets called by the ball
    public Vector2 Hit(Collision2D other)
    {
        ContactPoint2D cp = other.GetContact(0); 
        var sAngle = Vector2.SignedAngle(transform.position, cp.point);

        var degr = GetCustomReflectionAngle(cp.point, other.transform); 
        Vector2 dir = new Vector2(Mathf.Cos(degr * Mathf.Deg2Rad) * Mathf.Sign(sAngle), Mathf.Sin(degr * Mathf.Deg2Rad));

        
        EventManager.Instance.PaddleBounce();
        return dir.normalized;
    }

    //What: calculates the angle of the ball in relation to the paddle and returns a custom (reflection)angle
    //Why: Prevents accidental flat reflection angles that lead to almost endless bounces between walls
    private float GetCustomReflectionAngle(Vector2 contactPoint, Transform otherBody)
    {
        var impactangle = Vector2.Angle(contactPoint, otherBody.position);

        if (impactangle < 2)
        {
            return 80f;
        }
        else if (impactangle < 4 && impactangle >= 2)
        {
            return 60f;
        }
        else
        {
            return 30f;
        }
    }
}