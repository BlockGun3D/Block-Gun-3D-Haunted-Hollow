using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using Boo.Lang.Runtime;
using CompilerGenerated;
using UnityEngine;

[Serializable]
public class PlayerController : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class _0024BeenHit_0024237 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal float _0024_0024132_0024238;

			internal Rect _0024_0024133_0024239;

			internal int _0024_0024134_0024240;

			internal Rect _0024_0024135_0024241;

			internal float _0024damage_0024242;

			internal PlayerController _0024self__0024243;

			public _0024(float damage, PlayerController self_)
			{
				_0024damage_0024242 = damage;
				_0024self__0024243 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					_0024self__0024243.timeHit = Time.time;
					_0024self__0024243.health -= _0024damage_0024242;
					if (_0024self__0024243.health <= 0f)
					{
						int num = (_0024_0024134_0024240 = 0);
						Rect rect = (_0024_0024135_0024241 = _0024self__0024243.healthBar.pixelInset);
						float num3 = (_0024_0024135_0024241.width = _0024_0024134_0024240);
						Rect rect3 = (_0024self__0024243.healthBar.pixelInset = _0024_0024135_0024241);
					}
					else
					{
						float num4 = (_0024_0024132_0024238 = _0024self__0024243.healthBarWidth * (_0024self__0024243.health / (_0024self__0024243.fullHealth + (float)_0024self__0024243.gm.GetArmorLevel() * _0024self__0024243.fullHealth / 2f)));
						Rect rect4 = (_0024_0024133_0024239 = _0024self__0024243.healthBar.pixelInset);
						float num6 = (_0024_0024133_0024239.width = _0024_0024132_0024238);
						Rect rect6 = (_0024self__0024243.healthBar.pixelInset = _0024_0024133_0024239);
					}
					if (!(_0024self__0024243.health > 0.001f))
					{
						_0024self__0024243.GetComponent<AudioSource>().Play();
						result = (Yield(2, new WaitForSeconds(_0024self__0024243.timeDamageIsVisible / 2f)) ? 1 : 0);
						break;
					}
					goto IL_01b4;
				case 2:
					_0024self__0024243.gm.SetGameState(_0024self__0024243.deathState);
					goto IL_01b4;
				case 1:
					{
						result = 0;
						break;
					}
					IL_01b4:
					YieldDefault(1);
					goto case 1;
				}
				return (byte)result != 0;
			}
		}

		internal float _0024damage_0024244;

		internal PlayerController _0024self__0024245;

		public _0024BeenHit_0024237(float damage, PlayerController self_)
		{
			_0024damage_0024244 = damage;
			_0024self__0024245 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024damage_0024244, _0024self__0024245);
		}
	}

	public float health;

	public float timeDamageIsVisible;

	public GUITexture healthBar;

	public GUITexture hurtImage;

	public GameState deathState;

	private float timeHit;

	private float healthBarWidth;

	private float fullHealth;

	private Color hurtImageColor;

	private Vector3 initPos;

	private Quaternion initRot;

	private GameManager gm;

	public PlayerController()
	{
		health = 0.8f;
		timeDamageIsVisible = 0.2f;
		timeHit = -1f;
	}

	public virtual void Start()
	{
		gm = GameManager.GetInstance();
		healthBarWidth = healthBar.pixelInset.width;
		int width = Screen.width;
		Rect pixelInset = hurtImage.pixelInset;
		float num2 = (pixelInset.width = width);
		Rect rect2 = (hurtImage.pixelInset = pixelInset);
		int height = Screen.height;
		Rect pixelInset2 = hurtImage.pixelInset;
		float num4 = (pixelInset2.height = height);
		Rect rect4 = (hurtImage.pixelInset = pixelInset2);
		hurtImageColor = hurtImage.color;
		hurtImage.enabled = false;
		fullHealth = health;
		health = fullHealth + (float)gm.GetArmorLevel() * fullHealth / 2f;
		initPos = transform.position;
		initRot = transform.rotation;
		if (RuntimeServices.EqualityOperator(new __PlayerController_Start_0024callable0_002431_16__(gm.GetArmorLevel), 1))
		{
			healthBar.color = new Color(0.6f, 0.6f, 1f, 1f);
		}
		else if (RuntimeServices.EqualityOperator(new __PlayerController_Start_0024callable0_002431_16__(gm.GetArmorLevel), 2))
		{
			healthBar.color = new Color(0.3f, 0.3f, 1f, 1f);
		}
	}

	public virtual void Reset()
	{
		transform.position = initPos;
		transform.rotation = initRot;
		ResetHealth();
	}

	public virtual void ResetHealth()
	{
		health = fullHealth + (float)gm.GetArmorLevel() * fullHealth / 2f;
		float num = healthBarWidth;
		Rect pixelInset = healthBar.pixelInset;
		float num3 = (pixelInset.width = num);
		Rect rect2 = (healthBar.pixelInset = pixelInset);
		hurtImage.enabled = false;
		timeHit = -1f;
		if (RuntimeServices.EqualityOperator(new __PlayerController_Start_0024callable0_002431_16__(gm.GetArmorLevel), 1))
		{
			healthBar.color = new Color(0.6f, 0.6f, 1f, 1f);
		}
		else if (RuntimeServices.EqualityOperator(new __PlayerController_Start_0024callable0_002431_16__(gm.GetArmorLevel), 2))
		{
			healthBar.color = new Color(0.3f, 0.3f, 1f, 1f);
		}
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
				hurtImage.enabled = true;
				hurtImage.color = Color.Lerp(hurtImageColor, new Color(0f, 0f, 0f, 0f), (Time.time - timeHit) / timeDamageIsVisible);
			}
			else
			{
				hurtImage.enabled = false;
				timeHit = -1f;
			}
		}
	}

	public virtual IEnumerator BeenHit(float damage)
	{
		return new _0024BeenHit_0024237(damage, this).GetEnumerator();
	}

	public virtual void Main()
	{
	}
}
