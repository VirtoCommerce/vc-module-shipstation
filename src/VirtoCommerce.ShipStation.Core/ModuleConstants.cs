using System.Collections.Generic;
using VirtoCommerce.Platform.Core.Settings;

namespace VirtoCommerce.ShipStation.Core
{
    public static class ModuleConstants
    {
        public static class Security
        {
            public static class Permissions
            {
                public const string Access = "ShipStation:access";
                public const string Create = "ShipStation:create";
                public const string Read = "ShipStation:read";
                public const string Update = "ShipStation:update";
                public const string Delete = "ShipStation:delete";

                public static string[] AllPermissions { get; } = { Read, Create, Access, Update, Delete };
            }
        }

        public static class Settings
        {
            public static class General
            {
                public static SettingDescriptor ShipStationEnabled { get; } = new SettingDescriptor
                {
                    Name = "ShipStation.ShipStationEnabled",
                    GroupName = "ShipStation|General",
                    ValueType = SettingValueType.Boolean,
                    DefaultValue = false
                };

                public static SettingDescriptor ShipStationPassword { get; } = new SettingDescriptor
                {
                    Name = "ShipStation.ShipStationPassword",
                    GroupName = "ShipStation|Advanced",
                    ValueType = SettingValueType.SecureString,
                    DefaultValue = "qwerty"
                };

                public static IEnumerable<SettingDescriptor> AllSettings
                {
                    get
                    {
                        yield return ShipStationEnabled;
                        yield return ShipStationPassword;
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
}
