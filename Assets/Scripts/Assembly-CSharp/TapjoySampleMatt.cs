using System.Collections.Generic;
using UnityEngine;

public class TapjoySampleMatt : MonoBehaviour
{
	public GameObject receivingObject;

	private void Start()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			AndroidJNI.AttachCurrentThread();
		}
		TapjoyPlugin.EnableLogging(true);
		if (Application.platform == RuntimePlatform.Android)
		{
			TapjoyPlugin.RequestTapjoyConnect("6f7058f6-74ae-460f-87e9-ece1c47733c6", "DhQIfb6qmqkLzZOWEOWG");
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("TJC_OPTION_COLLECT_MAC_ADDRESS", "1");
			TapjoyPlugin.RequestTapjoyConnect("a065258f-5370-486b-a1e3-f21a49c05f3e", "83sR3Lvfb2qZVTXGBrLz", dictionary);
		}
		TapjoyPlugin.GetTapPoints();
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
		receivingObject.SendMessage("TJConnectSuccess");
	}

	public void HandleTapjoyConnectFailed()
	{
		Debug.Log("C#: HandleTapjoyConnectFailed");
		receivingObject.SendMessage("TJConnectFailed");
	}

	private void HandleGetTapPointsSucceeded(int points)
	{
		Debug.Log("C#: HandleGetTapPointsSucceeded: " + points);
		receivingObject.SendMessage("TJGetPointsSucceeded", TapjoyPlugin.QueryTapPoints());
	}

	public void HandleGetTapPointsFailed()
	{
		Debug.Log("C#: HandleGetTapPointsFailed");
		receivingObject.SendMessage("TJGetPointsFailed");
	}

	public void HandleSpendTapPointsSucceeded(int points)
	{
		Debug.Log("C#: HandleSpendTapPointsSucceeded: " + points);
		receivingObject.SendMessage("TJSpendPointsSuceeded", TapjoyPlugin.QueryTapPoints());
	}

	public void HandleSpendTapPointsFailed()
	{
		Debug.Log("C#: HandleSpendTapPointsFailed");
		receivingObject.SendMessage("TJSpendPointsFailed");
	}

	public void HandleAwardTapPointsSucceeded()
	{
		Debug.Log("C#: HandleAwardTapPointsSucceeded");
		receivingObject.SendMessage("TJAwardPointsSucceeded", TapjoyPlugin.QueryTapPoints());
	}

	public void HandleAwardTapPointsFailed()
	{
		Debug.Log("C#: HandleAwardTapPointsFailed");
		receivingObject.SendMessage("TJAwardPointsFailed");
	}

	public void HandleTapPointsEarned(int points)
	{
		Debug.Log("C#: CurrencyEarned: " + points);
		receivingObject.SendMessage("TJPointEarned", points);
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
		TapjoyPlugin.ShowDisplayAd();
		receivingObject.SendMessage("TJDisplayAdSucceeded");
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
		receivingObject.SendMessage("TJViewOpened");
	}

	public void HandleViewClosed(TapjoyViewType viewType)
	{
		Debug.Log("C#: HandleViewClosed of view type " + viewType);
		receivingObject.SendMessage("TJViewClosed");
	}

	public void HandleShowOffersFailed()
	{
		Debug.Log("C#: HandleShowOffersFailed");
		receivingObject.SendMessage("TJViewOffersFailed");
	}

	public void ShowTJOffers()
	{
		TapjoyPlugin.ShowOffers();
	}

	public void GetTJPoints()
	{
		TapjoyPlugin.GetTapPoints();
	}

	public void ShowDisplayAd()
	{
		TapjoyPlugin.GetDisplayAd();
	}

	public void HideDisplayAd()
	{
		TapjoyPlugin.HideDisplayAd();
	}

	public void AwardTJPoints(int points)
	{
		TapjoyPlugin.AwardTapPoints(points);
	}

	public void SpendTJPoints(int points)
	{
		TapjoyPlugin.SpendTapPoints(points);
	}
}
