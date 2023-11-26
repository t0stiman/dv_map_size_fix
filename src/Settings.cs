using UnityEngine;
using UnityModManagerNet;

namespace dv_map_size_fix
{
	public class Settings: UnityModManager.ModSettings
	{
		public float mapScale = 0.9f;
		private string text;

		public void Draw(UnityModManager.ModEntry modEntry)
		{
			if (text == "")
			{
				text = mapScale.ToString();
			}
			
			GUILayout.Label("Map size (1 is normal size)");
			text = GUILayout.TextField(text);
			
			float newValue;
			if (float.TryParse(text, out newValue))
			{
				mapScale = newValue;
			}
			else
			{
				GUILayout.Label($"'{text}' is not a number");
			}
		}
		
		public override void Save(UnityModManager.ModEntry modEntry)
		{
			Save(this, modEntry);
		}
	}
}