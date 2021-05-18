using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BeanTrader.Models
{
	public partial class BeanTraderServiceClient
	{

		public BeanTraderServiceClient(InstanceContext context) : base(context, EndpointConfiguration.NetTcpBinding_BeanTraderService)
		{
		}
	}
}
