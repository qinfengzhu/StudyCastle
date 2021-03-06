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

namespace Castle.MicroKernel
{
	using System;

	using Castle.Model;

	/// <summary>
	/// Possible states for a IHandler instance
	/// </summary>
	public enum HandlerState
	{
		/// <summary>
		/// The component can be requested
		/// </summary>
		Valid,
		/// <summary>
		/// The component can not be requested 
		/// as it still depending on a external 
		/// dependency not yet available
		/// </summary>
		WaitingDependency
	}

	/// <summary>
	/// Contract for the IHandler, which manages an
	/// component state and coordinates its creation 
	/// and destruction (dispatching to activators, lifestyle managers)
	/// </summary>
	public interface IHandler
	{
		/// <summary>
		/// Initializes the handler with a reference to the
		/// kernel.
		/// </summary>
		/// <param name="kernel"></param>
		void Init(IKernel kernel);

		/// <summary>
		/// Implementors should return a valid instance 
		/// for the component the handler is responsible.
		/// It should throw an exception in the case the component
		/// can't be created for some reason
		/// </summary>
		/// <returns></returns>
		object Resolve();

		/// <summary>
		/// Implementors should dispose the component instance
		/// </summary>
		/// <param name="instance"></param>
		void Release(object instance);

		/// <summary>
		/// Gets the state of the handler
		/// </summary>
		HandlerState CurrentState { get; }

		/// <summary>
		/// Gets the model of the component being 
		/// managed by this handler.
		/// </summary>
		ComponentModel ComponentModel { get; }
	}
}
