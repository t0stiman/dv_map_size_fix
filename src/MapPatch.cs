using System.Collections;
using HarmonyLib;
using UnityEngine;

namespace dv_map_size_fix
{
	[HarmonyPatch(typeof(WorldMap), "OnEnable")]
	class MapPatch
	{
		private static IEnumerator coroutine;
		
		static void Postfix(ref WorldMap __instance)
		{
			coroutine = SetScale(__instance);
			__instance.StartCoroutine(coroutine);
		}

		private static IEnumerator SetScale(WorldMap theWorldMap)
		{
			//yes, this is necessary
			yield return new WaitForSeconds(0.01f);

			var scale = Main.MySettings.mapScale;
			Main.Log($"set {scale}");
			theWorldMap.transform.localScale = new Vector3(scale, scale, scale);
		}
	}
}