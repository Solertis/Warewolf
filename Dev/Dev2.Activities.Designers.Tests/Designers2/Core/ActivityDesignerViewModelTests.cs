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
using System.Activities;
using System.Activities.Presentation.Model;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Caliburn.Micro;
using Dev2.Activities.Designers.Tests.Designers2.Core.Stubs;
using Dev2.Activities.Designers2.Core;
using Dev2.Activities.Designers2.Service;
using Dev2.Collections;
using Dev2.Common.Interfaces;
using Dev2.Common.Interfaces.Core.Collections;
using Dev2.Common.Interfaces.Infrastructure.Providers.Errors;
using Dev2.Common.Interfaces.Studio.Controller;
using Dev2.Communication;
using Dev2.Core.Tests;
using Dev2.Core.Tests.Environments;
using Dev2.Providers.Errors;
using Dev2.Studio.Interfaces;
using Dev2.Studio.ViewModels.Workflow;
using Dev2.Threading;
using Dev2.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;


namespace Dev2.Activities.Designers.Tests.Designers2.Core
{
    [TestClass]
    public class ActivityDesignerViewModelTests
    {
        [TestInitialize]
        public void Init()
        {
            var serverRepo = new Mock<IServerRepository>();
            CustomContainer.Register(serverRepo.Object);
        }

        [TestMethod]
        [TestCategory("ActivityDesignerViewModel_UnitTest")]
        [Description("Base activity view model can initialize")]
        [Owner("Ashley Lewis")]
        public void ActivityDesignerViewModel_Constructor_EmptyModelItem_ViewModelConstructed()
        {
            //init
            var mockModel = new Mock<ModelItem>();

            //exe
            var vm = new TestActivityDesignerViewModel(mockModel.Object);

            //assert
            Assert.IsInstanceOfType(vm, typeof(ActivityDesignerViewModel), "Activity view model base cannot initialize");
        }

        [TestMethod]
        [TestCategory("ActivityDesignerViewModel_ShowHelp")]
        [Owner("Tshepo Ntlhokoa")]
        public void ActivityDesignerViewModel_ShowHelp_SetToTrue_SetInitialFocusIsCalled()
        {
            var mockModel = new Mock<ModelItem>();
            var vm = new TestActivityDesignerViewModel(mockModel.Object);
            var wasCalled = false;

            vm.SetIntialFocusAction(() =>
            {
                wasCalled = true;
            });

            vm.ShowHelp = true;

            Assert.IsTrue(wasCalled);
        }

        /*
         * REMOVED ADD HELP TOGGLE TEST. THE HELP TOGGLE ON THE DESIGNERS HAVE BEEN REMOVED
        */

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("ActivityDesignerViewModel_Collapse")]
        public void ActivityDesignerViewModel_Collapse_SmallViewActive()
        {
            //------------Setup for test--------------------------
            var mockModelItem = GenerateMockModelItem();
            var viewModel = new TestActivityDesignerViewModel(mockModelItem.Object) { ShowLarge = true };

            //------------Execute Test---------------------------
            viewModel.Collapse();

            //------------Assert Results-------------------------
            Assert.IsTrue(viewModel.IsSmallViewActive);
        }

