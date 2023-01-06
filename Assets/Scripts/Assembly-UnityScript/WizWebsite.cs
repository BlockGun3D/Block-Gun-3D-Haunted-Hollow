using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class WizWebsite : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class _0024Start_0024263 : GenericGenerator<WWW>
	{
		[Serializable]
		[CompilerGenerated]
		internal sealed class _0024 : GenericGeneratorEnumerator<WWW>, IEnumerator
		{
			internal WWW _0024www_0024264;

			internal string _0024str_0024265;

			internal WizWebsite _0024self__0024266;

			public _0024(WizWebsite self_)
			{
				_0024self__0024266 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					_0024www_0024264 = new WWW(_0024self__0024266.url);
					result = (Yield(2, _0024www_0024264) ? 1 : 0);
					break;
				case 2:
					if (!string.IsNullOrEmpty(_0024www_0024264.error))
					{
						Debug.Log("MATTTTTTTT: " + _0024www_0024264.error);
					}
					else
					{
						_0024str_0024265 = _0024www_0024264.text;
						Debug.Log("MATTTTTTTTT str: " + _0024str_0024265);
						Global.adNetworkChoose = int.Parse(_0024str_0024265);
						YieldDefault(1);
					}
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal WizWebsite _0024self__0024267;

		public _0024Start_0024263(WizWebsite self_)
		{
			_0024self__0024267 = self_;
		}

		public override IEnumerator<WWW> GetEnumerator()
		{
			return new _0024(_0024self__0024267);
		}
	}

	public string url;

	public WizWebsite()
	{
		url = "http://wizardgames.ca/cbPosibility.txt";
	}

	public virtual IEnumerator Start()
	{
		return new _0024Start_0024263(this).GetEnumerator();
	}

	public virtual void Main()
	{
	}
}
