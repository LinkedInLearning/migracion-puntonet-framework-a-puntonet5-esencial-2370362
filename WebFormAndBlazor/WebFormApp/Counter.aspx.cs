using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormApp
{
	public partial class Counter : Page
	{

		public int CurrentCount
		{
			get
			{
				var val = ViewState["CurrentCount"];
				return val != null ? (int)val : default;
			}
			set
			{
				ViewState["CurrentCount"] = value;
			}
		}


		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void BtnCounter_Click(object sender, EventArgs e)
		{
			CurrentCount++;
		}
	}
}