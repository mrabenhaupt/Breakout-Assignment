using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class holding references to Audiosources and listening for related events
/// </summary>
public class AudioController : MonoBehaviour
{
    public AudioSource paddleSound;
    public AudioSource wallSound;
    public AudioSource brickSound;

    private void Start()
    {
        EventManager.Instance.onBrickDestruction += OnBrickDestruction;
        EventManager.Instance.onWallBounce += OnWallBounce;
        EventManager.Instance.onPaddleBounce += OnPaddleBounce;
    }
    private void OnBrickDestruction()
    {
        brickSound.Play();
    }
    private void OnWallBounce()
    {
        wallSound.Play();
    }
    private void OnPaddleBounce()
    {
        paddleSound.Play();
    }
}