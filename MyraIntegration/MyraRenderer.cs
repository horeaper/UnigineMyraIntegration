using System.Drawing;
using System.Numerics;
using FontStashSharp;
using FontStashSharp.Interfaces;
using Myra.Graphics2D;
using Myra.Platform;
using Unigine;

namespace UnigineApp.MyraIntegration
{
	class MyraRenderer : IMyraRenderer
	{
		public ITexture2DManager TextureManager { get; } = new Texture2DManager();
		RendererType IMyraRenderer.RendererType => RendererType.Quad;

		readonly QuadBatcher quadBatcher = new();
		Rectangle currentScissor;

		public MyraRenderer()
		{
			var clientRenderSize = WindowManager.MainWindow.ClientRenderSize;
			currentScissor = new Rectangle(0, 0, clientRenderSize.x, clientRenderSize.y);
		}

		public void NewRender()
		{
			quadBatcher.NewBatch();
		}

		public void DrawToOutput()
		{
			quadBatcher.RenderBatch();
		}

		Rectangle IMyraRenderer.Scissor
		{
			get => currentScissor;
			set {
				if (value != currentScissor) {
					currentScissor = value;
					quadBatcher.SetScissorTest(value);
				}
			}
		}

		void IMyraRenderer.Begin(TextureFiltering textureFiltering)
		{
			//ignored
		}

		void IMyraRenderer.End()
		{
			//ignored
		}

		void IMyraRenderer.DrawSprite(object texture, Vector2 pos, Rectangle? src, FSColor color, float rotation, Vector2 scale, float depth)
		{
			//ignored
		}

		void IMyraRenderer.DrawQuad(object texture, ref VertexPositionColorTexture topLeft, ref VertexPositionColorTexture topRight, ref VertexPositionColorTexture bottomLeft, ref VertexPositionColorTexture bottomRight)
		{
			quadBatcher.DrawQuad((Texture)texture, ref topLeft, ref topRight, ref bottomLeft, ref bottomRight);
		}
	}
}
