using UnityEditor;
using UnityEngine;

public class GradientAtlaser : Editor {
  [MenuItem("Gradient Atlaser/Atlas Gradients")]
  private static void AtlasGradients() {
    var atlaser = GameObject.FindObjectOfType<Atlaser>();
    if (atlaser != null) {
      GameObject.DestroyImmediate(atlaser.gameObject, true);
    }
    
    GameObject GradAtlas = new GameObject("Gradient Atlaser");
    GradAtlas.AddComponent<Atlaser>();
    GradAtlas.GetComponent<Atlaser>().Atlas();
    GameObject.DestroyImmediate(GradAtlas.gameObject, true);
  }
}
