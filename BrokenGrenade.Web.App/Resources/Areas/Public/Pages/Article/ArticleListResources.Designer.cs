﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BrokenGrenade.Web.App.Resources.Areas.Public.Pages.Article {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ArticleListResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ArticleListResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BrokenGrenade.Web.App.Resources.Areas.Public.Pages.Article.ArticleListResources", typeof(ArticleListResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Návody.
        /// </summary>
        internal static string Articles {
            get {
                return ResourceManager.GetString("Articles", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Návody od autora.
        /// </summary>
        internal static string ArticlesFrom {
            get {
                return ResourceManager.GetString("ArticlesFrom", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Vytvořil.
        /// </summary>
        internal static string CreatedBy {
            get {
                return ResourceManager.GetString("CreatedBy", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nepodařilo se načíst návody..
        /// </summary>
        internal static string FailedToLoadArticles {
            get {
                return ResourceManager.GetString("FailedToLoadArticles", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nebyly nalezeny žádné návody.
        /// </summary>
        internal static string NoArticlesFound {
            get {
                return ResourceManager.GetString("NoArticlesFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Náhled.
        /// </summary>
        internal static string Preview {
            get {
                return ResourceManager.GetString("Preview", resourceCulture);
            }
        }
    }
}
