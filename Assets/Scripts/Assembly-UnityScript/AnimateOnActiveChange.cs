using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class AnimateOnActiveChange : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class _0024Deactivate_0024204 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal AnimateOnActiveChange _0024self__0024205;

			public _0024(AnimateOnActiveChange self_)
			{
				_0024self__0024205 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					if (_0024self__0024205.outAnimation != string.Empty)
					{
						_0024self__0024205.GetComponent<Animation>().Play(_0024self__0024205.outAnimation);
						result = (Yield(2, new WaitForSeconds(_0024self__0024205.GetComponent<Animation>().clip.length)) ? 1 : 0);
						break;
					}
					goto case 2;
				case 2:
					_0024self__0024205.gameObject.SetActive(false);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal AnimateOnActiveChange _0024self__0024206;

		public _0024Deactivate_0024204(AnimateOnActiveChange self_)
		{
			_0024self__0024206 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024206);
		}
	}

	public string inAnimation;

	public string outAnimation;

	public Vector3 inPosition;

	public virtual void Activate()
	{
		if (inAnimation != string.Empty)
		{
			GetComponent<Animation>().Play(inAnimation);
		}
		else
		{
			transform.position = inPosition;
		}
	}

	public virtual IEnumerator Deactivate()
	{
		return new _0024Deactivate_0024204(this).GetEnumerator();
	}

	public virtual void Main()
	{
	}
}
