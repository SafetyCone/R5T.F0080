using System;
using System.Threading.Tasks;

using R5T.T0132;

using N002 = R5T.T0153.N002;
using N005 = R5T.T0153.N005;


namespace R5T.F0080
{
	[FunctionalityMarker]
	public partial interface IRepositoryOperations : IFunctionalityMarker
	{
		public void Checkin(
			string localRepositoryDirectoryPath,
			string commitMessage)
		{
			var logger = Instances.LoggingOperator.Get_NullLogger();

            RepositoryOperator.Instance.Checkin(
				localRepositoryDirectoryPath,
				commitMessage,
				logger);
		}

		/// <summary>
		/// Creates a remote repository, clones it to a local directory path, runs the provided repository setup action, then does a checkin of the local repository.
		/// </summary>
		/// <returns>The remote repository URL.</returns>
		public async Task<string> CreateRepository(
			N005.RepositoryContext repositoryContext,
			Func<Task> setupRepositoryAction = default)
		{
			var remoteRepositoryUrl = await Instances.RemoteRepositoryOperator.CreateRepository_NonIdempotent(
                repositoryContext.RepositoryOwner,
                repositoryContext.RepositoryName,
                repositoryContext.RepositoryDescription,
                repositoryContext.IsPrivate);

			await Instances.RemoteRepositoryOperator.CloneToLocal_Simple(
				remoteRepositoryUrl,
                repositoryContext.LocalDirectoryPath);

			await F0000.ActionOperator.Instance.Run(setupRepositoryAction);

			this.Checkin(
				repositoryContext.LocalDirectoryPath,
				Instances.CommitMessages.InitialCommit.Value);

			return remoteRepositoryUrl;
		}

		public async Task<N002.RepositoryContext> CreateRepository(
			string owner,
			string repositoryName,
			string repositoryDescription,
			bool isPrivate,
			string localRepositoryDirectoryPath)
		{
			var remoteRepositoryUrl = await Instances.RemoteRepositoryOperator.CreateRepository_NonIdempotent(
				owner,
				repositoryName,
				repositoryDescription,
				isPrivate);

			await Instances.RemoteRepositoryOperator.CloneToLocal_Simple(
				remoteRepositoryUrl,
				localRepositoryDirectoryPath);

			var repositoryContext = new N002.RepositoryContext
			{
				LocalDirectoryPath = localRepositoryDirectoryPath,
				RemoteUrl = remoteRepositoryUrl,
			};

			return repositoryContext;
		}

        public async Task DeleteRepository_Idempotent(
           N005.RepositoryContext repositoryContext)
        {
            Instances.LocalRepositoryOperator.Delete_Idempotent(
                repositoryContext.LocalDirectoryPath);

            await Instances.RemoteRepositoryOperator.DeleteRepository_Idempotent(
                repositoryContext.RepositoryOwner,
                repositoryContext.RepositoryName);
        }

        public async Task DeleteRepository_NonIdempotent(
            N005.RepositoryContext repositoryContext)
		{
            Instances.LocalRepositoryOperator.Delete(
                repositoryContext.LocalDirectoryPath);

            await Instances.RemoteRepositoryOperator.DeleteRepository_NonIdempotent(
                repositoryContext.RepositoryOwner,
                repositoryContext.RepositoryName);
        }

        public async Task DeleteRepository(
			string owner,
			string unadjustedRepositoryName,
			bool isPrivate,
			string localRepositoryDirectoryPath)
		{
            Instances.LocalRepositoryOperator.Delete(
                localRepositoryDirectoryPath);

            var repositoryNameContext = Instances.RepositoryContextOperations.GetRepositoryContext(
                unadjustedRepositoryName,
                isPrivate);

            await Instances.RemoteRepositoryOperator.DeleteRepository_NonIdempotent(
                owner,
                repositoryNameContext.RepositoryName);
        }

		public async Task ModifyRepository(
			string localRepositoryDirectoryPath,
			string commitMessage,
			Func<Task> repositoryAction = default)
		{
			await F0000.ActionOperator.Instance.Run(repositoryAction);

			this.Checkin(
				localRepositoryDirectoryPath,
				commitMessage);
		}

        public async Task Verify_RepositoryDoesNotExist(
            N005.RepositoryContext repositoryContext)
        {
			await this.Verify_RepositoryDoesNotExist(
				repositoryContext.RepositoryOwner,
				repositoryContext.RepositoryName);
        }

        public async Task Verify_RepositoryDoesNotExist(
           string repositoryOwnerName,
            string repositoryName)
        {
            await Instances.RepositoryOperator.Verify_RepositoryDoesNotExist(
                repositoryOwnerName,
                repositoryName);
        }
    }
}