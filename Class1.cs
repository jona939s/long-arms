using System;
using System.Collections.Generic;
using HarmonyLib;
using BepInEx;
using UnityEngine;
using System.Reflection;
using UnityEngine.XR;
using Photon.Pun;
using System.IO;
using System.Net;
using Photon.Realtime;
using UnityEngine.Rendering;

/* This mod was made by JJoe (jona939s on github) */

namespace longArms
{
    [BepInPlugin("org.J-JOE.monkeytag.LongArms", "LongArms", "0.5.0.1")]
    public class MyPatcher : BaseUnityPlugin
    {
        public void Awake()
        {
            var harmony = new Harmony("com.J-JOE.monkeytag.LongArms");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            GorillaLocomotion.Player.Instance.maxArmLength = 100f;
        }
    }

    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("Update", MethodType.Normal)]
    public class Class1
    {
        
        private static float timeSinceLastChange = 0f;

        static float myVarY1 = 0f;
        static float myVarY2 = 0f;

        static bool gain = false;
        static bool less = false;
        static bool reset = false;
        static void Postfix(GorillaLocomotion.Player __instance)
        {
            {
                List<InputDevice> list = new List<InputDevice>();
                InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller, list);
                list[0].TryGetFeatureValue(CommonUsages.gripButton, out gain);
                list[0].TryGetFeatureValue(CommonUsages.triggerButton, out less);
                list[0].TryGetFeatureValue(CommonUsages.primaryButton, out reset);

                timeSinceLastChange += Time.deltaTime;
                if (timeSinceLastChange <= 0.2f)
                {
                    return;
                }

                GorillaLocomotion.Player.Instance.leftHandOffset = new Vector3(0f, myVarY1, 0f);
                GorillaLocomotion.Player.Instance.rightHandOffset = new Vector3(0f, myVarY2, 0f);



                if (gain)
                {
                    timeSinceLastChange = 0f;

                    myVarY1 = myVarY1 + 0.1f;
                    myVarY2 = myVarY2 + 0.1f;

                    if (myVarY1 >= 51f)
                    {
                        myVarY1 = 50f;
                        myVarY2 = 50f;
                    }

                }

                if (less)
                {
                    timeSinceLastChange = 0f;

                    myVarY1 = myVarY1 - 0.1f;
                    myVarY2 = myVarY2 - 0.1f;

                    if (myVarY2 <= -6f)
                    {
                        myVarY1 = -5f;
                        myVarY2 = -5f;
                    }
                }
                if (reset)
                {
                    timeSinceLastChange = 0f;

                    myVarY1 = 0f;
                    myVarY2 = 0f;
                }
            }
        }
    }
}
/* This mod was made by JJoe (jona939s on github)*/