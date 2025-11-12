using System.Collections.Generic;
using UnityEngine;
using Whisper.Samples;

public class ColourChange : MonoBehaviour
{

    public Material material;


    public MicrophoneDemo microphone;


    
    
    private void OnTriggerStay(Collider collision)
    {
        if (collision.transform.gameObject.name == "Player")
        {
            if (microphone.testText != "")
            {
                ChangeColor(microphone.testText);
            }


        }
    }

    private Dictionary<string, Color> colorMap = new Dictionary<string, Color>
    {
        { "red", Color.red },
        { "green", Color.green },
        { "blue", Color.blue },
        { "white", Color.white },
        { "black", Color.black },
        { "clear", Color.clear },
        { "pink", Color.pink },
        { "purple", Color.purple },
        { "yellow", Color.yellow }
    };

    public void ChangeColor(string colorName)
    {
        if (colorMap.TryGetValue(colorName, out Color newColor))
        {
            GetComponent<Renderer>().material.color = newColor;
            microphone.TaskCompleted();
        }
        else
        {
            print("Color not found: " + colorName);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.gameObject.name == "Player")
        {
            microphone.proxCheck = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.transform.gameObject.name == "Player")
        {
            microphone.proxCheck = false;
        }
    }






    void Start()
    {
        material.color = Color.white;
    }


    void Update()
    {
        
    }
}
