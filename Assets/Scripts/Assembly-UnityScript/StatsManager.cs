using System;
using UnityEngine;

[Serializable]
public class StatsManager : MonoBehaviour
{
	public GUIText scoreText;

	public GUIText blocksText;

	public GUIText timeText;

	public virtual void ManageStats(bool stateIn)
	{
		if (stateIn)
		{
			scoreText.text = string.Empty + Global.gm.bestScore;
			blocksText.text = "Blocks: " + Global.gm.mostBlocks;
			float bestTime = Global.gm.bestTime;
			int num = (int)(bestTime % 60f);
			int num2 = (int)(bestTime / 60f);
			timeText.text = "Time: " + num2 + "m " + num + "s";
		}
	}

	public virtual void Main()
	{
	}
}
