using System;
using UnityEngine;

[Serializable]
public class ProjectileHandler : MonoBehaviour
{
	public string enemyTag;

	public string sendMessageOnImpact;

	public PoolType poolType;

	private float damage;

	public ProjectileHandler()
	{
		enemyTag = "Enemy";
		sendMessageOnImpact = string.Empty;
		damage = 1f;
	}

	public virtual void SetDamage(float inDamage)
	{
		damage = inDamage;
	}

	public virtual void OnCollisionEnter(Collision collision)
	{
		HandleCollision(collision.gameObject);
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		HandleCollision(other.gameObject);
	}

	private void HandleCollision(GameObject @object)
	{
		if (Global.gm.GetGameState() == GameState.PLAYING && (@object.tag == "LevelGeometry" || @object.tag == enemyTag))
		{
			if (sendMessageOnImpact == string.Empty)
			{
				PoolsManager.ReturnObject(gameObject, poolType);
			}
			else
			{
				gameObject.SendMessage(sendMessageOnImpact);
			}
			if (@object.tag == enemyTag)
			{
				@object.SendMessage("BeenHit", damage);
			}
		}
	}

	public virtual void Main()
	{
	}
}
