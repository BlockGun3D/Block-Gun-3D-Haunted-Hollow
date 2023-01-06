using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class ResetGame : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class _0024Activate_0024246 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal ResetGame _0024self__0024247;

			public _0024(ResetGame self_)
			{
				_0024self__0024247 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					Global.pm.Reset();
					Global.gm.SetReviveCost(1);
					Global.gm.ResetLevelBlockCount();
					Global.gm.ResetScore();
					result = (Yield(2, new WaitForSeconds(0.6f)) ? 1 : 0);
					break;
				case 2:
					Global.gm.SetGameState(_0024self__0024247.stateToResetTo);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal ResetGame _0024self__0024248;

		public _0024Activate_0024246(ResetGame self_)
		{
			_0024self__0024248 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__0024248);
		}
	}

	public GameState stateToResetTo;

	private GameManager gameManager;

	public virtual IEnumerator Activate()
	{
		return new _0024Activate_0024246(this).GetEnumerator();
	}

	public virtual void Deactivate()
	{
		gameObject.SetActive(false);
	}

	public virtual void Main()
	{
	}
}
