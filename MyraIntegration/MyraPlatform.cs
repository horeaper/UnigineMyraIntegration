using System.Drawing;
using Myra.Graphics2D.UI;
using Myra.Platform;
using Unigine;

namespace UnigineApp.MyraIntegration
{
	class MyraPlatform : IMyraPlatform
	{
		readonly Keys[] UnigineToMyraKeyMap = new Keys[(int)Input.KEY.NUM_KEYS];
		int mouseWheelValue;

		public MyraPlatform()
		{
			GenerateMyraKeyMap();
		}

		public IMyraRenderer Renderer { get; } = new MyraRenderer();

		Point IMyraPlatform.ViewSize
		{
			get {
				var clientRenderSize = WindowManager.MainWindow.ClientRenderSize;
				return new Point(clientRenderSize.x, clientRenderSize.y);
			}
		}

		MouseInfo IMyraPlatform.GetMouseInfo()
		{
			var position = Input.MousePosition - WindowManager.MainWindow.ClientPosition;
			mouseWheelValue += Input.MouseWheel;
			return new MouseInfo {
				IsLeftButtonDown = Input.IsMouseButtonPressed(Input.MOUSE_BUTTON.LEFT),
				IsRightButtonDown = Input.IsMouseButtonPressed(Input.MOUSE_BUTTON.RIGHT),
				IsMiddleButtonDown = Input.IsMouseButtonPressed(Input.MOUSE_BUTTON.MIDDLE),
				Position = new Point(position.x, position.y),
				Wheel = mouseWheelValue,
			};
		}

		void IMyraPlatform.SetKeysDown(bool[] keys)
		{
			for (int key = 0; key < (int)Input.KEY.NUM_KEYS; ++key) {
				var myraKey = UnigineToMyraKeyMap[key];
				keys[(int)myraKey] = Input.IsKeyDown((Input.KEY)key);
			}
		}

		void IMyraPlatform.SetMouseCursorType(MouseCursorType mouseCursorType)
		{
			//TODO: Set your own mouse cursor with Input.SetMouseCursorCustom()
		}

		TouchCollection IMyraPlatform.GetTouchState()
		{
			return TouchCollection.Empty;
		}

