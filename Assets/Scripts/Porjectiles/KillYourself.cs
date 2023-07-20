using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillYourself : MonoBehaviour
{
    private bool isQuitting;

    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    public void KillMyself()
    {
        if (!isQuitting)
        {
            Destroy(gameObject);
        }
    }
}
