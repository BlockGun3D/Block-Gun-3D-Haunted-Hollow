using UnityEngine;

public class timeSinceStart : MonoBehaviour
{
	public static float SinceTime;

	private void LateUpdate()
	{
		SinceTime = Time.time;
	}
}
