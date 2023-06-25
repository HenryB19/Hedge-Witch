using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Linq;

public class Atlaser : MonoBehaviour {
  string gradientsSourceDir = "/Textures/Gradient/Individual/";
  string outputDir = "/Textures/Gradient/gradientATLAS.png";
  string[] filePaths = new string[32];
  Texture2D atlasedTex;
  public void Atlas() {
    FindGradients();
    ReadWriteGradients();
    SaveGradients();
  }

  void FindGradients() {
    string assetsPath = Application.dataPath;
    string gradientsPath = assetsPath + gradientsSourceDir;
    DirectoryInfo info = new DirectoryInfo(gradientsPath);
    FileInfo[] fileInfo = info.GetFiles("*.png");
    foreach (var file in fileInfo) {
      filePaths[GetIntFromString(file.Name)] = file.FullName;
    }
  }

  void ReadWriteGradients() {
    Texture2D currentTex = new Texture2D(2, 2);
    byte[] rawData;
    Color[] blackpixels = Enumerable.Repeat(new Color(0, 0, 0, 1), 32).ToArray();
    atlasedTex = new Texture2D(1, 1024);
    for (int i = 0; i < filePaths.Length; ++i) {
      if (filePaths[i] == null) {
        atlasedTex.SetPixels(0, i*32, 1, 32, blackpixels);
      }
      else {
        rawData = System.IO.File.ReadAllBytes(filePaths[i]);
        currentTex.LoadImage(rawData);
        for (int j = 0; j < 32; ++j) {
          atlasedTex.SetPixel(0, (i*32)+j, currentTex.GetPixel(15, j));
        }
      }
    }
  }

  void SaveGradients() {
    atlasedTex.Apply();
    SaveTexture.SaveTextureToPNG(atlasedTex, Application.dataPath + outputDir, 1, 1024);
  }

  static int GetIntFromString(string s) {
    return int.Parse(Regex.Replace(s, "[^0-9]", ""));
  }
}
