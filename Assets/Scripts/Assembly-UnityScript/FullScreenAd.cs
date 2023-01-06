using System;
using UnityEngine;

[Serializable]
public class FullScreenAd : MonoBehaviour
{
	public GameObject chartboost;

	public GameObject rateUsPopup;

	[NonSerialized]
	private static float lastTimeAd;

	public virtual void Start()
	{
	}

	public virtual void FullScreenAdVoid(bool activated)
	{
		float time = Time.time;
	}

	public virtual void Main()
	{
	}
}
