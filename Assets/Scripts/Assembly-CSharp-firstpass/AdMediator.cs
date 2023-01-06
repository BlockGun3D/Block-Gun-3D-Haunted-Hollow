using UnityEngine;

public class AdMediator : MonoBehaviour
{
	private static AndroidJavaObject jo;

	public static void initAdNetworks()
	{
		jo = new AndroidJavaObject("com.fungamesandapps.admediator.AdMediator");
		jo.Call("initAdNetworksWithKey", 1, false);
	}

	public static int getElapsedSeconds()
	{
		return jo.Call<int>("getElapsedSeconds", new object[0]);
	}

	public static void showInterstitial()
	{
		jo.Call("showInterstitial", false);
	}

	public static void showInterstitialExit()
	{
		jo.Call("showInterstitial", true);
	}

	public static void cacheInterstitial()
	{
		jo.Call("cacheInterstitial");
	}

	public static void showVideo()
	{
		jo.Call("showVideo");
	}
}
