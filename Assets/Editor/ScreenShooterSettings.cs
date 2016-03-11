﻿/*
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy of
 * the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under
 * the License.
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using Format = Borodar.ScreenShooter.ScreenshotConfig.Format;

namespace Borodar.ScreenShooter
{
    public class ScreenShooterSettings : ScriptableObject
    {
        public const string RESOURCE_NAME = "ScreenShooterSettings";

        public Camera Camera = Camera.main;
        public List<ScreenshotConfig> ScreenshotConfigs;
        public string SaveFolder = Application.dataPath + "/Screenshots";

        //---------------------------------------------------------------------
        // Public
        //---------------------------------------------------------------------

        public static ScreenShooterSettings Load()
        {
            var settings = Resources.Load<ScreenShooterSettings>(RESOURCE_NAME);
            if (settings != null) return settings;

            CreateAsset<ScreenShooterSettings>(RESOURCE_NAME, "Assets/Resources");
            settings = Resources.Load<ScreenShooterSettings>(RESOURCE_NAME);

            // Initial values
            settings.ScreenshotConfigs = new List<ScreenshotConfig>
            {
                new ScreenshotConfig("scr_sample", 800, 200, Format.PNG),
                new ScreenshotConfig("scr_sample_2", 200, 800, Format.PNG),
                new ScreenshotConfig("scr_sample_3", 1024, 768, Format.PNG),
                new ScreenshotConfig("scr_sample_4", 1920, 1080, Format.PNG)
            };

            return settings;
        }

        //---------------------------------------------------------------------
        // Helpers
        //---------------------------------------------------------------------

        private static void CreateAsset<T>(string baseName, string path) where T : ScriptableObject
        {
            if (baseName.Contains("/"))
                throw new ArgumentException("Base name should not contain slashes");

            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Path should not be null or empty");

            Directory.CreateDirectory(path);
            var assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + baseName + ".asset");

            var asset = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(asset, assetPathAndName);
            AssetDatabase.SaveAssets();
        }
    }
}