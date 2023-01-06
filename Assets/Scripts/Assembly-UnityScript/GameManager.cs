using System;
using Boo.Lang.Runtime;
using UnityEngine;

[Serializable]
public class GameManager
{
	[NonSerialized]
	private static GameManager Instance;

	public int pressedRate;

	public int openedCount;

	public int ratePopupShown;

	private GameState[] stateStack;

	private int stateStackIndx;

	private GameState gameState;

	private float lastTimeChanged;

	private int[] blockCountTotal;

	private int[] blockCountLevel;

	private int reviveCost;

	private int score;

	private float timeLevelStarted;

	public int bestScore;

	public int mostBlocks;

	public float bestTime;

	private int showAds;

	public int pistolLevel;

	public int ak47Level;

	public int shotgunLevel;

	public int uziLevel;

	public int armorLevel;

	public int luckLevel;

	private int blockDoublerOn;

	private int soundOn;

	private int invertY;

	private FirstPersonControlCustom fpsController;

	private int viewSensitivity;

	public int waveToStartOn;

	private int tutorialDone;

	private GameManager()
	{
		reviveCost = 1;
		showAds = 1;
		pistolLevel = 1;
		shotgunLevel = 1;
		armorLevel = 1;
		luckLevel = 1;
		soundOn = 1;
		viewSensitivity = 5;
	}

	public static GameManager GetInstance()
	{
		if (RuntimeServices.EqualityOperator(Instance, null))
		{
			Instance = new GameManager();
			Instance.Start();
		}
		return Instance;
	}

	public virtual void Start()
	{
		blockCountLevel = new int[2];
		blockCountTotal = new int[2];
		stateStack = new GameState[30];
		blockCountTotal[1] = 1;
	}

	public virtual void SetGameState(GameState state)
	{
		if (this.gameState == state || !(Time.time - lastTimeChanged >= 0.5f))
		{
			return;
		}
		lastTimeChanged = Time.time;
		switch (state)
		{
		case GameState.PLAYING:
			stateStackIndx = 0;
			this.gameState = state;
			break;
		case GameState.LAST_STATE:
		{
			stateStackIndx--;
			GameState gameState = stateStack[stateStackIndx];
			this.gameState = ((gameState == this.gameState) ? stateStack[--stateStackIndx] : gameState);
			break;
		}
		default:
			if (this.gameState != GameState.PAUSED_QUIT_TO_MENU && this.gameState != GameState.PAUSED_RESTART && this.gameState != GameState.GET_BLOCKS_QUESTION && this.gameState != GameState.GET_BLOCKS_QUESTION_MENU && this.gameState != GameState.PURCHASE_SUCCESS && this.gameState != GameState.NO_STORE_ACCESS && this.gameState != GameState.RETRIEVING_STORE && this.gameState != GameState.MENU_GET_BLOCKS)
			{
				stateStack[stateStackIndx] = this.gameState;
				stateStackIndx++;
			}
			this.gameState = state;
			break;
		}
		if (state == GameState.PAUSED || state == GameState.DIED)
		{
			AdMediator.showInterstitial();
		}
	}

	public virtual GameState GetGameState()
	{
		return gameState;
	}

	public virtual void ResetLevelBlockCount()
	{
		for (int i = 0; i < 2; i++)
		{
			blockCountLevel[i] = 0;
		}
	}

	public virtual void AddBlock(BlockType blockType)
	{
		blockCountLevel[(int)blockType] = blockCountLevel[(int)blockType] + 1;
		blockCountTotal[(int)blockType] = blockCountTotal[(int)blockType] + 1;
		if (blockDoublerOn != 0)
		{
			blockCountLevel[(int)blockType] = blockCountLevel[(int)blockType] + 1;
			blockCountTotal[(int)blockType] = blockCountTotal[(int)blockType] + 1;
		}
	}

	public virtual int GetLevelBlockCount(BlockType blockType)
	{
		return blockCountLevel[(int)blockType];
	}

	public virtual void SubtractBlocksFromTotal(int num, BlockType type)
	{
		blockCountTotal[(int)type] = blockCountTotal[(int)type] - num;
	}

	public virtual void AddBlocksToTotal(int num, BlockType type)
	{
		blockCountTotal[(int)type] = blockCountTotal[(int)type] + num;
	}

	public virtual int GetTotalBlockCount(BlockType blockType)
	{
		return blockCountTotal[(int)blockType];
	}

	public virtual void SetTotalBlockCount(BlockType blockType, int count)
	{
		Debug.Log("MATT: SetTotalBlockCount : " + count);
		blockCountTotal[(int)blockType] = count;
	}

	public virtual void SetReviveCost(int cost)
	{
		reviveCost = cost;
	}

	public virtual void IncrementReviveCost()
	{
		reviveCost++;
	}

