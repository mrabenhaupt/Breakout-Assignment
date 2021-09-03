using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Regulates the balls speed and angle and triggers Hit events on collided objects
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.5f, deacceleration = -170f, turboSpeed = 45f;
    private Rigidbody2D rb;

    private void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        /*
        What: rapidly deaccelerates the ball back to it's default speed
        Why: paddle bounce is supposed to add extra speed
        */
        if(rb.velocity.magnitude > speed)
        {
            rb.AddForce(rb.velocity * deacceleration * Time.fixedDeltaTime);
        }
    }

    //What: Resets ball position
    //Why: Primarily Used by GameController for level reset
    public void ResetBall(Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    //What: Sets ball in motion towards the bottom/paddle
    //Why: Primarily Used by GameController for level reset 
    public void LaunchBall()
    {
        GetComponent<Rigidbody2D>().velocity = speed * Vector2.down;
    }

    //What: Checks collision for type and triggers method in collided object 
    //Why: Some objects interact with the ball while others just trigger gameplay events 
    private void OnCollisionEnter2D(Collision2D other)
    {
        IPassiveContact passiveContact = other.gameObject.GetComponent<IPassiveContact>();
        if (passiveContact != null)
        {
            passiveContact.Hit();
            return;
        }

        IActiveContact activeContact = other.gameObject.GetComponent<IActiveContact>();
        if (activeContact != null)
        {
            rb.velocity = activeContact.Hit(other) * turboSpeed;
            return;
        }
    }
}