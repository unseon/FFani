public class FFaniUpdater : FFaniMation {

	public FFani.Callback action;

	override public void Init () {
		base.Init();

		loop = 0;
	}

	// Use this for initialization
	override protected void OnUpdatePlay(float delta) {
		action();
	}
}
