using System.Collections;
using UnityEngine;

public class FireWorkController : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        this.gameObject.SetActive(false);
    }
}
