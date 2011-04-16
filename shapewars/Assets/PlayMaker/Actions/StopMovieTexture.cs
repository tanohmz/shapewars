// (c) Copyright HutongGames, LLC 2010-2011. All rights reserved.

#if !(UNITY_IPHONE || UNITY_ANDROID)

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Movie)]
	[Tooltip("Stops playing the Movie Texture, and rewinds it to the beginning.")]
	public class StopMovieTexture : FsmStateAction
	{
		[RequiredField]
		public MovieTexture movieTexture;

		public override void Reset()
		{
			movieTexture = null;
		}

		public override void OnEnter()
		{
			movieTexture.Stop();
			Finish();
		}
	}
}

#endif