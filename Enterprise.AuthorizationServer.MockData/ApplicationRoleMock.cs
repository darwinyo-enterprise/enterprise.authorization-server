using Enterprise.AuthorizationServer.Models;
using Enterprise.Constants.NetStandard;
using Enterprise.MockData.NetStandard;
using System;
using System.Collections.Generic;

namespace Enterprise.AuthorizationServer.MockData
{
    public class ApplicationRoleMock
    {
        public IEnumerable<ApplicationRoles> GetApplicationRoles()
        {
            return new List<ApplicationRoles>
            {
                #region Authorization Server
                new ApplicationRoles
                {
                    Id=IDMock.R_AuthorizationServer_Admin,
                    Name=AppRoleNames.AuthorizationServer_Administrator,
                    ProjectID=IDMock.P_AuthorizationServerID
                },
                new ApplicationRoles
                {
                    Id=IDMock.R_AuthorizationServer_SecurityAdmin,
                    Name=AppRoleNames.AuthorizationServer_SecurityAdministrator,
                    ProjectID=IDMock.P_AuthorizationServerID
                },
                new ApplicationRoles
                {
                    Id=IDMock.R_AuthorizationServer_SuperAdmin,
                    Name=AppRoleNames.AuthorizationServer_SuperAdministrator,
                    ProjectID=IDMock.P_AuthorizationServerID
                },
                new ApplicationRoles
                {
                    Id=IDMock.R_AuthorizationServer_UserAdmin,
                    Name=AppRoleNames.AuthorizationServer_UserAdministrator,
                    ProjectID=IDMock.P_AuthorizationServerID
                },
                #endregion

                #region Configuration Server
                new ApplicationRoles
                {
                    Id=IDMock.R_ConfigurationServer_Admin,
                    Name=AppRoleNames.ConfigurationServer_Administrator,
                    ProjectID=IDMock.P_ConfigurationServerID
                },
                new ApplicationRoles
                {
                    Id=IDMock.R_ConfigurationServer_ConfigAdmin,
                    Name=AppRoleNames.ConfigurationServer_ConfigAdministrator,
                    ProjectID=IDMock.P_ConfigurationServerID
                },
                new ApplicationRoles
                {
                    Id=IDMock.R_ConfigurationServer_SecurityAdmin,
                    Name=AppRoleNames.ConfigurationServer_SecurityAdministrator,
                    ProjectID=IDMock.P_ConfigurationServerID
                },
                new ApplicationRoles
                {
                    Id=IDMock.R_ConfigurationServer_SuperAdmin,
                    Name=AppRoleNames.ConfigurationServer_SuperAdministrator,
                    ProjectID=IDMock.P_ConfigurationServerID
                },
                #endregion

                #region Logging Server
                new ApplicationRoles
                {
                    Id=IDMock.R_LoggingServer_Admin,
                    Name=AppRoleNames.LoggingServer_Administrator,
                    ProjectID=IDMock.P_LoggingServerID
                },
                new ApplicationRoles
                {
                    Id=IDMock.R_LoggingServer_AuditAdmin,
                    Name=AppRoleNames.LoggingServer_AuditAdministrator,
                    ProjectID=IDMock.P_LoggingServerID
                },
                new ApplicationRoles
                {
                    Id=IDMock.R_LoggingServer_SecurityAdmin,
                    Name=AppRoleNames.LoggingServer_SecurityAdministrator,
                    ProjectID=IDMock.P_LoggingServerID
                },
                new ApplicationRoles
                {
                    Id=IDMock.R_LoggingServer_SuperAdmin,
                    Name=AppRoleNames.LoggingServer_SuperAdministrator,
                    ProjectID=IDMock.P_LoggingServerID
                },
                #endregion

                #region E-Commerce Roles
                new ApplicationRoles
                {
                    Id=IDMock.R_ECommerce_Admin,
                    Name=AppRoleNames.ECommerce_Administrator,
                    ProjectID=IDMock.P_ECommerceID
                },
                new ApplicationRoles
                {
                    Id=IDMock.R_ECommerce_SecurityAdmin,
                    Name=AppRoleNames.ECommerce_SecurityAdministrator,
                    ProjectID=IDMock.P_ECommerceID
                },
                new ApplicationRoles
                {
                    Id=IDMock.R_ECommerce_SuperAdmin,
                    Name=AppRoleNames.ECommerce_SuperAdministrator,
                    ProjectID=IDMock.P_ECommerceID
                },
	            #endregion

                #region Enterprise Roles
                new ApplicationRoles
                {
                    Id=IDMock.R_Enterprise_Admin,
                    Name=AppRoleNames.Enterprise_Administrator,
                    ProjectID=IDMock.P_EnterpriseID
                },
                new ApplicationRoles
                {
                    Id=IDMock.R_Enterprise_SecurityAdmin,
                    Name=AppRoleNames.Enterprise_SecurityAdministrator,
                    ProjectID=IDMock.P_EnterpriseID
                },
                new ApplicationRoles
                {
                    Id=IDMock.R_Enterprise_SuperAdmin,
                    Name=AppRoleNames.Enterprise_SuperAdministrator,
                    ProjectID=IDMock.P_EnterpriseID
                },
	            #endregion

            };
        }
    }
}
