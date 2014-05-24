using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Calculate the distance between two position")]
	public class Vector3Distance : FsmStateAction
	{
		[RequiredField]
        [Tooltip("First position.")]
		public FsmVector3 vector1;
		
		[RequiredField]
        [Tooltip("Second position.")]
		public FsmVector3 vector2;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("The float variable to be set.")]
		public FsmFloat targetVariable;
		
        [Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		public override void Reset()
		{
			vector1 = null;
			vector2 = null;
			targetVariable = null;
			everyFrame = false;
		}
		
		protected void apply() {
			targetVariable.Value = (vector1.Value - vector2.Value).magnitude;
			Debug.Log(targetVariable.Value);
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