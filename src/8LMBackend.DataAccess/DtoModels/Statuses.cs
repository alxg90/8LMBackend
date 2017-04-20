namespace _8LMBackend.DataAccess.DtoModels{
public static class Statuses
{
	public static class Users
	{
		public const int Active = 1;
		public const int Inactive = 2;
		public const int PaymentIssue = 3;
	}

	public static class UserToken
	{
		public const int Active = 4;
		public const int Expired = 5;
	}

	public static class Package
	{
		public const int New = 6;
		public const int Published = 7;
		public const int Deleted = 8;
	}

	public static class Invoice
	{
		public const int New = 9;
		public const int Captured = 10;
		public const int Refaunded = 11;
	}

	public static class Subscription
	{
		public const int Active = 12;
		public const int Inactive = 13;
		public const int Expired = 14;
	}

	public static class Campaign
	{
		public const int Active = 15;
		public const int Inactive = 16;
	}

	public static class Pages
	{
		public const int Active = 17;
		public const int Inactive = 18;
	}
}
}