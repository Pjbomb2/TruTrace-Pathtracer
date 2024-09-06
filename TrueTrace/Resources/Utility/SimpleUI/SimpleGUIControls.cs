using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonVars;

namespace TrueTrace {
    public class SimpleGUIControls : MonoBehaviour
    {
        RayTracingMaster RayMaster;
        public void Start() {
            RayMaster = GameObject.Find("Scene").GetComponent<RayTracingMaster>();
            RayMaster.LocalTTSettings.UseASVGF = true;
            RayMaster.LocalTTSettings.UseOIDN = false;
            RayMaster.LocalTTSettings.Accumulate = true;
            RayMaster.LocalTTSettings.OIDNFrameCount = 0;
        }
        public void ToggleASVGF(bool BoolIn) {
            RayMaster.LocalTTSettings.UseASVGF = BoolIn;
            RayMaster.LocalTTSettings.UseOIDN = !BoolIn;
        }
        public void SetOIDNFrameCount(string StringIn) {
            int IntResult = 0;
            if(int.TryParse(StringIn, out IntResult)) {
                RayMaster.LocalTTSettings.OIDNFrameCount = IntResult;
            }
        }

    }
}