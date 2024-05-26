﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalClock
{
    public class ClockStyle: ConfigurationSection
    {
        private static readonly string ConfigFilePath = "./Settings.xml";

        private static ClockStyle? _instance;
        public static ClockStyle Instance
        {
            get
            {
                _instance ??= Load();
                return _instance;
            }
        }

        private static ClockStyle Load()
        {
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = ConfigFilePath };
            var style = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            return (ClockStyle)style.GetSection("ClockStyle") ?? new ClockStyle();
        }

        /// <summary>
        /// 时钟位置
        /// </summary>
        [ConfigurationProperty(nameof(Location), IsRequired = true, DefaultValue = "TopLeft")]
        public string Location
        {
            get => (string)this[nameof(Location)];
            set => this[nameof(Location)] = value;
        }

        /// <summary>
        /// 背景颜色
        /// </summary>
        [ConfigurationProperty(nameof(BackColor), IsRequired = true, DefaultValue = "Black")]
        public string BackColor
        {
            get => (string)this[nameof(BackColor)];
            set => this[nameof(BackColor)] = value;
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [ConfigurationProperty(nameof(FontColor),IsRequired =true,DefaultValue ="White")]
        public string FontColor
        {
            get=> (string)this[nameof(FontColor)];
            set=> this[nameof(FontColor)] = value;
        }

        public void Save()
        {
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = ConfigFilePath };
            var style = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            style.Sections.Remove("ClockStyle");
            var newClockStyle = new ClockStyle();
            {
                newClockStyle.Location = this.Location;
                newClockStyle.BackColor = this.BackColor;
                newClockStyle.FontColor = this.FontColor;
            };
            style.Sections.Add("ClockStyle", newClockStyle);
            style.Save(ConfigurationSaveMode.Modified);
        }
    }
}
