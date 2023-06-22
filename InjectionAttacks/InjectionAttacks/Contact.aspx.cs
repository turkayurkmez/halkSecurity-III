using InjectionAttacks;
using System;
using System.Web.UI;

public partial class Contact : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AntiForgeryToken.Check(this, HiddenField1);
    }
}