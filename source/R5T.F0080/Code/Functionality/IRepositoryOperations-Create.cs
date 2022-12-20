using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.F0000;


namespace R5T.F0080
{
	public partial interface IRepositoryOperations
	{
        public async Task<RepositoryResult> CreateRepository(
            RepositorySpecification repositorySpecification,
            Func<RepositorySpecification, Task<RepositoryContext>> constructor,
            IEnumerable<Func<RepositoryContext, Task>> repositoryActions)
        {
            var repositoryContext = await constructor(repositorySpecification);

            await ActionOperator.Instance.Run(
                repositoryContext,
                repositoryActions);

            var repositoryResult = new RepositoryResult
            {
                RemoteRepositoryUrl = repositoryContext.RemoteRepositoryUrl,
                LocalRepositoryDirectoryPath = repositoryContext.LocalRepositoryDirectoryPath,
            };

            return repositoryResult;
        }

        public Task<RepositoryResult> CreateRepository(
            RepositorySpecification repositorySpecification,
            IEnumerable<Func<RepositoryContext, Task>> repositoryActions)
        {
            return this.CreateRepository(
                repositorySpecification,
                RepositoryOperator.Instance.CreateRepository,
                repositoryActions);
        }

        public Task<RepositoryResult> CreateRepository(
            string ownerName,
            string repositoryName,
            string repositoryDescription,
            bool isPrivate,
            IEnumerable<Func<RepositoryContext, Task>> repositoryActions)
        {
            var repositorySpecification = new RepositorySpecification
            {
                OwnerName = ownerName,
                RepositoryName = repositoryName,
                RepositoryDescription = repositoryDescription,
                IsPrivate = isPrivate,
            };

            return this.CreateRepository(
                repositorySpecification,
                RepositoryOperator.Instance.CreateRepository,
                repositoryActions);
        }

        public Task<RepositoryResult> CreateRepository(
            string ownerName,
            string repositoryName,
            string repositoryDescription,
            bool isPrivate,
            params Func<RepositoryContext, Task>[] repositoryActions)
        {
            return this.CreateRepository(
                ownerName,
                repositoryName,
                repositoryDescription,
                isPrivate,
                repositoryActions.AsEnumerable());
        }
    }
}