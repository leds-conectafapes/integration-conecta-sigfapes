using Hangfire.Dashboard;

namespace Integrador.Dashboard
{
    public class DashboardNoAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            // Permite qualquer acesso ao dashboard (somente para desenvolvimento)
            return true;
        }
    }

}
