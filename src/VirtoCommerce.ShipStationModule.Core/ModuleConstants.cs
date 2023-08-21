using System.Collections.Generic;
using System.Linq;
using VirtoCommerce.Platform.Core.Settings;

namespace VirtoCommerce.ShipStationModule.Core;

public static class ModuleConstants
{
    public const string DateTimeFormat = "MM'/'dd'/'yyyy HH:mm";
    public static class Security
    {
        public static class Permissions
        {
            public const string Read = "ShipStation:read";
            public const string Update = "ShipStation:update";

            public static string[] AllPermissions { get; } = { Read, Update};
        }
    }

    public static class Settings
    {
        public static class General
        {
            public static IEnumerable<SettingDescriptor> AllSettings
            {
                get
                {
                    return Enumerable.Empty<SettingDescriptor>();
                }
            }
        }

        public static IEnumerable<SettingDescriptor> AllSettings
        {
            get
            {
                return General.AllSettings;
            }
        }
    }
}
