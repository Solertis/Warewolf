﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.34209
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Dev2.Studio.UI.Specs.Tools
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UITesting.CodedUITestAttribute()]
    public partial class ControlFlow_SequenceFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "ControlFlow-Sequence.feature"
#line hidden
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "ControlFlow-Sequence", "In order to avoid silly mistakes\r\nAs a math idiot\r\nI want to be told the sum of t" +
                    "wo numbers", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupAttribute()]
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute()]
        public virtual void TestInitialize()
        {
            if (((TechTalk.SpecFlow.FeatureContext.Current != null) 
                        && (TechTalk.SpecFlow.FeatureContext.Current.FeatureInfo.Title != "ControlFlow-Sequence")))
            {
                Dev2.Studio.UI.Specs.Tools.ControlFlow_SequenceFeature.FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("SequenceSmallViewControlFlowNotAllowedWorkflowOtherAllowedLargeViewControlFlowNot" +
            "AllowedWorkflowOtherAllowed")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "ControlFlow-Sequence")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Sequence")]
        public virtual void SequenceSmallViewControlFlowNotAllowedWorkflowOtherAllowedLargeViewControlFlowNotAllowedWorkflowOtherAllowed()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("SequenceSmallViewControlFlowNotAllowedWorkflowOtherAllowedLargeViewControlFlowNot" +
                    "AllowedWorkflowOtherAllowed", new string[] {
                        "Sequence"});
#line 7
this.ScenarioSetup(scenarioInfo);
#line 10
 testRunner.Given("I have Warewolf running", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 11
 testRunner.And("all tabs are closed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 12
 testRunner.And("I click \"EXPLORER,UI_localhost_AutoID\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
 testRunner.And("I click \"RIBBONNEWENDPOINT\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
 testRunner.And("I double click \"TOOLBOX,PART_SearchBox\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
 testRunner.And("I send \"{DELETE}\" to \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
 testRunner.And("I drag \"TOOLSEQUENCE\" onto \"WORKSURFACE,StartSymbol\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
 testRunner.And("I drag \"TOOLDECISION\" to point \"5,5\" on \"WORKSURFACE,Sequence(SequenceDesigner),S" +
                    "mallViewContent,UI__DropPoint_AutoID\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 19
 testRunner.And("I drag \"TOOLSWITCH\" to point \"5,5\" on \"WORKSURFACE,Sequence(SequenceDesigner),Sma" +
                    "llViewContent,UI__DropPoint_AutoID\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 20
 testRunner.And("I drag \"TOOLCOUNT\" to point \"5,5\" on \"WORKSURFACE,Sequence(SequenceDesigner),Smal" +
                    "lViewContent,UI__DropPoint_AutoID\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 21
 testRunner.And("I send \"workflow\" to \"TOOLBOX,PART_SearchBox\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
 testRunner.And("I drag \"TOOLWORKFLOW\" to point \"5,5\" on \"WORKSURFACE,Sequence(SequenceDesigner),S" +
                    "mallViewContent,UI__DropPoint_AutoID\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 23
 testRunner.And("I type \"decision\" in \"RESOURCEPICKERFILTER\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 24
 testRunner.And("I click \"RESOURCEPICKERFOLDERS,UI_Acceptance Testing Resources_AutoID,UI_Decision" +
                    " Testing_AutoID\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 25
 testRunner.And("I click \"RESOURCEPICKEROKBUTTON\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 26
 testRunner.When("I double click point \"5,5\" on \"WORKSURFACE,Sequence(SequenceDesigner)\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 27
 testRunner.Then("\"WORKSURFACE,Sequence(SequenceDesigner),LargeViewContent,UI__ActivitiesPresenter_" +
                    "AutoID,Count Records(CountRecordsDesigner)\" is visible", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 28
 testRunner.And("\"WORKSURFACE,Sequence(SequenceDesigner),LargeViewContent,UI__ActivitiesPresenter_" +
                    "AutoID,Acceptance Testing Resources\\Decision Testing(ServiceDesigner)\" is visibl" +
                    "e", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
 testRunner.And("\"WORKSURFACE,Sequence(SequenceDesigner),LargeViewContent,UI__ActivitiesPresenter_" +
                    "AutoID,FlowDecisionDesigner\" is invisible within \"3\" seconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 31
 testRunner.When("I double click \"TOOLBOX,PART_SearchBox\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 32
 testRunner.And("I send \"{DELETE}\" to \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 33
 testRunner.And("I drag \"TOOLDECISION\" to point \"5,5\" on \"WORKSURFACE,Sequence(SequenceDesigner),L" +
                    "argeViewContent,UI__ActivitiesPresenter_AutoID,sacd:VerticalConnector_1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
 testRunner.And("I drag \"TOOLLENGTH\" to point \"5,5\" on \"WORKSURFACE,Sequence(SequenceDesigner),Lar" +
                    "geViewContent,UI__ActivitiesPresenter_AutoID,sacd:VerticalConnector_1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 35
 testRunner.And("I send \"workflow\" to \"TOOLBOX,PART_SearchBox\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 36
 testRunner.And("I drag \"TOOLWORKFLOW\" to point \"5,5\" on \"WORKSURFACE,Sequence(SequenceDesigner),L" +
                    "argeViewContent,UI__ActivitiesPresenter_AutoID,sacd:VerticalConnector_1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 37
 testRunner.And("I type \"javascript\" in \"RESOURCEPICKERFILTER\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 38
 testRunner.And("I click \"RESOURCEPICKERFOLDERS,UI_Acceptance Testing Resources_AutoID,UI_Javascri" +
                    "pt Testing_AutoID\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 39
 testRunner.And("I click \"RESOURCEPICKEROKBUTTON\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 40
 testRunner.Then("\"WORKSURFACE,Sequence(SequenceDesigner),LargeViewContent,UI__ActivitiesPresenter_" +
                    "AutoID,Length(RecordsLengthDesigner)\" is visible", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 41
 testRunner.And("\"WORKSURFACE,Sequence(SequenceDesigner),LargeViewContent,UI__ActivitiesPresenter_" +
                    "AutoID,Acceptance Testing Resources\\Javascript Testing(ServiceDesigner)\" is visi" +
                    "ble", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 42
 testRunner.And("\"WORKSURFACE,Sequence(SequenceDesigner),LargeViewContent,UI__ActivitiesPresenter_" +
                    "AutoID,FlowDecisionDesigner\" is invisible within \"3\" seconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion