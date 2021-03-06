﻿using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Wirehome.Core.Components;
using Wirehome.Core.Components.Exceptions;
using Wirehome.Core.Model;

namespace Wirehome.Core.HTTP.Controllers
{
    public class ComponentGroupsController : Controller
    {
        private readonly ComponentGroupRegistryService _componentGroupRegistryService;

        public ComponentGroupsController(ComponentGroupRegistryService componentGroupRegistryService)
        {
            _componentGroupRegistryService = componentGroupRegistryService ?? throw new ArgumentNullException(nameof(componentGroupRegistryService));
        }

        [HttpGet]
        [Route("api/v1/component_groups")]
        [ApiExplorerSettings(GroupName = "v1")]
        public List<ComponentGroup> GetComponentGroups()
        {
            return _componentGroupRegistryService.GetComponentGroups();
        }

        [HttpGet]
        [Route("api/v1/component_groups/{uid}")]
        [ApiExplorerSettings(GroupName = "v1")]
        public ComponentGroup GetComponentGroup(string uid)
        {
            return _componentGroupRegistryService.GetComponentGroup(uid);
        }

        [HttpPost]
        [Route("api/v1/component_groups/{uid}/configuration")]
        [ApiExplorerSettings(GroupName = "v1")]
        public void PostConfiguration(string uid, [FromBody] ComponentGroupConfiguration configuration)
        {
            _componentGroupRegistryService.WriteComponentGroupConfiguration(uid, configuration);
        }

        [HttpGet]
        [Route("api/v1/component_groups/{uid}/configuration")]
        [ApiExplorerSettings(GroupName = "v1")]
        public ComponentGroupConfiguration GetConfiguration(string uid)
        {
            try
            {
                return _componentGroupRegistryService.ReadComponentGroupConfiguration(uid);
            }
            catch (ComponentGroupNotFoundException)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }
        }

        [HttpDelete]
        [Route("/api/v1/component_groups/{uid}")]
        [ApiExplorerSettings(GroupName = "v1")]
        public void DeleteComponentGroup(string uid)
        {
            _componentGroupRegistryService.DeleteComponentGroup(uid);
        }

        [HttpPost]
        [Route("api/v1/component_groups/{componentGroupUid}/components/{componentUid}")]
        [ApiExplorerSettings(GroupName = "v1")]
        public void PostComponent(string componentGroupUid, string componentUid)
        {
            if (componentGroupUid == null) throw new ArgumentNullException(nameof(componentGroupUid));
            if (componentUid == null) throw new ArgumentNullException(nameof(componentUid));

            try
            {
                _componentGroupRegistryService.AssignComponent(componentGroupUid, componentUid);
            }
            catch (ComponentGroupNotFoundException)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
        }

        [HttpDelete]
        [Route("api/v1/component_groups/{componentGroupUid}/components/{componentUid}")]
        [ApiExplorerSettings(GroupName = "v1")]
        public void DeleteComponent(string componentGroupUid, string componentUid)
        {
            if (componentGroupUid == null) throw new ArgumentNullException(nameof(componentGroupUid));
            if (componentUid == null) throw new ArgumentNullException(nameof(componentUid));

            try
            {
                _componentGroupRegistryService.UnassignComponent(componentGroupUid, componentUid);
            }
            catch (ComponentGroupNotFoundException)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
        }

        [HttpGet]
        [Route("api/v1/component_groups/{componentGroupUid}/components/{componentUid}/settings/{settingUid}")]
        [ApiExplorerSettings(GroupName = "v1")]
        public object GetComponentAssociationSetting(string componentGroupUid, string componentUid, string settingUid)
        {
            if (componentGroupUid == null) throw new ArgumentNullException(nameof(componentGroupUid));
            if (componentUid == null) throw new ArgumentNullException(nameof(componentUid));

