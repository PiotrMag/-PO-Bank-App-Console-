using System;
using System.Dynamic;

public enum PaymentRequestResult
{
	ACCEPTED = 0,
	REJECTED_NO_SUCH_USER = 1,
}

public class ArchiveRecord
{
	public String CompanyName { get; }
	public String CompanyType { get; }
	public String BankName { get; }
	public int CardID { get; }
	public String OwnerName { get; }
	public double Amount { get; }
	public PaymentRequestResult Result { get; }

	public ArchiveRecord();
}
