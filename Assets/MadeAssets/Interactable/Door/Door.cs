using UnityEngine;
using Whisper.Samples;

public class Door : MonoBehaviour
{

    [Header("References")]
    public MicrophoneDemo microphone;
    
    [Header("Changeable Variables")]
    public int moveBy;
    public float speed = 1.0f;

    public bool puzzleDoor;
    public bool hold;
    public string stringToSearchFor;
    public string stringToSearchForFailsafe = "backup word";


    [Header("Do not change Variables")]
    public int puzzlePairs = 0;
    public int locks = 0;
    public bool moving = false;
    public bool opening = true;
    public bool stringCheckBool;



    private Vector3 startPos;
    private Vector3 endPos;
    private float delay = 0.0f;
    
    
    


    void Start()
    {
        startPos = transform.position;
        endPos = startPos;
        endPos.y += moveBy;

    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            
            if (opening)        //if door is oppening move towards open possition
            {
                MoveDoor(endPos);
                
            }
            else                //if door is closing move towards start possition
            {
                MoveDoor(startPos);
            }
        }      
    }

    void MoveDoor(Vector3 goalPos)
    {
        
        float dist = Vector3.Distance(transform.position, goalPos);     
        
        if (dist > .1f)         //if door isnt at target move
        {           
            transform.position = Vector3.Lerp(transform.position, goalPos, (speed / 10) * Time.deltaTime);
        }
        else
        {
            if (opening)
            {
                delay += Time.deltaTime;
                if (delay > 1.5f)
                {
                    if (!hold)
                    {
                        opening = false;
                    }
                }
                
            }
            else
            {

                moving = false;
                opening = true;

            }
        }
    }

    public bool Moving
    {
        get { return moving; }
        set { moving = value; }
    }



    private void OnTriggerStay(Collider collision)
    {
        if (collision.transform.gameObject.name == "Player")
        {
            
            if(microphone.testText.Contains(stringToSearchFor) | microphone.testText.Contains(stringToSearchForFailsafe))       //if target phrase
            {
                stringCheckBool = true;
                if (puzzleDoor)
                {
                    if (locks == puzzlePairs)
                    {
                        if (moving == false)
                        {
                            print("Opening second");
                            moving = true;
                            microphone.TaskCompleted();
                            microphone.DisplayText(stringToSearchFor);
                        }
                    }
                }
                else
                { 
                    if (moving == false)
                    {
                        print("Opening first");
                        moving = true;
                        microphone.TaskCompleted();
                        microphone.DisplayText(stringToSearchFor);
                    }
                }
            }
            
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

}

