using System;
using System.Threading.Tasks;
using R5T.T0132;


namespace R5T.F0080
{
    [FunctionalityMarker]
    public partial interface IRepositoryOperator : IFunctionalityMarker,
        F0042.IRepositoryOperator
    {
        /// <inheritdoc cref="Documentation.CreateRemoteAndLocalRepository"/>
        public async Task<RepositoryContext> CreateRepository(
            RepositorySpecification repositorySpecification)
        {
            var remoteRepositoryResult = await RemoteRepositoryOperations.Instance.CreateRemoteRepository(
                repositorySpecification.OwnerName,
                repositorySpecification.RepositoryName,
                repositorySpecification.RepositoryDescription,
                repositorySpecification.IsPrivate);

            var localrepositoryResult = await LocalRepositoryOperations.Instance.CreateLocalRepository(
                remoteRepositoryResult.RepositoryUrl,
                repositorySpecification.OwnerName,
                repositorySpecification.RepositoryName);

            var repositoryContext = new RepositoryContext
            {
                RemoteRepositoryUrl = remoteRepositoryResult.RepositoryUrl,
                LocalRepositoryDirectoryPath = localrepositoryResult.RepositoryDirectoryPath,
            };

            return repositoryContext;
        }
    }
}
