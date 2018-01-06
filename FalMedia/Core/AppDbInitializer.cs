using System.Collections.Generic;
using System.Data.Entity;
using System.Web;
using FalMedia.Areas.Admin.Core;
using FalMedia.Areas.Admin.Models;
using FalMedia.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FalMedia.Core
{
    public class AppDbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            InitializeUsers(context);
            InitializeData(context);
            InitializeResources(context);
            base.Seed(context);
        }

        public static void InitializeUsers(AppDbContext db)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<UserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<RoleManager>();
            const string username = "admin";
            const string name = "admin@admin.com";
            const string password = "123456";
            const string roleName = "Admin";
            const string roleDescription = "Administrator";

            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new Role(roleName, roleDescription);
                roleManager.Create(role);
            }

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new User {UserName = username, Email = name};
                userManager.Create(user, password);
            }

            var groupManager = new GroupManager();
            var newGroup = new Group("SuperAdmins", "Full Access");

            groupManager.CreateGroup(newGroup);
            groupManager.SetUserGroups(user.Id, newGroup.Id);
            groupManager.SetGroupRoles(newGroup.Id, role.Name);
        }

        public static void InitializeData(AppDbContext context)
        {
            var category = new List<Category>
            {
                new Category {Name = "Public", NameAr = "عام"}
            };


            context.Categories.AddRange(category);
            context.SaveChanges();
        }

        public static void InitializeResources(AppDbContext context)
        {
            var resource = new List<Resource>
            {
                new Resource {Culture = "en-us", Name = "Id", Value = "ID"},
                new Resource {Culture = "ar", Name = "Id", Value = "الرقم المتسلسل"},
                new Resource {Culture = "en-us", Name = "AddNew", Value = "Add New"},
                new Resource {Culture = "ar", Name = "AddNew", Value = "أضف جديداً"},
                new Resource {Culture = "en-us", Name = "AddNewPost", Value = "Add New Post"},
                new Resource {Culture = "ar", Name = "AddNewPost", Value = "أضف مقالةً جديدةً"},
                new Resource {Culture = "en-us", Name = "Edit", Value = "Edit"},
                new Resource {Culture = "ar", Name = "Edit", Value = "تعديل"},
                new Resource {Culture = "en-us", Name = "Update", Value = "Update"},
                new Resource {Culture = "ar", Name = "Update", Value = "تحديث"},
                new Resource {Culture = "en-us", Name = "Details", Value = "Details"},
                new Resource {Culture = "ar", Name = "Details", Value = "تفاصيل"},
                new Resource {Culture = "en-us", Name = "Delete", Value = "Delete"},
                new Resource {Culture = "ar", Name = "Delete", Value = "مسح"},
                new Resource {Culture = "en-us", Name = "Close", Value = "Close"},
                new Resource {Culture = "ar", Name = "Close", Value = "اغلاق"},
                new Resource {Culture = "en-us", Name = "Upload", Value = "Upload"},
                new Resource {Culture = "ar", Name = "Upload", Value = "تحميل"},
                new Resource {Culture = "en-us", Name = "Back", Value = "Back"},
                new Resource {Culture = "ar", Name = "Back", Value = "رجوع"},
                new Resource {Culture = "en-us", Name = "Send", Value = "Send"},
                new Resource {Culture = "ar", Name = "Send", Value = "ارسال"},
                new Resource {Culture = "en-us", Name = "Save", Value = "Save"},
                new Resource {Culture = "ar", Name = "Save", Value = "حفظ"},
                new Resource {Culture = "en-us", Name = "Settings", Value = "Settings"},
                new Resource {Culture = "ar", Name = "Settings", Value = "الإعدادات"},
                new Resource {Culture = "en-us", Name = "GeneralSettings", Value = "General Settings"},
                new Resource {Culture = "ar", Name = "GeneralSettings", Value = "الإعدادات العامة"},
                new Resource {Culture = "en-us", Name = "Users", Value = "Users"},
                new Resource {Culture = "ar", Name = "Users", Value = "المستخدمين"},
                new Resource {Culture = "en-us", Name = "Permissions", Value = "Permissions"},
                new Resource {Culture = "ar", Name = "Permissions", Value = "الصلاحيات"},
                new Resource {Culture = "en-us", Name = "Role", Value = "Role"},
                new Resource {Culture = "ar", Name = "Role", Value = "الصلاحية"},
                new Resource {Culture = "en-us", Name = "AddRole", Value = "Add Permission"},
                new Resource {Culture = "ar", Name = "AddRole", Value = "أضافة صلاحية"},
                new Resource {Culture = "en-us", Name = "DeleteRole", Value = "Delete Permission"},
                new Resource {Culture = "ar", Name = "DeleteRole", Value = "حذف صلاحية"},
                new Resource {Culture = "en-us", Name = "EditRole", Value = "Edit Permission"},
                new Resource {Culture = "ar", Name = "EditRole", Value = "تعديل صلاحية"},
                new Resource {Culture = "en-us", Name = "Groups", Value = "Groups"},
                new Resource {Culture = "ar", Name = "Groups", Value = "المجموعات"},
                new Resource {Culture = "en-us", Name = "Group", Value = "Group"},
                new Resource {Culture = "ar", Name = "Group", Value = "المجموعة"},
                new Resource {Culture = "en-us", Name = "AddGroup", Value = "Add Group"},
                new Resource {Culture = "ar", Name = "AddGroup", Value = "أضافة مجموعة"},
                new Resource {Culture = "en-us", Name = "DeleteGroup", Value = "Delete Group"},
                new Resource {Culture = "ar", Name = "DeleteGroup", Value = "حذف مجموعة"},
                new Resource {Culture = "en-us", Name = "EditGroup", Value = "Edit Group"},
                new Resource {Culture = "ar", Name = "EditGroup", Value = "تعديل مجموعة"},
                new Resource {Culture = "en-us", Name = "YourProfile", Value = "Your Profile"},
                new Resource {Culture = "ar", Name = "YourProfile", Value = "الملف الشخصي"},
                new Resource {Culture = "en-us", Name = "Profile", Value = "Profile"},
                new Resource {Culture = "ar", Name = "Profile", Value = "الملف الشخصي"},
                new Resource {Culture = "en-us", Name = "AllUsers", Value = "All Users"},
                new Resource {Culture = "ar", Name = "AllUsers", Value = "كل المستخدمين"},
                new Resource {Culture = "en-us", Name = "AddUser", Value = "Add User"},
                new Resource {Culture = "ar", Name = "AddUser", Value = "أضافة مستخدم"},
                new Resource {Culture = "en-us", Name = "DeleteUser", Value = "Delete User"},
                new Resource {Culture = "ar", Name = "DeleteUser", Value = "حذف مستخدم"},
                new Resource {Culture = "en-us", Name = "EditUser", Value = "Edit User"},
                new Resource {Culture = "ar", Name = "EditUser", Value = "تعديل مستخدم"},
                new Resource {Culture = "en-us", Name = "Login", Value = "Login"},
                new Resource {Culture = "ar", Name = "Login", Value = "تسجيل الدخول"},
                new Resource {Culture = "en-us", Name = "Logout", Value = "Logout"},
                new Resource {Culture = "ar", Name = "Logout", Value = "تسجيل الخروج"},
                new Resource {Culture = "en-us", Name = "Register", Value = "Register New User"},
                new Resource {Culture = "ar", Name = "Register", Value = "أضافة مستخدم جديد"},
                new Resource {Culture = "en-us", Name = "Posts", Value = "Posts"},
                new Resource {Culture = "ar", Name = "Posts", Value = "المقالات"},
                new Resource {Culture = "en-us", Name = "AllPosts", Value = "All Posts"},
                new Resource {Culture = "ar", Name = "AllPosts", Value = "كل المقالات"},
                new Resource {Culture = "en-us", Name = "EmailAddress", Value = "Email Address"},
                new Resource {Culture = "ar", Name = "EmailAddress", Value = "البريد الإلكتروني"},
                new Resource {Culture = "en-us", Name = "SiteTitle", Value = "Site Title"},
                new Resource {Culture = "ar", Name = "SiteTitle", Value = "عنوان الموقع"},
                new Resource {Culture = "en-us", Name = "Tagline", Value = "Tagline"},
                new Resource {Culture = "ar", Name = "Tagline", Value = "شعار"},
                new Resource {Culture = "en-us", Name = "DateTime", Value = "DateTime"},
                new Resource {Culture = "ar", Name = "DateTime", Value = "التاريخ والوقت"},
                new Resource {Culture = "en-us", Name = "Date", Value = "Date"},
                new Resource {Culture = "ar", Name = "Date", Value = "التاريخ"},
                new Resource {Culture = "en-us", Name = "Time", Value = "Time"},
                new Resource {Culture = "ar", Name = "Time", Value = "الوقت"},
                new Resource {Culture = "en-us", Name = "Name", Value = "Name"},
                new Resource {Culture = "ar", Name = "Name", Value = "الإسم"},
                new Resource {Culture = "en-us", Name = "NameAr", Value = "Arabic Name"},
                new Resource {Culture = "ar", Name = "NameAr", Value = "الإسم بالعربية"},
                new Resource {Culture = "en-us", Name = "Description", Value = "Description"},
                new Resource {Culture = "ar", Name = "Description", Value = "الوصف"},
                new Resource {Culture = "en-us", Name = "DescriptionAr", Value = "Arabic Description"},
                new Resource {Culture = "ar", Name = "DescriptionAr", Value = "الوصف بالعربية"},
                new Resource {Culture = "en-us", Name = "CreatedDate", Value = "Created Date"},
                new Resource {Culture = "ar", Name = "CreatedDate", Value = "تاريخ النشر"},
                new Resource {Culture = "en-us", Name = "UpdatedDate", Value = "Updated Date"},
                new Resource {Culture = "ar", Name = "UpdatedDate", Value = "تاريخ التحديث"},
                new Resource {Culture = "en-us", Name = "CreatedBy", Value = "Author"},
                new Resource {Culture = "ar", Name = "CreatedBy", Value = "الكاتب"},
                new Resource {Culture = "en-us", Name = "ModifiedBy", Value = "Modified By"},
                new Resource {Culture = "ar", Name = "ModifiedBy", Value = "عدل من قبل"},
                new Resource {Culture = "en-us", Name = "Category", Value = "Category"},
                new Resource {Culture = "ar", Name = "Category", Value = "تصنيف"},
                new Resource {Culture = "en-us", Name = "Categories", Value = "Categories"},
                new Resource {Culture = "ar", Name = "Categories", Value = "تصنيفات"},
                new Resource {Culture = "en-us", Name = "Title", Value = "Title"},
                new Resource {Culture = "ar", Name = "Title", Value = "العنوان"},
                new Resource {Culture = "en-us", Name = "TitleAr", Value = "Arabic Title"},
                new Resource {Culture = "ar", Name = "TitleAr", Value = "العنوان بالعربية"},
                new Resource {Culture = "en-us", Name = "ShortDescription", Value = "Short Description"},
                new Resource {Culture = "ar", Name = "ShortDescription", Value = "وصف مختصر"},
                new Resource {Culture = "en-us", Name = "ShortDescriptionAr", Value = "Arabic Short Description"},
                new Resource {Culture = "ar", Name = "ShortDescriptionAr", Value = "وصف مختصر بالعربية"},
                new Resource {Culture = "en-us", Name = "Content", Value = "Content"},
                new Resource {Culture = "ar", Name = "Content", Value = "المحتوي"},
                new Resource {Culture = "en-us", Name = "ContentAr", Value = "Arabic Content"},
                new Resource {Culture = "ar", Name = "ContentAr", Value = "المحتوي بالعربية"},
                new Resource {Culture = "en-us", Name = "Thumbnail", Value = "Thumbnail"},
                new Resource {Culture = "ar", Name = "Thumbnail", Value = "الصورة الظاهرة"},
                new Resource {Culture = "en-us", Name = "Status", Value = "Status"},
                new Resource {Culture = "ar", Name = "Status", Value = "الحالة"},
                new Resource {Culture = "en-us", Name = "Published", Value = "Published"},
                new Resource {Culture = "ar", Name = "Published", Value = "نشرت"},
                new Resource {Culture = "en-us", Name = "Pending", Value = "Pending"},
                new Resource {Culture = "ar", Name = "Pending", Value = "بانتظار المراجعة"},
                new Resource {Culture = "en-us", Name = "Draft", Value = "Draft"},
                new Resource {Culture = "ar", Name = "Draft", Value = "مسودة"},
                new Resource {Culture = "en-us", Name = "Value", Value = "Value"},
                new Resource {Culture = "ar", Name = "Value", Value = "القيمة"},
                new Resource {Culture = "en-us", Name = "ResourcesM", Value = "Resources"},
                new Resource {Culture = "ar", Name = "ResourcesM", Value = "المصادر"},
                new Resource {Culture = "en-us", Name = "LinkName", Value = "Link Name"},
                new Resource {Culture = "ar", Name = "LinkName", Value = "اسم الرابط"},
                new Resource {Culture = "en-us", Name = "LinkUrl", Value = "Link url"},
                new Resource {Culture = "ar", Name = "LinkUrl", Value = "عنوان الرابط"},
                new Resource {Culture = "en-us", Name = "LinkTarget", Value = "Target"},
                new Resource {Culture = "ar", Name = "LinkTarget", Value = "طريقة العمل"},
                new Resource {Culture = "en-us", Name = "Mobile", Value = "Mobile"},
                new Resource {Culture = "ar", Name = "Mobile", Value = "الجوال"},
                new Resource {Culture = "en-us", Name = "Company", Value = "Company"},
                new Resource {Culture = "ar", Name = "Company", Value = "الشركة"},
                new Resource {Culture = "en-us", Name = "Subject", Value = "Subject"},
                new Resource {Culture = "ar", Name = "Subject", Value = "الموضوع"},
                new Resource {Culture = "en-us", Name = "Message", Value = "Message"},
                new Resource {Culture = "ar", Name = "Message", Value = "الرسالة"},
                new Resource {Culture = "en-us", Name = "ContactUs", Value = "Contact Us"},
                new Resource {Culture = "ar", Name = "ContactUs", Value = "اتصل بنا"},
                new Resource {Culture = "en-us", Name = "ContactArrived", Value = "Incoming Messages"},
                new Resource {Culture = "ar", Name = "ContactArrived", Value = "الرسائل الواردة"},
                new Resource {Culture = "en-us", Name = "SettingsSaved", Value = "Settings Saved."},
                new Resource {Culture = "ar", Name = "SettingsSaved", Value = "تم حفظ الإعدادات."},
                new Resource {Culture = "en-us", Name = "ErrorSaved", Value = "Settings Saved Error."},
                new Resource {Culture = "ar", Name = "ErrorSaved", Value = "فشل في حفظ الإعدادات."},
                new Resource {Culture = "en-us", Name = "Error", Value = "Error!"},
                new Resource {Culture = "ar", Name = "Error", Value = "خطأ!"},
                new Resource {Culture = "en-us", Name = "UpdateDone", Value = "Update Successful"},
                new Resource {Culture = "ar", Name = "UpdateDone", Value = "تم التعديل بنجاح"},
                new Resource {Culture = "en-us", Name = "UpdateFail", Value = "Update Error!"},
                new Resource {Culture = "ar", Name = "UpdateFail", Value = "فشل في التعديل!"},
                new Resource {Culture = "en-us", Name = "Deleted", Value = "Deleted Successful"},
                new Resource {Culture = "ar", Name = "Deleted", Value = "تم الحذف بنجاح"},
                new Resource {Culture = "en-us", Name = "ChooseYourLanguage", Value = "Site Language"},
                new Resource {Culture = "ar", Name = "ChooseYourLanguage", Value = "لغة الموقع"},
                new Resource {Culture = "en-us", Name = "Language", Value = "Language"},
                new Resource {Culture = "ar", Name = "Language", Value = "اللغة"},
                new Resource {Culture = "en-us", Name = "Dashboard", Value = "Dashboard"},
                new Resource {Culture = "ar", Name = "Dashboard", Value = "لوحة التحكم"},
                new Resource {Culture = "en-us", Name = "Home", Value = "Home"},
                new Resource {Culture = "ar", Name = "Home", Value = "الرئيسية"},
                new Resource {Culture = "en-us", Name = "Username", Value = "Username"},
                new Resource {Culture = "ar", Name = "Username", Value = "اسم المستخدم"},
                new Resource {Culture = "en-us", Name = "Password", Value = "Password"},
                new Resource {Culture = "ar", Name = "Password", Value = "كلمة المرور"},
                new Resource {Culture = "en-us", Name = "RememberMe", Value = "Remember Me"},
                new Resource {Culture = "ar", Name = "RememberMe", Value = "تذكرني"},
                new Resource {Culture = "en-us", Name = "About", Value = "About Us"},
                new Resource {Culture = "ar", Name = "About", Value = "من نحن"},
                new Resource {Culture = "en-us", Name = "Services", Value = "Services"},
                new Resource {Culture = "ar", Name = "Services", Value = "الخدمات"},
                new Resource {Culture = "en-us", Name = "Projects", Value = "Projects"},
                new Resource {Culture = "ar", Name = "Projects", Value = "المشاريع"},
                new Resource {Culture = "en-us", Name = "OurProjects", Value = "Our Projects"},
                new Resource {Culture = "ar", Name = "OurProjects", Value = "مشاريعنا"},
                new Resource {Culture = "en-us", Name = "Partners", Value = "Partners"},
                new Resource {Culture = "ar", Name = "Partners", Value = "العملاء"},
                new Resource {Culture = "en-us", Name = "OurPartners", Value = "Our Partners"},
                new Resource {Culture = "ar", Name = "OurPartners", Value = "عملائنا"},
                new Resource {Culture = "en-us", Name = "ContactInfo", Value = "Contact Information"},
                new Resource {Culture = "ar", Name = "ContactInfo", Value = "معلومات الإتصال"},
                new Resource {Culture = "en-us", Name = "Copyrights", Value = "Copyrights"},
                new Resource {Culture = "ar", Name = "Copyrights", Value = "حقوق التأليف والنشر"},
                new Resource {Culture = "en-us", Name = "SocialMedia", Value = "Social Media"},
                new Resource {Culture = "ar", Name = "SocialMedia", Value = "المواقع الاجتماعية"},
                new Resource {Culture = "en-us", Name = "QuickEdit", Value = "Quick Edit"},
                new Resource {Culture = "ar", Name = "QuickEdit", Value = "تحرير سريع"},
                new Resource {Culture = "en-us", Name = "AddNewUser", Value = "Add New User"},
                new Resource {Culture = "ar", Name = "AddNewUser", Value = "أضافة مستخدم جديد"},
                new Resource {Culture = "en-us", Name = "AddNewGroup", Value = "Add New Group"},
                new Resource {Culture = "ar", Name = "AddNewGroup", Value = "أضافة مجموعة جديد"},
                new Resource {Culture = "en-us", Name = "UserGroup", Value = "User Group"},
                new Resource {Culture = "ar", Name = "UserGroup", Value = "مجموعة المستخدم"},
                new Resource {Culture = "en-us", Name = "UserPermission", Value = "User Permission"},
                new Resource {Culture = "ar", Name = "UserPermission", Value = "صلاحيات المستخدم"},
                new Resource {Culture = "en-us", Name = "AllCategories", Value = "All Categories"},
                new Resource {Culture = "ar", Name = "AllCategories", Value = "كل التصنيفات"},
                new Resource {Culture = "en-us", Name = "Count", Value = "Count"},
                new Resource {Culture = "ar", Name = "Count", Value = "العدد"},
                new Resource {Culture = "en-us", Name = "AddNewCategory", Value = "Add New Category"},
                new Resource {Culture = "ar", Name = "AddNewCategory", Value = "أضافة تصنيف جديد"},
                new Resource {Culture = "en-us", Name = "Slider", Value = "Slider"},
                new Resource {Culture = "ar", Name = "Slider", Value = "سلايدر"},
                new Resource {Culture = "en-us", Name = "ReadMore", Value = "Read More"},
                new Resource {Culture = "ar", Name = "ReadMore", Value = "المزيد"},
                new Resource {Culture = "en-us", Name = "Moadltitle", Value = "We are pleased to serve you"},
                new Resource {Culture = "ar", Name = "Moadltitle", Value = "يسرنا خدمتكم"},
                new Resource
                {
                    Culture = "en-us",
                    Name = "MoadlP",
                    Value = "Please fill out this form and we will get back to you as soon as possible"
                },
                new Resource
                {
                    Culture = "ar",
                    Name = "MoadlP",
                    Value = "يرجى ملء هذا النموذج وسوف نقوم بالرد عليكم في أقرب وقت ممكن"
                },
                new Resource {Culture = "en-us", Name = "LatestNews", Value = "Latest News"},
                new Resource {Culture = "ar", Name = "LatestNews", Value = "أخر الأخبار"},
                new Resource {Culture = "en-us", Name = "KeepinTouch", Value = "Keep in Touch"},
                new Resource {Culture = "ar", Name = "KeepinTouch", Value = "تواصل معنا"},
                new Resource {Culture = "en-us", Name = "All", Value = "All"},
                new Resource {Culture = "ar", Name = "All", Value = "الكل"},
                new Resource {Culture = "en-us", Name = "Logo", Value = "Logo"},
                new Resource {Culture = "ar", Name = "Logo", Value = "الشعار"},
                new Resource {Culture = "en-us", Name = "Branding", Value = "Branding"},
                new Resource {Culture = "ar", Name = "Branding", Value = "الهوية"},
                new Resource {Culture = "en-us", Name = "Web", Value = "Web"},
                new Resource {Culture = "ar", Name = "Web", Value = "مواقع الإنترنت"},
                new Resource {Culture = "en-us", Name = "Digital", Value = "Digital Marketing"},
                new Resource {Culture = "ar", Name = "Digital", Value = "التسويق الالكتروني"},
                new Resource {Culture = "en-us", Name = "Media", Value = "Media"},
                new Resource {Culture = "ar", Name = "Media", Value = "الإنتاج الإعلامي"},
                new Resource {Culture = "en-us", Name = "Events", Value = "Events"},
                new Resource {Culture = "ar", Name = "Events", Value = "المؤتمرات"},
                new Resource {Culture = "en-us", Name = "Execution", Value = "Execution"},
                new Resource {Culture = "ar", Name = "Execution", Value = "التنفيذ الدعائي"},
                new Resource {Culture = "en-us", Name = "Contactinformation", Value = "Contact Information"},
                new Resource {Culture = "ar", Name = "Contactinformation", Value = "معلومات الاتصال"}
            };

            context.Resources.AddRange(resource);
            context.SaveChanges();
        }
    }
}