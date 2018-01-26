// Copyright 2004-2006 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Castle.Facilities.NHibernateIntegration.Tests
{
	using System;
	using Castle.MicroKernel;
	using Castle.Services.Transaction;
	using NUnit.Framework;


	[Transactional]
	public class MyBusinessClass
	{
		private BlogDao _blogDao;
		private readonly IKernel kernel;

		public MyBusinessClass(BlogDao blogDao, IKernel kernel)
		{
			_blogDao = blogDao;
			this.kernel = kernel;
		}

		[Transaction(TransactionMode.RequiresNew)]
		public virtual Blog Create( String name )
		{
			ITransactionManager transactionManager = 
				kernel[ typeof(ITransactionManager) ] as ITransactionManager;

			Assert.IsNotNull( transactionManager );
			Assert.IsNotNull( transactionManager.CurrentTransaction );

			return _blogDao.CreateBlog( name );
		}

		[Transaction(TransactionMode.Requires)]
		public virtual Blog CreateWithError( String name )
		{
			ITransactionManager transactionManager = 
				kernel[ typeof(ITransactionManager) ] as ITransactionManager;

			Assert.IsNotNull( transactionManager );
			Assert.IsNotNull( transactionManager.CurrentTransaction );

			_blogDao.CreateBlog( name );

			throw new ApplicationException("Ugh!");
		}
	}
}