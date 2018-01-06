using System.Globalization;
using Resources.Abstract;
using Resources.Concrete;

namespace Resources
{
    public class Resources
    {
        private static readonly IResourceProvider resourceProvider = new DbResourceProvider();


        /// <summary>About Us</summary>
        public static string About
        {
            get { return (string) resourceProvider.GetResource("About", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Add Group</summary>
        public static string AddGroup
        {
            get { return (string) resourceProvider.GetResource("AddGroup", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Add New</summary>
        public static string AddNew
        {
            get { return (string) resourceProvider.GetResource("AddNew", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Add New Category</summary>
        public static string AddNewCategory
        {
            get { return (string) resourceProvider.GetResource("AddNewCategory", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Add New Group</summary>
        public static string AddNewGroup
        {
            get { return (string) resourceProvider.GetResource("AddNewGroup", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Add New Post</summary>
        public static string AddNewPost
        {
            get { return (string) resourceProvider.GetResource("AddNewPost", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Add New User</summary>
        public static string AddNewUser
        {
            get { return (string) resourceProvider.GetResource("AddNewUser", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Add Permission</summary>
        public static string AddRole
        {
            get { return (string) resourceProvider.GetResource("AddRole", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Add User</summary>
        public static string AddUser
        {
            get { return (string) resourceProvider.GetResource("AddUser", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>All</summary>
        public static string All
        {
            get { return (string) resourceProvider.GetResource("All", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>All Categories</summary>
        public static string AllCategories
        {
            get { return (string) resourceProvider.GetResource("AllCategories", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>All Posts</summary>
        public static string AllPosts
        {
            get { return (string) resourceProvider.GetResource("AllPosts", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>All Users</summary>
        public static string AllUsers
        {
            get { return (string) resourceProvider.GetResource("AllUsers", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Back</summary>
        public static string Back
        {
            get { return (string) resourceProvider.GetResource("Back", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Branding</summary>
        public static string Branding
        {
            get { return (string) resourceProvider.GetResource("Branding", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Categories</summary>
        public static string Categories
        {
            get { return (string) resourceProvider.GetResource("Categories", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Category</summary>
        public static string Category
        {
            get { return (string) resourceProvider.GetResource("Category", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Site Language</summary>
        public static string ChooseYourLanguage
        {
            get
            {
                return (string) resourceProvider.GetResource("ChooseYourLanguage", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Close</summary>
        public static string Close
        {
            get { return (string) resourceProvider.GetResource("Close", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Company</summary>
        public static string Company
        {
            get { return (string) resourceProvider.GetResource("Company", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Incoming Messages</summary>
        public static string ContactArrived
        {
            get { return (string) resourceProvider.GetResource("ContactArrived", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Contact Information</summary>
        public static string ContactInfo
        {
            get { return (string) resourceProvider.GetResource("ContactInfo", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Contact Information</summary>
        public static string Contactinformation
        {
            get
            {
                return (string) resourceProvider.GetResource("Contactinformation", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Contact Us</summary>
        public static string ContactUs
        {
            get { return (string) resourceProvider.GetResource("ContactUs", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Content</summary>
        public static string Content
        {
            get { return (string) resourceProvider.GetResource("Content", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Arabic Content</summary>
        public static string ContentAr
        {
            get { return (string) resourceProvider.GetResource("ContentAr", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Copyrights</summary>
        public static string Copyrights
        {
            get { return (string) resourceProvider.GetResource("Copyrights", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Count</summary>
        public static string Count
        {
            get { return (string) resourceProvider.GetResource("Count", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Author</summary>
        public static string CreatedBy
        {
            get { return (string) resourceProvider.GetResource("CreatedBy", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Created Date</summary>
        public static string CreatedDate
        {
            get { return (string) resourceProvider.GetResource("CreatedDate", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Dashboard</summary>
        public static string Dashboard
        {
            get { return (string) resourceProvider.GetResource("Dashboard", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Date</summary>
        public static string Date
        {
            get { return (string) resourceProvider.GetResource("Date", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>DateTime</summary>
        public static string DateTime
        {
            get { return (string) resourceProvider.GetResource("DateTime", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Delete</summary>
        public static string Delete
        {
            get { return (string) resourceProvider.GetResource("Delete", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Deleted Successful</summary>
        public static string Deleted
        {
            get { return (string) resourceProvider.GetResource("Deleted", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Delete Group</summary>
        public static string DeleteGroup
        {
            get { return (string) resourceProvider.GetResource("DeleteGroup", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Delete Permission</summary>
        public static string DeleteRole
        {
            get { return (string) resourceProvider.GetResource("DeleteRole", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Delete User</summary>
        public static string DeleteUser
        {
            get { return (string) resourceProvider.GetResource("DeleteUser", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Description</summary>
        public static string Description
        {
            get { return (string) resourceProvider.GetResource("Description", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Arabic Description</summary>
        public static string DescriptionAr
        {
            get { return (string) resourceProvider.GetResource("DescriptionAr", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Details</summary>
        public static string Details
        {
            get { return (string) resourceProvider.GetResource("Details", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Digital Marketing</summary>
        public static string Digital
        {
            get { return (string) resourceProvider.GetResource("Digital", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Draft</summary>
        public static string Draft
        {
            get { return (string) resourceProvider.GetResource("Draft", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Edit</summary>
        public static string Edit
        {
            get { return (string) resourceProvider.GetResource("Edit", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Edit Group</summary>
        public static string EditGroup
        {
            get { return (string) resourceProvider.GetResource("EditGroup", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Edit Permission</summary>
        public static string EditRole
        {
            get { return (string) resourceProvider.GetResource("EditRole", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Edit User</summary>
        public static string EditUser
        {
            get { return (string) resourceProvider.GetResource("EditUser", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Email Address</summary>
        public static string EmailAddress
        {
            get { return (string) resourceProvider.GetResource("EmailAddress", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Error!</summary>
        public static string Error
        {
            get { return (string) resourceProvider.GetResource("Error", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Settings Saved Error.</summary>
        public static string ErrorSaved
        {
            get { return (string) resourceProvider.GetResource("ErrorSaved", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Events</summary>
        public static string Events
        {
            get { return (string) resourceProvider.GetResource("Events", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Execution</summary>
        public static string Execution
        {
            get { return (string) resourceProvider.GetResource("Execution", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>General Settings</summary>
        public static string GeneralSettings
        {
            get { return (string) resourceProvider.GetResource("GeneralSettings", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Gift</summary>
        public static string Gift
        {
            get { return (string) resourceProvider.GetResource("Gift", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Group</summary>
        public static string Group
        {
            get { return (string) resourceProvider.GetResource("Group", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Groups</summary>
        public static string Groups
        {
            get { return (string) resourceProvider.GetResource("Groups", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Home</summary>
        public static string Home
        {
            get { return (string) resourceProvider.GetResource("Home", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>ID</summary>
        public static string Id
        {
            get { return (string) resourceProvider.GetResource("Id", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Keep in Touch</summary>
        public static string KeepinTouch
        {
            get { return (string) resourceProvider.GetResource("KeepinTouch", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Language</summary>
        public static string Language
        {
            get { return (string) resourceProvider.GetResource("Language", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Latest News</summary>
        public static string LatestNews
        {
            get { return (string) resourceProvider.GetResource("LatestNews", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Link Name</summary>
        public static string LinkName
        {
            get { return (string) resourceProvider.GetResource("LinkName", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Target</summary>
        public static string LinkTarget
        {
            get { return (string) resourceProvider.GetResource("LinkTarget", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Link url</summary>
        public static string LinkUrl
        {
            get { return (string) resourceProvider.GetResource("LinkUrl", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Login</summary>
        public static string Login
        {
            get { return (string) resourceProvider.GetResource("Login", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Logo</summary>
        public static string Logo
        {
            get { return (string) resourceProvider.GetResource("Logo", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Logout</summary>
        public static string Logout
        {
            get { return (string) resourceProvider.GetResource("Logout", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Media</summary>
        public static string Media
        {
            get { return (string) resourceProvider.GetResource("Media", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Message</summary>
        public static string Message
        {
            get { return (string) resourceProvider.GetResource("Message", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Please fill out this form and we will get back to you as soon as possible</summary>
        public static string MoadlP
        {
            get { return (string) resourceProvider.GetResource("MoadlP", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>We are pleased to serve you</summary>
        public static string Moadltitle
        {
            get { return (string) resourceProvider.GetResource("Moadltitle", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Mobile</summary>
        public static string Mobile
        {
            get { return (string) resourceProvider.GetResource("Mobile", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Modified By</summary>
        public static string ModifiedBy
        {
            get { return (string) resourceProvider.GetResource("ModifiedBy", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Name</summary>
        public static string Name
        {
            get { return (string) resourceProvider.GetResource("Name", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Arabic Name</summary>
        public static string NameAr
        {
            get { return (string) resourceProvider.GetResource("NameAr", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Our Partners</summary>
        public static string OurPartners
        {
            get { return (string) resourceProvider.GetResource("OurPartners", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Our Projects</summary>
        public static string OurProjects
        {
            get { return (string) resourceProvider.GetResource("OurProjects", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Partners</summary>
        public static string Partners
        {
            get { return (string) resourceProvider.GetResource("Partners", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Password</summary>
        public static string Password
        {
            get { return (string) resourceProvider.GetResource("Password", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Pending</summary>
        public static string Pending
        {
            get { return (string) resourceProvider.GetResource("Pending", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Permissions</summary>
        public static string Permissions
        {
            get { return (string) resourceProvider.GetResource("Permissions", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Posts</summary>
        public static string Posts
        {
            get { return (string) resourceProvider.GetResource("Posts", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Profile</summary>
        public static string Profile
        {
            get { return (string) resourceProvider.GetResource("Profile", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Projects</summary>
        public static string Projects
        {
            get { return (string) resourceProvider.GetResource("Projects", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Published</summary>
        public static string Published
        {
            get { return (string) resourceProvider.GetResource("Published", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Quick Edit</summary>
        public static string QuickEdit
        {
            get { return (string) resourceProvider.GetResource("QuickEdit", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Read More</summary>
        public static string ReadMore
        {
            get { return (string) resourceProvider.GetResource("ReadMore", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Register New User</summary>
        public static string Register
        {
            get { return (string) resourceProvider.GetResource("Register", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Remember Me</summary>
        public static string RememberMe
        {
            get { return (string) resourceProvider.GetResource("RememberMe", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Resources</summary>
        public static string ResourcesM
        {
            get { return (string) resourceProvider.GetResource("ResourcesM", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Role</summary>
        public static string Role
        {
            get { return (string) resourceProvider.GetResource("Role", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Save</summary>
        public static string Save
        {
            get { return (string) resourceProvider.GetResource("Save", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Send</summary>
        public static string Send
        {
            get { return (string) resourceProvider.GetResource("Send", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Services</summary>
        public static string Services
        {
            get { return (string) resourceProvider.GetResource("Services", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Settings</summary>
        public static string Settings
        {
            get { return (string) resourceProvider.GetResource("Settings", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Settings Saved.</summary>
        public static string SettingsSaved
        {
            get { return (string) resourceProvider.GetResource("SettingsSaved", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Short Description</summary>
        public static string ShortDescription
        {
            get { return (string) resourceProvider.GetResource("ShortDescription", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Arabic Short Description</summary>
        public static string ShortDescriptionAr
        {
            get
            {
                return (string) resourceProvider.GetResource("ShortDescriptionAr", CultureInfo.CurrentUICulture.Name);
            }
        }

        /// <summary>Site Title</summary>
        public static string SiteTitle
        {
            get { return (string) resourceProvider.GetResource("SiteTitle", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Slider</summary>
        public static string Slider
        {
            get { return (string) resourceProvider.GetResource("Slider", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Social Media</summary>
        public static string SocialMedia
        {
            get { return (string) resourceProvider.GetResource("SocialMedia", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Status</summary>
        public static string Status
        {
            get { return (string) resourceProvider.GetResource("Status", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Subject</summary>
        public static string Subject
        {
            get { return (string) resourceProvider.GetResource("Subject", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Tagline</summary>
        public static string Tagline
        {
            get { return (string) resourceProvider.GetResource("Tagline", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Thumbnail</summary>
        public static string Thumbnail
        {
            get { return (string) resourceProvider.GetResource("Thumbnail", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Time</summary>
        public static string Time
        {
            get { return (string) resourceProvider.GetResource("Time", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Title</summary>
        public static string Title
        {
            get { return (string) resourceProvider.GetResource("Title", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Arabic Title</summary>
        public static string TitleAr
        {
            get { return (string) resourceProvider.GetResource("TitleAr", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Update</summary>
        public static string Update
        {
            get { return (string) resourceProvider.GetResource("Update", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Updated Date</summary>
        public static string UpdatedDate
        {
            get { return (string) resourceProvider.GetResource("UpdatedDate", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Update Successful</summary>
        public static string UpdateDone
        {
            get { return (string) resourceProvider.GetResource("UpdateDone", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Update Error!</summary>
        public static string UpdateFail
        {
            get { return (string) resourceProvider.GetResource("UpdateFail", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Upload</summary>
        public static string Upload
        {
            get { return (string) resourceProvider.GetResource("Upload", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>User Group</summary>
        public static string UserGroup
        {
            get { return (string) resourceProvider.GetResource("UserGroup", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Username</summary>
        public static string Username
        {
            get { return (string) resourceProvider.GetResource("Username", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>User Permission</summary>
        public static string UserPermission
        {
            get { return (string) resourceProvider.GetResource("UserPermission", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Users</summary>
        public static string Users
        {
            get { return (string) resourceProvider.GetResource("Users", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Value</summary>
        public static string Value
        {
            get { return (string) resourceProvider.GetResource("Value", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Web</summary>
        public static string Web
        {
            get { return (string) resourceProvider.GetResource("Web", CultureInfo.CurrentUICulture.Name); }
        }

        /// <summary>Your Profile</summary>
        public static string YourProfile
        {
            get { return (string) resourceProvider.GetResource("YourProfile", CultureInfo.CurrentUICulture.Name); }
        }
    }
}