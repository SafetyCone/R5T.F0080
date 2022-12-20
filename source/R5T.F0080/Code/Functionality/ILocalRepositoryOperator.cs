using System;
using System.Threading.Tasks;

using R5T.T0132;


namespace R5T.F0080
{
    [FunctionalityMarker]
    public partial interface ILocalRepositoryOperator : IFunctionalityMarker
    {
        /// <inheritdoc cref="Documentation.CreateLocalRepository"/>
        public async Task<LocalRepositoryContext> CreateLocalRepository(
            string remoteRepositoryUrl,
            string localRepositoryDirectoryPath)
        {
            await F0042.RemoteRepositoryOperator.Instance.CloneToLocal_Simple(
                remoteRepositoryUrl,
                localRepositoryDirectoryPath);

            var localRepositoryContext = new LocalRepositoryContext
            {
                RepositoryDirectoryPath = localRepositoryDirectoryPath,
            };

            return localRepositoryContext;
        }
    }
}
