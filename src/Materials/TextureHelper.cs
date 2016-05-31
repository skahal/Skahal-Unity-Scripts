//using UnityEngine;
//using System.IO;
//using System.Text;
//using System.Collections.Generic;
//using Skahal.Logging;
//
//public static class TextureHelper
//{
//	// TODO: criar um leitor para ler de forma facil os Vector2 das posicoes das texturas internas.
// 	public static IDictionary<string, Rect> CreateTexturesAtlas(string atlasName, string texturesFolder, params string[] texturesNames)
// 	{
// 		Texture2D atlas = new Texture2D(512, 512, TextureFormat.RGB24, true);
// 		Texture2D[] atlasTextures = new Texture2D[texturesNames.Length];
// 		string texturesPath = Path.Combine(Application.dataPath, texturesFolder);
// 	
//		// Carrega as texturas.
// 		for (int i = 0; i < texturesNames.Length; i++)
// 		{
// 			atlasTextures[i] = LoadTexture(Path.Combine(texturesPath, texturesNames[i] + ".png"));
// 		}
// 	
//		// Monta o atlas.
// 		Rect[] atlasTexturesRects = atlas.PackTextures(atlasTextures, 1);
// 		Dictionary<string, Rect> resultRects = new Dictionary<string, Rect>();
// 	
//		// Cria o arquivo atlasName + .txt com as posicoes das texturas no mapa.
// 		StringBuilder builder = new StringBuilder();
// 		
//		for (int i = 0; i < atlasTextures.Length; i++)
// 		{
// 			Rect r = atlasTexturesRects[i];
// 			Rect resultR = new Rect(
//				r.xMin * atlas.width,
//				r.yMin * atlas.height,
//				r.width * atlas.width,
//				r.height * atlas.height);
//			
//			string textureName = atlasTextures[i].name;
// 		
//			resultRects.Add(textureName, resultR);
// 		
// 			builder.AppendFormat("{0} = ", atlasTextures[i].name);
// 			builder.AppendFormat("x: {0}, ", resultR.x);
// 			builder.AppendFormat("y: {0}, ", resultR.y);
// 			builder.AppendFormat("w: {0}, ", resultR.width);
// 			builder.AppendFormat("h: {0}{1}", resultR.height,
//				System.Environment.NewLine);
// 		}
// 	
//		string destFile = Path.Combine(texturesPath, atlasName);
// 
//		File.WriteAllText(destFile + ".txt", builder.ToString());
// 	
//		// Salva o arquivo do atlas.
// 		File.WriteAllBytes(destFile + ".png", atlas.EncodeToPNG());
// 	
//		SHLog.Debug("Texture atlas succeful generated on file " + destFile + ".png");
//		
//		return resultRects;
//    }
//	
//	private static Texture2D LoadTexture(string path)
//	{
//		Texture2D texture = new Texture2D(0, 0);
//		texture.LoadImage(File.ReadAllBytes(path));
//		texture.name = Path.GetFileNameWithoutExtension(path);
//		return texture;
//	}
//}
