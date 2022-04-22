using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtoCommerce.Platform.Core.Modularity;
using VirtoCommerce.Platform.Core.Security;
using VirtoCommerce.Platform.Core.Settings;
using VirtoCommerce.ShipStationModule.Core;
using VirtoCommerce.ShipStationModule.Core.Services;
using VirtoCommerce.ShipStationModule.Data.Services;

namespace VirtoCommerce.ShipStationModule.Web;

public class Module : IModule, IHasConfiguration
{
    public ManifestModuleInfo ModuleInfo { get; set; }
    public IConfiguration Configuration { get; set; }

    public void Initialize(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IShipStationService, ShipStationService>();
    }

    public void PostInitialize(IApplicationBuilder appBuilder)
    {
        // register settings
        var settingsRegistrar = appBuilder.ApplicationServices.GetRequiredService<ISettingsRegistrar>();
        settingsRegistrar.RegisterSettings(ModuleConstants.Settings.AllSettings, ModuleInfo.Id);

        // register permissions
        var permissionsProvider = appBuilder.ApplicationServices.GetRequiredService<IPermissionsRegistrar>();
        permissionsProvider.RegisterPermissions(ModuleConstants.Security.Permissions.AllPermissions.Select(x =>
            new Permission()
            {
                GroupName = "ShipStation",
                ModuleId = ModuleInfo.Id,
                Name = x
            }).ToArray());
    }

    public void Uninstall()
    {
        // do nothing in here
    }
}
