using System;


namespace R5T.F0080
{
    public class RemoteRepositoryOperations : IRemoteRepositoryOperations
    {
        #region Infrastructure

        public static IRemoteRepositoryOperations Instance { get; } = new RemoteRepositoryOperations();


        private RemoteRepositoryOperations()
        {
        }

        #endregion
    }
}
