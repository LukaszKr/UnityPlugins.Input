using ProceduralLevel.UnityPlugins.Input;
using ProceduralLevel.UnityPluginsEditor.ExtendedEditor;

namespace ProceduralLevel.UnityPluginsEditor.Input
{
	public class ActiveLayersScrollView: ScrollView<InputLayer>
	{
		public ActiveLayersScrollView(InputManager inputManager, string name) : base(inputManager.GetActiveLayers())
		{
			AddHeader(new LabelScrollViewComponent<InputLayer>(name));
		}

		protected override float DrawItem(LayoutData layoutPosition, InputLayer entry, bool render)
		{
			return base.DrawItem(layoutPosition, entry, render);
		}
	}
}
