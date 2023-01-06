using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class ChildStateManager : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class _0024SetAllChildrenActive_0024217 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal int _0024i_0024218;

			internal GameObject _0024child_0024219;

			internal bool _0024bActive_0024220;

			internal ChildStateManager _0024self__0024221;

			public _0024(bool bActive, ChildStateManager self_)
			{
				_0024bActive_0024220 = bActive;
				_0024self__0024221 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					if (_0024bActive_0024220 && !(_0024self__0024221.pauseBeforeActivation <= 0f))
					{
						result = (Yield(2, new WaitForSeconds(_0024self__0024221.pauseBeforeActivation)) ? 1 : 0);
						break;
					}
					goto case 2;
				case 2:
					for (_0024i_0024218 = 0; _0024i_0024218 < _0024self__0024221.transform.childCount; _0024i_0024218++)
					{
						_0024child_0024219 = _0024self__0024221.transform.GetChild(_0024i_0024218).gameObject;
						if (_0024child_0024219.tag != "Passive")
						{
							if (_0024bActive_0024220)
							{
								_0024child_0024219.SetActive(_0024bActive_0024220);
								_0024child_0024219.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
							}
							else if (_0024self__0024221.startDeactive)
							{
								_0024child_0024219.SetActive(false);
							}
							else
							{
								_0024child_0024219.SendMessage("Deactivate", SendMessageOptions.RequireReceiver);
							}
						}
					}
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal bool _0024bActive_0024222;

		internal ChildStateManager _0024self__0024223;

		public _0024SetAllChildrenActive_0024217(bool bActive, ChildStateManager self_)
		{
			_0024bActive_0024222 = bActive;
			_0024self__0024223 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024bActive_0024222, _0024self__0024223);
		}
	}

	public GameState[] activeStates;

	public float pauseBeforeActivation;

	public string sendMessage;

	public string sendMessage2;

	public string sendMessage3;

	public string sendMessage4;

	private bool isActive;

	private GameManager gm;

	private bool startDeactive;

	public ChildStateManager()
	{
		sendMessage = string.Empty;
		sendMessage2 = string.Empty;
		sendMessage3 = string.Empty;
		sendMessage4 = string.Empty;
	}

	public virtual void Start()
	{
		gm = GameManager.GetInstance();
		GameState gameState = gm.GetGameState();
		isActive = StateInList(gameState);
		startDeactive = !isActive;
		UpdateState(gameState);
		startDeactive = false;
	}

	public virtual void Update()
	{
		GameState gameState = gm.GetGameState();
		bool flag = StateInList(gameState);
		if ((!isActive && flag) || (isActive && !flag))
		{
			UpdateState(gameState);
		}
	}

	public virtual void UpdateState(GameState state)
	{
		bool flag = StateInList(state);
		if (sendMessage != string.Empty)
		{
			gameObject.SendMessage(sendMessage, flag);
		}
		if (sendMessage2 != string.Empty)
		{
			gameObject.SendMessage(sendMessage2, flag);
		}
		if (sendMessage3 != string.Empty)
		{
			gameObject.SendMessage(sendMessage3, flag);
		}
		if (flag)
		{
			StartCoroutine(SetAllChildrenActive(true));
			isActive = true;
		}
		else
		{
			StartCoroutine(SetAllChildrenActive(false));
			isActive = false;
		}
	}

	public virtual IEnumerator SetAllChildrenActive(bool bActive)
	{
		return new _0024SetAllChildrenActive_0024217(bActive, this).GetEnumerator();
	}

	private bool StateInList(GameState state)
	{
		int num = 0;
		GameState[] array = activeStates;
		int length = array.Length;
		int result;
		while (true)
		{
			if (num < length)
			{
				if (array[num] == state)
				{
					result = 1;
					break;
				}
				num++;
				continue;
			}
			result = 0;
			break;
		}
		return (byte)result != 0;
	}

	public virtual void Main()
	{
	}
}
