using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoder : MonoBehaviour
{
	AudioClip recordClip;

	void StartRecordMicrophone()
	{
		recordClip = Microphone.Start("Built-in Microphone", true, 100, 44100);
		print("되고 있는 거니?");
	}

	void StopRecordMicrophone()
	{
		int lastTime = Microphone.GetPosition(null);

		if (lastTime == 0)
			return;
		else
		{
			Microphone.End(Microphone.devices[0]);

			float[] samples = new float[recordClip.samples];

			recordClip.GetData(samples, 0);

			float[] cutSamples = new float[lastTime];

			Array.Copy(samples, cutSamples, cutSamples.Length - 1);

			recordClip = AudioClip.Create("TEST", cutSamples.Length, 1, 44100, false);

			recordClip.SetData(cutSamples, 0);
		}
	}
}