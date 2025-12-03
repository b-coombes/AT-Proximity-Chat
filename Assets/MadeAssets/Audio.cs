using UnityEngine;

public class Audio : MonoBehaviour
{
    bool runOnce;
    public AudioSource sound;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
  
            
        
    }

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
