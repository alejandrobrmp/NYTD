namespace NYTD.App {
    using System;
    using System.Collections.Generic;
    using Caliburn.Micro;
    using NYTD.App.Persistence;
    using NYTD.App.Services.Impl;
    using NYTD.App.Services.Interfaces;
    using NYTD.App.ViewModels;
    using NYTD.Lib;

    public class AppBootstrapper : BootstrapperBase {
        SimpleContainer container;

        public AppBootstrapper() {
            Initialize();
        }

        protected override void Configure() {
            container = new SimpleContainer();

            //container.Singleton<JSONHelper>();
            container.Singleton<IYouTubeApiService, YouTubeApiService>();
            container.GetInstance<IYouTubeApiService>().Initialize(Properties.Settings.Default.API_KEY);

            container.Singleton<ISearchService, SearchService>();

            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();
            container.PerRequest<IShell, ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key) {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance) {
            container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e) {
            DisplayRootViewFor<IShell>();
        }
    }
}