        [TestMethod]
        [Owner("Massimo Guerrera")]
        [TestCategory("ActivityDesignerViewModel_Collapse")]
        public void ActivityDesignerViewModel_Collapse_HelpButtonGetsRemovedOnCollapse()
        {
            //------------Setup for test--------------------------
            var mockModelItem = GenerateMockModelItem();
            Mock<IContextualResourceModel> setupResourceModelMock = Dev2MockFactory.SetupResourceModelMock();
            ErrorInfo errorInfo = new ErrorInfo { InstanceID = new Guid() };

            var envRepo = new Mock<IServerRepository>();
            envRepo.Setup(e => e.ActiveServer).Returns(setupResourceModelMock.Object.Environment);

            IObservableReadOnlyList<IErrorInfo> testErrors = new ObservableReadOnlyList<IErrorInfo> { errorInfo };
            setupResourceModelMock.Setup(c => c.Errors).Returns(testErrors);
            setupResourceModelMock.Setup(c => c.GetErrors(It.IsAny<Guid>())).Returns(new List<IErrorInfo> { errorInfo });
            var viewModel = new ServiceDesignerViewModel(mockModelItem.Object, setupResourceModelMock.Object, envRepo.Object, new Mock<IEventAggregator>().Object, new SynchronousAsyncWorker());

            Assert.AreEqual(1, viewModel.TitleBarToggles.Count);

            viewModel.ShowLarge = true;

            Assert.AreEqual(1, viewModel.TitleBarToggles.Count);

            //------------Execute Test---------------------------
            viewModel.Collapse();

            //------------Assert Results-------------------------
            Assert.AreEqual(1, viewModel.TitleBarToggles.Count);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("ActivityDesignerViewModel_Restore")]
        public void ActivityDesignerViewModel_RestoreFromPreviouslyViewedLargeView_LargeViewActive()
        {
            //------------Setup for test--------------------------
            var mockModelItem = GenerateMockModelItem();
            var viewModel = new TestActivityDesignerViewModel(mockModelItem.Object) { PreviousView = ActivityDesignerViewModel.ShowLargeProperty.Name };

            //------------Execute Test---------------------------
            viewModel.Restore();

            //------------Assert Results-------------------------
            Assert.IsFalse(viewModel.ShowLarge);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("ActivityDesignerViewModel_Expand")]
        public void ActivityDesignerViewModel_Expand_LargeViewActive()
        {
            //------------Setup for test--------------------------
            var mockModelItem = GenerateMockModelItem();
            var viewModel = new TestActivityDesignerViewModel(mockModelItem.Object);

            //------------Execute Test---------------------------
            viewModel.Expand();

            //------------Assert Results-------------------------
            Assert.IsTrue(viewModel.ShowLarge);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("ActivityDesignerViewModel_ThumbVisibility")]
        public void ActivityDesignerViewModel_ThumbVisibility_IsSelectedAndSmallViewNotActive_Visible()
        {
            //------------Setup for test--------------------------
            var mockModelItem = GenerateMockModelItem();
            var viewModel = new TestActivityDesignerViewModel(mockModelItem.Object) { IsSelected = true, ShowLarge = true };

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            Assert.AreEqual(Visibility.Visible, viewModel.ThumbVisibility);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("ActivityDesignerViewModel_ThumbVisibility")]
        public void ActivityDesignerViewModel_ThumbVisibility_IsSelectedAndSmallViewActive_Collapsed()
        {
            //------------Setup for test--------------------------
            var mockModelItem = GenerateMockModelItem();
            var viewModel = new TestActivityDesignerViewModel(mockModelItem.Object) { IsSelected = true, ShowLarge = true };

            //------------Execute Test---------------------------
            viewModel.ShowLarge = false;

            //------------Assert Results-------------------------
            Assert.AreEqual(Visibility.Collapsed, viewModel.ThumbVisibility);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("ActivityDesignerViewModel_ConnectorVisibility")]
        public void ActivityDesignerViewModel_ConnectorVisibility_IsSelectedAndSmallViewNotActive_Collapsed()
        {
            //------------Setup for test--------------------------
            var mockModelItem = GenerateMockModelItem();
            var viewModel = new TestActivityDesignerViewModel(mockModelItem.Object) { IsSelected = true, ShowLarge = true };

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            Assert.AreEqual(Visibility.Collapsed, viewModel.ConnectorVisibility);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("ActivityDesignerViewModel_ConnectorVisibility")]
        public void ActivityDesignerViewModel_ConnectorVisibility_IsSelectedAndSmallViewActive_Visible()
        {
            //------------Setup for test--------------------------
            var mockModelItem = GenerateMockModelItem();
            var viewModel = new TestActivityDesignerViewModel(mockModelItem.Object) { IsSelected = true, ShowLarge = true };

            //------------Execute Test---------------------------
            viewModel.ShowLarge = false;

            //------------Assert Results-------------------------
            Assert.AreEqual(Visibility.Visible, viewModel.ConnectorVisibility);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("ActivityDesignerViewModel_TitleBarTogglesVisibility")]
        public void ActivityDesignerViewModel_TitleBarTogglesVisibility_IsSelected_Visible()
        {
            //------------Setup for test--------------------------
            var mockModelItem = GenerateMockModelItem();
            var viewModel = new TestActivityDesignerViewModel(mockModelItem.Object) { IsSelected = true };

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            Assert.AreEqual(Visibility.Visible, viewModel.TitleBarTogglesVisibility);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("ActivityDesignerViewModel_TitleBarTogglesVisibility")]
        public void ActivityDesignerViewModel_TitleBarTogglesVisibility_NotIsSelected_Collapsed()
        {
            //------------Setup for test--------------------------
            var mockModelItem = GenerateMockModelItem();
            var viewModel = new TestActivityDesignerViewModel(mockModelItem.Object) { IsSelected = false };

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            Assert.AreEqual(Visibility.Collapsed, viewModel.TitleBarTogglesVisibility);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("ActivityDesignerViewModel_ZIndexPosition")]
        public void ActivityDesignerViewModel_ZIndexPosition_IsSelected_Front()
        {
            //------------Setup for test--------------------------
            var mockModelItem = GenerateMockModelItem();
            var viewModel = new TestActivityDesignerViewModel(mockModelItem.Object) { IsSelected = true };

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            Assert.AreEqual(ZIndexPosition.Front, viewModel.ZIndexPosition);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("ActivityDesignerViewModel_ZIndexPosition")]
        public void ActivityDesignerViewModel_ZIndexPosition_NotIsSelected_Back()
        {
            //------------Setup for test--------------------------
            var mockModelItem = GenerateMockModelItem();
            var viewModel = new TestActivityDesignerViewModel(mockModelItem.Object) { IsSelected = false };

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            Assert.AreEqual(ZIndexPosition.Back, viewModel.ZIndexPosition);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("ActivityDesignerViewModel_ThumbVisibility")]
        public void ActivityDesignerViewModel_ThumbVisibility_IsMouseOverAndSmallViewNotActive_Visible()
        {
            //------------Setup for test--------------------------
            var mockModelItem = GenerateMockModelItem();
            var viewModel = new TestActivityDesignerViewModel(mockModelItem.Object) { IsMouseOver = true, ShowLarge = true };

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            Assert.AreEqual(Visibility.Visible, viewModel.ThumbVisibility);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("ActivityDesignerViewModel_ThumbVisibility")]
        public void ActivityDesignerViewModel_ThumbVisibility_IsMouseOverAndSmallViewActive_Collapsed()
        {
            //------------Setup for test--------------------------
            var mockModelItem = GenerateMockModelItem();
            var viewModel = new TestActivityDesignerViewModel(mockModelItem.Object) { IsMouseOver = true, ShowLarge = true };

            //------------Execute Test---------------------------
            viewModel.ShowLarge = false;

            //------------Assert Results-------------------------
            Assert.AreEqual(Visibility.Collapsed, viewModel.ThumbVisibility);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("ActivityDesignerViewModel_ConnectorVisibility")]
        public void ActivityDesignerViewModel_ConnectorVisibility_IsMouseOverAndSmallViewNotActive_Collapsed()
        {
            //------------Setup for test--------------------------
            var mockModelItem = GenerateMockModelItem();
            var viewModel = new TestActivityDesignerViewModel(mockModelItem.Object) { IsMouseOver = true, ShowLarge = true };

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            Assert.AreEqual(Visibility.Collapsed, viewModel.ConnectorVisibility);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("ActivityDesignerViewModel_ConnectorVisibility")]
        public void ActivityDesignerViewModel_ConnectorVisibility_IsMouseOverAndSmallViewActive_Visible()
        {
            //------------Setup for test--------------------------
            var mockModelItem = GenerateMockModelItem();
            var viewModel = new TestActivityDesignerViewModel(mockModelItem.Object) { IsMouseOver = true, ShowLarge = true };

            //------------Execute Test---------------------------
            viewModel.ShowLarge = false;

            //------------Assert Results-------------------------
            Assert.AreEqual(Visibility.Visible, viewModel.ConnectorVisibility);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("ActivityDesignerViewModel_TitleBarTogglesVisibility")]
        public void ActivityDesignerViewModel_TitleBarTogglesVisibility_IsMouseOver_Visible()
        {
            //------------Setup for test--------------------------
            var mockModelItem = GenerateMockModelItem();
            var viewModel = new TestActivityDesignerViewModel(mockModelItem.Object) { IsMouseOver = true };

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            Assert.AreEqual(Visibility.Visible, viewModel.TitleBarTogglesVisibility);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("ActivityDesignerViewModel_TitleBarTogglesVisibility")]
        public void ActivityDesignerViewModel_TitleBarTogglesVisibility_NotIsMouseOver_Collapsed()
        {
            //------------Setup for test--------------------------
            var mockModelItem = GenerateMockModelItem();
            var viewModel = new TestActivityDesignerViewModel(mockModelItem.Object) { IsMouseOver = false };

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            Assert.AreEqual(Visibility.Collapsed, viewModel.TitleBarTogglesVisibility);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("ActivityDesignerViewModel_ZIndexPosition")]
        public void ActivityDesignerViewModel_ZIndexPosition_IsMouseOver_Front()
        {
            //------------Setup for test--------------------------
            var mockModelItem = GenerateMockModelItem();
            var viewModel = new TestActivityDesignerViewModel(mockModelItem.Object) { IsMouseOver = true };

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            Assert.AreEqual(ZIndexPosition.Front, viewModel.ZIndexPosition);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("ActivityDesignerViewModel_ZIndexPosition")]
        public void ActivityDesignerViewModel_ZIndexPosition_NotIsMouseOver_Back()
        {
            //------------Setup for test--------------------------
            var mockModelItem = GenerateMockModelItem();
            var viewModel = new TestActivityDesignerViewModel(mockModelItem.Object) { IsMouseOver = false };

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            Assert.AreEqual(ZIndexPosition.Back, viewModel.ZIndexPosition);
        }
        
