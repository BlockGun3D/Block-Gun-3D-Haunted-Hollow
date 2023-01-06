using System;
using System.Collections;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class EnemyAssault : MonoBehaviour
{
	public int difficulty;

	public float startDelay;

	public float startDelayVariance;

	public float spawnFrequency;

	public float increaseFrequencyPerSecond;

	public float spawnFrequencyVariance;

	public Transform spawnAreaTopCorner;

	public Transform spawnAreaBottomCorner;

	public DifficultyLevel[] enemyList;

	public GameObject targetObject;

	public GameObject[] spawnAssaults;

	public GameObject[] spawnAlternate;

	public float alternateChance;

	public bool lastAssault;

	public EnemyController enemyController;

	private float goTime;

	internal float lastSpawnTime;

	private float lastFreqIncreaseTime;

	private int difficultyLeft;

	private Transform thisTransform;

	private float nextSpawnDelta;

	private int spawnedEnemies;

	private int deadEnemies;

	private bool paused;

	private float timePaused;

	private float waitStartTime;

	public EnemyAssault()
	{
		spawnFrequency = 1f;
		alternateChance = 0.5f;
		goTime = -1f;
	}

	public virtual void Go()
	{
		goTime = Time.time;
		lastSpawnTime = 0f;
		thisTransform = transform;
		difficultyLeft = difficulty;
		nextSpawnDelta = StaticFuncs.RandomVal(spawnFrequency, spawnFrequencyVariance);
		waitStartTime = StaticFuncs.RandomVal(startDelay, startDelayVariance);
	}

	public virtual void Reset()
	{
		goTime = -1f;
		lastSpawnTime = 0f;
		difficultyLeft = difficulty;
		nextSpawnDelta = 0f;
		spawnedEnemies = 0;
		deadEnemies = 0;
		paused = false;
		timePaused = 0f;
		waitStartTime = StaticFuncs.RandomVal(startDelay, startDelayVariance);
	}

	public virtual void Update()
	{
		GameManager instance = GameManager.GetInstance();
		switch (instance.GetGameState())
		{
		case GameState.PLAYING:
			if (paused)
			{
				paused = false;
				float time = Time.time;
				lastSpawnTime = time - (timePaused - lastSpawnTime);
				if (!(goTime < 0f))
				{
					goTime = time - (timePaused - goTime);
				}
			}
			UpdateGameplay();
			break;
		case GameState.PAUSED:
			if (!paused)
			{
				paused = true;
				timePaused = Time.time;
			}
			break;
		}
	}

	public virtual void UpdateGameplay()
	{
		if (goTime <= 0f || Time.time - goTime <= waitStartTime)
		{
			return;
		}
		if (!(Time.time - lastSpawnTime <= nextSpawnDelta) && difficultyLeft > 0)
		{
			int num = UnityEngine.Random.Range(0, Extensions.get_length((System.Array)enemyList));
			Enemy enemy = enemyController.GetEnemy(enemyList[num]);
			difficultyLeft -= enemy.difficultyValue;
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(enemy.@object, CalcRandomSpot(), thisTransform.rotation);
			spawnedEnemies++;
			if ((bool)targetObject)
			{
				gameObject.SendMessage("SetTarget", targetObject);
			}
			IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(gameObject.transform);
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				if (!(obj is Transform))
				{
					obj = RuntimeServices.Coerce(obj, typeof(Transform));
				}
				Transform transform = (Transform)obj;
				transform.gameObject.SendMessage("AddReportMessage", "EnemyDeath", SendMessageOptions.DontRequireReceiver);
				UnityRuntimeServices.Update(enumerator, transform);
				transform.gameObject.SendMessage("AddReportee", this.gameObject, SendMessageOptions.DontRequireReceiver);
				UnityRuntimeServices.Update(enumerator, transform);
			}
			gameObject.SendMessage("AddReportMessage", "EnemyDeath", SendMessageOptions.DontRequireReceiver);
			gameObject.SendMessage("AddReportee", this.gameObject, SendMessageOptions.DontRequireReceiver);
			lastSpawnTime = Time.time;
			nextSpawnDelta = StaticFuncs.RandomVal(spawnFrequency, spawnFrequencyVariance);
		}
		else if (difficultyLeft <= 0)
		{
			goTime = -1f;
		}
		if (!(Time.time - lastFreqIncreaseTime <= 1f))
		{
			if (!(spawnFrequency <= 0f))
			{
				spawnFrequency -= increaseFrequencyPerSecond;
			}
			if (!(spawnFrequencyVariance <= 0f))
			{
				spawnFrequencyVariance -= increaseFrequencyPerSecond;
			}
			lastFreqIncreaseTime = Time.time;
		}
	}

	public virtual Vector3 CalcRandomSpot()
	{
		return new Vector3(Mathf.Lerp(spawnAreaTopCorner.position.x, spawnAreaBottomCorner.position.x, UnityEngine.Random.value), (spawnAreaTopCorner.position.y + spawnAreaBottomCorner.position.y) / 2f, Mathf.Lerp(spawnAreaTopCorner.position.z, spawnAreaBottomCorner.position.z, UnityEngine.Random.value));
	}

	public virtual void SpawnOtherAssaults()
	{
		for (int i = 0; i < spawnAssaults.Length; i++)
		{
			if (i < spawnAlternate.Length && !(UnityEngine.Random.value >= alternateChance))
			{
				spawnAlternate[i].SendMessage("Go");
			}
			else
			{
				spawnAssaults[i].SendMessage("Go");
			}
		}
	}

	public virtual void EnemyDeath()
	{
		deadEnemies++;
		if (difficultyLeft <= 0 && deadEnemies == spawnedEnemies)
		{
			SpawnOtherAssaults();
			if (lastAssault)
			{
				enemyController.StartNewWave();
			}
		}
	}

	public virtual void Main()
	{
	}
}
