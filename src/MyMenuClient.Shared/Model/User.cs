
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace MyMenu
{
	[DataContract(Name="user")]
	public class User : BaseModel
	{
		public User (MobileServiceUser user)
		{
			UserId = user.UserId;
			AuthToken = user.MobileServiceAuthenticationToken;
		}

		public User ()
		{
			
		}
        
		[JsonProperty("userid")]
		public string UserId {
			get;
			set;
		}

		[JsonProperty("authToken")]
		public string AuthToken {
			get;
			set;
		}
	}
}

