using UnityEngine;

public class Audio : MonoBehaviour
{
    bool runOnce;
    public AudioSource sound;

    public void PlaySound()
    {
        if (!runOnce)
        {
            sound.Play();
            print("ricking your roll");
            runOnce = true;
        }
    }
}