	public virtual int GetReviveCost()
	{
		return reviveCost;
	}

	public virtual void AddToScore(int val)
	{
		score += val;
	}

	public virtual void ResetScore()
	{
		score = 0;
	}

	public virtual int GetScore()
	{
		return score;
	}

	public virtual void setTimeLevelStarted(float time)
	{
		timeLevelStarted = time;
	}

	public virtual float getTimeLevelStarted()
	{
		return timeLevelStarted;
	}

	public virtual void SubtractPauseTime(float val)
	{
		timeLevelStarted += val;
	}

	public virtual float GetTimeInLevel()
	{
		return Time.time - timeLevelStarted;
	}

	public virtual bool SubmitScore()
	{
		int result;
		if (score > bestScore)
		{
			mostBlocks = blockCountLevel[0];
			bestScore = score;
			bestTime = GetTimeInLevel();
			result = 1;
		}
		else
		{
			result = 0;
		}
		return (byte)result != 0;
	}

	public virtual bool ShouldShowAds()
	{
		return showAds == 1;
	}

	public virtual void TurnOffAds()
	{
		showAds = 0;
	}

	public virtual int GetUpgradesLeft()
	{
		return 4 - pistolLevel + 4 - ak47Level + 4 - uziLevel + 4 - shotgunLevel + 3 - armorLevel + 3 - luckLevel;
	}

	public virtual int GetGunLevel(GunType type)
	{
		return (GunType.PISTOL == type) ? pistolLevel : ((GunType.AK47 == type) ? ak47Level : ((GunType.SHOTGUN == type) ? shotgunLevel : ((GunType.UZI == type) ? uziLevel : 0)));
	}

	public virtual void IncreaseGunLevel(GunType type)
	{
		if (GunType.PISTOL == type)
		{
			pistolLevel++;
		}
		if (GunType.AK47 == type)
		{
			ak47Level++;
		}
		if (GunType.SHOTGUN == type)
		{
			shotgunLevel++;
		}
		if (GunType.UZI == type)
		{
			uziLevel++;
		}
		SaveGameData();
	}

	public virtual int GetArmorLevel()
	{
		return armorLevel;
	}

	public virtual void IncreaseArmorLevel()
	{
		armorLevel++;
		SaveGameData();
	}

	public virtual int GetLuckLevel()
	{
		return luckLevel;
	}

	public virtual void IncreaseLuckLevel()
	{
		luckLevel++;
		SaveGameData();
	}

	public virtual void TurnOnBlockDoubler()
	{
		blockDoublerOn = 1;
	}

	public virtual bool DoubleBlocks()
	{
		return blockDoublerOn == 1;
	}

	public virtual void ToggleSound()
	{
		if (soundOn != 0)
		{
			soundOn = 0;
			AudioListener.volume = 0f;
		}
		else
		{
			soundOn = 1;
			AudioListener.volume = 1f;
		}
	}

	public virtual bool IsSoundOn()
	{
		return soundOn == 1;
	}

	public virtual void ToggleYAxis()
	{
		if (invertY != 0)
		{
			fpsController.invertYAxis = false;
			invertY = 0;
		}
		else
		{
			fpsController.invertYAxis = true;
			invertY = 1;
		}
	}

	public virtual bool IsYAxisInverted()
	{
		return invertY == 1;
	}

	public virtual void SetFPSController(FirstPersonControlCustom controller)
	{
		fpsController = controller;
		fpsController.invertYAxis = invertY == 1;
	}

	public virtual int GetSensitivityLevel()
	{
		return viewSensitivity;
	}

	public virtual float GetSensitivityValue()
	{
		float result;
		switch (viewSensitivity)
		{
		case 1:
			result = 0.2f;
			break;
		case 2:
			result = 0.4f;
			break;
		case 3:
			result = 0.6f;
			break;
		case 5:
			result = 1f;
			break;
		case 6:
			result = 1.2f;
			break;
		case 7:
			result = 1.4f;
			break;
		case 8:
			result = 1.6f;
			break;
		case 9:
			result = 1.8f;
			break;
		default:
			result = 0f;
			break;
		}
		return result;
	}

	public virtual void IncreaseSensitivity()
	{
		if (viewSensitivity < 9)
		{
			viewSensitivity++;
		}
	}

	public virtual void DecreaseSensitivity()
	{
		if (viewSensitivity > 1)
		{
			viewSensitivity--;
		}
	}

