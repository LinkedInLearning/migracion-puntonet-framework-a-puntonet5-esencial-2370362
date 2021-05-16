using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormApp
{
	public partial class CounterAjax : Page
	{



		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				CounterParagraph.Text= HdnCounter.Value;
			}
		}

		protected void BtnCounter_Click(object sender, EventArgs e)
		{
			var val = int.Parse(HdnCounter.Value);
			val++;
			HdnCounter.Value = val.ToString();
			CounterParagraph.Text = HdnCounter.Value;
		}
	}
}