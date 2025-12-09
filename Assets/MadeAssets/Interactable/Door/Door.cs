using UnityEngine;
using Whisper.Samples;

public class Door : MonoBehaviour
{

    public int moveBy;
    public float speed = 1.0f;

    public bool puzzleDoor;
    public int puzzlePairs = 0;
    public int locks = 0;

    public bool moving = false;
    public bool opening = true;
    public bool hold;
    private Vector3 startPos;
    private Vector3 endPos;
    private float delay = 0.0f;
    public string stringToSearchFor;
    public string stringToSearchForFailsafe = "backup word";
    public bool stringCheckBool;

    public MicrophoneDemo microphone;
    public Audio sound;

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
            
            if (opening)
            {
                MoveDoor(endPos);
                
            }
            else
            {
                MoveDoor(startPos);
            }
        }      
    }

    void MoveDoor(Vector3 goalPos)
    {
        
        float dist = Vector3.Distance(transform.position, goalPos);
        
        if (dist > .1f)
        {
            transform.position = Vector3.Lerp(transform.position, goalPos, (speed / 10) * Time.deltaTime);
            if (puzzleDoor)
            {
                sound.PlaySound();
            }
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
            
            if(microphone.testText.Contains(stringToSearchFor) | microphone.testText.Contains(stringToSearchForFailsafe))
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
                            microphone.displayText(stringToSearchFor);
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
                        microphone.displayText(stringToSearchFor);
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

