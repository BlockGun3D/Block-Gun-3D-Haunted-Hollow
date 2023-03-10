using System;
using UnityEngine;

[Serializable]
public class Global : MonoBehaviour
{
	[NonSerialized]
	public static Vector2 defaultScreen = new Vector2(480f, 320f);

	[NonSerialized]
	public static GameManager gm;

	[NonSerialized]
	public static EnemyController em;

	[NonSerialized]
	public static PlayerController pm;

	[NonSerialized]
	public static WeaponManager wm;

	[NonSerialized]
	public static bool storeRetrieved;

	[NonSerialized]
	public static bool bannerShowing;

	[NonSerialized]
	public static GameObject tapPref;

	[NonSerialized]
	public static int adNetworkChoose = 1;

	[NonSerialized]
	public static bool popupEnabled;

	public EnemyController enemyManager;

	public PlayerController playerController;

	public WeaponManager weaponManager;

	public FirstPersonControlCustom fpsController;

	public GameObject tapjoyPrefab;

	public virtual void Start()
	{
		Application.targetFrameRate = 60;
		gm = GameManager.GetInstance();
		em = enemyManager;
		pm = playerController;
		wm = weaponManager;
		gm.LoadGameData();
		gm.SetFPSController(fpsController);
		if ((bool)tapjoyPrefab)
		{
			tapPref = tapjoyPrefab;
		}
		gm.openedCount++;
	}

	public virtual void OnApplicationPause(bool pauseStatus)
	{
		Debug.Log("MATT: OnApplicationPause - pauseStatus: " + pauseStatus);
		if (gm == null)
		{
			return;
		}
		if (!pauseStatus)
		{
			gm.SaveGameData();
			return;
		}
		gm.LoadGameData();
		if ((bool)tapPref)
		{
			tapPref.SendMessage("GetTJPoints");
		}
		if (gm.GetGameState() == GameState.PLAYING)
		{
			gm.SetGameState(GameState.PAUSED);
		}
	}

	public virtual void OnApplicationFocus(bool focusStatus)
	{
		Debug.Log("MATT: OnApplicationFocus - pauseStatus: " + focusStatus);
		if (gm == null)
		{
			return;
		}
		if (focusStatus)
		{
			gm.SaveGameData();
			return;
		}
		gm.LoadGameData();
		if ((bool)tapPref)
		{
			tapPref.SendMessage("GetTJPoints");
		}
		if (gm.GetGameState() == GameState.PLAYING)
		{
			gm.SetGameState(GameState.PAUSED);
		}
	}

	public virtual void OnApplicationQuit()
	{
		gm.SaveGameData();
	}

	public virtual void Update()
	{
		GameState gameState = gm.GetGameState();
		if (Input.GetKeyDown("escape"))
		{
			switch (gameState)
			{
			case GameState.START:
				AdMediator.showInterstitialExit();
				break;
			case GameState.PLAYING:
			case GameState.PAUSED_UPGRADES:
				gm.SetGameState(GameState.PAUSED);
				break;
			case GameState.PAUSED:
				gm.SetGameState(GameState.PLAYING);
				break;
			case GameState.TUTORIAL_LOOK:
			case GameState.TUTORIAL_MOVE:
			case GameState.TUTORIAL_FIRE:
			case GameState.TUTORIAL_RELOAD:
			case GameState.MENU_MAIN:
				gm.SetGameState(GameState.START);
				break;
			case GameState.SAVEME:
			case GameState.GET_BLOCKS_QUESTION:
				gm.SetGameState(GameState.DIED);
				break;
			case GameState.DIED:
			case GameState.MENU_UPGRADES:
				gm.SetGameState(GameState.MENU_MAIN);
				break;
			default:
				gm.SetGameState(GameState.LAST_STATE);
				break;
			}
		}
	}

	public static void ResetAndChangeState(GameState state)
	{
		pm.Reset();
		gm.SetReviveCost(1);
		gm.ResetLevelBlockCount();
		gm.ResetScore();
		gm.SetGameState(state);
	}

	public static void SaveGameData()
	{
		gm.SaveGameData();
		if ((bool)tapPref)
		{
			tapPref.SendMessage("GetTJPoints");
		}
	}

	public virtual void Main()
	{
	}
}
