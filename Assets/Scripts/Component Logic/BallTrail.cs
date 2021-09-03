using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Turns ball trail on/off based on the balls contacts with other obejcts
/// </summary>
public class BallTrail : MonoBehaviour
{
    public ParticleSystem particleSys;
    private void Start()
    {
        EventManager.Instance.onBrickDestruction += OnWallBounce;
        EventManager.Instance.onWallBounce += OnWallBounce;
        EventManager.Instance.onPaddleBounce += OnPaddleBounce;

        particleSys = GetComponent<ParticleSystem>();
    }

    private void OnWallBounce()
    {
        particleSys.Stop();
    }

    private void OnPaddleBounce()
    {
        particleSys.Play();
    }
}