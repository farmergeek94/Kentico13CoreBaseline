//--------------------------------------------------------------------------------------------------
// <auto-generated>
//
//     This code was generated by code generator tool.
//
//     To customize the code use your own partial class. For more info about how to use and customize
//     the generated code see the documentation at https://docs.xperience.io/.
//
// </auto-generated>
//--------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using CMS;
using CMS.Base;
using CMS.Helpers;
using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.CMS;

[assembly: RegisterDocumentType(Root.CLASS_NAME, typeof(Root))]

namespace CMS.DocumentEngine.Types.CMS
{
	/// <summary>
	/// Represents a content item of type Root.
	/// </summary>
	public partial class Root : TreeNode
	{
		#region "Constants and variables"

		/// <summary>
		/// The name of the data class.
		/// </summary>
		public const string CLASS_NAME = "CMS.Root";


		/// <summary>
		/// The instance of the class that provides extended API for working with Root fields.
		/// </summary>
		private readonly RootFields mFields;

		#endregion


		#region "Properties"

		/// <summary>
		/// Gets an object that provides extended API for working with Root fields.
		/// </summary>
		[RegisterProperty]
		public RootFields Fields
		{
			get
			{
				return mFields;
			}
		}


		/// <summary>
		/// Provides extended API for working with Root fields.
		/// </summary>
		[RegisterAllProperties]
		public partial class RootFields : AbstractHierarchicalObject<RootFields>
		{
			/// <summary>
			/// The content item of type Root that is a target of the extended API.
			/// </summary>
			private readonly Root mInstance;


			/// <summary>
			/// Initializes a new instance of the <see cref="RootFields" /> class with the specified content item of type Root.
			/// </summary>
			/// <param name="instance">The content item of type Root that is a target of the extended API.</param>
			public RootFields(Root instance)
			{
				mInstance = instance;
			}
		}

		#endregion


		#region "Constructors"

		/// <summary>
		/// Initializes a new instance of the <see cref="Root" /> class.
		/// </summary>
		public Root() : base(CLASS_NAME)
		{
			mFields = new RootFields(this);
		}

		#endregion
	}
}