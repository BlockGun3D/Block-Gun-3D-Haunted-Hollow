using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class StoreRetrieve : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class _0024RetrieveStore_0024249 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal bool _0024activating_0024250;

			internal StoreRetrieve _0024self__0024251;

			public _0024(bool activating, StoreRetrieve self_)
			{
				_0024activating_0024250 = activating;
				_0024self__0024251 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					if (_0024activating_0024250)
					{
						if (Global.storeRetrieved)
						{
							result = (Yield(2, new WaitForSeconds(0.51f)) ? 1 : 0);
							break;
						}
						_0024self__0024251.timeStart = Time.time;
						_0024self__0024251.biller.InitializeUnibill();
						_0024self__0024251.retrieving = true;
					}
					goto IL_007f;
				case 2:
					Global.gm.SetGameState(GameState.MENU_GET_BLOCKS);
					goto IL_007f;
				case 1:
					{
						result = 0;
						break;
					}
					IL_007f:
					YieldDefault(1);
					goto case 1;
				}
				return (byte)result != 0;
			}
		}

		internal bool _0024activating_0024252;

		internal StoreRetrieve _0024self__0024253;

		public _0024RetrieveStore_0024249(bool activating, StoreRetrieve self_)
		{
			_0024activating_0024252 = activating;
			_0024self__0024253 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024activating_0024252, _0024self__0024253);
		}
	}

	public UnibillWiz biller;

	public float timeOut;

	private bool retrieving;

	private float timeStart;

	public StoreRetrieve()
	{
		timeOut = 7f;
	}

	public virtual IEnumerator RetrieveStore(bool activating)
	{
		return new _0024RetrieveStore_0024249(activating, this).GetEnumerator();
	}

	public virtual void Update()
	{
		if (retrieving && !Global.storeRetrieved)
		{
			if (Global.gm.GetGameState() != GameState.RETRIEVING_STORE)
			{
				retrieving = false;
			}
			else if (!(Time.time - timeStart <= timeOut))
			{
				retrieving = false;
				Global.gm.SetGameState(GameState.NO_STORE_ACCESS);
			}
		}
	}

	public virtual void Main()
	{
	}
}
