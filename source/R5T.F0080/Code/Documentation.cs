using System;


namespace R5T.F0080
{
	/// <summary>
	/// Repository operations (joint local and remote).
	/// </summary>
	public static class Documentation
	{
        /// <summary>
        /// Clones a remote Git repository to a local directory to create the local repository.
        /// </summary>
        public static readonly object CreateLocalRepository;

        /// <summary>
        /// Creates a local repository using the specified constructor.
        /// </summary>
        public static readonly object CreateLocalRepositoryUsingConstructor;

        /// <summary>
        /// Creates the remote (GitHub) repository and clones it to a local directory to create the local repository.
        /// </summary>
        public static readonly object CreateRemoteAndLocalRepository;

        /// <summary>
        /// Creates a remote (GitHub) repository.
        /// </summary>
        public static readonly object CreateRemoteRepository;

        /// <summary>
        /// Creates a remote repository using the specified constructor.
        /// </summary>
        public static readonly object CreateRemoteRepositoryUsingConstructor;
    }
}