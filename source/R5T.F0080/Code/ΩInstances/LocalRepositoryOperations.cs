using System;


namespace R5T.F0080
{
    public class LocalRepositoryOperations : ILocalRepositoryOperations
    {
        #region Infrastructure

        public static ILocalRepositoryOperations Instance { get; } = new LocalRepositoryOperations();


        private LocalRepositoryOperations()
        {
        }

        #endregion
    }
}
