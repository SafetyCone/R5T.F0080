using System;


namespace R5T.F0080
{
    public class RepositoryOperator : IRepositoryOperator
    {
        #region Infrastructure

        public static IRepositoryOperator Instance { get; } = new RepositoryOperator();


        private RepositoryOperator()
        {
        }

        #endregion
    }
}
