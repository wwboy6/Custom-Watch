namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Set Vector3 according to the boolean")]
	public class SetV3If : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("The Bool variable to test.")]
		public FsmBool boolVariable;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("The Vector3 variable to be set.")]
		public FsmVector3 targetVariable;
		
		[UIHint(UIHint.Variable)]
        [Tooltip("The Vector3 value for the bool is true.")]
		public FsmVector3 trueValue;
		
		[UIHint(UIHint.Variable)]
        [Tooltip("The Vector3 value for the bool is false.")]
		public FsmVector3 falseValue;

        [Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		public override void Reset()
		{
			boolVariable = null;
			targetVariable = null;
			trueValue = null;
			falseValue = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			if (boolVariable.Value && trueValue != null) {
				targetVariable.Value = trueValue.Value;
			} else if (!boolVariable.Value && falseValue != null) {
				targetVariable.Value = falseValue.Value;
			}
			
			if (!everyFrame)
			{
			    Finish();
			}
		}
		
		public override void OnUpdate()
		{
			if (boolVariable.Value && trueValue != null) {
				targetVariable.Value = trueValue.Value;
			} else if (!boolVariable.Value && falseValue != null) {
				targetVariable.Value = falseValue.Value;
			}
		}
	}
}