using UnityEngine;

public class RatePlugin : MonoBehaviour
{
	private static AndroidJavaObject jo;

	public static void show()
	{
		jo = new AndroidJavaObject("com.appholdings.ratedlg.MainActivity");
		jo.Call("show");
	}
}
