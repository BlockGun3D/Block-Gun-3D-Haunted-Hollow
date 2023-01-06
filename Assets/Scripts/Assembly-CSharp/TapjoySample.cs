using System.Collections.Generic;
using UnityEngine;

public class TapjoySample : MonoBehaviour
{
	private string tapPointsLabel = string.Empty;

	private bool autoRefresh;

	private bool openingFullScreenAd;

	private void Start()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			AndroidJNI.AttachCurrentThread();
		}
		TapjoyPlugin.EnableLogging(true);
		if (Application.platform == RuntimePlatform.Android)
		{
			TapjoyPlugin.RequestTapjoyConnect("bba49f11-b87f-4c0f-9632-21aa810dd6f1", "yiQIURFEeKm0zbOggubu");
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("TJC_OPTION_COLLECT_MAC_ADDRESS", "1");
			TapjoyPlugin.RequestTapjoyConnect("13b0ae6a-8516-4405-9dcf-fe4e526486b2", "XHdOwPa8de7p4aseeYP0", dictionary);
		}
		TapjoyPlugin.GetTapPoints();
		TapjoyPlugin.GetDisplayAd();
	}

	private void Awake()
	{
		Debug.Log("C#: Awaking and adding Tapjoy Events");
		TapjoyPlugin.connectCallSucceeded += HandleTapjoyConnectSuccess;
		TapjoyPlugin.connectCallFailed += HandleTapjoyConnectFailed;
		TapjoyPlugin.getTapPointsSucceeded += HandleGetTapPointsSucceeded;
		TapjoyPlugin.getTapPointsFailed += HandleGetTapPointsFailed;
		TapjoyPlugin.spendTapPointsSucceeded += HandleSpendTapPointsSucceeded;
		TapjoyPlugin.spendTapPointsFailed += HandleSpendTapPointsFailed;
		TapjoyPlugin.awardTapPointsSucceeded += HandleAwardTapPointsSucceeded;
		TapjoyPlugin.awardTapPointsFailed += HandleAwardTapPointsFailed;
		TapjoyPlugin.tapPointsEarned += HandleTapPointsEarned;
		TapjoyPlugin.getFullScreenAdSucceeded += HandleGetFullScreenAdSucceeded;
		TapjoyPlugin.getFullScreenAdFailed += HandleGetFullScreenAdFailed;
		TapjoyPlugin.getDisplayAdSucceeded += HandleGetDisplayAdSucceeded;
		TapjoyPlugin.getDisplayAdFailed += HandleGetDisplayAdFailed;
		TapjoyPlugin.videoAdStarted += HandleVideoAdStarted;
		TapjoyPlugin.videoAdFailed += HandleVideoAdFailed;
		TapjoyPlugin.videoAdCompleted += HandleVideoAdCompleted;
		TapjoyPlugin.viewOpened += HandleViewOpened;
		TapjoyPlugin.viewClosed += HandleViewClosed;
		TapjoyPlugin.showOffersFailed += HandleShowOffersFailed;
	}

	private void OnDisable()
	{
		Debug.Log("C#: Disabling and removing Tapjoy Events");
		TapjoyPlugin.connectCallSucceeded -= HandleTapjoyConnectSuccess;
		TapjoyPlugin.connectCallFailed -= HandleTapjoyConnectFailed;
		TapjoyPlugin.getTapPointsSucceeded -= HandleGetTapPointsSucceeded;
		TapjoyPlugin.getTapPointsFailed -= HandleGetTapPointsFailed;
		TapjoyPlugin.spendTapPointsSucceeded -= HandleSpendTapPointsSucceeded;
		TapjoyPlugin.spendTapPointsFailed -= HandleSpendTapPointsFailed;
		TapjoyPlugin.awardTapPointsSucceeded -= HandleAwardTapPointsSucceeded;
		TapjoyPlugin.awardTapPointsFailed -= HandleAwardTapPointsFailed;
		TapjoyPlugin.tapPointsEarned -= HandleTapPointsEarned;
		TapjoyPlugin.getFullScreenAdSucceeded -= HandleGetFullScreenAdSucceeded;
		TapjoyPlugin.getFullScreenAdFailed -= HandleGetFullScreenAdFailed;
		TapjoyPlugin.getDisplayAdSucceeded -= HandleGetDisplayAdSucceeded;
		TapjoyPlugin.getDisplayAdFailed -= HandleGetDisplayAdFailed;
		TapjoyPlugin.videoAdStarted -= HandleVideoAdStarted;
		TapjoyPlugin.videoAdFailed -= HandleVideoAdFailed;
		TapjoyPlugin.videoAdCompleted -= HandleVideoAdCompleted;
		TapjoyPlugin.viewOpened -= HandleViewOpened;
		TapjoyPlugin.viewClosed -= HandleViewClosed;
		TapjoyPlugin.showOffersFailed -= HandleShowOffersFailed;
	}

	public void HandleTapjoyConnectSuccess()
	{
		Debug.Log("C#: HandleTapjoyConnectSuccess");
	}

	public void HandleTapjoyConnectFailed()
	{
		Debug.Log("C#: HandleTapjoyConnectFailed");
	}

	private void HandleGetTapPointsSucceeded(int points)
	{
		Debug.Log("C#: HandleGetTapPointsSucceeded: " + points);
		tapPointsLabel = "Total TapPoints: " + TapjoyPlugin.QueryTapPoints();
	}

	public void HandleGetTapPointsFailed()
	{
		Debug.Log("C#: HandleGetTapPointsFailed");
	}

	public void HandleSpendTapPointsSucceeded(int points)
	{
		Debug.Log("C#: HandleSpendTapPointsSucceeded: " + points);
		tapPointsLabel = "Total TapPoints: " + TapjoyPlugin.QueryTapPoints();
	}

	public void HandleSpendTapPointsFailed()
	{
		Debug.Log("C#: HandleSpendTapPointsFailed");
	}

	public void HandleAwardTapPointsSucceeded()
	{
		Debug.Log("C#: HandleAwardTapPointsSucceeded");
		tapPointsLabel = "Total TapPoints: " + TapjoyPlugin.QueryTapPoints();
	}

	public void HandleAwardTapPointsFailed()
	{
		Debug.Log("C#: HandleAwardTapPointsFailed");
	}

	public void HandleTapPointsEarned(int points)
	{
		Debug.Log("C#: CurrencyEarned: " + points);
		tapPointsLabel = "Currency Earned: " + points;
		TapjoyPlugin.ShowDefaultEarnedCurrencyAlert();
	}

	public void HandleGetFullScreenAdSucceeded()
	{
		Debug.Log("C#: HandleGetFullScreenAdSucceeded");
		TapjoyPlugin.ShowFullScreenAd();
	}

	public void HandleGetFullScreenAdFailed()
	{
		Debug.Log("C#: HandleGetFullScreenAdFailed");
	}

	public void HandleGetDisplayAdSucceeded()
	{
		Debug.Log("C#: HandleGetDisplayAdSucceeded");
		if (!openingFullScreenAd)
		{
			TapjoyPlugin.ShowDisplayAd();
		}
	}

	public void HandleGetDisplayAdFailed()
	{
		Debug.Log("C#: HandleGetDisplayAdFailed");
	}

	public void HandleVideoAdStarted()
	{
		Debug.Log("C#: HandleVideoAdStarted");
	}

	public void HandleVideoAdFailed()
	{
		Debug.Log("C#: HandleVideoAdFailed");
	}

	public void HandleVideoAdCompleted()
	{
		Debug.Log("C#: HandleVideoAdCompleted");
	}

	public void HandleViewOpened(TapjoyViewType viewType)
	{
		Debug.Log("C#: HandleViewOpened of view type " + viewType);
		openingFullScreenAd = true;
	}

	public void HandleViewClosed(TapjoyViewType viewType)
	{
		Debug.Log("C#: HandleViewClosed of view type " + viewType);
		openingFullScreenAd = false;
	}

	public void HandleShowOffersFailed()
	{
		Debug.Log("C#: HandleShowOffersFailed");
	}

	public void ResetTapPointsLabel()
	{
		tapPointsLabel = "Updating Tap Points...";
	}

	private void OnGUI()
	{
		if (!openingFullScreenAd)
		{
			GUIStyle gUIStyle = new GUIStyle();
			gUIStyle.alignment = TextAnchor.MiddleCenter;
			gUIStyle.normal.textColor = Color.white;
			gUIStyle.wordWrap = true;
			float num = Screen.width / 2;
			float num2 = 60f;
			float num3 = 300f;
			float height = 50f;
			float num4 = 20f;
			float num5 = 100f;
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Application.Quit();
			}
			GUI.Label(new Rect(num - 200f, num5, 400f, 25f), "Tapjoy Connect Sample App", gUIStyle);
			num5 += num4 + 10f;
			if (GUI.Button(new Rect(num - num3 / 2f, num5, num3, height), "Show Offers"))
			{
				TapjoyPlugin.ShowOffers();
			}
			num5 += num2;
			if (GUI.Button(new Rect(num - num3 / 2f, num5, num3, height), "Get Display Ad"))
			{
				TapjoyPlugin.GetDisplayAd();
			}
			num5 += num2;
			if (GUI.Button(new Rect(num - num3 / 2f, num5, num3, height), "Hide Display Ad"))
			{
				TapjoyPlugin.HideDisplayAd();
			}
			num5 += num2;
			if (GUI.Button(new Rect(num - num3 / 2f, num5, num3, height), "Toggle Display Ad Auto-Refresh"))
			{
				autoRefresh = !autoRefresh;
				TapjoyPlugin.EnableDisplayAdAutoRefresh(autoRefresh);
			}
			num5 += num2;
			if (GUI.Button(new Rect(num - num3 / 2f, num5, num3, height), "Show FullScreen Ad"))
			{
				TapjoyPlugin.GetFullScreenAd();
			}
			num5 += num2;
			if (GUI.Button(new Rect(num - num3 / 2f, num5, num3, height), "Get Tap Points"))
			{
				TapjoyPlugin.GetTapPoints();
				ResetTapPointsLabel();
			}
			num5 += num2;
			if (GUI.Button(new Rect(num - num3 / 2f, num5, num3, height), "Spend Tap Points"))
			{
				TapjoyPlugin.SpendTapPoints(10);
				ResetTapPointsLabel();
			}
			num5 += num2;
			if (GUI.Button(new Rect(num - num3 / 2f, num5, num3, height), "Award Tap Points"))
			{
				TapjoyPlugin.AwardTapPoints(10);
				ResetTapPointsLabel();
			}
			num5 += num4;
			GUI.Label(new Rect(num - 200f, num5, 400f, 150f), tapPointsLabel, gUIStyle);
		}
	}
}
