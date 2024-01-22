using LMS.UserMgtService;
using PrimaryBaseLibrary;
using System;
using System.Collections.Generic;

namespace LMS.Web
{
    public class UserMgtAgent
    {
        #region User Info Operation
        public static Int32 InsertUserData(User user)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.InsertUserData(user);
            }
        }

        public static void DeleteUserData(int id)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                umService.DeleteUserData(id);
            }
        }

        public static int UpdateUserData(User user)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.UpdateUserData(user);
            }
        }

        public static int InserUserRoles(int userId, List<Role> roleList, int applicationId, int moduleId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.InsertUserRoles(userId, roleList, applicationId, moduleId);
            }
        }

        public static List<User> GetUserList()
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                List<User> list = umService.GetUserList();
                return list;
            }
        }

        public static LMS.UserMgtService.User GetUser(int id)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetUser(id);
            }
        }

        public static LMS.UserMgtService.User GetUserByLoginId(string loginId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetUserByLoginId(loginId);
            }
        }

        public static List<LMS.UserMgtService.User> GetUserListByCraiteria(LMS.UserMgtService.User user)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetUserListByCraiteria(user);
            }
        }

        #endregion

        #region Group Operation

        public static Int32 CreateGroup(UserGroup item, List<Role> roleList)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.CreateGroup(item, roleList);
            }
        }

        public static Int32 UpdateGroup(UserGroup item, List<Role> roleList)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.UpdateGroup(item, roleList);
            }
        }

        public static Int32 DeleteGroup(int id)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.DeleteGroup(id);
            }
        }

        public static UserGroup GetUserGroupById(int id)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetGroupById(id);
            }
        }

        public static List<UserGroup> GetGroupList()
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetGroupList();
            }
        }

        #endregion

        #region Roles
        public static List<Role> GetRoleList()
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetRolesList();
            }
        }

        public static List<Role> GetRoleListByCraiteria(Role role)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetRolesListByCraiteria(role);
            }
        }

        public static List<RoleGroup> GetRoleGroupList()
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetRoleGroups();
            }
        }

        public static List<Role> GetRoleListByUser(int userId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetRolesListByUser(userId);
            }
        }

        public static List<Role> GetRolesListByUserGroup(int userGroupId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetRolesListByUserGroup(userGroupId);
            }
        }

        public static Role GetRoleById(int roleId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetRoleById(roleId);
            }
        }

        public static int InsertRole(Role role, List<Menu> menuList, List<Right> rightList)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.InsertRolesMenusRight(role, menuList, rightList);
            }
        }

        public static int UpdateRole(Role role, List<Menu> menuList, List<Right> rightList)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.UpdateRolesMenusRight(role, menuList, rightList);
            }
        }

        public static Int32 CreateRoleGroup(RoleGroup item)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.CreateRoleGroup(item);
            }
        }


        public static Int32 UpdateRoleGroup(RoleGroup item)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.UpdateRoleGroup(item);
            }
        }

        public static Int32 DeleteRoleGroup(int Id)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.DeleteRoleGroup(Id);
            }
        }

        public static RoleGroup GetRoleGroupById(int Id)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetRoleGroupById(Id);
            }
        }

        #endregion

        #region Menus


        public static int InsertMenuData(Menu menu)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.InsertMenuData(menu);
            }
        }

        public static int UpdateMenuData(Menu menu)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.UpdateMenuData(menu);
            }
        }

        public static int DeleteMenuData(int id)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.DeleteMenuData(id);
            }
        }

        public static List<Menu> GetAllMenus()
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetAllMenus();
            }
        }

        public static List<Menu> GetMenusByParent(int parentId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetMenusByParent(parentId);
            }
        }

        public static List<Menu> GetMenusByRoleId(int roleId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetMenuListByRoleId(roleId);
            }
        }


        public static Menu GetMenusByMenuId(int menuId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetMenuByMenuId(menuId);
            }
        }

        public static Menu GetMenuByMenuNameAndLoginId(string loginId, string MenuName)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetMenuByMenuNameAndLoginId(loginId, MenuName);
            }

        }

        public static List<Menu> GetMenusByApplicationAndModuleId(int roleId, int applicationId, int moduleId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetMenusByApplicationAndModuleId(roleId, applicationId, moduleId);
            }
        }


        public static List<Menu> GetMenusByApplicationAndModuleName(string appName, string modName)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetMenusByApplicationAndModuleName(appName, modName);
            }
        }

        public static List<Menu> GetMenus(string loginId, string appName, string modName)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetMenus(loginId, appName, modName);
            }
        }

        public static List<Menu> GetParentMenusByLoginId(string loginId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetParentMenuListByLoginId(loginId);
            }
        }


        public static List<Menu> GetChildMenusByLoginAndParentId(string loginId, int parentId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetChildMenuListByLoginAndParentId(loginId, parentId);
            }
        }

        public static Menu GetMenuByMenuName(string MenuName)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetMenuByMenuName(MenuName);
            }

        }

        #endregion

        #region Rights
        public static List<Right> GetAllRights()
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetAllRights();
            }
        }

        public static List<Right> GetAllRightsMapedByRole(int roleId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetAllRightsMapedByRole(roleId);
            }
        }

        public static Right GetRightByLoginIdAndRightName(string loginId, string rightName)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetRightByLoginIdAndRightName(loginId, rightName);
            }

        }

        public static List<Right> GetRightByRoleAndAppAndModule(int roleId, int appId, int moduleId)
        {
            using (UserMgtService.UserManagementServiceClient umService = new UserMgtService.UserManagementServiceClient())
            {
                return umService.GetRightsByRoleAndAppAndModule(roleId, appId, moduleId);
            }

        }

        #endregion
    }
}
