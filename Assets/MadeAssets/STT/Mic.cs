using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Whisper.Utils;
using System.Collections;
using Button = UnityEngine.UI.Button;
using Toggle = UnityEngine.UI.Toggle;
using Unity.VisualScripting;

namespace Whisper.Samples
{

    public class MicrophoneDemo : MonoBehaviour
    {
        public WhisperManager whisper;
        public MicrophoneRecord microphoneRecord;
        public PlayerController playerController;
        public bool streamSegments = true;
        public bool printLanguage = true;

        public bool proxCheck = false;
        [Header("UI")]

        public Text outputText;
        public Text micStatus;

        public string testText;

        private string _buffer;

        private void Awake()
        {
            //whisper.OnNewSegment += OnNewSegment;

            microphoneRecord.OnRecordStop += OnRecordStop;

        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.V))
            {
                OnButtonPressed();
                playerController.vPressed = !playerController.vPressed;
            }
            if (playerController.vPressed)
            {
                if (proxCheck)
                {
                    micStatus.text = "ON";
                }
            }
            if (!playerController.vPressed)
            {
                micStatus.text = "OFF";
            }


        }


        private void OnVadChanged(bool vadStop)
        {
            microphoneRecord.vadStop = vadStop;
        }

        private void OnButtonPressed()
        {

                if (!microphoneRecord.IsRecording)
                {
                    microphoneRecord.StartRecord();
                }
                else
                {
                    microphoneRecord.StopRecord();
                }
            
        }
      

        private async void OnRecordStop(AudioChunk recordedAudio)
        {

            _buffer = "";

            var sw = new Stopwatch();
            sw.Start();

            var res = await whisper.GetTextAsync(recordedAudio.Data, recordedAudio.Frequency, recordedAudio.Channels);
            if (res == null || !outputText)
                return;



            var text = res.Result;
            if (text == " [BLANK_AUDIO]")
            {
                text = "(No input detected) ";
            }

            if (proxCheck)
            {
                outputText.text = text.ToUpper();
                outputText.text = outputText.text.Remove(outputText.text.Length - 1);
                testText = text.ToLower();
                testText = testText.Remove(testText.Length - 1);
                testText = testText.Substring(1);
            }
        }
        /*
        private void OnNewSegment(WhisperSegment segment)
        {
            if (!streamSegments || !outputText)
                return;

            _buffer += segment.Text;
            outputText.text = _buffer;//  + "...";


        }
        */
        public void TaskCompleted()
        {
            //outputText.text = "";
            testText = "";
        }
    }
}