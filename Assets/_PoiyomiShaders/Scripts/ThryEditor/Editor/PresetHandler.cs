// Material/Shader Inspector for Unity 2017/2018
// Copyright (C) 2019 Thryrallo

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Thry
{
    public class PresetHandler
    {

        //variables for the presets
        private bool hasPresets = false;
        private bool presetsLoaded = false;
        private string presetsFilePath = null;
        private string[] propertyNames;
        private Dictionary<string, List<string[]>> presets = new Dictionary<string, List<string[]>>(); //presets

        //variabled for the preset selector
        public int selectedPreset = 0;
        private string[] presetOptions;
        string newPresetName = Locale.editor.Get("new_preset_name");

        private MaterialProperty[] props;
        private Material[] materials;

        public PresetHandler(MaterialProperty[] props)
        {
            testPresetsChanged(props);
        }

        public PresetHandler(MaterialProperty p)
        {
            testPresetsChanged(p);
        }

        public PresetHandler(Shader s)
        {
            Material m = new Material(s);
            MaterialProperty[] props = MaterialEditor.GetMaterialProperties(new Material[] { m });
            testPresetsChanged(props);
        }

        //test if the path to the presets has changed
        public void testPresetsChanged(MaterialProperty[] props)
        {
            MaterialProperty presetsProperty = ShaderEditor.FindProperty(props, ShaderEditor.PROPERTY_NAME_PRESETS_FILE);
            loadProperties(props);
            if (presetsProperty != null)
            {
                hasPresets = true;
                testPresetsChanged(presetsProperty);
            }
            else
            {
                hasPresets = false;
            }
        }

        //test if the path to the presets has changed
        public void testPresetsChanged(MaterialProperty p)
        {
            string newPath = stringToFilePath(p.displayName);
            if (presetsFilePath != newPath)
            {
                presetsFilePath = newPath;
                if (newPath != null)
                {
                    loadPresets();
                }
            }
        }

        //get the path to the presets from file name
        private string stringToFilePath(string name)
        {
            string[] guid = AssetDatabase.FindAssets(name, null);
            if (guid.Length > 0)
            {
                return AssetDatabase.GUIDToAssetPath(guid[0]);
            }
            return null;
        }

        public Dictionary<string, List<string[]>> getPresets()
        {
            return presets;
        }

        public bool shaderHasPresetPath()
        {
            return hasPresets;
        }

        //draws presets if exists
        public void drawPresets(MaterialProperty[] props, Material[] materials)
        {
            this.props = props;
            this.materials = materials;
            if (hasPresets && presetsLoaded)
            {
                if(GUILayout.Button(Styles.presets_icon, EditorStyles.largeLabel,GUILayout.MaxWidth(20), GUILayout.MaxHeight(20)))
                {
                    Popup(GUILayoutUtility.GetLastRect(), this);
                }
                /*
                int pressetPreset = EditorGUILayout.Popup(selectedPreset, presetOptions, GUILayout.MaxWidth(100));
                if (pressetPreset != selectedPreset)
                {
                    ;
                    if (pressetPreset < presetOptions.Length - 2) applyPreset(presetOptions[pressetPreset], props, materials);
                    if (pressetPreset == presetOptions.Length - 2) PresetEditor.open();
                }
                if (pressetPreset == presetOptions.Length - 1) selectedPreset = pressetPreset;
                else selectedPreset = 0;
                if (pressetPreset == presetOptions.Length - 1) drawNewPreset(props, materials);*/
            }
            else if (hasPresets && !presetsLoaded)
            {
                GUILayout.Label(Locale.editor.Get("message_presets_file_missing"));
            }
        }

        public void drawNewPreset()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(Locale.editor.Get("add_preset"));
            newPresetName = GUILayout.TextField(newPresetName, GUILayout.MaxWidth(100));

            if (GUILayout.Button(Locale.editor.Get("add"), GUILayout.Width(40), GUILayout.Height(20)))
            {
                addNewPreset(newPresetName, props, materials);
            }
            GUILayout.EndHorizontal();
        }

        //loads presets from file
        public void loadPresets()
        {
            presets.Clear();
            StreamReader reader = new StreamReader(presetsFilePath);
            string line;
            List<string[]> currentPreset = null;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Length > 0 && !line.StartsWith("//"))
                {
                    if (line.Contains("="))
                    {
                        currentPreset.Add(line.Split(new string[] { " = " }, System.StringSplitOptions.None));
                    }
                    else
                    {
                        currentPreset = new List<string[]>();
                        presets.Add(line, currentPreset);
                    }
                }
            }
            reader.Close();
            presetOptions = new string[presets.Count];
            int i = 0;
            foreach (string k in presets.Keys) presetOptions[i++] = k;
            presetsLoaded = true;
        }

        private void loadProperties(MaterialProperty[] props)
        {
            List<string> propertyNames = new List<string>();
            for (int i = 0; i < props.Length; i++)
            {
                if (props[i].flags != MaterialProperty.PropFlags.HideInInspector) propertyNames.Add(props[i].name);
            }
            this.propertyNames = propertyNames.ToArray();
        }

        public string[] getPropertyNames()
        {
            return propertyNames;
        }

        public List<string[]> getPropertiesOfPreset(string presetName)
        {
            List<string[]> returnList = new List<string[]>();
            presets.TryGetValue(presetName, out returnList);
            return returnList;
        }

        public void updatePresetProperty(string presetName, string property, string value)
        {
            List<string[]> properties = new List<string[]>();
            presets.TryGetValue(presetName, out properties);
            for (int i = 0; i < properties.Count; i++) if (properties[i][0] == property) properties[i][1] = value;
            this.savePresets();
        }

        public void removePresetProperty(string presetName, string property)
        {
            List<string[]> properties = new List<string[]>();
            presets.TryGetValue(presetName, out properties);
            for (int i = 0; i < properties.Count; i++) if (properties[i][0] == property) properties.RemoveAt(i);
            this.savePresets();
        }

        public void removePreset(string presetName)
        {
            removeFromPresetOptions(presetName);
            presets.Remove(presetName);
            savePresets();
            ShaderEditor.repaint();
        }

        public void addNewPreset(string presetName)
        {
            presets.Add(presetName, new List<string[]>());
            addToPresetOptions(presetName);
            savePresets();
        }

        public void addNewPreset(string name, MaterialProperty[] props, Material[] materials)
        {
            //find all non default values

            //add to presets list
            List<string[]> sets = new List<string[]>();

            Material defaultValues = new Material(materials[0].shader);

            foreach (MaterialProperty p in props)
            {
                string[] set = new string[] { p.name, "" };
                bool empty = false;
                switch (p.type)
                {
                    case MaterialProperty.PropType.Float:
                    case MaterialProperty.PropType.Range:
                        if (defaultValues.GetFloat(Shader.PropertyToID(set[0])) == p.floatValue) empty = true;
                        set[1] = "" + p.floatValue;
                        break;
                    case MaterialProperty.PropType.Texture:
                        if (p.textureValue == null || p.textureValue.Equals(defaultValues.GetTexture(Shader.PropertyToID(set[0])))) empty = true;
                        else set[1] = "" + AssetDatabase.GetAssetPath(p.textureValue);
                        /*Vector2 default_scale = defaultValues.GetTextureScale(Shader.PropertyToID(set[0]));
                        Vector2 default_offset = defaultValues.GetTextureOffset(Shader.PropertyToID(set[0]));
                        if (p.textureScaleAndOffset.x != default_scale.x || p.textureScaleAndOffset.y != default_scale.y || p.textureScaleAndOffset.z != default_offset.x || p.textureScaleAndOffset.w != default_offset.y)
                            set[1]*/ 
                        break;
                    case MaterialProperty.PropType.Vector:
                        if (p.vectorValue.Equals(defaultValues.GetVector(Shader.PropertyToID(set[0])))) empty = true;
                        set[1] = "" + p.vectorValue.x + "," + p.vectorValue.y + "," + p.vectorValue.z + "," + p.vectorValue.w;
                        break;
                    case MaterialProperty.PropType.Color:
                        if (p.colorValue.Equals(defaultValues.GetColor(Shader.PropertyToID(set[0])))) empty = true;
                        set[1] = "" + p.colorValue.r + "," + p.colorValue.g + "," + p.colorValue.b + "," + p.colorValue.a;
                        break;
                }
                if (p.flags != MaterialProperty.PropFlags.HideInInspector && !empty) sets.Add(set);
            }

            //fix all preset variables
            presets.Add(name, sets);
            addToPresetOptions(name);
            newPresetName = Locale.editor.Get("new_preset_name");

            //save all presets into file
            savePresets();
        }

        private void addToPresetOptions(string name)
        {
            string[] newPresetOptions = new string[presetOptions.Length + 1];
            for (int i = 0; i < presetOptions.Length; i++) newPresetOptions[i] = presetOptions[i];
            newPresetOptions[newPresetOptions.Length - 1] = name;
            presetOptions = newPresetOptions;
        }

        private void removeFromPresetOptions(string name)
        {
            string[] newPresetOptions = new string[presetOptions.Length - 1];
            int i = 0;
            foreach (string p in presetOptions) if (p != name) newPresetOptions[i++] = p;
            presetOptions = newPresetOptions;
        }

        public void setPreset(string presetName, List<string[]> list)
        {
            presets.Remove(presetName);
            presets.Add(presetName, list);
            savePresets();
        }

        private void savePresets()
        {
            StreamWriter writer = new StreamWriter(presetsFilePath, false);
            foreach (KeyValuePair<string, List<string[]>> preset in presets)
            {
                writer.WriteLine(preset.Key);
                foreach (string[] set in preset.Value) writer.WriteLine(set[0] + " = " + set[1]);
                writer.WriteLine("");
            }
            writer.Close();
        }

        public void applyPreset(string presetName, MaterialProperty[] props, Material[] materials)
        {
            ShaderEditor.addUndo(Locale.editor.Get("apply_preset") +": " + presetName);
            List<string[]> sets;
            if (presets.TryGetValue(presetName, out sets))
            {
                foreach (string[] set in sets)
                {
                    MaterialHelper.SetMaterialValue(set[0], set[1]);
                }
            }
            ShaderEditor.loadValuesFromMaterial();
            ShaderEditor.repaint();
        }

        private static PresetPopup window;
        public static void Popup(Rect activeation_rect, PresetHandler presetHandler)
        {
            Vector2 pos = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
            pos.x = Mathf.Min(EditorWindow.focusedWindow.position.x + EditorWindow.focusedWindow.position.width - 250, pos.x);
            pos.y = Mathf.Min(EditorWindow.focusedWindow.position.y + EditorWindow.focusedWindow.position.height - 200, pos.y);

            if (window != null)
                window.Close();
            window = ScriptableObject.CreateInstance<PresetPopup>();
            window.position = new Rect(pos.x, pos.y, 250, 200);
            window.Init(presetHandler);
            window.ShowPopup();
        }

        private class PresetPopup : EditorWindow
        {
            private Vector2 scrollPos;
            private PresetHandler presetHandler;

            public void Init(PresetHandler presetHandler)
            {
                this.presetHandler = presetHandler;
            }

            public new Vector2 minSize = new Vector2(250, 200);

            void OnGUI()
            {
                GUILayout.Label(Locale.editor.Get("presets"), EditorStyles.boldLabel);

                if (GUILayout.Button(Locale.editor.Get("manage_presets"))){
                    PresetEditor.open();
                }
                presetHandler.drawNewPreset();

                float listMaxHeight = this.position.height - 30;

                scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.MaxHeight(listMaxHeight));
                for (int i = 0; i < presetHandler.presetOptions.Length; i++)
                {
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button(Locale.editor.Get("apply_preset")))
                    {
                        presetHandler.applyPreset(presetHandler.presetOptions[i], presetHandler.props, presetHandler.materials);
                    }
                    EditorGUILayout.LabelField(presetHandler.presetOptions[i]);
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndScrollView();
                if (GUI.Button(new Rect(0, this.position.height - 30, this.position.width, 30), "Close"))
                    this.Close();
            }
        }
    }
}