	public virtual void LoadGameData()
	{
		if (PlayerPrefs.HasKey("openedCount"))
		{
			openedCount = PlayerPrefs.GetInt("openedCount");
		}
		if (PlayerPrefs.HasKey("ratePopupShown"))
		{
			ratePopupShown = PlayerPrefs.GetInt("ratePopupShown");
		}
		if (PlayerPrefs.HasKey("pressedRate"))
		{
			pressedRate = PlayerPrefs.GetInt("pressedRate");
		}
		if (PlayerPrefs.HasKey("GreenBlocks"))
		{
			SetTotalBlockCount(BlockType.GREEN, PlayerPrefs.GetInt("GreenBlocks"));
		}
		if (PlayerPrefs.HasKey("SilverBlocks"))
		{
			SetTotalBlockCount(BlockType.SILVER, PlayerPrefs.GetInt("SilverBlocks"));
		}
		Debug.Log("MATT:LoadGameData: blockCount: " + GetTotalBlockCount(BlockType.GREEN));
		if (PlayerPrefs.HasKey("tutorialDone"))
		{
			tutorialDone = PlayerPrefs.GetInt("tutorialDone");
		}
		if (PlayerPrefs.HasKey("mostBlocks"))
		{
			mostBlocks = PlayerPrefs.GetInt("mostBlocks");
		}
		if (PlayerPrefs.HasKey("bestScore"))
		{
			bestScore = PlayerPrefs.GetInt("bestScore");
		}
		if (PlayerPrefs.HasKey("bestTime"))
		{
			bestTime = PlayerPrefs.GetFloat("bestTime");
		}
		if (PlayerPrefs.HasKey("soundOn"))
		{
			soundOn = PlayerPrefs.GetInt("soundOn");
		}
		if (PlayerPrefs.HasKey("invertY"))
		{
			invertY = PlayerPrefs.GetInt("invertY");
		}
		if (PlayerPrefs.HasKey("viewSensitivity"))
		{
			viewSensitivity = PlayerPrefs.GetInt("viewSensitivity");
		}
		if (PlayerPrefs.HasKey("pistolLevel"))
		{
			pistolLevel = PlayerPrefs.GetInt("pistolLevel");
		}
		if (PlayerPrefs.HasKey("ak47Level"))
		{
			ak47Level = PlayerPrefs.GetInt("ak47Level");
		}
		if (PlayerPrefs.HasKey("shotgunLevel"))
		{
			shotgunLevel = PlayerPrefs.GetInt("shotgunLevel");
		}
		if (PlayerPrefs.HasKey("uziLevel"))
		{
			uziLevel = PlayerPrefs.GetInt("uziLevel");
		}
		if (PlayerPrefs.HasKey("armorLevel"))
		{
			armorLevel = PlayerPrefs.GetInt("armorLevel");
		}
		if (PlayerPrefs.HasKey("luckLevel"))
		{
			luckLevel = PlayerPrefs.GetInt("luckLevel");
		}
		if (PlayerPrefs.HasKey("blockDoublerOn"))
		{
			blockDoublerOn = PlayerPrefs.GetInt("blockDoublerOn");
		}
		if (PlayerPrefs.HasKey("showAds"))
		{
			showAds = PlayerPrefs.GetInt("showAds");
		}
	}

	public virtual void SaveGameData()
	{
		PlayerPrefs.SetInt("openedCount", openedCount);
		PlayerPrefs.SetInt("ratePopupShown", ratePopupShown);
		PlayerPrefs.SetInt("pressedRate", pressedRate);
		PlayerPrefs.SetInt("GreenBlocks", GetTotalBlockCount(BlockType.GREEN));
		PlayerPrefs.SetInt("SilverBlocks", GetTotalBlockCount(BlockType.SILVER));
		Debug.Log("MATT:SaveGameData: blockCount: " + GetTotalBlockCount(BlockType.GREEN));
		PlayerPrefs.SetInt("tutorialDone", tutorialDone);
		PlayerPrefs.SetInt("mostBlocks", mostBlocks);
		PlayerPrefs.SetInt("bestScore", bestScore);
		PlayerPrefs.SetFloat("bestTime", bestTime);
		PlayerPrefs.SetInt("soundOn", soundOn);
		PlayerPrefs.SetInt("invertY", invertY);
		PlayerPrefs.SetInt("viewSensitivity", viewSensitivity);
		PlayerPrefs.SetInt("pistolLevel", pistolLevel);
		PlayerPrefs.SetInt("ak47Level", ak47Level);
		PlayerPrefs.SetInt("uziLevel", uziLevel);
		PlayerPrefs.SetInt("shotgunLevel", shotgunLevel);
		PlayerPrefs.SetInt("armorLevel", armorLevel);
		PlayerPrefs.SetInt("luckLevel", luckLevel);
		PlayerPrefs.SetInt("blockDoublerOn", blockDoublerOn);
		PlayerPrefs.SetInt("showAds", showAds);
		PlayerPrefs.Save();
	}

	public virtual bool IsTutorialDone()
	{
		return tutorialDone != 0;
	}

	public virtual void SetTutorialDone(int done)
	{
		tutorialDone = done;
	}
}
