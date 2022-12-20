using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.F0000;
using R5T.T0132;


namespace R5T.F0080
{
    [FunctionalityMarker]
    public partial interface ILocalRepositoryOperations : IFunctionalityMarker
    {
        /// <inheritdoc cref="Documentation.CreateLocalRepository"/>
        public Func<Task<LocalRepositoryContext>> CreateLocalRepository(
            string remoteRepositoryUrl,
            string localRepositoryDirectoryPath)
        {
            return () => LocalRepositoryOperator.Instance.CreateLocalRepository(
                remoteRepositoryUrl,
                localRepositoryDirectoryPath);
        }

        /// <inheritdoc cref="Documentation.CreateLocalRepository"/>
        public Func<Task<LocalRepositoryContext>> CreateLocalRepository(
            string remoteRepositoryUrl,
            LocalRepositorySpecification localRepositorySpecification)
        {
            var localRepositoryDirectoryPath = F0041.DirectoryPathOperator.Instance.GetLocalRepositoryDirectoryPath(
                localRepositorySpecification.RepositoryName,
                localRepositorySpecification.OwnerName);

            return () => LocalRepositoryOperator.Instance.CreateLocalRepository(
                remoteRepositoryUrl,
                localRepositoryDirectoryPath);
        }

        /// <inheritdoc cref="Documentation.CreateLocalRepositoryUsingConstructor"/>
        public async Task<LocalRepositoryResult> CreateLocalRepository(
            Func<Task<LocalRepositoryContext>> constructor,
            IEnumerable<Func<LocalRepositoryContext, Task>> actions)
        {
            var localRepositoryContext = await constructor();

            await ActionOperator.Instance.Run(
                localRepositoryContext,
                actions);

            var localRepositoryResult = new LocalRepositoryResult
            {
                RepositoryDirectoryPath = localRepositoryContext.RepositoryDirectoryPath,
            };

            return localRepositoryResult;
        }

        /// <inheritdoc cref="Documentation.CreateLocalRepository"/>
        public async Task<LocalRepositoryResult> CreateLocalRepository(
            string remoteRepositoryUrl,
            string localRepositoryDirectoryPath,
            IEnumerable<Func<LocalRepositoryContext, Task>> actions)
        {
            var localRepositoryResult =  await this.CreateLocalRepository(
                this.CreateLocalRepository(
                    remoteRepositoryUrl,
                    localRepositoryDirectoryPath),
                actions);

            return localRepositoryResult;
        }

        /// <inheritdoc cref="Documentation.CreateLocalRepository"/>
        public Task<LocalRepositoryResult> CreateLocalRepository(
            string remoteRepositoryUrl,
            string localRepositoryDirectoryPath,
            params Func<LocalRepositoryContext, Task>[] actions)
        {
            return this.CreateLocalRepository(
                remoteRepositoryUrl,
                localRepositoryDirectoryPath,
                actions.AsEnumerable());
        }

        /// <inheritdoc cref="Documentation.CreateLocalRepository"/>
        public async Task<LocalRepositoryResult> CreateLocalRepository(
           string remoteRepositoryUrl,
           LocalRepositorySpecification localRepositorySpecification,
           IEnumerable<Func<LocalRepositoryContext, Task>> actions)
        {
            var localRepositoryResult = await this.CreateLocalRepository(
                this.CreateLocalRepository(
                    remoteRepositoryUrl,
                    localRepositorySpecification),
                actions);

            return localRepositoryResult;
        }

        /// <inheritdoc cref="Documentation.CreateLocalRepository"/>
        public async Task<LocalRepositoryResult> CreateLocalRepository(
           string remoteRepositoryUrl,
           string ownerName,
           string repositoryName,
           IEnumerable<Func<LocalRepositoryContext, Task>> actions)
        {
            var localRepositorySpecification = new LocalRepositorySpecification
            {
                OwnerName = ownerName,
                RepositoryName = repositoryName,
            };

            var localRepositoryResult = await this.CreateLocalRepository(
                remoteRepositoryUrl,
                localRepositorySpecification,
                actions);

            return localRepositoryResult;
        }

        /// <inheritdoc cref="Documentation.CreateLocalRepository"/>
        public Task<LocalRepositoryResult> CreateLocalRepository(
            string remoteRepositoryUrl,
            string ownerName,
            string repositoryName,
            params Func<LocalRepositoryContext, Task>[] actions)
        {
            return this.CreateLocalRepository(
                remoteRepositoryUrl,
                ownerName,
                repositoryName,
                actions.AsEnumerable());
        }
    }
}
