using System.Web;
using System.Web.Mvc;

namespace Nhom3_BTL_NguyenDuyHung
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
