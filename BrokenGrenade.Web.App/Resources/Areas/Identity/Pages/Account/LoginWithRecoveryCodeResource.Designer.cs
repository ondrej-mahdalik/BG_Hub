﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BrokenGrenade.Web.App.Resources.Areas.Identity.Pages.Account {
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
    public class LoginWithRecoveryCodeResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal LoginWithRecoveryCodeResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BrokenGrenade.Web.App.Resources.Areas.Identity.Pages.Account.LoginWithRecoveryCod" +
                            "eResource", typeof(LoginWithRecoveryCodeResource).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Zadaný záložní kód je nesprávný..
        /// </summary>
        public static string InvalidRecoveryCode {
            get {
                return ResourceManager.GetString("InvalidRecoveryCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Přihlašuješ se pomocí záložního kódu. Toto přihlášení nebude zapamatováno dokud nezadáš ověřovací kód z aplikace nebo nedeaktivuješ dvoufázové ověřování..
        /// </summary>
        public static string LoginWithRecoveryCodeMessage {
            get {
                return ResourceManager.GetString("LoginWithRecoveryCodeMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Přihlášení pomocí záložního kódu.
        /// </summary>
        public static string LoginWithRecoveryCodeTitle {
            get {
                return ResourceManager.GetString("LoginWithRecoveryCodeTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Záložní kód.
        /// </summary>
        public static string RecoveryCode {
            get {
                return ResourceManager.GetString("RecoveryCode", resourceCulture);
            }
        }
    }
}
