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

namespace Castle.Services.Transaction.Tests
{
	using System;

	using NUnit.Framework;

	[TestFixture]
	public class TransactionManagerTestCase
	{
		[Test]
		public void SynchronizationsAndCommit()
		{
			DefaultTransactionManager tm = new DefaultTransactionManager();
			ITransaction transaction = 
				tm.CreateTransaction(TransactionMode.Unspecified, IsolationMode.Unspecified);

			transaction.Begin();
	
			SynchronizationImpl sync = new SynchronizationImpl();

			transaction.RegisterSynchronization( sync );

			Assert.AreEqual( DateTime.MinValue, sync.Before );
			Assert.AreEqual( DateTime.MinValue, sync.After );

			transaction.Commit();

			Assert.IsTrue( sync.Before > DateTime.MinValue );
			Assert.IsTrue( sync.After > DateTime.MinValue );
		}

		[Test]
		public void SynchronizationsAndRollback()
		{
			DefaultTransactionManager tm = new DefaultTransactionManager();
			ITransaction transaction = 
				tm.CreateTransaction(TransactionMode.Unspecified, IsolationMode.Unspecified);

			transaction.Begin();
	
			SynchronizationImpl sync = new SynchronizationImpl();

			transaction.RegisterSynchronization( sync );

			Assert.AreEqual( DateTime.MinValue, sync.Before );
			Assert.AreEqual( DateTime.MinValue, sync.After );

			transaction.Rollback();

			Assert.IsTrue( sync.Before > DateTime.MinValue );
			Assert.IsTrue( sync.After > DateTime.MinValue );
		}

		[Test]
		public void ResourcesAndCommit()
		{
			DefaultTransactionManager tm = new DefaultTransactionManager();
			ITransaction transaction = 
				tm.CreateTransaction(TransactionMode.Unspecified, IsolationMode.Unspecified);
	
			ResourceImpl resource = new ResourceImpl();

			transaction.Enlist( resource );

			Assert.IsFalse( resource.Started );
			Assert.IsFalse( resource.Committed );
			Assert.IsFalse( resource.Rolledback );

			transaction.Begin();

			Assert.IsTrue( resource.Started );
			Assert.IsFalse( resource.Committed );
			Assert.IsFalse( resource.Rolledback );

			transaction.Commit();

			Assert.IsTrue( resource.Started );
			Assert.IsTrue( resource.Committed );
			Assert.IsFalse( resource.Rolledback );
		}

		[Test]
		public void ResourcesAndRollback()
		{
			DefaultTransactionManager tm = new DefaultTransactionManager();
			ITransaction transaction = 
				tm.CreateTransaction(TransactionMode.Unspecified, IsolationMode.Unspecified);
	
			ResourceImpl resource = new ResourceImpl();

			transaction.Enlist( resource );

			Assert.IsFalse( resource.Started );
			Assert.IsFalse( resource.Committed );
			Assert.IsFalse( resource.Rolledback );

			transaction.Begin();

			Assert.IsTrue( resource.Started );
			Assert.IsFalse( resource.Committed );
			Assert.IsFalse( resource.Rolledback );

			transaction.Rollback();

			Assert.IsTrue( resource.Started );
			Assert.IsTrue( resource.Rolledback );
			Assert.IsFalse( resource.Committed );
		}

		[Test]
		[ExpectedException( typeof(TransactionException) )]
		public void InvalidBegin()
		{
			DefaultTransactionManager tm = new DefaultTransactionManager();
			
			ITransaction transaction = tm.CreateTransaction(
				TransactionMode.Requires, IsolationMode.Unspecified);

			transaction.Begin();
			transaction.Begin();
		}

		[Test]
		[ExpectedException( typeof(TransactionException) )]
		public void InvalidCommit()
		{
			DefaultTransactionManager tm = new DefaultTransactionManager();
			
			ITransaction transaction = tm.CreateTransaction(
				TransactionMode.Requires, IsolationMode.Unspecified);

			transaction.Begin();
			transaction.Rollback();
			
			transaction.Commit();
		}
	}
}
