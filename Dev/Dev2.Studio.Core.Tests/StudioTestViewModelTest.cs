﻿using System.Collections.Generic;
using Caliburn.Micro;
using Dev2.Common.Interfaces.Diagnostics.Debug;
using Dev2.Common.Interfaces.Infrastructure.Events;
using Dev2.Common.Interfaces.Studio.Controller;
using Dev2.Studio.Core.Interfaces;
using Dev2.Studio.Core.Messages;
using Dev2.Studio.Core.ViewModels;
using Dev2.Studio.ViewModels.Diagnostics;
using Dev2.ViewModels;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Dev2.Core.Tests
{
    [TestClass]
    public class StudioTestViewModelTest
    {
        [TestMethod]
        [Owner("Hagashen Naidu")]
        [TestCategory("WorkSurfaceContextViewModel_Constructor")]
        public void WorkSurfaceContextViewModel_Constructor_ValidArguments_DebugOutputViewModelNotNull()
        {
            //------------Setup for test--------------------------
            var mockWorkSurfaceViewModel = new Mock<IWorkflowDesignerViewModel>();
            var mockedConn = new Mock<IEnvironmentConnection>();
            mockedConn.Setup(conn => conn.ServerEvents).Returns(new Mock<IEventPublisher>().Object);
            var mockEnvironmentModel = new Mock<IEnvironmentModel>();
            mockEnvironmentModel.Setup(model => model.Connection).Returns(mockedConn.Object);
            mockEnvironmentModel.Setup(e => e.Name).Returns("My Env");
            var environmentModel = mockEnvironmentModel.Object;
            mockWorkSurfaceViewModel.Setup(model => model.EnvironmentModel).Returns(environmentModel);
            //------------Execute Test---------------------------
            CustomContainer.Register(new Mock<IPopupController>().Object);
            var eventAggregator = new Mock<IEventAggregator>();
            var resourceModel = new Mock<IContextualResourceModel>();
            resourceModel.Setup(model => model.IsWorkflowSaved).Returns(true);
            mockWorkSurfaceViewModel.Setup(model => model.ResourceModel).Returns(resourceModel.Object);
            var serviceTestViewModel = new Mock<IServiceTestViewModel>();
            serviceTestViewModel.Setup(model => model.WorkflowDesignerViewModel).Returns(mockWorkSurfaceViewModel.Object);
            var vm = new StudioTestViewModel(eventAggregator.Object, serviceTestViewModel.Object, new Mock<IPopupController>().Object, null);

            //------------Assert Results-------------------------
            Assert.IsNotNull(vm);
            Assert.IsNotNull(vm.DebugOutputViewModel);
            Assert.IsFalse(vm.HasVariables);
            Assert.IsTrue(vm.HasDebugOutput);
            Assert.IsNull(vm.DisplayName);
            Assert.AreEqual("ServiceTestsViewer", vm.ResourceType);
            Assert.IsNull(vm.HelpText);
            Assert.IsFalse(vm.IsDirty);
        }

        [TestMethod]
        [Owner("Pieter Terblanche")]
        [TestCategory("StudioTestViewModel_GetView")]
        public void StudioTestViewModel_GetView_ReturnsIView_NotNull()
        {
            //------------Setup for test--------------------------
            var mockWorkSurfaceViewModel = new Mock<IWorkflowDesignerViewModel>();
            var mockedConn = new Mock<IEnvironmentConnection>();
            mockedConn.Setup(conn => conn.ServerEvents).Returns(new Mock<IEventPublisher>().Object);
            var mockEnvironmentModel = new Mock<IEnvironmentModel>();
            mockEnvironmentModel.Setup(model => model.Connection).Returns(mockedConn.Object);
            mockEnvironmentModel.Setup(e => e.Name).Returns("My Env");
            var environmentModel = mockEnvironmentModel.Object;
            mockWorkSurfaceViewModel.Setup(model => model.EnvironmentModel).Returns(environmentModel);

            //------------Execute Test---------------------------
            CustomContainer.Register(new Mock<IPopupController>().Object);
            var eventAggregator = new Mock<IEventAggregator>();
            var resourceModel = new Mock<IContextualResourceModel>();
            resourceModel.Setup(model => model.IsWorkflowSaved).Returns(true);
            mockWorkSurfaceViewModel.Setup(model => model.ResourceModel).Returns(resourceModel.Object);
            var serviceTestViewModel = new Mock<IServiceTestViewModel>();
            serviceTestViewModel.Setup(model => model.WorkflowDesignerViewModel).Returns(mockWorkSurfaceViewModel.Object);
            var vm = new StudioTestViewModel(eventAggregator.Object, serviceTestViewModel.Object, new Mock<IPopupController>().Object, new Mock<IView>().Object);

            Assert.IsNotNull(vm);
            Assert.IsNotNull(vm.DebugOutputViewModel);
            Assert.IsFalse(vm.HasVariables);
            Assert.IsTrue(vm.HasDebugOutput);
            Assert.IsNull(vm.DisplayName);
            Assert.AreEqual("ServiceTestsViewer", vm.ResourceType);
            Assert.IsNull(vm.HelpText);
            Assert.IsFalse(vm.IsDirty);

            var view = vm.GetView();

            //------------Assert Results-------------------------
            Assert.IsNotNull(view);
        }

        [TestMethod]
        [Owner("Pieter Terblanche")]
        [TestCategory("StudioTestViewModel_DebugOutputMessage")]
        public void StudioTestViewModel_DebugOutputMessage_Handle_NotNull()
        {
            //------------Setup for test--------------------------
            var mockWorkSurfaceViewModel = new Mock<IWorkflowDesignerViewModel>();
            var mockedConn = new Mock<IEnvironmentConnection>();
            mockedConn.Setup(conn => conn.ServerEvents).Returns(new Mock<IEventPublisher>().Object);
            var mockEnvironmentModel = new Mock<IEnvironmentModel>();
            mockEnvironmentModel.Setup(model => model.Connection).Returns(mockedConn.Object);
            mockEnvironmentModel.Setup(e => e.Name).Returns("My Env");
            var environmentModel = mockEnvironmentModel.Object;
            mockWorkSurfaceViewModel.Setup(model => model.EnvironmentModel).Returns(environmentModel);

            //------------Execute Test---------------------------
            CustomContainer.Register(new Mock<IPopupController>().Object);
            var eventAggregator = new Mock<IEventAggregator>();
            var resourceModel = new Mock<IContextualResourceModel>();
            resourceModel.Setup(model => model.IsWorkflowSaved).Returns(true);
            mockWorkSurfaceViewModel.Setup(model => model.ResourceModel).Returns(resourceModel.Object);
            var serviceTestViewModel = new Mock<IServiceTestViewModel>();
            serviceTestViewModel.Setup(model => model.WorkflowDesignerViewModel).Returns(mockWorkSurfaceViewModel.Object);
            var vm = new StudioTestViewModel(eventAggregator.Object, serviceTestViewModel.Object, new Mock<IPopupController>().Object, new Mock<IView>().Object);

            Assert.IsNotNull(vm);
            Assert.IsNotNull(vm.DebugOutputViewModel);
            Assert.IsFalse(vm.HasVariables);
            Assert.IsTrue(vm.HasDebugOutput);
            Assert.IsNull(vm.DisplayName);
            Assert.AreEqual("ServiceTestsViewer", vm.ResourceType);
            Assert.IsNull(vm.HelpText);
            Assert.IsFalse(vm.IsDirty);

            //------------Assert Results-------------------------
            vm.Handle(new DebugOutputMessage(new List<IDebugState>()));

            Assert.AreEqual(DebugStatus.Ready, vm.DebugOutputViewModel.DebugStatus);
        }

        [TestMethod]
        [Owner("Pieter Terblanche")]
        [TestCategory("StudioTestViewModel_DoDeactivate")]
        public void StudioTestViewModel_DoDeactivate_CanSave_ExpectedFalse()
        {
            //------------Setup for test--------------------------
            var mockWorkSurfaceViewModel = new Mock<IWorkflowDesignerViewModel>();
            var mockedConn = new Mock<IEnvironmentConnection>();
            mockedConn.Setup(conn => conn.ServerEvents).Returns(new Mock<IEventPublisher>().Object);
            var mockEnvironmentModel = new Mock<IEnvironmentModel>();
            mockEnvironmentModel.Setup(model => model.Connection).Returns(mockedConn.Object);
            mockEnvironmentModel.Setup(e => e.Name).Returns("My Env");
            var environmentModel = mockEnvironmentModel.Object;
            mockWorkSurfaceViewModel.Setup(model => model.EnvironmentModel).Returns(environmentModel);

            //------------Execute Test---------------------------
            CustomContainer.Register(new Mock<IPopupController>().Object);
            var eventAggregator = new Mock<IEventAggregator>();
            var resourceModel = new Mock<IContextualResourceModel>();
            resourceModel.Setup(model => model.IsWorkflowSaved).Returns(true);
            mockWorkSurfaceViewModel.Setup(model => model.ResourceModel).Returns(resourceModel.Object);
            var serviceTestViewModel = new Mock<IServiceTestViewModel>();
            serviceTestViewModel.Setup(model => model.WorkflowDesignerViewModel).Returns(mockWorkSurfaceViewModel.Object);
            var vm = new StudioTestViewModel(eventAggregator.Object, serviceTestViewModel.Object, new Mock<IPopupController>().Object, new Mock<IView>().Object);

            Assert.IsNotNull(vm);
            Assert.IsNotNull(vm.DebugOutputViewModel);
            Assert.IsFalse(vm.HasVariables);
            Assert.IsTrue(vm.HasDebugOutput);
            Assert.IsNull(vm.DisplayName);
            Assert.AreEqual("ServiceTestsViewer", vm.ResourceType);
            Assert.IsNull(vm.HelpText);
            Assert.IsFalse(vm.IsDirty);

            var expectedValue = vm.DoDeactivate(false);

            //------------Assert Results-------------------------

            Assert.IsFalse(vm.IsDirty);
            Assert.IsNull(vm.HelpText);
            Assert.IsTrue(expectedValue);
        }

        [TestMethod]
        [Owner("Pieter Terblanche")]
        [TestCategory("StudioTestViewModel_OnDispose")]
        public void StudioTestViewModel_OnDispose_ViewModel_Dispose()
        {
            //------------Setup for test--------------------------
            var mockWorkSurfaceViewModel = new Mock<IWorkflowDesignerViewModel>();
            var mockedConn = new Mock<IEnvironmentConnection>();
            mockedConn.Setup(conn => conn.ServerEvents).Returns(new Mock<IEventPublisher>().Object);
            var mockEnvironmentModel = new Mock<IEnvironmentModel>();
            mockEnvironmentModel.Setup(model => model.Connection).Returns(mockedConn.Object);
            mockEnvironmentModel.Setup(e => e.Name).Returns("My Env");
            var environmentModel = mockEnvironmentModel.Object;
            mockWorkSurfaceViewModel.Setup(model => model.EnvironmentModel).Returns(environmentModel);

            //------------Execute Test---------------------------
            CustomContainer.Register(new Mock<IPopupController>().Object);
            var eventAggregator = new Mock<IEventAggregator>();
            var resourceModel = new Mock<IContextualResourceModel>();
            resourceModel.Setup(model => model.IsWorkflowSaved).Returns(true);
            mockWorkSurfaceViewModel.Setup(model => model.ResourceModel).Returns(resourceModel.Object);
            var serviceTestViewModel = new Mock<IServiceTestViewModel>();
            serviceTestViewModel.Setup(model => model.WorkflowDesignerViewModel).Returns(mockWorkSurfaceViewModel.Object);
            var vm = new StudioTestViewModel(eventAggregator.Object, serviceTestViewModel.Object, new Mock<IPopupController>().Object, new Mock<IView>().Object);

            Assert.IsNotNull(vm);
            Assert.IsNotNull(vm.DebugOutputViewModel);
            Assert.IsFalse(vm.HasVariables);
            Assert.IsTrue(vm.HasDebugOutput);
            Assert.IsNull(vm.DisplayName);
            Assert.AreEqual("ServiceTestsViewer", vm.ResourceType);
            Assert.IsNull(vm.HelpText);
            Assert.IsFalse(vm.IsDirty);

            vm.Dispose();

            //------------Assert Results-------------------------
        }

    }
}
