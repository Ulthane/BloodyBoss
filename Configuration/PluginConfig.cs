﻿using System.IO;
using System.Text.RegularExpressions;
using BepInEx.Configuration;


namespace BloodyBoss.Configuration
{
    internal class PluginConfig
    {
        private static ConfigFile _mainConfig;

        public static ConfigEntry<bool> Enabled { get; private set; }
        public static ConfigEntry<int> BuffForWorldBoss { get; private set; }
        public static ConfigEntry<string> SpawnMessageBossTemplate { get; private set; }
        public static ConfigEntry<string> DespawnMessageBossTemplate { get; private set; }
        public static ConfigEntry<string> KillMessageBossTemplate { get; private set; }
        public static ConfigEntry<string> VBloodFinalConcatCharacters { get; private set; }

        public static void Initialize()
        {
            var bepInExConfigFolder = BepInEx.Paths.ConfigPath ?? Path.Combine("BepInEx", "config");
            var configFolder = Path.Combine(bepInExConfigFolder, "BloodyBoss");
            if (!Directory.Exists(configFolder))
            {
                Directory.CreateDirectory(configFolder);
            }
            var mainConfigFilePath = Path.Combine(configFolder, "BloodyBoss.cfg");
            _mainConfig = File.Exists(mainConfigFilePath) ? new ConfigFile(mainConfigFilePath, false) : new ConfigFile(mainConfigFilePath, true);

            Enabled = _mainConfig.Bind("Main", "Enabled", true, "Determines whether the random encounter timer is enabled or not.");
            KillMessageBossTemplate = _mainConfig.Bind("Main", "KillMessageBossTemplate", "The Boss has been defeated. Congratulations to #user# for beating #vblood#!", "The message that will appear globally once the boss gets killed.");
            SpawnMessageBossTemplate = _mainConfig.Bind("Main", "SpawnMessageBossTemplate", "A Boss #worldbossname# has been summon you got #time# minutes to defeat it!.", "The message that will appear globally one the boss gets spawned.");
            DespawnMessageBossTemplate = _mainConfig.Bind("Main", "DespawnMessageBossTemplate", "You failed to kill the Boss #worldbossname# in time.", "The message that will appear globally if the players failed to kill the boss.");
            BuffForWorldBoss = _mainConfig.Bind("Main", "BuffForWorldBoss", 1163490655, "Buff that applies to each of the Bosses that we create with our mod.");
            VBloodFinalConcatCharacters = _mainConfig.Bind("Main", "WorldBossFinalConcatCharacters", "and", "Final string for concat two or more players kill a WorldBoss Boss.");
        }
        public static void Destroy()
        {
            _mainConfig.Clear();
        }

        private static string CleanupName(string name)
        {
            var rgx = new Regex("[^a-zA-Z0-9 -]");
            return rgx.Replace(name, "");
        }

    }
}