using System;
using UnityEngine;

[Serializable]
public class Attacks : MonoBehaviour
{
	public float attackDamage;

	public AudioClip attackSound;

	public PoolType projectileType;

	public float projectileSpeed;

	public Vector3 projectileSpawnOffset;

	private Transform thisTransform;

	public Attacks()
	{
		attackDamage = 0.1f;
		projectileSpeed = 1f;
	}

	public virtual void Start()
	{
		thisTransform = gameObject.transform;
	}

	public virtual void Bite(GameObject target)
	{
		if ((bool)attackSound)
		{
			AudioSource.PlayClipAtPoint(attackSound, thisTransform.position);
		}
		target.SendMessage("BeenHit", attackDamage);
	}

	public virtual void Shoot(GameObject target)
	{
		Vector3 vector = thisTransform.TransformDirection(projectileSpawnOffset);
		GameObject @object = PoolsManager.GetObject(projectileType, thisTransform.position + vector, thisTransform.rotation);
		@object.transform.LookAt(target.transform);
		Vector3 vector2 = target.transform.position - @object.transform.position;
		vector2.Normalize();
		@object.GetComponent<Rigidbody>().velocity = vector2 * projectileSpeed;
		AudioSource.PlayClipAtPoint(attackSound, thisTransform.position + vector);
		@object.SendMessage("SetDamage", attackDamage);
	}

	public virtual void Main()
	{
	}
}
