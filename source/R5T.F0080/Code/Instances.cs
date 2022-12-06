using System;


namespace R5T.F0080
{
    public static class Instances
    {
        public static F0042.ILocalRepositoryOperator LocalRepositoryOperator => F0042.LocalRepositoryOperator.Instance;
        public static F0042.IRemoteRepositoryOperator RemoteRepositoryOperator => F0042.RemoteRepositoryOperator.Instance;
        public static F0089.IRepositoryContextOperations RepositoryContextOperations => F0089.RepositoryContextOperations.Instance;
        public static F0042.IRepositoryOperator RepositoryOperator => F0042.RepositoryOperator.Instance;
    }
}