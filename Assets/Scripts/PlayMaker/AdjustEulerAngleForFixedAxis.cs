using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Set Vector3 according to the boolean")]
	public class AdjustEulerAngleForFixedAxis : FsmStateAction
	{
		public enum Dimension {
			X, Y, Z
		}

		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("The euler angle variable to test.")]
		public FsmVector3 eulerAngles;
		
		[RequiredField]
        [Tooltip("The axis to be fixed.")]
		public Dimension dimension;

        [Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		public override void Reset()
		{
			eulerAngles = null;
			dimension = Dimension.Z;
			everyFrame = false;
		}
		
		protected void apply() {
			int i=0, j=1, k=2;

			switch (dimension) {
			case Dimension.X:
				k = 0;
				i = 1;
				j = 2;
				break;
			case Dimension.Y:
				k = 1;
				i = 0;
				j = 2;
				break;
			case Dimension.Z:
				k = 2;
				i = 0;
				j = 1;
				break;
			}

			Vector3 angles = eulerAngles.Value;
			if (angles[k] < - 90 || angles[k] > 90) {
				//flip i
				angles[i] = (180 - angles[i]) % 360;
				if (angles[i]<0) angles[i] += 360;
				//j += 180
				angles[j] = (angles[j] + 180) % 360;
				if (angles[j]<0) angles[j] += 360;
			}

			angles[k] = 0;

			eulerAngles.Value = angles;
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