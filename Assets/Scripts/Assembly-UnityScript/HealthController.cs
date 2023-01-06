using System;
using System.Collections;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class HealthController : MonoBehaviour
{
	public float health;

	public float timeDamageIsVisible;

	public Color damageColor;

	private float timeHit;

	private GameManager gm;

	private bool dead;

	public HealthController()
	{
		health = 1f;
		timeDamageIsVisible = 0.2f;
		damageColor = Color.red;
		timeHit = -1f;
	}

	public virtual void Start()
	{
		gm = GameManager.GetInstance();
	}

	public virtual void Update()
	{
		GameState gameState = gm.GetGameState();
		if (gameState == GameState.PLAYING)
		{
			UpdateGameplay();
		}
	}

	public virtual void UpdateGameplay()
	{
		if (!(timeHit <= 0f))
		{
			if (!(Time.time - timeHit >= timeDamageIsVisible))
			{
				ChangeColor(damageColor);
			}
			else
			{
				ChangeColor(Color.white);
			}
		}
	}

	public virtual void BeenHit(float damage)
	{
		timeHit = Time.time;
		health -= damage;
		if (!(health > 0f) && !dead)
		{
			dead = true;
			SendMessage("Die");
		}
	}

	public virtual void ChangeColor(Color color)
	{
		if ((bool)GetComponent<Renderer>())
		{
			color.a = GetComponent<Renderer>().material.color.a;
			GetComponent<Renderer>().material.color = color;
		}
		IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(this.transform);
		while (enumerator.MoveNext())
		{
			object obj = enumerator.Current;
			if (!(obj is Transform))
			{
				obj = RuntimeServices.Coerce(obj, typeof(Transform));
			}
			Transform transform = (Transform)obj;
			if ((bool)transform.GetComponent<Renderer>())
			{
				color.a = transform.GetComponent<Renderer>().material.color.a;
				UnityRuntimeServices.Update(enumerator, transform);
				transform.GetComponent<Renderer>().material.color = color;
				UnityRuntimeServices.Update(enumerator, transform);
			}
		}
	}

	public virtual void Main()
	{
	}
}
