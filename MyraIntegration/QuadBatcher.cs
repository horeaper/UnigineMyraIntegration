using System.Buffers;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using FontStashSharp.Interfaces;
using Unigine;

namespace UnigineApp.MyraIntegration
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct VertexLayout(VertexPositionColorTexture vertexData)
	{
		public vec2 Position = new(vertexData.Position.X, vertexData.Position.Y);
		public vec2 TexCoord = new(vertexData.TextureCoordinate.X, vertexData.TextureCoordinate.Y);
		public uint Color = vertexData.Color.PackedValue;
	}

	class QuadBatcher
	{
		const int MaxQuads = 2048;
		const int MaxVertices = MaxQuads * 4;
		const int MaxIndices = MaxQuads * 6;

		readonly MeshDynamic quadMesh;
		readonly Material quadMaterial;

		readonly VertexLayout[] vertexData = new VertexLayout[MaxVertices];
		int vertexCount;

		readonly List<(Texture?, Rectangle?, int)> renderData = new(16);
		Texture? lastTexture;
		Rectangle? lastScissor;

		public QuadBatcher()
		{
			//Mesh
			quadMesh = new MeshDynamic(MeshDynamic.USAGE_DYNAMIC_VERTEX);
			var vertexFormat = new MeshDynamic.Attribute[3];
			vertexFormat[0].type = MeshDynamic.TYPE_FLOAT;
			vertexFormat[0].offset = 0;
			vertexFormat[0].size = 2;
			vertexFormat[1].type = MeshDynamic.TYPE_FLOAT;
			vertexFormat[1].offset = 8;
			vertexFormat[1].size = 2;
			vertexFormat[2].type = MeshDynamic.TYPE_UCHAR;
			vertexFormat[2].offset = 16;
			vertexFormat[2].size = 4;
			quadMesh.SetVertexFormat(vertexFormat);

			//Material (uses ImGui's material)
			quadMaterial = Materials.FindManualMaterial("imgui").Inherit();

			//Pre-alloc indices
			var indexData = new int[MaxIndices];
			for (int i = 0, j = 0; i < MaxIndices; i += 6, j += 4) {
				indexData[i + 0] = j + 0;
				indexData[i + 1] = j + 1;
				indexData[i + 2] = j + 2;
				indexData[i + 3] = j + 3;
				indexData[i + 4] = j + 2;
				indexData[i + 5] = j + 1;
			}
			quadMesh.SetIndicesArray(indexData);
			quadMesh.FlushIndices();
		}

		public void NewBatch()
		{
			vertexCount = 0;
			renderData.Clear();
			lastTexture = null;
			lastScissor = null;
		}

		public void DrawQuad(Texture texture, ref VertexPositionColorTexture topLeft, ref VertexPositionColorTexture topRight, ref VertexPositionColorTexture bottomLeft, ref VertexPositionColorTexture bottomRight)
		{
			if (lastTexture != texture) {
				lastTexture = texture;
				renderData.Add((texture, null, vertexCount));
			}

			vertexData[vertexCount++] = new VertexLayout(topLeft);
			vertexData[vertexCount++] = new VertexLayout(topRight);
			vertexData[vertexCount++] = new VertexLayout(bottomLeft);
			vertexData[vertexCount++] = new VertexLayout(bottomRight);

			if (vertexCount >= MaxVertices) {
				RenderBatch();
				NewBatch();
			}
		}

		public void SetScissorTest(in Rectangle scissor)
		{
			if (lastScissor != scissor) {
				lastScissor = scissor;
				renderData.Add((null, lastScissor, vertexCount));
			}
		}

		public void RenderBatch()
		{
			if (vertexCount == 0 || renderData.Count == 0) {
				return;
			}

			var clientRenderSize = WindowManager.MainWindow.ClientRenderSize;

			//Render state
			RenderState.SaveState();
			RenderState.ClearStates();
			RenderState.SetBlendFunc(RenderState.BLEND_ONE, RenderState.BLEND_ONE_MINUS_SRC_ALPHA);
			RenderState.PolygonCull = RenderState.CULL_NONE;
			RenderState.DepthFunc = RenderState.DEPTH_NONE;
			RenderState.FlushStates();

			//Orthographic projection matrix
			float left = 0;
			float right = clientRenderSize.x;
			float top = 0;
			float bottom = clientRenderSize.y;
			var orthoProj = new mat4 {
				m00 = 2.0f / (right - left),
				m03 = (right + left) / (left - right),
				m11 = 2.0f / (top - bottom),
				m13 = (top + bottom) / (bottom - top),
				m22 = 0.5f,
				m23 = 0.5f,
				m33 = 1.0f
			};
			Renderer.Projection = orthoProj;

			//Shader
			var shader = quadMaterial.GetShaderForce("imgui");
			var pass = quadMaterial.GetRenderPass("imgui");
			Renderer.SetShaderParameters(pass, shader, quadMaterial, false);

			//Go
			quadMesh.Bind();

			//Write vertex data into dynamic mesh
			quadMesh.ClearVertex();
			unsafe {
				fixed (void* pVertexData = vertexData) {
					quadMesh.SetVertexArray((nint)pVertexData, vertexCount);
				}
			}
			quadMesh.FlushVertex();

			//Render everything out
			int currentVertex = 0;
			foreach (var (texture, scissor, vertexIndex) in renderData) {
				if (vertexIndex > currentVertex) {
					quadMesh.RenderSurface(MeshDynamic.MODE_TRIANGLES, 0, currentVertex / 4 * 6, vertexIndex / 4 * 6);
					currentVertex = vertexIndex;
				}

				if (texture != null) {
					RenderState.SetTexture(RenderState.BIND_FRAGMENT, 0, texture);
				}
				if (scissor != null) {
					int y = clientRenderSize.y - (scissor.Value.Y + scissor.Value.Height); //ScissorTest是右手坐标系，Y轴从屏幕下方往上数
					RenderState.SetScissorTest((float)scissor.Value.X / clientRenderSize.x, (float)y / clientRenderSize.y, (float)scissor.Value.Width / clientRenderSize.x, (float)scissor.Value.Height / clientRenderSize.y);
				}
			}
			if (currentVertex < vertexCount) {
				quadMesh.RenderSurface(MeshDynamic.MODE_TRIANGLES, 0, currentVertex / 4 * 6, vertexCount / 4 * 6);
			}

			//Clean up
			quadMesh.Unbind();
			RenderState.RestoreState();
		}
	}
}
