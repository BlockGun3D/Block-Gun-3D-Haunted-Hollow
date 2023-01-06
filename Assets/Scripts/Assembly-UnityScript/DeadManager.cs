using System;
using UnityEngine;

[Serializable]
public class DeadManager : MonoBehaviour
{
	public GUIText scoreText;

	public GUIText blocksText;

	public GUIText timeText;

	public GUIText newBest;

	public AudioSource stopMusic;

	public virtual void DeadState(bool died)
	{
		if (died)
		{
			scoreText.text = "Score: " + Global.gm.GetScore();
			blocksText.text = "Blocks: " + Global.gm.GetLevelBlockCount(BlockType.GREEN);
			int num = (int)Global.gm.GetTimeInLevel();
			int num2 = num % 60;
			int num3 = num / 60;
			timeText.text = "Time: " + num3 + "m " + num2 + "s";
			if (Global.gm.SubmitScore())
			{
				newBest.color = new Color(1f, 1f, 1f, 1f);
			}
			else
			{
				newBest.color = new Color(1f, 1f, 1f, 0f);
			}
			Global.gm.SaveGameData();
			stopMusic.Stop();
		}
	}

	public virtual void Main()
	{
	}
}
