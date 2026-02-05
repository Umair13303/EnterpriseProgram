using Newtonsoft.Json;

namespace OrganisationSetup.Global
{
    public static class SessionManager
    {
        public static class SessionKeys
        {
            public const string UserId = "Session_UserId";
            public const string UserName = "Session_UserName";
            public const string CompanyId = "Session_CompanyId";
            public const string CompanyName = "Session_CompanyName";
            public const string CompanyLogo = "Session_CompanyLogo";
            public const string UserMenu = "Session_UserMenu";
        }
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T? GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
