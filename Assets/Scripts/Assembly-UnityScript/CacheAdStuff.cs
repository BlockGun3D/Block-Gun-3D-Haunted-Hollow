using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class CacheAdStuff : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class _0024CacheMoreApps_0024212 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal bool _0024active_0024213;

			internal CacheAdStuff _0024self__0024214;

			public _0024(bool active, CacheAdStuff self_)
			{
				_0024active_0024213 = active;
				_0024self__0024214 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					if (!_0024active_0024213)
					{
						goto case 1;
					}
					if ((bool)_0024self__0024214.chartboost)
					{
						_0024self__0024214.chartboost.SendMessage("CacheMoreApps");
						_0024self__0024214.chartboost.SendMessage("CacheInterstitial");
						result = (Yield(2, new WaitForSeconds(0.5f)) ? 1 : 0);
						break;
					}
					goto IL_0091;
				case 2:
					_0024self__0024214.chartboost.SendMessage("ShowInterstitial");
					goto IL_0091;
				case 1:
					{
						result = 0;
						break;
					}
					IL_0091:
					YieldDefault(1);
					goto case 1;
				}
				return (byte)result != 0;
			}
		}

		internal bool _0024active_0024215;

		internal CacheAdStuff _0024self__0024216;

		public _0024CacheMoreApps_0024212(bool active, CacheAdStuff self_)
		{
			_0024active_0024215 = active;
			_0024self__0024216 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024active_0024215, _0024self__0024216);
		}
	}

	public GameObject chartboost;

	public virtual IEnumerator CacheMoreApps(bool active)
	{
		return new _0024CacheMoreApps_0024212(active, this).GetEnumerator();
	}

	public virtual void Main()
	{
	}
}
