using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CommonServiceLocator;
using Unity;
using Unity.Lifetime;
using Unity.ServiceLocation;
using XamarinBasic.Core;

namespace BasicXamarin.Droid
{
    public static class ServiceRegistry
    {
        public static UnityContainer BuildContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IPlatformSettingsProvider, AndroidSettingsProvider>(Singleton);
            //containerRegistry.Register(typeof(IRepository), typeof(Repository));

            return container;
        }

        private static ContainerControlledLifetimeManager Singleton =>
            new ContainerControlledLifetimeManager();

        public static void SetLocatorProvider(IUnityContainer container)
        {
            ServiceLocator.SetLocatorProvider(() =>
                 new UnityServiceLocator(container));
        }
    }
}