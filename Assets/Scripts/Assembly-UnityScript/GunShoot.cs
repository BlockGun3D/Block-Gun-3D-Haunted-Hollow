using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(AudioSource))]
public class GunShoot : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class _0024Shoot_0024224 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal int _0024i_0024225;

			internal Vector3 _0024pos_0024226;

			internal float _0024varianceX_0024227;

			internal float _0024varianceY_0024228;

			internal GameObject _0024clone_0024229;

			internal Vector3 _0024vec_0024230;

			internal GunShoot _0024self__0024231;

			public _0024(GunShoot self_)
			{
				_0024self__0024231 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					if (_0024self__0024231.clipLeft != 0f)
					{
						_0024self__0024231.clipLeft -= 1f;
						for (_0024i_0024225 = 0; _0024i_0024225 < _0024self__0024231.numProjectiles; _0024i_0024225++)
						{
							_0024pos_0024226 = _0024self__0024231.thisTransform.TransformDirection(_0024self__0024231.bulletSpawnOffset);
							_0024varianceX_0024227 = _0024self__0024231.accuracy * StaticFuncs.RandomVal(0f, _0024self__0024231.aimVariance);
							_0024varianceY_0024228 = _0024self__0024231.accuracy * StaticFuncs.RandomVal(0f, _0024self__0024231.aimVariance);
							_0024clone_0024229 = PoolsManager.GetObject(_0024self__0024231.projectileType, _0024self__0024231.thisTransform.position + _0024pos_0024226, _0024self__0024231.thisTransform.rotation);
							_0024vec_0024230 = new Vector3(_0024varianceX_0024227, _0024varianceY_0024228, 1f);
							Vector3.Normalize(_0024vec_0024230);
							_0024clone_0024229.GetComponent<Rigidbody>().velocity = _0024self__0024231.thisTransform.TransformDirection(_0024vec_0024230 * _0024self__0024231.speed);
							_0024clone_0024229.SendMessage("SetDamage", _0024self__0024231.damage);
						}
						_0024self__0024231.parent.GetComponent<Animation>().Play(_0024self__0024231.recoilAnimationName);
						if ((bool)_0024self__0024231.childAnimation)
						{
							_0024self__0024231.childAnimation.Play();
						}
						_0024self__0024231.shotSound.Play();
						if ((bool)_0024self__0024231.particleSys)
						{
							_0024self__0024231.particleSys.Emit(1);
						}
						if ((bool)_0024self__0024231.thisLight)
						{
							_0024self__0024231.thisLight.enabled = true;
							result = (Yield(2, new WaitForSeconds(0.2f)) ? 1 : 0);
							break;
						}
					}
					else
					{
						_0024self__0024231.clickSound.Play();
						if (_0024self__0024231.ammoLeft > 0 || _0024self__0024231.infiniteAmmo)
						{
							_0024self__0024231.reloadButton.GetComponent<Animation>().Play("flashRed");
						}
						else
						{
							_0024self__0024231.changeWeaponButton.GetComponent<Animation>().Play("flashRed");
						}
					}
					goto IL_02d2;
				case 2:
					_0024self__0024231.thisLight.enabled = false;
					goto IL_02d2;
				case 1:
					{
						result = 0;
						break;
					}
					IL_02d2:
					YieldDefault(1);
					goto case 1;
				}
				return (byte)result != 0;
			}
		}

		internal GunShoot _0024self__0024232;

		public _0024Shoot_0024224(GunShoot self_)
		{
			_0024self__0024232 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024232);
		}
	}

	[Serializable]
	[CompilerGenerated]
	internal sealed class _0024Reload_0024233 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal float _0024clipEmpty_0024234;

			internal GunShoot _0024self__0024235;

			public _0024(GunShoot self_)
			{
				_0024self__0024235 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					if (_0024self__0024235.ammoLeft > 0)
					{
						_0024self__0024235.reloadButton.GetComponent<Animation>().Stop();
						WeaponManager.canFire = false;
						_0024self__0024235.parent.GetComponent<Animation>().PlayQueued("Reload", QueueMode.CompleteOthers);
						result = (Yield(2, new WaitForSeconds(0.2f)) ? 1 : 0);
						break;
					}
					_0024self__0024235.clickSound.Play();
					if (_0024self__0024235.clipLeft == 0f && !_0024self__0024235.infiniteAmmo)
					{
						_0024self__0024235.changeWeaponButton.GetComponent<Animation>().Play("flashRed");
					}
					goto IL_0218;
				case 2:
					_0024self__0024235.reloadSound.Play();
					result = (Yield(3, new WaitForSeconds(0.3f)) ? 1 : 0);
					break;
				case 3:
					WeaponManager.canFire = true;
					if (_0024self__0024235.infiniteAmmo)
					{
						_0024self__0024235.clipLeft = _0024self__0024235.clipSize;
					}
					else
					{
						_0024clipEmpty_0024234 = (float)_0024self__0024235.clipSize - _0024self__0024235.clipLeft;
						if (_0024self__0024235.ammoLeft >= _0024self__0024235.clipSize)
						{
							_0024self__0024235.clipLeft = _0024self__0024235.clipSize;
							_0024self__0024235.ammoLeft = (int)((float)_0024self__0024235.ammoLeft - _0024clipEmpty_0024234);
						}
						else if (!(_0024clipEmpty_0024234 <= (float)_0024self__0024235.ammoLeft))
						{
							_0024self__0024235.clipLeft += (float)_0024self__0024235.ammoLeft;
							_0024self__0024235.ammoLeft = 0;
						}
						else
						{
							_0024self__0024235.clipLeft = _0024self__0024235.clipSize;
							_0024self__0024235.ammoLeft = (int)((float)_0024self__0024235.ammoLeft - _0024clipEmpty_0024234);
						}
					}
					goto IL_0218;
				case 1:
					{
						result = 0;
						break;
					}
					IL_0218:
					YieldDefault(1);
					goto case 1;
				}
				return (byte)result != 0;
			}
		}

		internal GunShoot _0024self__0024236;

		public _0024Reload_0024233(GunShoot self_)
		{
			_0024self__0024236 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024236);
		}
	}

	public GunType gunType;

	public PoolType projectileType;

	public int numProjectiles;

	public float speed;

	public float damage;

	public GUITexture fireButton;

	public GUITexture reloadButton;

	public GUITexture changeWeaponButton;

	public GUIText ammoGUI;

	public bool continuous;

	public Vector3 bulletSpawnOffset;

	public ParticleSystem particleSys;

	public string recoilAnimationName;

	public bool startingWithOneAmmo;

	public float aimVariance;

	public bool infiniteAmmo;

	public bool infiniteClip;

	public int maxAmmo;

	public int[] clipSizes;

	public float[] fireRates;

	public float[] accuracies;

	public AudioSource shotSound;

	public AudioSource clickSound;

	public AudioSource reloadSound;

	public Animation childAnimation;

	internal float nextFire;

	private Transform thisTransform;

	private int shootingFinger;

	private GameObject parent;

	private Light thisLight;

	private int ammoLeft;

	private int clipSize;

	private float fireRate;

	private float accuracy;

	private float clipLeft;

	private GameManager gm;

	public GunShoot()
	{
		numProjectiles = 1;
		speed = 150f;
		damage = 1f;
		bulletSpawnOffset = new Vector3(0f, 0f, 0f);
		clipSizes = new int[4];
		fireRates = new float[4];
		accuracies = new float[4];
		shootingFinger = -1;
	}

	public virtual void Start()
	{
		gm = GameManager.GetInstance();
		thisTransform = transform;
		parent = thisTransform.parent.gameObject;
		thisLight = (Light)parent.GetComponentInChildren(typeof(Light));
		Reset();
	}

	public virtual void Reset()
	{
		int num = Global.gm.GetGunLevel(gunType);
		if (num > 0)
		{
			num--;
		}
		clipSize = clipSizes[num];
		clipLeft = clipSize;
		ammoLeft = clipSize;
		if (startingWithOneAmmo)
		{
			clipLeft = 1f;
		}
		fireRate = fireRates[num];
		accuracy = accuracies[num];
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
		//Discarded unreachable code: IL_01b0
		if (infiniteClip)
		{
			ammoGUI.text = string.Empty + "==";
		}
		else
		{
			ammoGUI.text = string.Empty + clipLeft;
		}
		if (infiniteAmmo)
		{
			ammoGUI.text += "/==";
		}
		else
		{
			ammoGUI.text += "/" + ammoLeft;
		}
		if (!WeaponManager.canFire)
		{
			return;
		}
		if ((StaticFuncs.TestButtonTouchBegan(reloadButton) || Input.GetKeyDown("r")) && clipLeft != (float)clipSize)
		{
			StartCoroutine(Reload());
		}
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.WP8Player)
		{
			int touchCount = Input2.touchCount;
			for (int i = 0; i < touchCount; i++)
			{
				Touch touch = Input2.GetTouch(i);
				if ((fireButton.HitTest(touch.position) || touch.fingerId == shootingFinger) && !(Time.time <= nextFire) && (continuous || touch.phase == TouchPhase.Began))
				{
					shootingFinger = touch.fingerId;
					nextFire = Time.time + fireRate;
					StartCoroutine(Shoot());
					break;
				}
				if (touch.phase == TouchPhase.Ended)
				{
					shootingFinger = -1;
				}
			}
		}
		else if (!(Time.time <= nextFire) && (Input.GetButtonDown("Fire1") || (Input.GetButton("Fire1") && continuous)))
		{
			nextFire = Time.time + fireRate;
			StartCoroutine(Shoot());
		}
	}

	public virtual IEnumerator Shoot()
	{
		return new _0024Shoot_0024224(this).GetEnumerator();
	}

	public virtual IEnumerator Reload()
	{
		return new _0024Reload_0024233(this).GetEnumerator();
	}

	public virtual void AddAmmo(int numClips)
	{
		if (!infiniteAmmo)
		{
			ammoLeft += clipSize * numClips;
			if (ammoLeft > maxAmmo)
			{
				ammoLeft = maxAmmo;
			}
		}
	}

	public virtual void Main()
	{
	}
}
