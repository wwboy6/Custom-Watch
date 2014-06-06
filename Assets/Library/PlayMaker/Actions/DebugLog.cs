// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Sends a log message to the PlayMaker Log Window.")]
	public class DebugLog : FsmStateAction
	{
        [Tooltip("Info, Warning, or Error.")]
		public LogLevel logLevel;

        [Tooltip("Text to print to the PlayMaker log window.")]
		public FsmString text;
		
        [Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		public override void Reset()
		{
			logLevel = LogLevel.Info;
			text = "";
		}
		
		protected void apply() {
			if (!string.IsNullOrEmpty(text.Value))
				ActionHelpers.DebugLog(Fsm, logLevel, text.Value);
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