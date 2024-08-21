namespace JO_MOVIES.Areas.Admin.Models
{
    public static class Admin
    {
        public static string _username { get; set; }
        public static string _password { get; set; }

        // Property to check if the admin is logged in
        public static bool IsLoggedIn
        {
            get
            {

                return _username == CorrectPassowrd.username && _password == CorrectPassowrd.password;
            }
        }
    }
}
