using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVRemote : MonoBehaviour
{
    [Tooltip("The ball that will collide with the remote to turn on the TV.")]
    public GameObject triggerBall;
    [Tooltip("The TV GameObject whose particle system will play when the trigger ball collides with the remote.")]
    public GameObject television;
    // Becomes true the first time the trigger ball collides with the remote
    private bool hasBeenTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != triggerBall || hasBeenTriggered)
        {
            return;
        }

        hasBeenTriggered = true;
        television.GetComponent<ParticleSystem>().Play();
    }
}
