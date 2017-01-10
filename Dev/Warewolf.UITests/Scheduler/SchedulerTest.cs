﻿using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Warewolf.UITests
{
    [CodedUITest]
    public class SchedulerTest
    {
        [TestMethod]
        public void CreateAndSaveNewScheduleUITest()
        {            
            UIMap.Click_Scheduler_Create_New_Task_Ribbon_Button();
            UIMap.Click_Scheduler_ResourcePicker();
            UIMap.Select_First_Service_From_Service_Picker_Dialog("Hello World");
            UIMap.Enter_LocalSchedulerAdmin_Credentials_Into_Scheduler_Tab();
            UIMap.Click_Scheduler_Disable_Task_Radio_Button();
            UIMap.Click_Save_Ribbon_Button_With_No_Save_Dialog(30000);
            UIMap.Click_Scheduler_Delete_Hello_World_Task();
            UIMap.Click_MessageBox_Yes();
        }

        #region Additional test attributes

        [TestInitialize()]
        public void MyTestInitialize()
        {
            UIMap.SetPlaybackSettings();
            UIMap.AssertStudioIsRunning();
            UIMap.Click_Scheduler_Ribbon_Button();
        }

        UIMap UIMap
        {
            get
            {
                if (_UIMap == null)
                {
                    _UIMap = new UIMap();
                }

                return _UIMap;
            }
        }

        private UIMap _UIMap;

        #endregion
    }
}
