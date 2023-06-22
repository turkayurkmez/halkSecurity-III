using System;
using System.Web.UI;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ButtonAddComment_Click(object sender, EventArgs e)
    {
        //XSS: Cross Side Scripting
        LabelLastComment.Text = TextBoxInput.Text;
    }
}