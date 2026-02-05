namespace OrganisationSetup.Models.Enums
{
    public class CommonRoute
    {
        public enum RouteAreas
        {
            OSAUser = 1,
        }
        public enum RouteController
        {
            OSUDashboard = 1,
            OSUAuthentication = 2,
        }
        public enum RouteAction
        {
            OSULogin = 1,
            OSUDashboardDefault = 1,
        }
    }
}
