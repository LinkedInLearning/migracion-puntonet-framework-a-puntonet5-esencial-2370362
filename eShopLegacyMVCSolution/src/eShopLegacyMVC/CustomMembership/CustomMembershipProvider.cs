using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace eShopLegacyMVC.CustomMembership
{
	public class CustomMembershipProvider : MembershipProvider
	{

		private readonly List<CustomMembershipUser> _users =
		                                                new List<CustomMembershipUser> {
			new CustomMembershipUser("juanjo", "juanjo@jmontiel.es", "Pass123456", "Juanjo", "Montiel,", new List<string> { "admin" })
	};

		public override bool EnablePasswordRetrieval => false;

		public override bool EnablePasswordReset => false;

		public override bool RequiresQuestionAndAnswer => false;

		public override string ApplicationName { get; set; } = "eShop legacy";

		public override int MaxInvalidPasswordAttempts => 3;

		public override int PasswordAttemptWindow => 3;

		public override bool RequiresUniqueEmail => true;

		public override MembershipPasswordFormat PasswordFormat => MembershipPasswordFormat.Hashed;

		public override int MinRequiredPasswordLength => 6;

		public override int MinRequiredNonAlphanumericCharacters => throw new NotImplementedException();

		public override string PasswordStrengthRegularExpression => null;

		public override bool ChangePassword(string username, string oldPassword, string newPassword)
		{
			throw new NotImplementedException();
		}

		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
		{
			throw new NotImplementedException();
		}

		public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
		{
			throw new NotImplementedException();
		}

		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override int GetNumberOfUsersOnline()
		{
			throw new NotImplementedException();
		}

		public override string GetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			throw new NotImplementedException();
		}

		public override MembershipUser GetUser(string username, bool userIsOnline)
		{
			return _users.SingleOrDefault(u => u.UserName == username) as MembershipUser;
		}

		public override string GetUserNameByEmail(string email)
		{
			throw new NotImplementedException();
		}

		public override string ResetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override bool UnlockUser(string userName)
		{
			throw new NotImplementedException();
		}

		public override void UpdateUser(MembershipUser user)
		{
			throw new NotImplementedException();
		}

		public override bool ValidateUser(string username, string password)
		{
			var user = _users.SingleOrDefault(u => u.UserName == username) as CustomMembershipUser;
			if (user is null)
			{
				return false;
			}
			return user.Password == password;
		}
	}
}