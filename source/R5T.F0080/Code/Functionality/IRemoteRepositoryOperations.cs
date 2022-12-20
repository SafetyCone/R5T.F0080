using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.F0000;
using R5T.T0132;


namespace R5T.F0080
{
    [FunctionalityMarker]
    public partial interface IRemoteRepositoryOperations : IFunctionalityMarker
    {
        /// <inheritdoc cref="Documentation.CreateRemoteRepository"/>
        public async Task<RemoteRepositoryContext> CreateRemoteRepository(
            RemoteRepositorySpecification remoteRepositorySpecification)
        {
            var repositoryUrl = await F0042.RemoteRepositoryOperator.Instance.CreateRepository_NonIdempotent(
                remoteRepositorySpecification.OwnerName,
                remoteRepositorySpecification.RepositoryName,
                remoteRepositorySpecification.RepositoryDescription,
                remoteRepositorySpecification.IsPrivate);

            var result = new RemoteRepositoryContext
            {
                RepositoryUrl = repositoryUrl,
            };

            return result;
        }

        /// <inheritdoc cref="Documentation.CreateRemoteRepositoryUsingConstructor"/>
        public async Task<RemoteRepositoryResult> CreateRemoteRepository(
            RemoteRepositorySpecification remoteRepositorySpecification,
            Func<RemoteRepositorySpecification, Task<RemoteRepositoryContext>> remoteRepositoryConstructor,
            IEnumerable<Func<RemoteRepositoryContext, Task>> remoteRepositoryActions)
        {
            var remoteRepositoryContext = await remoteRepositoryConstructor(remoteRepositorySpecification);

            await ActionOperator.Instance.Run(
                remoteRepositoryContext,
                remoteRepositoryActions);

            var remoteRepositoryResult = new RemoteRepositoryResult
            {
                RepositoryUrl = remoteRepositoryContext.RepositoryUrl,
            };

            return remoteRepositoryResult;
        }

        /// <inheritdoc cref="Documentation.CreateRemoteRepositoryUsingConstructor"/>
        public async Task<RemoteRepositoryResult> CreateRemoteRepository(
            string ownerName,
            string repositoryName,
            string repositoryDescription,
            bool isPrivate,
            Func<RemoteRepositorySpecification, Task<RemoteRepositoryContext>> remoteRepositoryConstructor,
            IEnumerable<Func<RemoteRepositoryContext, Task>> remoteRepositoryActions)
        {
            var remoteRepositorySpecification = new RemoteRepositorySpecification
            {
                OwnerName = ownerName,
                RepositoryName = repositoryName,
                RepositoryDescription = repositoryDescription,
                IsPrivate = isPrivate,
            };

            var remoteRepositoryResult = await this.CreateRemoteRepository(
                remoteRepositorySpecification,
                remoteRepositoryConstructor,
                remoteRepositoryActions);

            return remoteRepositoryResult;
        }

        /// <inheritdoc cref="Documentation.CreateRemoteRepository"/>
        public async Task<RemoteRepositoryResult> CreateRemoteRepository(
            string ownerName,
            string repositoryName,
            string repositoryDescription,
            bool isPrivate,
            IEnumerable<Func<RemoteRepositoryContext, Task>> remoteRepositoryActions)
        {
            var remoteRepositoryResult = await this.CreateRemoteRepository(
                ownerName,
                repositoryName,
                repositoryDescription,
                isPrivate,
                this.CreateRemoteRepository,
                remoteRepositoryActions);

            return remoteRepositoryResult;
        }

        /// <inheritdoc cref="Documentation.CreateRemoteRepository"/>
        public async Task<RemoteRepositoryResult> CreateRemoteRepository(
            string ownerName,
            string repositoryName,
            string repositoryDescription,
            bool isPrivate,
            params Func<RemoteRepositoryContext, Task>[] remoteRepositoryActions)
        {
            var remoteRepositoryResult = await this.CreateRemoteRepository(
                ownerName,
                repositoryName,
                repositoryDescription,
                isPrivate,
                remoteRepositoryActions.AsEnumerable());

            return remoteRepositoryResult;
        }
    }
}
