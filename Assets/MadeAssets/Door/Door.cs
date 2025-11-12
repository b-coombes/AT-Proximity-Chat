using UnityEngine;
using Whisper.Samples;

public class Door : MonoBehaviour
{

    public int moveBy;
    public float speed = 1.0f;

    public bool moving = false;
    public bool opening = true;
    private Vector3 startPos;
    private Vector3 endPos;
    private float delay = 0.0f;
    public string stringToSearchFor;

    public MicrophoneDemo microphone;


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
            transform.position = Vector3.Lerp(transform.position, goalPos, speed * Time.deltaTime);
        }
        else
        {
            if (opening)
            {
                delay += Time.deltaTime;
                if (delay > 1.5f)
                {
                    opening = false;
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
            print("check1");
            if(microphone.testText.Contains(stringToSearchFor))
            {
                print("check2");
                if (moving == false)
                {
                    print("check3");
                    moving = true;
                    microphone.TaskCompleted();
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

