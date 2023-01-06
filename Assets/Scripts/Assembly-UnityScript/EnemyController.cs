using System;
using System.Collections;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class EnemyController : MonoBehaviour
{
	public GameObject[] enemyPrefabs;

	public int[] difficultyValues;

	public EnemyAssault[] waves;

	private int currentWave;

	public virtual Enemy GetEnemy(DifficultyLevel level)
	{
		Enemy enemy = new Enemy();
		enemy.@object = enemyPrefabs[(int)level];
		enemy.difficultyValue = difficultyValues[(int)level];
		return enemy;
	}

	public virtual void Activate()
	{
		currentWave = Global.gm.waveToStartOn;
		waves[currentWave].SendMessage("Go");
	}

	public virtual void Deactivate()
	{
		Reset();
		gameObject.SetActive(false);
	}

	public virtual void StartNewWave()
	{
		waves[++currentWave].SendMessage("Go");
	}

	public virtual void Reset()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Enemy");
		int i = 0;
		GameObject[] array2 = array;
		for (int length = array2.Length; i < length; i++)
		{
			UnityEngine.Object.Destroy(array2[i]);
		}
		currentWave = 0;
		IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(this.transform);
		while (enumerator.MoveNext())
		{
			object obj = enumerator.Current;
			if (!(obj is Transform))
			{
				obj = RuntimeServices.Coerce(obj, typeof(Transform));
			}
			Transform transform = (Transform)obj;
			transform.gameObject.SendMessage("Reset");
			UnityRuntimeServices.Update(enumerator, transform);
		}
	}

	public virtual void Main()
	{
	}
}
