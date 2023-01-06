using System;
using UnityEngine;

[Serializable]
public class StopMusic : MonoBehaviour
{
	public virtual void MusicPlay(bool start)
	{
		if (start)
		{
			GetComponent<AudioSource>().Play();
		}
		else
		{
			GetComponent<AudioSource>().Stop();
		}
	}

	public virtual void Main()
	{
	}
}
