using UnityEngine;
using System.IO;

public class MicrophoneRecoder : MonoBehaviour
{
    AudioSource audioSource; // AudioSource 컴포넌트를 저장할 변수
    AudioClip recording; // 녹음된 오디오 클립을 저장할 변수

    bool isRecording = false; // 현재 녹음 중인지 여부를 나타내는 변수

    //마이크 목록
    string[] micList;
    //마이크 선택 idx
    int micIdx;
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트 가져오기

        micList = Microphone.devices;
        for(int i = 0; i < micList.Length; i++)
        {
            if(micList[i].Contains("Headset Microphone"))
            {
                micIdx = i;
                break;
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartRecording();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StopRecordingAndSave();
        }

    }

    public void StartRecording()
    {
        if (!Microphone.IsRecording(micList[micIdx])) // 현재 녹음 중이 아니라면
        {
            recording = Microphone.Start(micList[micIdx], true, 10, 44100); // 마이크로폰 녹음 시작
            isRecording = true; // 녹음 중 플래그 설정
        }
    }

    public void StopRecordingAndSave()
    {
        if (isRecording) // 녹음 중이었다면
        {
            Microphone.End(micList[micIdx]); // 마이크로폰 녹음 종료
            isRecording = false; // 녹음 중 플래그 해제

            string filePath = Path.Combine(Application.dataPath, "TEST.wav"); // 저장할 파일 경로 설정
            SavWav.Save(filePath, recording); // WAV 파일로 저장
            Debug.Log("Recording saved to: " + filePath); // 저장된 파일 경로를 로그로 출력
        }
    }
}