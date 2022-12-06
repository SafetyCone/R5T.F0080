using System;


namespace R5T.F0080
{
	public class RepositoryOperations : IRepositoryOperations
	{
		#region Infrastructure

	    public static IRepositoryOperations Instance { get; } = new RepositoryOperations();

	    private RepositoryOperations()
	    {
        }

	    #endregion
	}
}