/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2017 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System;
using System.Activities.Core.Presentation;
using System.Activities.Presentation;
using System.Activities.Presentation.Metadata;
using System.Activities.Presentation.View;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Versioning;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Xaml;
using Dev2.Common;
using Dev2.Common.Common;
using Dev2.Common.Interfaces;
using Dev2.Common.Interfaces.Help;
using Dev2.Common.Interfaces.Studio.Controller;
using Dev2.Common.Interfaces.Threading;
using Dev2.Common.Interfaces.Toolbox;
using Dev2.Common.Wrappers;
using Dev2.CustomControls.Progress;
using Dev2.Diagnostics.Debug;
using Dev2.Instrumentation;
using Dev2.Studio.ActivityDesigners;
using Dev2.Studio.Controller;
using Dev2.Studio.Core;
using Dev2.Studio.Core.Views;
using Dev2.Threading;
using Dev2.Utilities;
using Infragistics.Windows.DockManager;
using Microsoft.Practices.Prism.PubSubEvents;
using Warewolf.Core;

using Warewolf.Studio.Models.Help;
using Warewolf.Studio.Models.Toolbox;
using Warewolf.Studio.ViewModels.Help;
using Warewolf.Studio.ViewModels.ToolBox;

using Dev2.Utils;
using log4net.Config;
using Warewolf.Studio.ViewModels;
using Warewolf.Studio.Views;
using Dev2.Studio.Diagnostics;
using Dev2.Studio.ViewModels;
using Dev2.Util;


namespace Dev2.Studio

