using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Set float variable according to the integer")]
	public class SetFloatFromInt : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("The Bool variable to test.")]
		public FsmFloat floatVariable;
		
		[RequiredField]
        [Tooltip("The integer to set the variable.")]
		public FsmInt integer;
		
        [Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		public override void Reset()
		{
			floatVariable = null;
			integer = null;
			everyFrame = false;
		}
		
		protected void apply() {
			floatVariable.Value = integer.Value;
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