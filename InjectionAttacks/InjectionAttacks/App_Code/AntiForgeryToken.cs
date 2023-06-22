using System;
using System.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InjectionAttacks
{
    public class AntiForgeryToken
    {
        // For more information on Bundling, visit https://go.microsoft.com/fwlink/?LinkID=303951
        public static void Check(Page page, HiddenField hiddenField)
        {
            //Bu metodun amacı, login olan kişiye token üretmek VEYA
            //request gönderen kişinin token'ını denetlemek.
            if (!page.IsPostBack)
            {
                Guid guid = Guid.NewGuid();
                hiddenField.Value = guid.ToString();
                page.Session["token"] = guid;
            }
            else
            {
                Guid client = new Guid(hiddenField.Value);
                Guid server = (Guid)page.Session["token"];

                if (client != server)
                {
                    throw new SecurityException("CSRF Atağı yakalandı");
                }
            }

        }
    }
}