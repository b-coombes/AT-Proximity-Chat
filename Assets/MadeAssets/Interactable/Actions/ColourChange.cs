using System.Collections.Generic;
using UnityEngine;
using Whisper.Samples;
using Random = System.Random;

public class ColourChange : MonoBehaviour
{
    public Material material;
    public MicrophoneDemo microphone;
    public ColourChange checks;
    public Door door;

    [Header("Changeable Variables")]
    public bool changeable = false;

    [Header("Do not change Variables")]
    public string colourCheck;

    private bool runOnce = false;

    private void OnTriggerStay(Collider collision)
    {
        if (changeable)
        {
            if (collision.transform.gameObject.name == "Player")
            {
                if (microphone.testText != "")
                {
                    ChangeColor(microphone.testText);
                }


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
                colourCheck = colorName;
            }
            else
            {
                print("Color not found: " + colorName);
            }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.gameObject.name == "Player" && changeable)
        {
            microphone.proxCheck = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.transform.gameObject.name == "Player" && changeable)
        {
            microphone.proxCheck = false;
        }
    }






    void Start()
    {
        if (changeable)
        {
            material.color = Color.white;
        }
        if (!changeable)
        {
            Random rnd = new();
            string[] colours = { "red", "green", "blue", "white", "black", "pink", "purple", "yellow" };
            int listNum = rnd.Next(colours.Length);
            ChangeColor(colours[listNum]);
            colourCheck = colours[listNum];
        }
    }


    void Update()
    {
        if (!changeable)
        {
            if(colourCheck == checks.colourCheck)
            {
                if (!runOnce)
                {
                    door.locks += 1;
                    runOnce = true;
                }
            }
            if (colourCheck != checks.colourCheck)
            {
                if (runOnce)
                {
                    door.locks -= 1;
                    runOnce = false;
                }
            }
        }
    }
}
