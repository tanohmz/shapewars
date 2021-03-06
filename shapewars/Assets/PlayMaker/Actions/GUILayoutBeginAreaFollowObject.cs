// (c) Copyright HutongGames, LLC 2010-2011. All rights reserved.

using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("Begin a GUILayout area that follows the specified game object. Useful for overlays (e.g., playerName). NOTE: Block must end with a corresponding GUILayoutEndArea.")]
	public class GUILayoutBeginAreaFollowObject : FsmStateAction
	{
		[RequiredField]
		public FsmGameObject gameObject;
		[RequiredField]
		public FsmFloat offsetLeft;
		[RequiredField]
		public FsmFloat offsetTop;
		[RequiredField]
		public FsmFloat width;
		[RequiredField]
		public FsmFloat height;
		public FsmBool normalized;
		public FsmString style;
		
		public override void Reset()
		{
			gameObject = null;
			offsetLeft = 0f;
			offsetTop = 0f;
			width = 1f;
			height = 1f;
			normalized = true;
			style = "";
		}

		public override void OnGUI()
		{
			var go = gameObject.Value;
			
			if (go == null || Camera.main == null)
			{
				DummyBeginArea();
				return;
			}
			
			// get go position in camera space
			
			var worldPosition = go.transform.position;
			var positionInCameraSpace = Camera.main.transform.InverseTransformPoint(worldPosition);
			if (positionInCameraSpace.z < 0)
			{
				// behind camera
				// TODO option to keep onscreen
				DummyBeginArea();
				return;
			}

			// get screen position
			
			Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPosition);
			
			float left = screenPos.x + (normalized.Value == true ? offsetLeft.Value * Screen.width : offsetLeft.Value);
			float top = screenPos.y + (normalized.Value == true ? offsetTop.Value * Screen.width : offsetTop.Value);
			
			Rect rect = new Rect(left, top, width.Value, height.Value);
			
			if (normalized.Value)
			{
				rect.width *= Screen.width;
				rect.height *= Screen.height;
			}
			
			// convert screen coordinates
			rect.y = Screen.height - rect.y;
			
			GUILayout.BeginArea(rect, style.Value);
		}
		
		void DummyBeginArea()
		{
			GUILayout.BeginArea(new Rect());
		}
	}
}