        static Mock<ModelItem> GenerateMockModelItem()
        {
            var properties = new Dictionary<string, Mock<ModelProperty>>();
            var propertyCollection = new Mock<ModelPropertyCollection>();

            var displayName = new Mock<ModelProperty>();
            displayName.Setup(p => p.ComputedValue).Returns("Activity");
            properties.Add("DisplayName", displayName);
            propertyCollection.Protected().Setup<ModelProperty>("Find", "DisplayName", true).Returns(displayName.Object);

            var mockModelItem = new Mock<ModelItem>();
            mockModelItem.Setup(mi => mi.ItemType).Returns(typeof(TestActivity));
            mockModelItem.Setup(s => s.Properties).Returns(propertyCollection.Object);

            var repo = new Mock<IResourceRepository>();
            repo.Setup(r => r.FetchResourceDefinition(It.IsAny<IServer>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<bool>())).Returns(new ExecuteMessage());
            var env = EnviromentRepositoryTest.CreateMockEnvironment();
            env.Setup(e => e.ResourceRepository).Returns(repo.Object);
            var crm = new Mock<IContextualResourceModel>();
            crm.Setup(r => r.Environment).Returns(env.Object);
            crm.Setup(r => r.ResourceName).Returns("Test");

            var wfd = CreateWorkflowDesignerViewModel(crm.Object);

            mockModelItem.Setup(mi => mi.View).Returns(wfd.DesignerView);

            return mockModelItem;
        }

