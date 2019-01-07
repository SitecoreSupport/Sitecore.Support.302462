﻿namespace Sitecore.Support
{
  using Microsoft.Extensions.DependencyInjection;
  using Sitecore.DependencyInjection;
  using Sitecore.XA.Foundation.Multisite;

  public class RegisterDependencies : IServicesConfigurator
  {
    public void Configure(IServiceCollection serviceCollection)
    {
      serviceCollection.AddTransient<ISiteInfoResolver, Sitecore.Support.XA.Foundation.Multisite.SiteInfoResolver>();
    }
  }
}