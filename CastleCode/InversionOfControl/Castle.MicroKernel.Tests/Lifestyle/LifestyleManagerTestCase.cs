namespace Castle.MicroKernel.Tests.Lifestyle
{
	using System;
	using System.Threading;

	using NUnit.Framework;

	using Castle.MicroKernel;
	using Castle.MicroKernel.Tests.Lifestyle.Components;

	/// <summary>
	/// Summary description for LifestyleManagerTestCase.
	/// </summary>
	[TestFixture]
	public class LifestyleManagerTestCase
	{
		private IKernel m_kernel;

		private IComponent m_instance3;

		[SetUp]
		public void CreateContainer()
		{
			m_kernel = new DefaultKernel();
		}

		[TearDown]
		public void DisposeContainer()
		{
			m_kernel.Dispose();
		}

		[Test]
		public void TestTransient()
		{
			m_kernel.AddComponent( "a", typeof(IComponent), typeof(TransientComponent) );

			IHandler handler = m_kernel.GetHandler("a");
			
			IComponent instance1 = handler.Resolve() as IComponent;
			IComponent instance2 = handler.Resolve() as IComponent;

			Assert.IsNotNull( instance1 );
			Assert.IsNotNull( instance2 );

			Assert.IsTrue( !instance1.Equals( instance2 ) );
			Assert.IsTrue( instance1.ID != instance2.ID );

			handler.Release( instance1 );
			handler.Release( instance2 );
		}

		[Test]
		public void TestSingleton()
		{
			m_kernel.AddComponent( "a", typeof(IComponent), typeof(SingletonComponent) );

			IHandler handler = m_kernel.GetHandler("a");
			
			IComponent instance1 = handler.Resolve() as IComponent;
			IComponent instance2 = handler.Resolve() as IComponent;

			Assert.IsNotNull( instance1 );
			Assert.IsNotNull( instance2 );

			Assert.IsTrue( instance1.Equals( instance2 ) );
			Assert.IsTrue( instance1.ID == instance2.ID );

			handler.Release( instance1 );
			handler.Release( instance2 );
		}

		[Test]
		public void TestCustom()
		{
			m_kernel.AddComponent( "a", typeof(IComponent), typeof(CustomComponent) );

			IHandler handler = m_kernel.GetHandler("a");
			
			IComponent instance1 = handler.Resolve() as IComponent;

			Assert.IsNotNull( instance1 );
		}

		[Test]
		public void TestPerThread()
		{
			m_kernel.AddComponent( "a", typeof(IComponent), typeof(PerThreadComponent) );

			IHandler handler = m_kernel.GetHandler("a");
			
			IComponent instance1 = handler.Resolve() as IComponent;
			IComponent instance2 = handler.Resolve() as IComponent;

			Assert.IsNotNull( instance1 );
			Assert.IsNotNull( instance2 );

			Assert.IsTrue( instance1.Equals( instance2 ) );
			Assert.IsTrue( instance1.ID == instance2.ID );

			Thread thread = new Thread( new ThreadStart(OtherThread) );
			thread.Start();
			thread.Join();

			Assert.IsNotNull( m_instance3 );
			Assert.IsTrue( !instance1.Equals( m_instance3 ) );
			Assert.IsTrue( instance1.ID != m_instance3.ID );

			handler.Release( instance1 );
			handler.Release( instance2 );
		}

		private void OtherThread()
		{
			IHandler handler = m_kernel.GetHandler( "a" );
			m_instance3 = handler.Resolve() as IComponent;
		}
	}
}
