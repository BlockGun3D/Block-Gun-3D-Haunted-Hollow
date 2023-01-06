using System;
using UnityEngine;

[Serializable]
public class PausingManager : MonoBehaviour
{
	private float timePaused;

	public AudioSource music;

	public virtual void PauseGamePlay(bool pausing)
	{
		if (pausing)
		{
			timePaused = Time.time;
			if ((bool)music && music.isPlaying)
			{
				music.Pause();
			}
		}
		else
		{
			Global.gm.SubtractPauseTime(Time.time - timePaused);
			if ((bool)music && Global.gm.GetGameState() == GameState.PLAYING)
			{
				music.Play();
			}
		}
	}

	public virtual void StartLevelTimer(bool start)
	{
		if (start)
		{
			Global.gm.setTimeLevelStarted(Time.time);
			if ((bool)music)
			{
				music.Stop();
				music.Play();
			}
		}
	}

	public virtual void Main()
	{
	}
}