		void GenerateMyraKeyMap()
		{
			UnigineToMyraKeyMap[(int)Input.KEY.ESC] = Keys.Escape;
			UnigineToMyraKeyMap[(int)Input.KEY.F1] = Keys.F1;
			UnigineToMyraKeyMap[(int)Input.KEY.F2] = Keys.F2;
			UnigineToMyraKeyMap[(int)Input.KEY.F3] = Keys.F3;
			UnigineToMyraKeyMap[(int)Input.KEY.F4] = Keys.F4;
			UnigineToMyraKeyMap[(int)Input.KEY.F5] = Keys.F5;
			UnigineToMyraKeyMap[(int)Input.KEY.F6] = Keys.F6;
			UnigineToMyraKeyMap[(int)Input.KEY.F7] = Keys.F7;
			UnigineToMyraKeyMap[(int)Input.KEY.F8] = Keys.F8;
			UnigineToMyraKeyMap[(int)Input.KEY.F9] = Keys.F9;
			UnigineToMyraKeyMap[(int)Input.KEY.F10] = Keys.F10;
			UnigineToMyraKeyMap[(int)Input.KEY.F11] = Keys.F11;
			UnigineToMyraKeyMap[(int)Input.KEY.F12] = Keys.F12;
			UnigineToMyraKeyMap[(int)Input.KEY.PRINTSCREEN] = Keys.PrintScreen;
			UnigineToMyraKeyMap[(int)Input.KEY.SCROLL_LOCK] = Keys.Scroll;
			UnigineToMyraKeyMap[(int)Input.KEY.PAUSE] = Keys.Pause;
			UnigineToMyraKeyMap[(int)Input.KEY.BACK_QUOTE] = Keys.OemTilde;
			UnigineToMyraKeyMap[(int)Input.KEY.DIGIT_1] = Keys.D1;
			UnigineToMyraKeyMap[(int)Input.KEY.DIGIT_2] = Keys.D2;
			UnigineToMyraKeyMap[(int)Input.KEY.DIGIT_3] = Keys.D3;
			UnigineToMyraKeyMap[(int)Input.KEY.DIGIT_4] = Keys.D4;
			UnigineToMyraKeyMap[(int)Input.KEY.DIGIT_5] = Keys.D5;
			UnigineToMyraKeyMap[(int)Input.KEY.DIGIT_6] = Keys.D6;
			UnigineToMyraKeyMap[(int)Input.KEY.DIGIT_7] = Keys.D7;
			UnigineToMyraKeyMap[(int)Input.KEY.DIGIT_8] = Keys.D8;
			UnigineToMyraKeyMap[(int)Input.KEY.DIGIT_9] = Keys.D9;
			UnigineToMyraKeyMap[(int)Input.KEY.DIGIT_0] = Keys.D0;
			UnigineToMyraKeyMap[(int)Input.KEY.MINUS] = Keys.OemMinus;
			UnigineToMyraKeyMap[(int)Input.KEY.EQUALS] = Keys.OemPlus;
			UnigineToMyraKeyMap[(int)Input.KEY.BACKSPACE] = Keys.Back;
			UnigineToMyraKeyMap[(int)Input.KEY.TAB] = Keys.Tab;
			UnigineToMyraKeyMap[(int)Input.KEY.Q] = Keys.Q;
			UnigineToMyraKeyMap[(int)Input.KEY.W] = Keys.W;
			UnigineToMyraKeyMap[(int)Input.KEY.E] = Keys.E;
			UnigineToMyraKeyMap[(int)Input.KEY.R] = Keys.R;
			UnigineToMyraKeyMap[(int)Input.KEY.T] = Keys.T;
			UnigineToMyraKeyMap[(int)Input.KEY.Y] = Keys.Y;
			UnigineToMyraKeyMap[(int)Input.KEY.U] = Keys.U;
			UnigineToMyraKeyMap[(int)Input.KEY.I] = Keys.I;
			UnigineToMyraKeyMap[(int)Input.KEY.O] = Keys.O;
			UnigineToMyraKeyMap[(int)Input.KEY.P] = Keys.P;
			UnigineToMyraKeyMap[(int)Input.KEY.LEFT_BRACKET] = Keys.OemOpenBrackets;
			UnigineToMyraKeyMap[(int)Input.KEY.RIGHT_BRACKET] = Keys.OemCloseBrackets;
			UnigineToMyraKeyMap[(int)Input.KEY.ENTER] = Keys.Enter;
			UnigineToMyraKeyMap[(int)Input.KEY.CAPS_LOCK] = Keys.CapsLock;
			UnigineToMyraKeyMap[(int)Input.KEY.A] = Keys.A;
			UnigineToMyraKeyMap[(int)Input.KEY.S] = Keys.S;
			UnigineToMyraKeyMap[(int)Input.KEY.D] = Keys.D;
			UnigineToMyraKeyMap[(int)Input.KEY.F] = Keys.F;
			UnigineToMyraKeyMap[(int)Input.KEY.G] = Keys.G;
			UnigineToMyraKeyMap[(int)Input.KEY.H] = Keys.H;
			UnigineToMyraKeyMap[(int)Input.KEY.J] = Keys.J;
			UnigineToMyraKeyMap[(int)Input.KEY.K] = Keys.K;
			UnigineToMyraKeyMap[(int)Input.KEY.L] = Keys.L;
			UnigineToMyraKeyMap[(int)Input.KEY.SEMICOLON] = Keys.OemSemicolon;
			UnigineToMyraKeyMap[(int)Input.KEY.QUOTE] = Keys.OemQuotes;
			UnigineToMyraKeyMap[(int)Input.KEY.BACK_SLASH] = Keys.OemBackslash;
			UnigineToMyraKeyMap[(int)Input.KEY.LEFT_SHIFT] = Keys.LeftShift;
			UnigineToMyraKeyMap[(int)Input.KEY.LESS] = Keys.Apps;
			UnigineToMyraKeyMap[(int)Input.KEY.Z] = Keys.Z;
			UnigineToMyraKeyMap[(int)Input.KEY.X] = Keys.X;
			UnigineToMyraKeyMap[(int)Input.KEY.C] = Keys.C;
			UnigineToMyraKeyMap[(int)Input.KEY.V] = Keys.V;
			UnigineToMyraKeyMap[(int)Input.KEY.B] = Keys.B;
			UnigineToMyraKeyMap[(int)Input.KEY.N] = Keys.N;
			UnigineToMyraKeyMap[(int)Input.KEY.M] = Keys.M;
			UnigineToMyraKeyMap[(int)Input.KEY.COMMA] = Keys.OemComma;
			UnigineToMyraKeyMap[(int)Input.KEY.DOT] = Keys.OemPeriod;
			UnigineToMyraKeyMap[(int)Input.KEY.SLASH] = Keys.OemQuestion;
			UnigineToMyraKeyMap[(int)Input.KEY.RIGHT_SHIFT] = Keys.RightShift;
			UnigineToMyraKeyMap[(int)Input.KEY.LEFT_CTRL] = Keys.LeftControl;
			UnigineToMyraKeyMap[(int)Input.KEY.LEFT_CMD] = Keys.LeftWindows;
			UnigineToMyraKeyMap[(int)Input.KEY.LEFT_ALT] = Keys.LeftAlt;
			UnigineToMyraKeyMap[(int)Input.KEY.SPACE] = Keys.Space;
			UnigineToMyraKeyMap[(int)Input.KEY.RIGHT_ALT] = Keys.RightAlt;
			UnigineToMyraKeyMap[(int)Input.KEY.RIGHT_CMD] = Keys.RightWindows;
			UnigineToMyraKeyMap[(int)Input.KEY.MENU] = Keys.None;
			UnigineToMyraKeyMap[(int)Input.KEY.RIGHT_CTRL] = Keys.RightControl;
			UnigineToMyraKeyMap[(int)Input.KEY.INSERT] = Keys.Insert;
			UnigineToMyraKeyMap[(int)Input.KEY.DELETE] = Keys.Delete;
			UnigineToMyraKeyMap[(int)Input.KEY.HOME] = Keys.Home;
			UnigineToMyraKeyMap[(int)Input.KEY.END] = Keys.End;
			UnigineToMyraKeyMap[(int)Input.KEY.PGUP] = Keys.PageUp;
			UnigineToMyraKeyMap[(int)Input.KEY.PGDOWN] = Keys.PageDown;
			UnigineToMyraKeyMap[(int)Input.KEY.UP] = Keys.Up;
			UnigineToMyraKeyMap[(int)Input.KEY.LEFT] = Keys.Left;
			UnigineToMyraKeyMap[(int)Input.KEY.DOWN] = Keys.Down;
			UnigineToMyraKeyMap[(int)Input.KEY.RIGHT] = Keys.Right;
			UnigineToMyraKeyMap[(int)Input.KEY.NUM_LOCK] = Keys.NumLock;
			UnigineToMyraKeyMap[(int)Input.KEY.NUMPAD_DIVIDE] = Keys.Divide;
			UnigineToMyraKeyMap[(int)Input.KEY.NUMPAD_MULTIPLY] = Keys.Multiply;
			UnigineToMyraKeyMap[(int)Input.KEY.NUMPAD_MINUS] = Keys.Subtract;
			UnigineToMyraKeyMap[(int)Input.KEY.NUMPAD_DIGIT_7] = Keys.NumPad7;
			UnigineToMyraKeyMap[(int)Input.KEY.NUMPAD_DIGIT_8] = Keys.NumPad8;
			UnigineToMyraKeyMap[(int)Input.KEY.NUMPAD_DIGIT_9] = Keys.NumPad9;
			UnigineToMyraKeyMap[(int)Input.KEY.NUMPAD_PLUS] = Keys.Add;
			UnigineToMyraKeyMap[(int)Input.KEY.NUMPAD_DIGIT_4] = Keys.NumPad4;
			UnigineToMyraKeyMap[(int)Input.KEY.NUMPAD_DIGIT_5] = Keys.NumPad5;
			UnigineToMyraKeyMap[(int)Input.KEY.NUMPAD_DIGIT_6] = Keys.NumPad6;
			UnigineToMyraKeyMap[(int)Input.KEY.NUMPAD_DIGIT_1] = Keys.NumPad1;
			UnigineToMyraKeyMap[(int)Input.KEY.NUMPAD_DIGIT_2] = Keys.NumPad2;
			UnigineToMyraKeyMap[(int)Input.KEY.NUMPAD_DIGIT_3] = Keys.NumPad3;
			UnigineToMyraKeyMap[(int)Input.KEY.NUMPAD_ENTER] = Keys.Enter;
			UnigineToMyraKeyMap[(int)Input.KEY.NUMPAD_DIGIT_0] = Keys.NumPad0;
			UnigineToMyraKeyMap[(int)Input.KEY.NUMPAD_DOT] = Keys.Decimal;
		}
	}
}