            try
            {
                return _componentGroupRegistryService.GetComponentAssociationSetting(componentGroupUid, componentUid, settingUid);
            }
            catch (ComponentGroupNotFoundException)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }
        }

        [HttpPost]
        [Route("api/v1/component_groups/{componentGroupUid}/components/{componentUid}/settings/{settingUid}")]
        [ApiExplorerSettings(GroupName = "v1")]
        public void PostComponentAssociationSetting(string componentGroupUid, string componentUid, string settingUid, [FromBody] object settingValue)
        {
            if (componentGroupUid == null) throw new ArgumentNullException(nameof(componentGroupUid));
            if (componentUid == null) throw new ArgumentNullException(nameof(componentUid));

            try
            {
                _componentGroupRegistryService.SetComponentAssociationSetting(componentGroupUid, componentUid, settingUid, settingValue);
            }
            catch (ComponentGroupNotFoundException)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
        }

        [HttpDelete]
        [Route("api/v1/component_groups/{componentGroupUid}/components/{componentUid}/settings/{settingUid}")]
        [ApiExplorerSettings(GroupName = "v1")]
        public void DeleteComponentAssociationSetting(string componentGroupUid, string componentUid, string settingUid)
        {
            if (componentGroupUid == null) throw new ArgumentNullException(nameof(componentGroupUid));
            if (componentUid == null) throw new ArgumentNullException(nameof(componentUid));

            try
            {
                _componentGroupRegistryService.RemoveComponentAssociationSetting(componentGroupUid, componentUid, settingUid);
            }
            catch (ComponentGroupNotFoundException)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
        }

        //[HttpPost]
        //[Route("api/v1/component_groups/{componentGroupUid}/macros/{macroUid}")]
        //[ApiExplorerSettings(GroupName = "v1")]
        //public void PostMacro(string componentGroupUid, string macroUid)
        //{
        //    if (componentGroupUid == null) throw new ArgumentNullException(nameof(componentGroupUid));
        //    if (macroUid == null) throw new ArgumentNullException(nameof(macroUid));

        //    if (!_componentGroupRegistryService.TryGetComponentGroup(componentGroupUid, out var componentGroup))
        //    {
        //        HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
        //        return;
        //    }

        //    componentGroup.Components.Add(macroUid);
        //    _componentGroupRegistryService.Save();
        //}

        //[HttpDelete]
        //[Route("api/v1/component_groups/{componentGroupUid}/macros/{macroUid}")]
        //[ApiExplorerSettings(GroupName = "v1")]
        //public void DeleteMacro(string componentGroupUid, string macroUid)
        //{
        //    if (componentGroupUid == null) throw new ArgumentNullException(nameof(componentGroupUid));
        //    if (macroUid == null) throw new ArgumentNullException(nameof(macroUid));

        //    if (!_componentGroupRegistryService.TryGetComponentGroup(componentGroupUid, out var componentGroup))
        //    {
        //        HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
        //        return;
        //    }

        //    componentGroup.Macros.Remove(macroUid);
        //    _componentGroupRegistryService.Save();
        //}

        [HttpGet]
        [Route("/api/v1/component_groups/{uid}/status")]
        [ApiExplorerSettings(GroupName = "v1")]
        public ConcurrentWirehomeDictionary GetStatus(string uid)
        {
            if (!_componentGroupRegistryService.TryGetComponentGroup(uid, out var componentGroup))
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }

            return componentGroup.Status;
        }

        [HttpGet]
        [Route("/api/v1/component_groups/{uid}/settings")]
        [ApiExplorerSettings(GroupName = "v1")]
        public ConcurrentWirehomeDictionary GetSettings(string uid)
        {
            if (!_componentGroupRegistryService.TryGetComponentGroup(uid, out var componentGroup))
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }

            return componentGroup.Settings;
        }

        [HttpPost]
        [Route("/api/v1/component_groups/{componentGroupUid}/settings/{settingUid}")]
        [ApiExplorerSettings(GroupName = "v1")]
        public void PostSetting(string componentGroupUid, string settingUid, [FromBody] object value)
        {
            _componentGroupRegistryService.SetComponentGroupSetting(componentGroupUid, settingUid, value);
        }

        [HttpGet]
        [Route("/api/v1/component_groups/{componentGroupUid}/settings/{settingUid}")]
        [ApiExplorerSettings(GroupName = "v1")]
        public object GetSetting(string componentGroupUid, string settingUid)
        {
            return _componentGroupRegistryService.GetComponentGroupSetting(componentGroupUid, settingUid);
        }

        [HttpDelete]
        [Route("/api/v1/component_groups/{componentGroupUid}/settings/{settingUid}")]
        [ApiExplorerSettings(GroupName = "v1")]
        public object DeleteSetting(string componentGroupUid, string settingUid)
        {
            return _componentGroupRegistryService.RemoveComponentGroupSetting(componentGroupUid, settingUid);
        }
    }
}
