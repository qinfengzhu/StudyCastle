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

namespace Castle.MonoRail.Framework
{
	using System;

	using Castle.MonoRail.Framework.Filters;

	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=true), Serializable]
	public class LocalizationFilterAttribute : FilterAttribute
	{
		private bool _failOnError = false, _useBrowser = true;
		private String _key			= "locale";
		private RequestStore _requestStore = RequestStore.Cookie;
		
		public LocalizationFilterAttribute() : base( ExecuteEnum.BeforeAction, typeof(LocalizationFilter) )
		{
		}

		/// <summary>
		/// Defines a new LocalizationFilter.
		/// </summary>
		/// <param name="store">Location where the localization parameter is stored.</param>
		/// <param name="key">Name of the parameter in the store.</param>
		public LocalizationFilterAttribute( RequestStore store, String key ) : this()
		{
			_key	= key;
			_requestStore	= store;
		}

        #region Properties

		/// <summary>
		/// Key under which the locale value is stored.
		/// </summary>
		public String Key
		{
			get { return _key; }
			set { _key = value; }
		}

		/// <summary>
		/// Location where the locale value is to be stored.
		/// </summary>
		public RequestStore Store
		{
			get { return _requestStore; }
			set { _requestStore = value; }
		}

		/// <summary>
		/// True if an exception is to be thrown when a specific
		/// culture appears to be incorrect (can't be created).
		/// </summary>
		public bool FailOnError
		{
			get { return _failOnError; }
			set { _failOnError = value; }
		}

		/// <summary>
		/// Use client browser defined languages as default.
		/// </summary>
		public bool UseBrowser
		{
			get { return _useBrowser; }
			set { _useBrowser = value; }
		}

		#endregion		
	}
}
