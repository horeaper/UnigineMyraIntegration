using System.Drawing;
using FontStashSharp.Interfaces;
using Unigine;

namespace UnigineApp.MyraIntegration
{
	class Texture2DManager : ITexture2DManager
	{
		object ITexture2DManager.CreateTexture(int width, int height)
		{
			var texture = new Texture();
			texture.Create2D(width, height, Texture.FORMAT_RGBA8, Texture.FORMAT_USAGE_DYNAMIC | Texture.SAMPLER_FILTER_POINT);
			return texture;
		}

		Point ITexture2DManager.GetTextureSize(object obj)
		{
			var texture = (Texture)obj;
			return new Point(texture.GetWidth(), texture.GetHeight());
		}

		void ITexture2DManager.SetTextureData(object obj, Rectangle bounds, byte[] data)
		{
			using var image = new Image();
			image.Create2D(bounds.Width, bounds.Height, Image.FORMAT_RGBA8, 1, false);
			image.SetPixels(data);

			var texture = (Texture)obj;
			texture.SetImage2D(image, bounds.X, bounds.Y);

			image.SetPixels((byte[])null!);
		}
	}
}
