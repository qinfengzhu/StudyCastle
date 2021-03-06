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

namespace Castle.DynamicProxy.Test.Classes
{
	using System;


	public class ClassWithIndexer
	{
		private int[] array = new int[10];

		public virtual int this[int index]
		{
			get { return array[index]; }
			set { array[index] = value; }
		}

		public virtual int this[string name]
		{
			get { return 0; }
			set { ; }
		}
	}
}
