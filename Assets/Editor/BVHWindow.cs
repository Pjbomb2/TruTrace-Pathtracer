 using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
 using UnityEngine;
 using UnityEditor;
using CommonVars;
 using System.Xml;


public class EditModeFunctions : EditorWindow {
     [MenuItem("Window/BVH Options")]
     public static void ShowWindow() {
         GetWindow<EditModeFunctions>("BVH Rebuild");
     }

      private void OnStartAsyncCombined() {
         EditorUtility.SetDirty(GameObject.Find("Scene").GetComponent<AssetManager>());
         GameObject.Find("Scene").GetComponent<AssetManager>().EditorBuild();
      }
      public bool UseSVGF = false;
      public bool UseNEE = false;
      public int Atrous_Kernel_Sizes = 1;
      public int BounceCount = 24;
      public bool UseRussianRoulette = true;
      public bool AllowConverge = true;
      public bool DynamicTLAS = true;
      private RayTracingMaster RayMaster;
      public float SunDir = 0.0f;
      private void OnGUI() {
         RayMaster = Camera.main.GetComponent<RayTracingMaster>();
         Rect CombinedBuilderButton = new Rect(10,10,(position.width - 10) / 2, 20);
         Rect ClearParentData = new Rect(10,35,(position.width - 10) / 2, 20);
         Rect SunDirLabel   =         new Rect(10,60,(position.width - 10) / 2, 20);
         Rect Sun_Dir_Slider = new Rect(Mathf.Max((position.width - 10) / 4,145), 60, (position.width - 10) / 4, 20);
         Rect BounceCountInput =   new Rect(Mathf.Max((position.width - 10) / 4,145), 85, (position.width - 10) / 4, 20);
         Rect BounceCountLabel =   new Rect(10, 85, Mathf.Max((position.width - 10) / 4,145), 20);
         Rect RussianRouletteToggle = new Rect(10, 110, (position.width - 10) / 2, 20);
         Rect DynamicTLASToggle =       new Rect(10, 135, (position.width - 10) / 2, 20);
         Rect AllowConvergeToggle =       new Rect(10, 160, (position.width - 10) / 2, 20);
         Rect UseNEEToggle =       new Rect(10, 185, (position.width - 10) / 2, 20);
         Rect SVGFToggle =       new Rect(10, 210, (position.width - 10) / 2, 20);
         int SVGFVertOffset = 235;
         
         AllowConverge = GUI.Toggle(AllowConvergeToggle, AllowConverge, "Allow Image Accumulation");
         DynamicTLAS = GUI.Toggle(DynamicTLASToggle, DynamicTLAS, "Enable Object Moving");
         UseNEE = GUI.Toggle(UseNEEToggle, UseNEE, "Use Next Event Estimation");
         RayMaster.DoTLASUpdates = DynamicTLAS;
         RayMaster.AllowConverge = AllowConverge;
         RayMaster.UseNEE = UseNEE;
         
         if (GUI.Button(CombinedBuilderButton, "Build Aggregated BVH")) {
            OnStartAsyncCombined();
         }
         if (GUI.Button(ClearParentData, "Clear Parent Data")) {
            EditorUtility.SetDirty(GameObject.Find("Scene").GetComponent<AssetManager>());
            GameObject.Find("Scene").GetComponent<AssetManager>().ClearAll();
         }
         
         GUI.Label(BounceCountLabel, "Max Bounces");
         GUI.Label(SunDirLabel, "Sun Position");
         SunDir = GUI.HorizontalSlider(Sun_Dir_Slider, SunDir, 0.0f, 3.14159f * 2.0f);
         RayMaster.SunDirFloat = SunDir;
         BounceCount = EditorGUI.IntField(BounceCountInput, BounceCount);
         RayMaster.bouncecount = BounceCount;
         UseRussianRoulette = GUI.Toggle(RussianRouletteToggle, UseRussianRoulette, "Use Russian Roulette");
         RayMaster.UseRussianRoulette = UseRussianRoulette;
         UseSVGF = GUI.Toggle(SVGFToggle, UseSVGF, "Use SVGF Denoiser");
         RayMaster = Camera.main.GetComponent<RayTracingMaster>() as RayTracingMaster;
         if(UseSVGF) {
            Rect Atrous_Kernel_Size = new Rect(Mathf.Max((position.width - 10) / 4,145), SVGFVertOffset, (position.width - 10) / 4, 20);
            Rect Atrous_Kernel_Size_Lable = new Rect(10, SVGFVertOffset, Mathf.Max((position.width - 10) / 4,145), 20);
            
            GUI.Label(Atrous_Kernel_Size_Lable, "Atrous Kernel Size");
            Atrous_Kernel_Sizes = EditorGUI.IntField(Atrous_Kernel_Size, Atrous_Kernel_Sizes);
            SVGFVertOffset += 100;
            RayMaster.AtrousKernelSizes = Atrous_Kernel_Sizes;
         }
         RayMaster.UseSVGF = UseSVGF;
     }

void OnInspectorUpdate() {
   Repaint();
}


}
 


 