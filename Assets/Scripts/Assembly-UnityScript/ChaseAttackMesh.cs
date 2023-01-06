using System;
using System.Collections;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class ChaseAttackMesh : MonoBehaviour
{
	public GameObject targetObject;

	public float movementSpeed;

	public float firstAttackSpeed;

	public float subsequentAttackSpeed;

	public float attackDistance;

	public float stopDistAlternate;

	public bool shouldWander;

	public float maxAwarenessDistance;

	public float wanderSpeed;

	public float wanderDuration;

	public float wanderDistance;

	public string walkAnimation;

	public string attackAnimation;

	public string attackFunc;

	private Transform thisTransform;

	private float timeInRange;

	private float timeWandering;

	private bool doneFirstAttack;

	private UnityEngine.AI.NavMeshAgent navAgent;

	private GameManager gm;

	private ChaseState chaseState;

	public ChaseAttackMesh()
	{
		movementSpeed = 2f;
		firstAttackSpeed = 0.2f;
		subsequentAttackSpeed = 1f;
		attackDistance = 1f;
		stopDistAlternate = -1f;
		shouldWander = true;
		maxAwarenessDistance = 20f;
		wanderSpeed = 1.5f;
		wanderDuration = 5f;
		wanderDistance = 20f;
		walkAnimation = string.Empty;
		attackAnimation = string.Empty;
		attackFunc = string.Empty;
		timeInRange = -1f;
		timeWandering = -1f;
	}

	public virtual void Start()
	{
		gm = GameManager.GetInstance();
		thisTransform = transform;
		navAgent = (UnityEngine.AI.NavMeshAgent)GetComponent(typeof(UnityEngine.AI.NavMeshAgent));
		navAgent.stoppingDistance = ((stopDistAlternate <= 0f) ? (attackDistance - 0.1f) : stopDistAlternate);
		chaseState = ChaseState.NONE;
	}

	public virtual void Update()
	{
		GameState gameState = gm.GetGameState();
		if (gameState == GameState.PLAYING)
		{
			if ((bool)GetComponent<AudioSource>() && !GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().Play();
			}
			UpdateGameplay();
			return;
		}
		navAgent.Stop();
		if ((bool)GetComponent<Animation>())
		{
			GetComponent<Animation>().Stop();
		}
		chaseState = ChaseState.NONE;
		if ((bool)GetComponent<AudioSource>())
		{
			GetComponent<AudioSource>().Stop();
		}
	}

	public virtual void UpdateGameplay()
	{
		Transform transform = targetObject.transform;
		float deltaTime = Time.deltaTime;
		float num = (transform.position - thisTransform.position).sqrMagnitude - navAgent.radius * navAgent.radius;
		if (!navAgent.hasPath)
		{
			if (!navAgent.pathPending)
			{
				navAgent.destination = transform.position;
			}
			return;
		}
		if (!(num <= maxAwarenessDistance * maxAwarenessDistance) && shouldWander)
		{
			Wander(deltaTime);
			if (!(walkAnimation != string.Empty) || chaseState == ChaseState.WALK_SLOW)
			{
				return;
			}
			IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(GetComponent<Animation>());
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				if (!(obj is AnimationState))
				{
					obj = RuntimeServices.Coerce(obj, typeof(AnimationState));
				}
				AnimationState animationState = (AnimationState)obj;
				animationState.speed = 0.5f;
				UnityRuntimeServices.Update(enumerator, animationState);
			}
			if ((bool)GetComponent<Animation>())
			{
				GetComponent<Animation>().Play(walkAnimation);
			}
			chaseState = ChaseState.WALK_SLOW;
			return;
		}
		if (!(num <= attackDistance * attackDistance))
		{
			navAgent.destination = transform.position;
			navAgent.speed = movementSpeed;
			timeInRange = -1f;
			timeWandering = -1f;
			doneFirstAttack = false;
			if (!(walkAnimation != string.Empty) || chaseState == ChaseState.WALK_FAST)
			{
				return;
			}
			IEnumerator enumerator2 = UnityRuntimeServices.GetEnumerator(GetComponent<Animation>());
			while (enumerator2.MoveNext())
			{
				object obj2 = enumerator2.Current;
				if (!(obj2 is AnimationState))
				{
					obj2 = RuntimeServices.Coerce(obj2, typeof(AnimationState));
				}
				AnimationState animationState2 = (AnimationState)obj2;
				animationState2.speed = 1f;
				UnityRuntimeServices.Update(enumerator2, animationState2);
			}
			if ((bool)GetComponent<Animation>())
			{
				GetComponent<Animation>().Play(walkAnimation);
			}
			chaseState = ChaseState.WALK_FAST;
			return;
		}
		thisTransform.LookAt(new Vector3(transform.position.x, thisTransform.position.y, transform.position.z));
		navAgent.destination = transform.position;
		if (!(timeInRange < 0f))
		{
			if (attackAnimation != string.Empty && chaseState != ChaseState.ATTACK)
			{
				if ((bool)GetComponent<Animation>())
				{
					GetComponent<Animation>().Play(attackAnimation);
				}
				chaseState = ChaseState.ATTACK;
			}
			if ((!doneFirstAttack && timeInRange >= firstAttackSpeed) || !(timeInRange < subsequentAttackSpeed))
			{
				gameObject.SendMessage(attackFunc, targetObject);
				timeInRange = 0f;
				doneFirstAttack = true;
			}
			else
			{
				timeInRange += deltaTime;
			}
		}
		else
		{
			timeInRange = 0f;
		}
	}

	public virtual void Wander(float timeDelta)
	{
		if (timeWandering < 0f || !(timeWandering <= wanderDuration))
		{
			Vector2 insideUnitCircle = UnityEngine.Random.insideUnitCircle;
			Vector3 destination = new Vector3(thisTransform.position.x + insideUnitCircle.x * wanderDistance, thisTransform.position.y, thisTransform.position.z + insideUnitCircle.y * wanderDistance);
			navAgent.destination = destination;
			navAgent.speed = wanderSpeed;
			timeWandering = 0f;
		}
		else
		{
			timeWandering += timeDelta;
		}
	}

	public virtual void SetTarget(GameObject target)
	{
		targetObject = target;
	}

	public virtual void Main()
	{
	}
}
