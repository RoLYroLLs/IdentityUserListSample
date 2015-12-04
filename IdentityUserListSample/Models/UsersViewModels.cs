using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IdentityUserListSample.Models {
	public class UsersViewModel {
		public string Id { get; set; }

		[Display(Name = "UserName")]
		public string UserName { get; set; }

		[Display(Name = "Roles")]
		public string Roles { get; set; }
	}
}