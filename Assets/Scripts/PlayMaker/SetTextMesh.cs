using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.ScriptControl)]
	[Tooltip("Set text of TextMesh")]
	public class SetTextMesh : FsmStateAction
	{
		[RequiredField]
        [ObjectType(typeof(TextMesh))]
        [Tooltip("The TextMesh to be set.")]
		public FsmObject textMesh;
		
        [Tooltip("The string to set the TestMesh.")]
		public FsmString text;

        [Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		public override void Reset()
		{
			textMesh = null;
			text = null;
			everyFrame = false;
		}
		
		protected void apply() {
			((TextMesh)textMesh.Value).text = text.Value;
		}

		public override void OnEnter()
		{
			apply();
			
			if (!everyFrame)
			{
			    Finish();
			}
		}
		
		public override void OnUpdate()
		{
			apply();
		}
	}
}