using Myra;
using Myra.Graphics2D.UI;
using Unigine;

namespace UnigineApp.MyraIntegration
{
	static class MyraIntegration
	{
		static Desktop Desktop { get; } = new();

		public static void Init()
		{
			MyraEnvironment.Platform = new MyraPlatform();
			MyraEnvironment.EnableModalDarkening = true;

			//Set layout root
			//Desktop.Root = new AllWidgets(); //TODO: Set your own root layout widgets

			//Set extertal input (unicode characters)
			Desktop.HasExternalTextInput = true;
			Input.EventTextPress.Connect(static text => {
				if (!Input.MouseGrab && text <= char.MaxValue) {
					Desktop.OnChar((char)text);
				}
			});

			//Handle engine events
			Engine.EventBeginRender.Connect(Engine_OnBeginRender);
			Engine.EventEndPluginsGui.Connect(Engine_OnEndPluginGui);
		}

		public static void NewFrame()
		{
			//Intercept mouse event when mouse interacts GUI elements
			if (!Input.MouseGrab) {
				ControlsApp.Enabled = !Desktop.IsMouseOverGUI;
				if (Desktop.IsMouseOverGUI) {
					ControlsApp.MouseDX = 0;
					ControlsApp.MouseDY = 0;
				}

				if (Desktop.IsMouseOverGUI) {
					Input.MouseHandle = Input.MOUSE_HANDLE.USER;
				}
				else {
					Input.MouseHandle = Input.MOUSE_HANDLE.GRAB;
				}
			}

			//Unfocus keyboard if mouse is grabbed
			if (Input.MouseGrab && Desktop.FocusedKeyboardWidget != null) {
				Desktop.FocusedKeyboardWidget = null;
			}
		}

		static void Engine_OnBeginRender()
		{
			if (Desktop.IsMouseOverGUI && !Input.MouseGrab) {
				Gui.GetCurrent().MouseButtons = 0;
			}
		}

		static void Engine_OnEndPluginGui()
		{
			var renderer = (MyraRenderer)MyraEnvironment.Platform.Renderer;

			renderer.NewRender();
			Desktop.Render();
			renderer.DrawToOutput();
		}
	}
}
