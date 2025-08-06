using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web_ProjectName.Lib
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class PermissionAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string role;

        public PermissionAuthorizeAttribute(string role)
        {
            this.role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousPermisAttribute>().Any()) return;

            //Check authorize
            var rolesSesison = ExtensionMethods.SessionExtensionMethod.GetObject<List<string>>(context.HttpContext.Session, "roles") ?? new List<string>();
            bool authorize = false;
            var roles = role.Split(',');
            for (int i = 0; i < roles.Count(); i++)
            {
                if (rolesSesison.Contains(roles[i]))
                {
                    authorize = true; break;
                }
            }
            if (!authorize)
                context.Result = new ForbidResult();
        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        public class AllowAnonymousPermisAttribute : Attribute
        {
            public void OnAllowAnonymousAttribute()
            {
                // Method intentionally left empty.
            }
        }
    }

    public static class RolesData
    {
        public static class Ro_Functions
        {
            //Mặc định
            //public const string NONE = "NONE"; //Ký hiệu NO

            //Quản trị
            public const string ADMIN = "ADMIN"; //Ký hiệu AD

        }

		public static class Ro_Controllers
		{
            #region Admin
            //QL sản phẩm
            public const string AD_PRODUCT_MANAGE = "AD_PRODUCT_MANAGE";
            
            //Danh mục sản phẩm
            public const string AD_PRODUCT_CATEGORY = "AD_PRODUCT_CATEGORY";
            
            //QL tin tức
            public const string AD_NEWS_MANAGE = "AD_NEWS_MANAGE";
            
            //Danh mục tin tức
            public const string AD_NEWS_CATEGORY = "AD_NEWS_CATEGORY";

            //QL dịch vụ
            public const string AD_SERVICE_MANAGE = "AD_SERVICE_MANAGE";
            
            //Danh mục dịch vụ
            public const string AD_SERVICE_CATEGORY = "AD_SERVICE_CATEGORY";

            //QL dự án
            public const string AD_PROJECT_MANAGE = "AD_PROJECT_MANAGE";

            //Danh mục dự án
            public const string AD_PROJECT_CATEGORY = "AD_PROJECT_CATEGORY";
            
            //QL giải pháp
            public const string AD_SOLUTION_MANAGE = "AD_SOLUTION_MANAGE";

            //Danh mục giải pháp
            public const string AD_SOLUTION_CATEGORY = "AD_SOLUTION_CATEGORY";

            //QL thư viện
            public const string AD_LIBRARY_MANAGE = "AD_LIBRARY_MANAGE";

            //Danh mục thư viện
            public const string AD_LIBRARY_CATEGORY = "AD_LIBRARY_CATEGORY";

            //QL tuyển dụng
            public const string AD_RECRUITMENT_MANAGE = "AD_RECRUITMENT_MANAGE";

            //Đơn ứng tuyển
            public const string AD_RECRUITMENT_REGISTER = "AD_RECRUITMENT_REGISTER";

            //Danh mục tuyển dụng
            public const string AD_RECRUITMENT_CATEGORY = "AD_RECRUITMENT_CATEGORY";

            //Giới thiệu
            public const string AD_INTRODUCE_MANAGE = "AD_INTRODUCE_MANAGE";
            
            //QL ảnh
            public const string AD_BANNER_MANAGE = "AD_BANNER_MANAGE";
            
            //QL đối tác
            public const string AD_PARTNER_MANAGE = "AD_PARTNER_MANAGE";
            
            //QL liên hệ
            public const string AD_CONTACT_MANAGE = "AD_CONTACT_MANAGE";
            
            //Thông tin website
            public const string AD_INFO_MANAGE = "AD_INFO_MANAGE";
            
            //Schema SEO
            public const string AD_SCHEMA_MANAGE = "AD_SCHEMA_MANAGE";

            //QL tài khoản
            public const string AD_ACCOUNT_MANAGE = "AD_ACCOUNT_MANAGE";

            //Danh mục quyền
            public const string AD_GROUP_PERMIT_MANAGE = "AD_GROUP_PERMIT_MANAGE";
            #endregion
        }

		public static class Ro_Actions
        {
            public const string CREATE = "CREATE";
            public const string READ = "READ";
            public const string UPDATE = "UPDATE";
            public const string DELETE = "DELETE";
            public const string UPDATE_STATUS = "UPDATE_STATUS";
        }
    }
}
