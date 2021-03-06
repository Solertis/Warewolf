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
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Dev2.Common;
using Dev2.Common.Interfaces.Core.DynamicServices;
using Dev2.Communication;
using Dev2.DynamicServices;
using Dev2.DynamicServices.Objects;
using Dev2.Workspaces;
using Warewolf.Resource.Errors;

namespace Dev2.Runtime.ESB.Management.Services
{
    public class FetchDebugItemFile : DefaultEsbManagementEndpoint
    {
        public override StringBuilder Execute(Dictionary<string, StringBuilder> values, IWorkspace theWorkspace)
        {
            Dev2Logger.Info("Fetch Debug Item File Started", GlobalConstants.WarewolfInfo);
            try
            {        
                var result = new ExecuteMessage { HasError = false };

                if(values == null)
                {
                    Dev2Logger.Debug(ErrorResource.valuesAreMissing, GlobalConstants.WarewolfDebug);
                    throw new InvalidDataContractException(ErrorResource.valuesAreMissing);
                }

                    values.TryGetValue("DebugItemFilePath", out StringBuilder tmp);
                    if (tmp == null || tmp.Length == 0)
                {
                    Dev2Logger.Debug("DebugItemFilePath is missing", GlobalConstants.WarewolfDebug);
                    throw new InvalidDataContractException(string.Format(ErrorResource.PropertyMusHaveAValue, "DebugItemFilePath "));
                }

                string debugItemFilePath = tmp.ToString();

                if(File.Exists(debugItemFilePath))
                {
                    Dev2Logger.Debug("DebugItemFilePath found", GlobalConstants.WarewolfDebug);

                    var lines = File.ReadLines(debugItemFilePath);
                    foreach(var line in lines)
                    {
                        result.Message.AppendLine(line);
                    }

                    Dev2JsonSerializer serializer = new Dev2JsonSerializer();
                    return serializer.SerializeToBuilder(result);
                }
                Dev2Logger.Debug("DebugItemFilePath not found, throwing an exception", GlobalConstants.WarewolfDebug);
                throw new InvalidDataContractException(string.Format(string.Format(ErrorResource.NotFound, debugItemFilePath)));
            }
            catch (Exception e)
            {
                Dev2Logger.Error(e, GlobalConstants.WarewolfError);
                throw;
            }

        }

        public override DynamicService CreateServiceEntry()
        {
            var findDirectoryService = new DynamicService
            {
                Name = HandlesType(),
                DataListSpecification = new StringBuilder("<DataList><DebugItemFilePath ColumnIODirection=\"Input\"></DebugItemFilePath><Dev2System.ManagmentServicePayload ColumnIODirection=\"Both\"></Dev2System.ManagmentServicePayload></DataList>")
            };

            var findDirectoryServiceAction = new ServiceAction
            {
                Name = HandlesType(),
                ActionType = enActionType.InvokeManagementDynamicService,
                SourceMethod = HandlesType()
            };

            findDirectoryService.Actions.Add(findDirectoryServiceAction);

            return findDirectoryService;
        }

        public override string HandlesType()
        {
            return "FetchDebugItemFileService";
        }
    }
}
