using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ETEditor
{
	public class AssetImportSetting : AssetPostprocessor
	{
		private void OnPreprocessTexture()
		{
			TextureImporter importer = assetImporter as TextureImporter;
			importer.textureType = TextureImporterType.Sprite;
		}
	}
}
