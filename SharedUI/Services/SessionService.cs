using SharedUI.Models.ViewModel;
using SharedUI.Global;
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Mvc;
using static SharedUI.Global.SessionManager;

public interface ISessionService
{
    int UserId { get; }
    string UserName { get; }
    string CompanyName { get; }
    string CompanyLogo { get; }
    List<VMMenu> UserMenu { get; }

    void SetSessionInformation(int id, string name, string company, string logo, List<VMMenu> menu);
    void DestroySession();
}
public class SessionService : ISessionService
{
    private readonly IHttpContextAccessor _accessor;
    public SessionService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }
    private ISession Session => _accessor.HttpContext.Session;
    public int UserId => Session.GetInt32(SessionKeys.UserId) ?? 0;
    public string UserName => Session.GetString(SessionKeys.UserName) ?? "Developer";
    public string CompanyName => Session.GetString(SessionKeys.CompanyName) ?? "Inventory Cloth";
    public string CompanyLogo => Session.GetString(SessionKeys.CompanyLogo) ?? "/images/default-logo.png";
    public List<VMMenu> UserMenu => Session.GetObjectFromJson<List<VMMenu>>(SessionManager.SessionKeys.UserMenu) ?? new List<VMMenu>();
    public void SetSessionInformation(int id, string name, string company, string logo, List<VMMenu> menu)
    {
        Session.SetInt32(SessionManager.SessionKeys.UserId, id);
        Session.SetString(SessionManager.SessionKeys.UserName, name);
        Session.SetString(SessionManager.SessionKeys.CompanyName, company);
        Session.SetString(SessionManager.SessionKeys.CompanyLogo, logo);
        Session.SetObjectAsJson(SessionManager.SessionKeys.UserMenu, menu);
    }

    public void DestroySession() => Session.Clear();
}
