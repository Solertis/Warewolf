﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.2.0.0
//      SpecFlow Generator Version:2.2.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Warewolf.Tools.Specs.Toolbox.Database.PostgresSql
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.2.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class PostgresSqlConnectorFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "PostgresSqlConnector.feature"
#line hidden
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner(null, 0);
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "PostgresSqlConnector", "\tIn order to manage my database services\r\n\tAs a Warewolf User\r\n\tI want to be show" +
                    "n the database service setup", ProgrammingLanguage.CSharp, new string[] {
                        "Database"});
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
            if (((testRunner.FeatureContext != null) 
                        && (testRunner.FeatureContext.FeatureInfo.Title != "PostgresSqlConnector")))
            {
                global::Warewolf.Tools.Specs.Toolbox.Database.PostgresSql.PostgresSqlConnectorFeature.FeatureSetup(null);
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
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Creating PostgresSql Connector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "PostgresSqlConnector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Database")]
        public virtual void CreatingPostgresSqlConnector()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Creating PostgresSql Connector", ((string[])(null)));
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("I drag a PostgresSql Server database connector", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.When("I select DemoPostgres as the source", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 10
 testRunner.When("I select getemployees as the action", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table1515 = new TechTalk.SpecFlow.Table(new string[] {
                        "Input",
                        "Value",
                        "Empty is Null"});
            table1515.AddRow(new string[] {
                        "fname",
                        "",
                        "false"});
#line 11
 testRunner.Then("Test PostgresSql Inputs appear As", ((string)(null)), table1515, "Then ");
#line 14
 testRunner.Then("Inputs Are Enabled for PostgresSql", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table1516 = new TechTalk.SpecFlow.Table(new string[] {
                        "fname"});
            table1516.AddRow(new string[] {
                        "Bill"});
#line 15
 testRunner.Given("I Enter a value as the input", ((string)(null)), table1516, "Given ");
#line 18
 testRunner.Then("Test button is Enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table1517 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "salary",
                        "age"});
            table1517.AddRow(new string[] {
                        "Bill",
                        "4200",
                        "45"});
#line 19
 testRunner.Then("button is clicked", ((string)(null)), table1517, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Opening Saved workflow with PostgresSql tool")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "PostgresSqlConnector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Database")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("OpeningSavedWorkflowWithPostgresServerTool")]
        public virtual void OpeningSavedWorkflowWithPostgresSqlTool()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Opening Saved workflow with PostgresSql tool", new string[] {
                        "OpeningSavedWorkflowWithPostgresServerTool"});
#line 24
this.ScenarioSetup(scenarioInfo);
#line 25
 testRunner.Given("I Open workflow with PostgreSql connector", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 26
 testRunner.And("PostgresSql Source Is Enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 27
 testRunner.And("PostgresSql Source Is \"postgressql\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 28
 testRunner.And("PostgresSql Action Is Enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
 testRunner.And("PostgresSql Action Is \"getemployees\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 30
 testRunner.And("PostgresSql Inputs Are Enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table1518 = new TechTalk.SpecFlow.Table(new string[] {
                        "Input",
                        "Value",
                        "Empty is Null"});
            table1518.AddRow(new string[] {
                        "fname",
                        "[[fname]]",
                        "false"});
#line 31
 testRunner.Then("PostgresSql Inputs appear As", ((string)(null)), table1518, "Then ");
#line 34
 testRunner.And("Validate PostgresSql Is Enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Change the source on existing PostgresSql tool")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "PostgresSqlConnector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Database")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("ChangeTheSourceOnExistingPostgresql")]
        public virtual void ChangeTheSourceOnExistingPostgresSqlTool()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change the source on existing PostgresSql tool", new string[] {
                        "ChangeTheSourceOnExistingPostgresql"});
#line 37
this.ScenarioSetup(scenarioInfo);
#line 38
 testRunner.Given("I Open workflow with PostgreSql connector", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 39
 testRunner.And("PostgresSql Source Is Enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 40
 testRunner.And("PostgresSql Source Is \"postgressql\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
 testRunner.And("PostgresSql Action Is Enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 42
 testRunner.And("PostgresSql Action Is \"getemployees\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 43
 testRunner.And("PostgresSql Inputs Are Enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table1519 = new TechTalk.SpecFlow.Table(new string[] {
                        "Input",
                        "Value",
                        "Empty is Null"});
            table1519.AddRow(new string[] {
                        "fname",
                        "[[fname]]",
                        "false"});
#line 44
 testRunner.Then("PostgresSql Inputs appear As", ((string)(null)), table1519, "Then ");
#line 47
 testRunner.And("Validate PostgresSql Is Enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Change the action on existing PostgresSql tool")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "PostgresSqlConnector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Database")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("ChangeTheActionOnExistingPostgresql")]
        public virtual void ChangeTheActionOnExistingPostgresSqlTool()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change the action on existing PostgresSql tool", new string[] {
                        "ChangeTheActionOnExistingPostgresql"});
#line 50
this.ScenarioSetup(scenarioInfo);
#line 51
 testRunner.Given("I Open workflow with PostgreSql connector", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 52
 testRunner.And("PostgresSql Source Is Enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 53
 testRunner.And("PostgresSql Source Is \"postgressql\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 54
 testRunner.And("PostgresSql Action Is Enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 55
 testRunner.And("PostgresSql Action Is \"getemployees\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 56
 testRunner.And("PostgresSql Inputs Are Enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table1520 = new TechTalk.SpecFlow.Table(new string[] {
                        "Input",
                        "Value",
                        "Empty is Null"});
            table1520.AddRow(new string[] {
                        "fname",
                        "[[fname]]",
                        "false"});
#line 57
 testRunner.Then("PostgresSql Inputs appear As", ((string)(null)), table1520, "Then ");
#line 60
 testRunner.And("Validate PostgresSql Is Enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Change the recordset on existing PostgresSql tool")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "PostgresSqlConnector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Database")]
        public virtual void ChangeTheRecordsetOnExistingPostgresSqlTool()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change the recordset on existing PostgresSql tool", ((string[])(null)));
#line 62
this.ScenarioSetup(scenarioInfo);
#line 63
 testRunner.Given("I Open workflow with PostgreSql connector", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 64
 testRunner.And("PostgresSql Source Is Enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 65
 testRunner.And("PostgresSql Source Is \"postgressql\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 66
 testRunner.And("PostgresSql Action Is Enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 67
 testRunner.And("PostgresSql Action Is \"getemployees\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 68
 testRunner.And("PostgresSql Inputs Are Enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table1521 = new TechTalk.SpecFlow.Table(new string[] {
                        "Input",
                        "Value",
                        "Empty is Null"});
            table1521.AddRow(new string[] {
                        "fname",
                        "[[fname]]",
                        "false"});
#line 69
 testRunner.Then("PostgresSql Inputs appear As", ((string)(null)), table1521, "Then ");
#line 72
 testRunner.And("Validate PostgresSql Is Enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
