using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UnityObject)]
	[Tooltip("Load resource according to the path")]
	public class LoadPrefab : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The path of the resouce.")]
		public FsmString resourcePath;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The variable to store the resouce.")]
		public FsmGameObject targetVariable;
		
		public override void Reset()
		{
			resourcePath = null;
			targetVariable = null;
		}
		
		public override void OnEnter()
		{
			targetVariable.Value = Resources.Load(resourcePath.Value);

			Finish();
		}
	}
}