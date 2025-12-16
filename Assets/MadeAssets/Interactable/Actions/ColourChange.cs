using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using Whisper.Samples;
using Random = System.Random;

public class ColourChange : MonoBehaviour
{
    [Header("References")]
    public Material material;
    public MicrophoneDemo microphone;
    public ColourChange checks;
    public Door door;

    [Header("Changeable Variables")]
    public bool changeable = false;

    [Header("Do not change Variables")]
    public string colorCheck;

    public bool matching;
    
    
    
    
    
    
    
    
    
    
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

    private static readonly Dictionary<string, Color> colorDictionary = new()       //dictionary to check colour
    {
        { "red", Color.red },
        { "green", Color.green },
        { "blue", Color.blue },
        { "black", Color.black },
        { "pink", Color.pink },
        { "yellow", Color.yellow }
    };

    private static readonly Dictionary<string, string> colorCheckDictionary = new()     //dictionary to catch translation errors
    {
        { "rad", "red" },
        { "right","red" },
        { "pinkt", "pink" },
        { "yello", "yellow" },
        { "hello", "blue" }
    };

    public void ChangeColor(string colorName)
    {
        if (colorDictionary.TryGetValue(colorName, out Color newColor))         //if first dictionary match
        {
            GetComponent<Renderer>().material.color = newColor;
            microphone.TaskCompleted();
            colorCheck = colorName;
            print("Colour: " + colorName);
        }
        else
        {
            if (colorCheckDictionary.TryGetValue(colorName, out string colorTranslateErrorCatch))           //if second dictionary match
            {
                colorDictionary.TryGetValue(colorTranslateErrorCatch, out Color newColor2);
                GetComponent<Renderer>().material.color = newColor2;
                microphone.TaskCompleted();
                colorCheck = colorTranslateErrorCatch;
                print("String input: " + colorName + ", Colour out: " + colorTranslateErrorCatch);
            }
            else {                                                                                          //else no matches
                print("Colour not found: " + colorName);
                microphone.testText = "";
            } 
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
            string[] colours = { "red", "green", "blue", "black", "pink", "yellow" };
            int listNum = rnd.Next(colours.Length);
            ChangeColor(colours[listNum]);
            colorCheck = colours[listNum];
            door.puzzlePairs += 1;
        }
    }


    void Update()
    {
        if (!changeable)
        {
            if(colorCheck == checks.colorCheck)
            {
                if (!runOnce)
                {
                    door.locks += 1;
                    runOnce = true;
                    matching = true;
                    microphone.DisplayText(colorCheck);
                }
            }
            if (colorCheck != checks.colorCheck)
            {
                if (runOnce)
                {
                    door.locks -= 1;
                    runOnce = false;
                    matching = false;
                }
            }
        }
    }
}