        static WorkflowDesignerViewModel CreateWorkflowDesignerViewModel(IContextualResourceModel resourceModel, IWorkflowHelper workflowHelper = null, bool createDesigner = true, string helperText = null)
        {
            return CreateWorkflowDesignerViewModel(null, resourceModel, workflowHelper, createDesigner, helperText);
        }

        static WorkflowDesignerViewModel CreateWorkflowDesignerViewModel(IEventAggregator eventPublisher, IContextualResourceModel resourceModel, IWorkflowHelper workflowHelper = null, bool createDesigner = true, string helperText = null)
        {
            eventPublisher = eventPublisher ?? new Mock<IEventAggregator>().Object;

            var popupController = new Mock<IPopupController>();

            if (workflowHelper == null)
            {
                var wh = new Mock<IWorkflowHelper>();
                wh.Setup(h => h.CreateWorkflow(It.IsAny<string>())).Returns(() => new ActivityBuilder { Implementation = new DynamicActivity() });
                if (helperText != null)
                {
                    wh.Setup(h => h.SanitizeXaml(It.IsAny<StringBuilder>())).Returns(new StringBuilder(helperText));
                }
                workflowHelper = wh.Object;
            }

            var viewModel = new WorkflowDesignerViewModel(eventPublisher, resourceModel, workflowHelper, popupController.Object, new SynchronousAsyncWorker(), createDesigner, true);

            return viewModel;
        }
    }

}
