using UnityEngine;

public class AdController : MonoBehaviour
{
	private bool startAdShowed;

	private bool exitAdShowed;

	public static bool flag;

	private bool initialized;

	private GameObject menu_start;

	private void Start()
	{
		PlayerPrefs.SetInt("flag", 0);
		Debug.Log("timeSinceStart: " + timeSinceStart.SinceTime);
		menu_start = GameObject.Find("RateUsButton");
		if (timeSinceStart.SinceTime < 5f)
		{
			AdMediator.initAdNetworks();
			if (base.gameObject.name == "mainmenuad")
			{
				showAds();
			}
			initialized = true;
		}
		else
		{
			Debug.Log("GO:" + base.gameObject.name);
			if (base.gameObject.name == "mainmenuad")
			{
				showAds();
			}
		}
	}

	private void showAds()
	{
		Debug.Log("Remove Ads:" + PlayerPrefs.GetInt("ra"));
		if (PlayerPrefs.GetInt("ra") != 1)
		{
			AdMediator.showInterstitial();
		}
	}

	private void OnApplicationPause(bool paused)
	{
		Debug.Log("OnApplicationPause");
		if (!paused)
		{
			Debug.Log("AdMediator.getElapsedSeconds():" + AdMediator.getElapsedSeconds());
			if (AdMediator.getElapsedSeconds() > 2)
			{
				showAds();
			}
		}
	}

	private void LateUpdate()
	{
	}

	private void Update()
	{
	}

	private void ShowMoreApps()
	{
		AdMediator.showInterstitial();
	}
}