{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : IApp, IDisposable
    {
        ShellViewModel _shellViewModel;
        //This is ignored because when starting the studio twice the second one crashes without this line
        
        
        
        private Mutex _processGuard = null;
        
        
        private AppExceptionHandler _appExceptionHandler;
        private bool _hasShutdownStarted;

        public App()
        {
            // PrincipalPolicy must be set to WindowsPrincipal to check roles.
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            _hasShutdownStarted = false;
            ShouldRestart = false;

            try
            {
                AppSettings.LocalHost = ConfigurationManager.AppSettings["LocalHostServer"];
                InitializeComponent();
            }
            catch(Exception e)
            {
                Dev2Logger.Error(e.Message,e, "Warewolf Error");
                AppSettings.LocalHost = "http://localhost:3142";
            }
        }

        [PrincipalPermission(SecurityAction.Demand)]  // Principal must be authenticated
        protected override void OnStartup(StartupEventArgs e)
        {
            Tracker.StartStudio();
            ShutdownMode = ShutdownMode.OnMainWindowClose;
            Task.Factory.StartNew(() =>
                {
                    var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Warewolf", "Feedback");
                    DirectoryHelper.CleanUp(path);
                    DirectoryHelper.CleanUp(Path.Combine(GlobalConstants.TempLocation, "Warewolf", "Debug"));
                });


            var localprocessGuard = e.Args.Length > 0
                                        ? new Mutex(true, e.Args[0], out bool createdNew)
                                        : new Mutex(true, "Warewolf Studio", out createdNew);

            if (createdNew)
            {
                _processGuard = localprocessGuard;
            }
            else
            {
                Environment.Exit(Environment.ExitCode);
            }

         

            
            InitializeShell(e);
#if ! (DEBUG)
            var versionChecker = new VersionChecker();
            if(versionChecker.GetNewerVersion())
            {
                WebLatestVersionDialog dialog = new WebLatestVersionDialog();
                dialog.ShowDialog();
            }
#endif
        }

        private static ISplashView _splashView;

        private ManualResetEvent _resetSplashCreated;
        private Thread _splashThread;
        protected void InitializeShell(StartupEventArgs e)
        {
            _resetSplashCreated = new ManualResetEvent(false);

            _splashThread = new Thread(ShowSplash);
            _splashThread.SetApartmentState(ApartmentState.STA);
            _splashThread.IsBackground = true;
            _splashThread.Name = "Splash Screen";
            _splashThread.Start();
            _resetSplashCreated.WaitOne();
            new Bootstrapper().Start();
            
            base.OnStartup(e);
            _shellViewModel = MainWindow.DataContext as ShellViewModel;
            if(_shellViewModel != null)
            {
                CreateDummyWorkflowDesignerForCaching();
                SplashView.CloseSplash(false);
                CheckForDuplicateResources();
                var settingsConfigFile = HelperUtils.GetStudioLogSettingsConfigFile();
                if (!File.Exists(settingsConfigFile))
                {
                    File.WriteAllText(settingsConfigFile, GlobalConstants.DefaultStudioLogFileConfig);
                }
                Dev2Logger.AddEventLogging(settingsConfigFile,"Warewolf Studio");
                XmlConfigurator.ConfigureAndWatch(new FileInfo(settingsConfigFile));
                _appExceptionHandler = new AppExceptionHandler(this, _shellViewModel);
            }
            var toolboxPane = Current.MainWindow.FindName("Toolbox") as ContentPane;
            toolboxPane?.Activate();
        }

        private static void CreateDummyWorkflowDesignerForCaching()
        {            
            var workflowDesigner = new WorkflowDesigner();
            workflowDesigner.PropertyInspectorFontAndColorData = XamlServices.Save(ActivityDesignerHelper.GetDesignerHashTable());
            var designerConfigService = workflowDesigner.Context.Services.GetService<DesignerConfigurationService>();
            if (designerConfigService != null)
            {
                // set the runtime Framework version to 4.5 as new features are in .NET 4.5 and do not exist in .NET 4
                designerConfigService.TargetFrameworkName = new FrameworkName(".NETFramework", new Version(4, 5));
                designerConfigService.AutoConnectEnabled = true;
                designerConfigService.AutoSplitEnabled = true;
                designerConfigService.PanModeEnabled = true;
                designerConfigService.RubberBandSelectionEnabled = true;
                designerConfigService.BackgroundValidationEnabled = true;

                // prevent design-time background validation from blocking UI thread
                // Disabled for now
                designerConfigService.AnnotationEnabled = false;
                designerConfigService.AutoSurroundWithSequenceEnabled = false;
            }
            var meta = new DesignerMetadata();
            meta.Register();

            var builder = new AttributeTableBuilder();
            foreach (var designerAttribute in ActivityDesignerHelper.DesignerAttributes)
            {
                builder.AddCustomAttributes(designerAttribute.Key, new DesignerAttribute(designerAttribute.Value));
            }

            MetadataStore.AddAttributeTable(builder.CreateTable());
            workflowDesigner.Context.Services.Subscribe<DesignerView>(instance=>
            {
                instance.WorkflowShellHeaderItemsVisibility = ShellHeaderItemsVisibility.All;
                instance.WorkflowShellBarItemVisibility = ShellBarItemVisibility.None;
                instance.WorkflowShellBarItemVisibility = ShellBarItemVisibility.Zoom | ShellBarItemVisibility.PanMode | ShellBarItemVisibility.MiniMap;
            });
            var activityBuilder = new WorkflowHelper().CreateWorkflow("DummyWF");
            workflowDesigner.Load(activityBuilder);
        }

        private async void CheckForDuplicateResources()
        {
            var server = ServerRepository.Instance.Source;
            var loadExplorerDuplicates = await server.LoadExplorerDuplicates();
            if (loadExplorerDuplicates?.Count > 0)
            {
                var newLoadExplorerDuplicates = loadExplorerDuplicates.Select(duplicate => duplicate.Remove(duplicate.LastIndexOf(Environment.NewLine, StringComparison.Ordinal))).ToList();
                var controller = CustomContainer.Get<IPopupController>();
                controller.ShowResourcesConflict(newLoadExplorerDuplicates);
            }
        }

        private void ShowSplash()
        {
            var repository = ServerRepository.Instance;
            var server = repository.Source;
            server.Connect();
            CustomContainer.Register(server);
            CustomContainer.Register(repository);
            var toolBoxViewModel = new ToolboxViewModel(new ToolboxModel(server, server, null), new ToolboxModel(server, server, null));
            CustomContainer.Register<IToolboxViewModel>(toolBoxViewModel);

            var textToDisplay = Warewolf.Studio.Resources.Languages.Core.StandardStyling.Replace("\r\n", "") +
                                Warewolf.Studio.Resources.Languages.HelpText.WarewolfDefaultHelpDescription +
                                Warewolf.Studio.Resources.Languages.Core.StandardBodyParagraphClosing;

            var helpViewModel = new HelpWindowViewModel(new HelpDescriptorViewModel(new HelpDescriptor("", textToDisplay, null)), new HelpModel(new EventAggregator()));
            CustomContainer.Register<IHelpWindowViewModel>(helpViewModel);
            CustomContainer.Register<IEventAggregator>(new EventAggregator());
            CustomContainer.Register<IPopupController>(new PopupController());
            CustomContainer.Register<IAsyncWorker>(new AsyncWorker());
            CustomContainer.Register<IWarewolfWebClient>(new WarewolfWebClient(new WebClient { Credentials = CredentialCache.DefaultCredentials }));
            CustomContainer.RegisterInstancePerRequestType<IRequestServiceNameView>(() => new RequestServiceNameView());
            CustomContainer.RegisterInstancePerRequestType<IJsonObjectsView>(() => new JsonObjectsView());
            CustomContainer.RegisterInstancePerRequestType<IChooseDLLView>(() => new ChooseDLLView());
            CustomContainer.RegisterInstancePerRequestType<IFileChooserView>(() => new FileChooserView());
            
            
          
            var splashViewModel = new SplashViewModel(server, new ExternalProcessExecutor());

            var splashPage = new SplashPage { DataContext = splashViewModel };
            SplashView = splashPage;
            SplashView.Show(false);
            
            _resetSplashCreated?.Set();
            splashViewModel.ShowServerVersion();
            Dispatcher.Run();           
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Tracker.Stop();
            SplashView.CloseSplash(true);
            // this is already handled ;)
            _shellViewModel?.PersistTabs(true);
            ProgressFileDownloader.PerformCleanup(new DirectoryWrapper(), GlobalConstants.VersionDownloadPath, new FileWrapper());
            HasShutdownStarted = true;
            DebugDispatcher.Instance.Shutdown();
            try
            {
                base.OnExit(e);
            }
            
            catch
            
            {
                // Best effort ;)
            }

            ForceShutdown();
        }

        void ForceShutdown()
        {
            if (ShouldRestart)
            {
                Task.Run(() => Process.Start(ResourceAssembly.Location, Guid.NewGuid().ToString()));
            }
            Environment.Exit(0);
        }


        public new void Shutdown()
        {
            try
            {
                SplashView.CloseSplash(true);
                base.Shutdown();
            }            
            catch (Exception e) 
            {
                Dev2Logger.Warn(e.Message, "Warewolf Warn");
            }
            ForceShutdown();
        }

      
        public bool ShouldRestart { get; set; }

        public bool HasShutdownStarted
        {
            get
            {
                return Dispatcher.CurrentDispatcher.HasShutdownStarted || Dispatcher.CurrentDispatcher.HasShutdownFinished || _hasShutdownStarted;
            }
            set
            {
                _hasShutdownStarted = value;
            }
        }

        public static ISplashView SplashView { get => _splashView; set => _splashView = value; }

        private void OnApplicationDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Tracker.TrackException(GetType().Name, "OnApplicationDispatcherUnhandledException", e.Exception);
            if (_appExceptionHandler != null)
            {
                e.Handled = HasShutdownStarted || _appExceptionHandler.Handle(e.Exception);
            }
            else
            {
                MessageBox.Show("Fatal Error : " + e.Exception);
            }
        }

        public void Dispose()
        {
            _resetSplashCreated.Dispose();
        }
    }
}
